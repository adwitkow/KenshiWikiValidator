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

        public ItemMapper(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public IItem Map(DataItem baseItem, IItem builtItem)
        {
            var properties = builtItem.GetType().GetProperties();
            var valueProperties = properties
                .Where(prop => prop.IsDefined(typeof(ValueAttribute), false))
                .ToDictionary(prop => ((ValueAttribute)prop.GetCustomAttributes(typeof(ValueAttribute), false).Single()).Name, prop => prop);

            foreach (var pair in baseItem.Values)
            {
                var prop = valueProperties[pair.Key];
                var convertedValue = ChangeType(pair.Value, prop.PropertyType);
                prop.SetValue(builtItem, convertedValue);
            }

            var referenceProperties = properties
                .Where(prop => prop.IsDefined(typeof(ReferenceAttribute), false))
                .ToLookup(prop => ((ReferenceAttribute)prop.GetCustomAttributes(typeof(ReferenceAttribute), false).Single()).Category, prop => prop);

            foreach (var refCategory in baseItem.ReferenceCategories)
            {
                foreach (var prop in referenceProperties[refCategory.Key])
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
    }
}
