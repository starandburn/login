using System.ComponentModel;

namespace Nkk.IT.Trial.Programing.Login.Models
{
    public static class EnumExtender
	{
        public static string GetDescription(this Enum thisValue)
        {
            var field = thisValue.GetType().GetField(thisValue.ToString());
            if (field is null) return thisValue.ToString();
            var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            var ret = attr?.Description ?? thisValue.ToString();
            return ret;
        }
    }
}
