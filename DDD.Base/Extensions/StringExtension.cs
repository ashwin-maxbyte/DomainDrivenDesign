using System;
using System.Text;

namespace DDD.Base.Extensions
{
    public static class StringExtension
    {
        public static string AttachParams<T>(this string message, params T[] values) 
        {
            StringBuilder stringBuilder = new(message);
            for (int i = 0; i < values.Length; i++)
            {
                string replacement = "null";
                if (values[i] != null)
                {
                    replacement = values[i].ToString();
                }
                stringBuilder.Replace($"@{i}", replacement);
            }
            return stringBuilder.ToString();
        }
    }
}
