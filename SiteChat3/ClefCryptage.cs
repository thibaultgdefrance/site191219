using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteChat3
{
    public class ClefCryptage
    {
        public string create()
        {
            DateTime dateTime = DateTime.Now;
            long graduate = dateTime.Ticks;
            string firstTab = graduate.ToString().Substring(0, 4);
            string upFirstTab = firstTab + "a" + DateTime.Now.Year;

            string secondTab = graduate.ToString().Substring(4, graduate.ToString().Length - 4);
            string upSecondTab = secondTab + "yx" + DateTime.Now.Month;
            return upFirstTab + upSecondTab;
        }


        public bool verify(string token)
        {
            DateTime dateTime = DateTime.Now;
            DateTime dateTime2 = DateTime.Now.AddSeconds(-30);

            //long graduate = dateTime.Ticks;
            string firstTab = token.ToString().Substring(0, 4);
            string secondTab = token.ToString().Substring(9);
            string[] thirdTable = secondTab.Split('y');
            try
            {
                long getToken = long.Parse(firstTab + thirdTable[0]);
                DateTime dtTimeToken = new DateTime(getToken);
                if (dtTimeToken != null && dateTime2 < dtTimeToken && dateTime > dtTimeToken)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;

            }
            return false;
        }
    }
}