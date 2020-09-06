using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskerCore
{
    public static class MaskExtentions
    {
        public static string CSMask(this string value, string ProviderCode) {
            Masker masker = new Masker(value, ProviderCode);
            return masker.Masked;
        }
        public static string CSMask(this string value, Func<string,string> MaskMethod)
        {
            Masker masker = new Masker(value, MaskMethod);
            return masker.Masked;
        }
        public static string CSMaskIP(this string value)
        {
            Masker masker = new Masker(value, "IPADDRESS");
            return masker.Masked;
        }
        public static string CSMaskCIDR(this string value)
        {
            Masker masker = new Masker(value, "ALPHANUMERICMASK");
            return masker.Masked;
        }
        public static string CSMaskSSN(this string value)
        {
            Masker masker = new Masker(value, "SSN");
            return masker.Masked;
        }
        public static T ParseTypeEnum<T>(this string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch (Exception)
            {
                return (T)Enum.Parse(typeof(T), "NONE"); 
            }
            
        }
    }
}
