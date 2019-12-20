using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiteChat3.Models;

namespace SiteChat3.Controllers
{
    public class MessagesController : Controller
    {
        private Chat2Entities1 db = new Chat2Entities1();

        // GET: Messages
        public async Task<ActionResult> Index()
        {
            var message = db.Message.Include(m => m.Discussion).Include(m => m.Ton).Include(m => m.Utilisateur);
            return View(await message.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Message.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion");
            ViewBag.IdTon = new SelectList(db.Ton, "IdTon", "LibelleTon");
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur");
            return View();
        }

        // POST: Messages/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdMessage,IdUtilisateur,DateEnvoi,TexteMessage,IdDiscussion,IdTon,StatutMessage")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Message.Add(message);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion", message.IdDiscussion);
            ViewBag.IdTon = new SelectList(db.Ton, "IdTon", "LibelleTon", message.IdTon);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", message.IdUtilisateur);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Message.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion", message.IdDiscussion);
            ViewBag.IdTon = new SelectList(db.Ton, "IdTon", "LibelleTon", message.IdTon);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", message.IdUtilisateur);
            return View(message);
        }

        // POST: Messages/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdMessage,IdUtilisateur,DateEnvoi,TexteMessage,IdDiscussion,IdTon,StatutMessage")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion", message.IdDiscussion);
            ViewBag.IdTon = new SelectList(db.Ton, "IdTon", "LibelleTon", message.IdTon);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", message.IdUtilisateur);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Message.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Message message = await db.Message.FindAsync(id);
            db.Message.Remove(message);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*public ActionResult MessagesApi()
        {
            
            return View();
        }*/

        [HttpPost]
        public ActionResult Connexion(FormCollection collection)
        {
            
            var MailUtilisateur = collection["mail"];
            var MDPUtilisateur = collection["MDP"];

            
            if (MailUtilisateur=="" || MDPUtilisateur=="")
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

        public ActionResult Inscription(FormCollection collection)
        {
            ViewBag.messageErreure = "";
            var MailUtilisateur = collection["mail"];
            var MDPUtilisateur = collection["MDP"];
            int utilisateurExiste = (from u in db.Utilisateur where u.EmailUtilisateur == MailUtilisateur select u).Count();
            if (utilisateurExiste > 0)
            {
                Console.WriteLine("ok");
                ViewBag.MessageErreure = "un compte associé à cette adresse mail existe déjà";
                return RedirectToAction("Index", "Home", ViewBag.MessageErreure);
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
                    utilisateur.NumeroUtilisateur = collection["numeroInscription"];
                    utilisateur.DateDeNaissanceUtilisateur = Convert.ToDateTime(collection["dateNaissance"]);
                    utilisateur.DateCreationUtilisateur = DateTime.Now;
                    utilisateur.MotDePasseUtilisateur = collection["MDPInscription"];
                    utilisateur.IdStatutUtilisateur = 1;
                    utilisateur.TokenUtilisateur = "sd4gs64g54s63s64ss4dv64";
                    db.Utilisateur.Add(utilisateur);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
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
        public List<Discussion> GetDiscussions(string requestToken)
        {
            List<Discussion> discussions =(from d in db.Discussion select d).ToList();
            return discussions;
        }
    }
}
