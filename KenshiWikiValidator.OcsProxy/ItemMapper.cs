using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using KenshiWikiValidator.OcsProxy.Models;
using OpenConstructionSet.Data.Models;

namespace KenshiWikiValidator.OcsProxy
{
    internal class ItemMapper
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
                var convertedValue = ChangeType(pair.Value, prop.PropertyType);
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
                    var instance = (IList)Activator.CreateInstance(listType)!;

                    var elementGenericType = propertyGenericType.GenericTypeArguments[0];
                    var elementType = typeof(ItemReference<>).MakeGenericType(elementGenericType);

                    foreach (var baseReference in baseReferences)
                    {
                        var item = this.itemRepository.GetItemByStringId(baseReference.TargetId);
                        var args = new object[] { item, baseReference.Value0, baseReference.Value1, baseReference.Value2 };

                        try
                        {
                            var properReference = Activator.CreateInstance(elementType, args);
                            instance.Add(properReference);
                        }
                        catch (MissingMethodException)
                        {
                            Console.Error.WriteLine($"Could not establish a reference of category '{refCategory.Key}' between item: '{baseItem.Name}' ('{baseItem.StringId}') -> '{item.Name}' ('{item.StringId}') ({baseReference.Value0}, {baseReference.Value1}, {baseReference.Value2})");
                        }
                    }

                    prop.SetValue(builtItem, instance);
                }
            }

            return builtItem;
        }

        private static object? ChangeType(object value, Type conversion)
        {
            if (value is not IConvertible)
            {
                return value;
            }

            if (conversion.IsGenericType && conversion.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                conversion = Nullable.GetUnderlyingType(conversion)!;
            }

            if (conversion.IsEnum)
            {
                return Enum.ToObject(conversion, value);
            }

            return Convert.ChangeType(value, conversion);
        }

        private sealed class PropertyContainer
        {
            private readonly IDictionary<string, PropertyInfo> Values;
            private readonly ILookup<string, PropertyInfo> References;

            public PropertyContainer(Type type)
            {
                var properties = type.GetProperties();
                this.Values = properties
                    .Where(prop => prop.IsDefined(typeof(ValueAttribute), false))
                    .ToDictionary(prop => GetCustomAttribute<ValueAttribute>(prop).Name, prop => prop);
                this.References = properties
                    .Where(prop => prop.IsDefined(typeof(ReferenceAttribute), false))
                    .ToLookup(prop => GetCustomAttribute<ReferenceAttribute>(prop).Category, prop => prop);
            }

            public PropertyInfo GetValueProperty(string propertyName)
            {
                return this.Values[propertyName];
            }

            public IEnumerable<PropertyInfo> GetReferenceProperties(string propertyName)
            {
                return this.References[propertyName];
            }

            private static T GetCustomAttribute<T>(PropertyInfo property)
            {
                return (T)property.GetCustomAttributes(typeof(T), false).Single();
            }
        }
    }
}
