using SiteChat3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SiteChat3.Controllers
{
    public class HomeController : Controller
    {
        
        Chat2Entities db = new Chat2Entities();
        Workflow workflow = new Workflow();
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }
        public ActionResult Index(string erreure)
        {
            
            
            Utilisateur utilisateur = new Utilisateur();
           


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult Connexion(FormCollection collection)
        {

            var MailUtilisateur = collection["mail"];
            var MDPUtilisateur = collection["MDP"];


            if (MailUtilisateur == "" || MDPUtilisateur == "")
            {
                ViewBag.messageErreureConnexion = "email et/ou mot de passe incorrecte(s)";
                return View("Index");
            }
            else
            {
                int utilisateurExiste = (from u in db.Utilisateur where u.EmailUtilisateur == MailUtilisateur && u.MotDePasseUtilisateur == MDPUtilisateur select u).Count();
                if (utilisateurExiste > 0)
                {
                    Utilisateur utilisateur = (from u in db.Utilisateur where u.EmailUtilisateur == MailUtilisateur && u.MotDePasseUtilisateur == MDPUtilisateur select u).First();
                    System.Web.HttpContext.Current.Session["acces"] = utilisateur.IdAcces;
                    
                    ViewBag.requestToken = utilisateur.TokenUtilisateur;
                    return View("MessagesApi");

                }
                else
                {

                    ViewBag.messageErreureConnexion = "email et/ou mot de passe incorrecte(s)";
                    return View("Index");
                };
            }


        }





        [HttpPost]
        
        public  ActionResult Inscription(FormCollection collection)
        {
            if (collection["nomInscription"]=="")
            {
                ViewBag.messageErreure = "le champ nom est obligatoire";
                return View("index");

            }
            else if (collection["prenonInscription"] =="")
            {
                ViewBag.messageErreure = "le champ prénom est obligatoire";
                return View("index");
            }
            else if (collection["emailIncription"] == "")
            {
                ViewBag.messageErreure = "le champ email est obligatoire";
                return View("index");
            }
            else if (collection["pseudoInscription"] == "")
            {
                ViewBag.messageErreure = "le champ pseudo est obligatoire";
                return View("index");
            }
            
            else if (collection["MDPInscription"] == "")
            {
                ViewBag.messageErreure = "le champ mot de passe est obligatoire";
                return View("index");
            }
            else if (collection["confirmationMDP"] == "" || collection["confirmationMDP"] != collection["MDPInscription"])
            {
                ViewBag.messageErreure = "le mot de passe de confirmation est différent du mot de passe choisi";
                return View("index");
            }
            else if (collection["dateNaissance"] == "")
            {
                ViewBag.messageErreure = "veillez renseigner votre date de naissance";
                return View("index");
            }
            else
            {
                var MailUtilisateur = collection["emailIncription"];
                var MDPUtilisateur = collection["MDPInscription"];
                int utilisateurExiste = (from u in db.Utilisateur where u.EmailUtilisateur == MailUtilisateur select u).Count();
                if (utilisateurExiste > 0)
                {
                    Console.WriteLine("ok");
                    ViewBag.MessageErreure = "un compte associé à cette adresse mail existe déjà";
                    return View("index");
                }
                else
                {
                    if (ModelState.IsValid)
                    {

                        Utilisateur utilisateur = new Utilisateur();
                        utilisateur.NomUtilisateur = collection["nomInscription"];
                        utilisateur.PrenomUtilisateur = collection["prenonInscription"];
                        utilisateur.PseudoUtilisateur = collection["pseudoInscription"];
                        utilisateur.EmailUtilisateur = collection["emailIncription"];
                        utilisateur.IdAvatar = 1;
                        utilisateur.IdAcces = 4;
                        if (collection["numeroInscription"].ToString() == "")
                        {
                            utilisateur.NumeroUtilisateur = "0000000000";
                        }
                        else
                        {
                            utilisateur.NumeroUtilisateur = collection["numeroInscription"].ToString();
                        }

                        utilisateur.DateDeNaissanceUtilisateur = Convert.ToDateTime(collection["dateNaissance"]);
                        utilisateur.DateCreationUtilisateur = DateTime.Now;
                        utilisateur.MotDePasseUtilisateur = collection["MDPInscription"];
                        utilisateur.IdStatutUtilisateur = 1;
                        utilisateur.TokenUtilisateur = workflow.createToken();
                        db.Utilisateur.Add(utilisateur);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    /*ViewBag.messageErreure = "";
                    //Instanciation du client
                    SmtpClient smtpClient = new SmtpClient("webmaster@cucarachat.com", 25);
                    //On indique au client d'utiliser les informations qu'on va lui fournir
                    smtpClient.UseDefaultCredentials = true;
                    //Ajout des informations de connexion
                    smtpClient.Credentials = new System.Net.NetworkCredential("webmaster@cucarachat.com", "hq2y5877hsb6");
                    //On indique que l'on envoie le mail par le réseau
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //On active le protocole SSL
                    smtpClient.EnableSsl = true;

                    MailMessage mail = new MailMessage();
                    //Expéditeur
                    mail.From = new MailAddress("webmaster@cucarachat.com", "Cucarachat");
                    //Destinataire
                    mail.To.Add(new MailAddress("thibaustin@hotmail.fr"));

                    mail.Subject = "lien de confirmation";
                    mail.Body = "dxfb1d35hb4fd54h53t4d35ffh3h3";
                    //Copie
                    //mail.CC.Add(new MailAddress("toto@gmail.com"));

                    smtpClient.Send(mail);*/


                }
            }
            


        }
        public ActionResult MessagesApi()
        {
            return View();
        }


    }
}