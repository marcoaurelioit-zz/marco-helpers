using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Marco.Helpers
{
    public static class EnumHelper
    {
        public static string GetDisplayName<T>(object value) where T : struct, IConvertible
        {
            var result = TryParseEnum<T>(value);
            var attr = GetDisplayAttribute(result);
            return attr != null ? attr.Name : null;
        }

        public static string GetDescription<T>(object value) where T : struct, IConvertible
        {
            var result = TryParseEnum<T>(value);
            var attr = GetDisplayAttribute(result);
            return attr != null ? attr.Description : null;
        }

        public static T TryParseEnum<T>(object value) where T : struct, IConvertible
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException("O tipo informado não é um enumerador.");

            if (Enum.IsDefined(type, value))
                return (T)Enum.Parse(type, value.ToString(), true);

            return default(T);
        }

        public static T? TryParseEnumOrNull<T>(object value) where T : struct, IConvertible
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException("O tipo informado não é um enumerador.");

            if (Enum.IsDefined(type, value))
                return (T)Enum.Parse(type, value.ToString(), true);

            return default(T?);
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var type = value.GetType();
            var field = type.GetField(value.ToString());
            return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
        }
    }
}
