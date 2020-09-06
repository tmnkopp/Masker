using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskerCore
{
    public enum MaskFormulaTypes { 
        NONE,
        REGEX,
        FUNC
    }
   
    public class MaskFormula
    {
        public MaskFormula()
        {

        }
        public MaskFormula(string ProviderCode)
        {
            ProviderCode = ProviderCode;
        }
        public static string DefaultMaskChar = "*";
        public string ProviderCode { get; set; }
        public string MaskExpression { get; set; }
        public string ReplaceRegex { get; set; }
        public MaskFormulaTypes MaskFormulaType { get; set; }   
        private string _MaskChar = MaskFormula.DefaultMaskChar;
        public string MaskChar
        {
            get { return _MaskChar; }
            set { _MaskChar = value; }
        }

    }
}
