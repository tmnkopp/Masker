using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskerCore
{
    public class MaskFormulaProvider
    { 
        public IEnumerable<MaskFormula> MaskFormulas
        {
            get { return this.GetMaskFormulas(); }
        }
        public IEnumerable<MaskFormula> GetMaskFormulas()
        { 
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CAClientConnectionString"].ConnectionString)){
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_MaskFormulas", conn))  {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MODE", "MASK_FORMULAS");
                    using (SqlDataReader rdr = cmd.ExecuteReader())  {
                        while (rdr.Read())   {
                            yield return new MaskFormula()
                            { 
                                ProviderCode = rdr["ProviderCode"].ToString(),
                                MaskFormulaType = GetMaskFormulaType(rdr["FormulaType"].ToString()),
                                MaskChar = GetMaskChar(rdr["MaskChar"].ToString()),
                                ReplaceRegex = rdr["ReplaceRegex"].ToString(),
                                MaskExpression = rdr["MaskExpression"].ToString()

                            }; 
                        }
                    }
                }
            }
        }
        private string GetMaskChar(string MaskChar)
        {
            if (string.IsNullOrEmpty(MaskChar))
                return MaskFormula.DefaultMaskChar;
            return MaskChar;
        }
        private MaskFormulaTypes GetMaskFormulaType(string FormulaType) {
            if (string.IsNullOrEmpty(FormulaType)) 
                return MaskFormulaTypes.NONE;
            return FormulaType.ParseTypeEnum<MaskFormulaTypes>();
        }
    }
}
