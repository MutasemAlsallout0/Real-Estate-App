using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Aqar.Core.Enums
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo != null)
            {
                var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }

            return value.ToString();
        }
    }
}