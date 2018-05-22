using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// holds indivdual game metaData
/// </summary>
namespace AIFinal.App_Code
{
    public class metaDataGen
    {
        public double match_id = 0;
        public double weightedGame = 0;
        public double weightedEmote = 0;

        public metaDataGen(string lofMeta)
        {
                try { 
                string[] f = System.IO.File.ReadAllLines(lofMeta);
                double tempgameWeight = 0;
                string strInten = f[2].Replace('.', ',');
                if (double.TryParse(strInten, out tempgameWeight))
                    weightedGame = tempgameWeight;

                double tempEmote = 0;
                string strEmote = f[2].Replace('.', ',');
                if (double.TryParse(strEmote, out tempEmote))
                    weightedEmote = tempEmote;

                double match = 0;
                string strmatc = f[2].Replace('.', ',');
                if (double.TryParse(strmatc, out match))
                    match_id = match;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

}