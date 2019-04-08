using System;

namespace Extensions
{
    public static class StringEx
    {
        public static string ToShortTitle(this string title)
        {
            string[] parts = title.ToLower().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            switch (parts.Length)
            {
                case 3:
                    return $"{parts[1][0]}.{parts[2][0]}.{parts[0]}";
                case 2:
                    return $"{parts[1][0]}.{parts[0]}";
                default:
                    return title.ToLower();
            }
        }
    }
}
