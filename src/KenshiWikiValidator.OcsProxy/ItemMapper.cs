// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
// Copyright (C) 2021  Adam Witkowski <https://github.com/adwitkow/>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Collections;
using System.Reflection;
using KenshiWikiValidator.OcsProxy.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy
{
    public class ItemMapper
    {
        private readonly IItemRepository itemRepository;
        private readonly Dictionary<Type, PropertyContainer> propertyMap;

        public ItemMapper(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
            this.propertyMap = new Dictionary<Type, PropertyContainer>();
        }

        public IItem Map(DataItem baseItem, IItem builtItem)
        {
            var type = builtItem.GetType();
            if (!this.propertyMap.TryGetValue(type, out var propertyContainer))
            {
                propertyContainer = new PropertyContainer(type);
                this.propertyMap.Add(type, propertyContainer);
            }

            foreach (var pair in baseItem.Values)
            {
                var prop = propertyContainer.GetValueProperty(pair.Key);

                // phew... ugly!
                object? convertedValue;
                if (prop.Name == nameof(DialogAction.CompareBy))
                {
                    convertedValue = pair.Value switch
                    {
                        0 => '=',
                        1 => '>',
                        2 => '<',
                        "==" => '=',
                        _ => Convert.ToChar(pair.Value),
                    };
                }
                else
                {
                    convertedValue = ChangeType(pair.Value, prop.PropertyType);
                }

                prop.SetValue(builtItem, convertedValue);
            }

            foreach (var refCategory in baseItem.ReferenceCategories)
            {
                var referenceProperties = propertyContainer.GetReferenceProperties(refCategory.Key);

                foreach (var prop in referenceProperties)
                {
                    var baseReferences = refCategory.Value.Values;

                    var propertyGenericType = prop.PropertyType.GenericTypeArguments[0];
                    var listType = typeof(List<>).MakeGenericType(propertyGenericType);

                    if (Activator.CreateInstance(listType) is not IList list)
                    {
                        break;
                    }

                    var elementGenericType = propertyGenericType.GenericTypeArguments[0];
                    var elementType = typeof(ItemReference<>).MakeGenericType(elementGenericType);

                    foreach (var baseReference in baseReferences)
                    {
                        var item = this.itemRepository.GetItemByStringId(baseReference.TargetId);
                        var args = new object[] { item, baseReference.Value0, baseReference.Value1, baseReference.Value2 };

                        try
                        {
                            var properReference = Activator.CreateInstance(elementType, args);
                            list.Add(properReference);
                        }
                        catch (MissingMethodException)
                        {
                            Console.Error.WriteLine($"Could not establish a reference of category '{refCategory.Key}' between item: '{baseItem.Name}' ('{baseItem.StringId}') -> '{item.Name}' ('{item.StringId}') ({baseReference.Value0}, {baseReference.Value1}, {baseReference.Value2})");
                        }
                    }

                    prop.SetValue(builtItem, list);
                }
            }

            return builtItem;
        }

        private static object? ChangeType(object value, Type conversion)
        {
            if (value is FileValue fv)
            {
                return fv.Path;
            }

            if (value is not IConvertible)
            {
                return value;
            }

            if (conversion.IsGenericType && conversion.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                var underlyingType = Nullable.GetUnderlyingType(conversion);

                conversion = underlyingType!;
            }

            if (conversion.IsEnum)
            {
                return Enum.ToObject(conversion, value);
            }

            return Convert.ChangeType(value, conversion);
        }

        private sealed class PropertyContainer
        {
            private readonly IDictionary<string, PropertyInfo> values;
            private readonly ILookup<string, PropertyInfo> references;

            public PropertyContainer(Type type)
            {
                var properties = type.GetProperties();
                this.values = properties
                    .Where(prop => prop.IsDefined(typeof(ValueAttribute), false))
                    .ToDictionary(prop => GetCustomAttribute<ValueAttribute>(prop).Name, prop => prop);
                this.references = properties
                    .Where(prop => prop.IsDefined(typeof(ReferenceAttribute), false))
                    .ToLookup(prop => GetCustomAttribute<ReferenceAttribute>(prop).Category, prop => prop);
            }

            public PropertyInfo GetValueProperty(string propertyName)
            {
                return this.values[propertyName];
            }

            public IEnumerable<PropertyInfo> GetReferenceProperties(string propertyName)
            {
                return this.references[propertyName];
            }

            private static T GetCustomAttribute<T>(PropertyInfo property)
            {
                return (T)property.GetCustomAttributes(typeof(T), false).Single();
            }
        }
    }
}
