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
        
        Chat2Entities1 db = new Chat2Entities1();
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
                    
                    System.Web.HttpContext.Current.Session["accesUtilisateur"] = utilisateur.IdAcces;
                    int acces = (int)System.Web.HttpContext.Current.Session["accesUtilisateur"];
                    
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
            string cgu = collection["CGU"];
            string email = collection["pseudoInscription"];
            ViewBag.nom = "";
            ViewBag.prenom = "";
            ViewBag.mail = "";
            ViewBag.pseudo = "";
            ViewBag.mdp = "";
            ViewBag.confirmationmdp ="";
            ViewBag.tel = "";
            int utilisateurExiste2 = (from u in db.Utilisateur where u.PseudoUtilisateur == email  select u).Count();
            if (collection["nomInscription"]=="")
            {
                ViewBag.messageErreure = "le champ nom est obligatoire";
                
                return View("index");

            }
            else if (collection["nomInscription"].Length>30)
            {
                ViewBag.messageErreure = "le nom ne peut pas être supérieure à 20 caractères";
                ViewBag.nom = collection["nomInscription"];
                return View("index");
            }
            else if (collection["prenomInscription"] =="")
            {
                ViewBag.messageErreure = "le champ prénom est obligatoire";
                ViewBag.nom = collection["nomInscription"];
                return View("index");
            }
            else if (collection["prenomInscription"].Length>30)
            {
                ViewBag.messageErreure = "le prénom ne doit pas être supérieure à 20 caractères";
                ViewBag.nom = collection["nomInscription"];
                ViewBag.prenom = collection["prenomInscription"];
                return View("index");
            }
            else if (collection["emailInscription"] == "")
            {
                ViewBag.messageErreure = "le champ email est obligatoire";
                ViewBag.nom = collection["nomInscription"];
                ViewBag.prenom = collection["prenomInscription"];
                return View("index");
            }
            else if (collection["pseudoInscription"] == "")
            {
                ViewBag.nom = collection["nomInscription"];
                ViewBag.prenom = collection["prenomInscription"];
                ViewBag.mail = collection["emailInscription"];
                ViewBag.tel = collection["numeroInscription"];
                ViewBag.messageErreure = "le champ pseudo est obligatoire";
                return View("index");
            }
            else if (collection["pseudoInscription"].Length>30)
            {
                ViewBag.messageErreure = "le pseudo doit faire 15 caractères max";
                ViewBag.nom = collection["nomInscription"];
                ViewBag.prenom = collection["prenomInscription"];
                ViewBag.mail = collection["emailInscription"];
                ViewBag.pseudo = collection["pseudoInscription"];
                ViewBag.tel = collection["numeroInscription"];
                return View("index");
            }
            else if (utilisateurExiste2>0)
            {
                ViewBag.messageErreure = "Ce pseudo éxiste déjà";
                ViewBag.nom = collection["nomInscription"];
                ViewBag.prenom = collection["prenomInscription"];
                ViewBag.mail = collection["emailInscription"];
                ViewBag.pseudo = collection["pseudoInscription"];
                ViewBag.tel = collection["numeroInscription"];
                return View("index");
            }
            else if (collection["MDPInscription"] == "")
            {
                ViewBag.nom = collection["nomInscription"];
                ViewBag.prenom = collection["prenomInscription"];
                ViewBag.pseudo = collection["pseudoInscription"];
                ViewBag.mail = collection["emailInscription"];
                ViewBag.tel = collection["numeroInscription"];
                ViewBag.messageErreure = "le champ mot de passe est obligatoire";
                
                return View("index");
            }
            else if (collection["MDPInscription"].Length<8)
            {
                ViewBag.nom = collection["nomInscription"];
                ViewBag.prenom = collection["prenomInscription"];
                ViewBag.mail = collection["emailInscription"];
                ViewBag.pseudo = collection["pseudoInscription"];
                ViewBag.tel = collection["numeroInscription"];
                ViewBag.messageErreure = "le mot de passe doit faire plus de 8 caractères";
                return View("index");
            }
            else if (collection["confirmationMDP"] == "" || collection["confirmationMDP"] != collection["MDPInscription"])
            {
                ViewBag.nom = collection["nomInscription"];
                ViewBag.prenom = collection["prenomInscription"];
                ViewBag.mail = collection["emailInscription"];
                ViewBag.pseudo = collection["pseudoInscription"];
                ViewBag.tel = collection["numeroInscription"];
                ViewBag.mdp = collection["MDPInscription"]; 
                ViewBag.messageErreure = "le mot de passe de confirmation est différent du mot de passe choisi";
                return View("index");
            }
            else if (collection["dateNaissance"] == "")
            {
                ViewBag.nom = collection["nomInscription"];
                ViewBag.prenom = collection["prenomInscription"];
                ViewBag.mail = collection["emailInscription"];
                ViewBag.pseudo = collection["pseudoInscription"];
                ViewBag.tel = collection["numeroInscription"];
                ViewBag.mdp = collection["MDPInscription"];
                ViewBag.confirmationmdp = collection["confirmationmdp"];
                ViewBag.messageErreure = "veillez renseigner votre date de naissance";
                return View("index");
            }
            else if (collection["CGU"]=="false")
            {
                ViewBag.nom = collection["nomInscription"];
                ViewBag.prenom = collection["prenomInscription"];
                ViewBag.mail = collection["emailInscription"];
                ViewBag.pseudo = collection["pseudoInscription"];
                ViewBag.tel = collection["numeroInscription"];
                ViewBag.mdp = collection["MDPInscription"];
                ViewBag.confirmationmdp = collection["confirmationmdp"];
                ViewBag.date = collection["dateNaissance"];
                ViewBag.messageErreure = "Vous devez accepter les condition d'utilisation";
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
                        utilisateur.PrenomUtilisateur = collection["prenomInscription"];
                        utilisateur.PseudoUtilisateur = collection["pseudoInscription"];
                        utilisateur.EmailUtilisateur = collection["emailInscription"];
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
                        int tokenExist = (from u in db.Utilisateur where u.TokenUtilisateur == utilisateur.TokenUtilisateur select u).Count();
                        if (tokenExist>0)
                        {
                            int test = tokenExist;
                            
                            while (tokenExist>0)
                            {
                                utilisateur.TokenUtilisateur = workflow.createToken();
                                test--;
                            }
                        }
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

            if (workflow.verifierAcces()==true)
            {
                return View();
            }

            return View("Index");

        }

        public ActionResult Deconnexion()
        {
            System.Web.HttpContext.Current.Session["acces"] = 4;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Administration()
        {
            return View();
        }
    }
}