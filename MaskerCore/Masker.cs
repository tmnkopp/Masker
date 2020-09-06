using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MaskerCore
{
    public class Masker
    {

        #region CTOR
         
        private MaskFormulaProvider _provider;
        public Masker()
        { 
            _provider = new MaskFormulaProvider(); 
        }
        public Masker(string Input, Func<string,string> Formatter): this()
        { 
            Strategy = Formatter;
            _input = Input;
        }
        public Masker(string Input, string ProviderCode) : this()
        {
            _MaskFormula = new MaskFormula(ProviderCode) { 
                MaskFormulaType= MaskFormulaTypes.REGEX
            };
            Strategy = RegexStrategy;
            _ProviderCode = ProviderCode;
            _input = Input;
        }
        #endregion

        #region Props 
        private string _input; 
        private string _ProviderCode;
        public MaskFormula _MaskFormula; 
        private Func<string,string> _Strategy; 
        public Func<string,string> Strategy
        {
            get { return _Strategy ; }
            set { _Strategy = value; }
        }

        private string _Masked; 
        public string Masked
        {
            get {
                if (_Masked == null) 
                    Mask(); 
                return this._Masked;
            }
        } 
        public string Unmasked
        {
            get { return _input; } 
        }

        #endregion

        #region Methods

        private void Mask() { 
            if (Strategy==null) 
                Strategy = DefaultStrategy; 
            _Masked = Strategy(_input); 
        } 
 
        #endregion
         
        #region Delegates
        private string DefaultStrategy(string Input) {
            string _masked = "";
            Input = Input.Replace(" ","-"); 
            for (int pos = 0; pos < Input.Length; pos++)
            {
                string chr = Input.Substring(pos, 1);
                Match match = Regex.Match(chr, @"\d");
                if (match.Success) 
                    _masked += MaskFormula.DefaultMaskChar; 
                else
                    _masked += chr;
            }
            return _masked;
        }
        private string RegexStrategy(string Input) {
            MaskFormula maskFormula = _provider.MaskFormulas
                .Where(m => m.ProviderCode == _ProviderCode)
                .Single();
            Match match = Regex.Match(Input, maskFormula.MaskExpression);
            string _masked;
            int startPos = 0;
            int matchLen = 0;
            if (match.Success)
            {
                startPos = match.Groups.Count > 0 ? match.Groups[1].Index : 0;
                matchLen = match.Groups.Count > 0 ? match.Groups[1].Length : 0;
            }
            _masked = Input.Substring(0, startPos);
            for (int pos = startPos; pos < Input.Length; pos++)
            {
                string chr = Input.Substring(pos, 1);
                match = Regex.Match(chr, maskFormula.ReplaceRegex);
                if (match.Success && pos < (matchLen + startPos)) 
                    _masked += maskFormula.MaskChar; 
                else
                    _masked += chr;
            }
            return _masked;
        }
        #endregion

    }
}
