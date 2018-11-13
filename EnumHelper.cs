using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Marco.Helpers
{
    public static class EnumHelper
    {
        public static string GetDisplayName<T>(int value) where T : struct, IConvertible
        {
            var result = TryParseEnumOrNull<T>(value);
            var attr = GetDisplayAttribute(result);
            return attr != null ? attr.Name : null;
        }

        public static string GetDescription<T>(int value) where T : struct, IConvertible
        {
            var result = TryParseEnumOrNull<T>(value);
            var attr = GetDisplayAttribute(result);
            return attr != null ? attr.Description : null;
        }     
        public static T TryParseEnumOrDefault<T>(int intValue) where T : struct, IConvertible
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException("O tipo informado não é um enumerador.");

            if (Enum.IsDefined(type, intValue))
                return (T)Enum.ToObject(type, intValue);

            return default(T);
        }

        public static T? TryParseEnumOrNull<T>(int value) where T : struct, IConvertible
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException("O tipo informado não é um enumerador.");

            if (Enum.IsDefined(type, value))
                return (T)Enum.ToObject(type, value);

            return null;
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            if (value is null) return null;

            var type = value.GetType();
            var field = type.GetField(value.ToString());

            return field != null ? field.GetCustomAttribute<DisplayAttribute>() : null;
        }
    }
}
