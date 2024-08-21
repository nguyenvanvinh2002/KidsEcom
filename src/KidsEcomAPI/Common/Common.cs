using System.ComponentModel;
using System.Reflection;
namespace KidsEcomAPI.Common
{
    public static class Common
    {
        public static string GetDescriptionNew(this Enum value)
        {
            try
            {
                var da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
                return da.Length > 0 ? da[0].Description : value.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
