using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteChat3.Models;
using System.Configuration;
using System.Globalization;
using System.Data.Entity;

namespace SiteChat3
{
    public class Workflow
    {
        public string createToken()
        {
            
            var rand = new Random();

            string[] choix = new string[36] {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","0","1","2","3","4","5","6","7","8","9"};
            string token = "";
            for (int i = 0; i < 40; i++)
            {
                
                token += choix[rand.Next(0,36)];
            }
            return token;
        }

        public bool verifierAcces()
        {
            try
            {
                int accesUtilisateur = (int)(System.Web.HttpContext.Current.Session["accesUtilisateur"]);
                if (accesUtilisateur == 4)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception)
            {

                return false;
            }
            
           
        }
    }
}