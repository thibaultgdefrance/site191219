var element = document.getElementById("affichageMessages");
var tokenUtilisateur = document.getElementById("utilisateurToken");

function boucle() {
    if (document.getElementById("discussionToken") != "") {
        afficherMessages(document.getElementById("discussionToken").value, document.getElementById("utilisateurToken").value);
    }
   
    
    
    console.log("boucle");
}
function timerBoucle() {
    setInterval(boucle, 10000)
}
timerBoucle();

//créer une nouvelle discussion de groupe
function creerDiscussion() {
    console.log("discussion créée!")
}





//afficher les messages quand on clic sur une discussion
function afficherMessages(discussionToken2,typeDiscussion) {
    var messagesVisibles = document.getElementsByClassName("contourMessage");
    
    document.getElementById("affichageMessages").innerHTML = "";
    document.getElementById("affichageInfo").innerHTML = "";

    /*for (var i = 0; i < messagesVisibles.length; i++) {
        messagesVisibles[i].setAttribute("style", "height:0px;");
        messagesVisibles[i].setAttribute("style", "display:none;");

    }*/
    
    console.log(discussionToken2);
    document.getElementById("discussionToken").value = discussionToken2;
    var tokenDiscussion = discussionToken2;

    var urlMessages = "http://localhost:61994/api/Messages?tokenDiscussion=" + document.getElementById("discussionToken").value + "&tokenUtilisateur=" + document.getElementById("utilisateurToken").value;
    console.log(urlMessages);
    
    $.ajax({
        url: urlMessages,
        type: 'GET',
        dataType: 'json',
        success: function (data3) {
            if (data3 == null) {
                console.log("null")
            } else {
               
                for (var i = 0; i < data3.length; i++) {
                    //discussionGroupe.textContent += data2[i].TitreDiscussion;
                    var contourMessage = document.createElement("div");

                    var cadre = document.createElement("div");
                    var blocMessage = document.createElement("div");
                    blocMessage.setAttribute("id", "blocMessage" + i);
                    if (data3[i].VerifMessage==true) {
                        blocMessage.setAttribute("class", "blocMessage2");
                        cadre.setAttribute("class", "cadre2");
                    } else {
                        blocMessage.setAttribute("class", "blocMessage");
                        cadre.setAttribute("class","cadre")
                    }


                    var texteMessage = document.createElement("p");
                    var dateEnvoi = document.createElement("p");
                    
                    dateEnvoi.textContent = data3[i].PseudoUtilisateur + " " + data3[i].DateEnvoi;
                    dateEnvoi.setAttribute("class", "dateEnvoi");
                    texteMessage.setAttribute("id", data3[i].IdMessage);
                    texteMessage.setAttribute("class", "texteMessages");
                    texteMessage.textContent = data3[i].TexteMessage;
                    document.getElementById("affichageMessages").appendChild(contourMessage);
                    cadre.appendChild(texteMessage);
                    cadre.appendChild(dateEnvoi);
                    blocMessage.appendChild(cadre);
                    contourMessage.appendChild(blocMessage);
                    
                    var retour = document.createElement("br");
                    document.getElementById("affichageMessages").appendChild(retour);
                    document.getElementById("titreDiscussionEnCour").innerText = data3[i].TitreDiscussion;

                }

                element.scrollTop = element.scrollHeight;
                afficherInfo(typeDiscussion);
            }
        },


        error: function () {
            console.log("erreure");
        }

    });    
    
}









//afficher les infos d'une discussion
function afficherInfo(typeDiscussion) {
    console.log("affichageInfo" + typeDiscussion);
    var urlInfo = "http://localhost:61994/api/Utilisateurs?tokenDiscussion=" + document.getElementById("discussionToken").value + "&tokenUtilisateur=" + document.getElementById("utilisateurToken").value;
    $.ajax({
        url: urlInfo,
        type: 'GET',
        dataType: 'json',
        success: function (data4) {
            if (data4 == null) {
                
                console.log("null")
            } else {

                if (data4[0].Verif == true && typeDiscussion==2) {
                    
                    document.getElementById("affichageInfo").innerHTML = "";
                    var contourBarreDeRecherche = document.createElement("div");
                    contourBarreDeRecherche.setAttribute("id", "contourBarreDeRecherche");
                    var barreDeRecherche = document.createElement("input");
                    barreDeRecherche.setAttribute("type", "search");
                    barreDeRecherche.setAttribute("placeholder", "inviter un nouveau participant");
                    barreDeRecherche.setAttribute("id", "barreDeRecherche");
                    barreDeRecherche.setAttribute("class", "form-control");
                    barreDeRecherche.setAttribute("onkeypress", "rechercheParticipant('listeRecherche')");
                    barreDeRecherche.setAttribute("onblur", "effacerRecherche('listeRecherche')");
                    barreDeRecherche.setAttribute("value", "");
                    var listeRecherche = document.createElement("div");
                    listeRecherche.setAttribute("style", "height:auto;max-height:300px;width:100%;max-width:250px;background-color:white;position:fixed;overflow-y:auto");
                    listeRecherche.setAttribute("id", "listeRecherche");
                    contourBarreDeRecherche.appendChild(barreDeRecherche);
                    contourBarreDeRecherche.appendChild(listeRecherche);
                    document.getElementById("affichageInfo").appendChild(contourBarreDeRecherche);
                    console.log("input créer");
                } else {
                    console.log("nope");
                }

                
                
                var titreParticipant = document.createElement("h2");
                titreParticipant.innerText = "Participants";
                document.getElementById("affichageInfo").appendChild(titreParticipant);
                for (var i = 0; i < data4.length; i++) {
                    var participant = document.createElement("div");
                    var pseudoParticipant = document.createElement("p");
                    pseudoParticipant.setAttribute("style","color:grey;font-size:20px;")
                    var emailParticipant = document.createElement("p");
                    emailParticipant.setAttribute("style", "color:grey;font-size:10px;")
                    pseudoParticipant.innerText = data4[i].PseudoUtilisateur;
                    emailParticipant.innerText = data4[i].EmailUtilisateur;
                    participant.appendChild(pseudoParticipant);
                    participant.appendChild(emailParticipant);
                    participant.setAttribute("onclick", "creerNotif");
                    document.getElementById("affichageInfo").appendChild(participant);
                    console.log("utilisateur ajouté");
                }  
            }
        },


        error: function () {
            console.log("erreure");
        }

    });
}



















//afficher les discussions de l'utilisateur 






function afficherDiscussions() {
        document.getElementById("discussionsGroupe").innerHTML = "";
        var discussionGroupe = document.createElement("p");
        discussionsGroupe.appendChild(discussionGroupe);
        var utilisateurToken = document.getElementById('utilisateurToken').value;
        var urlGetDiscussion = 'http://localhost:61994/api/Discussions?token=' + utilisateurToken;
        console.log("!!"+urlGetDiscussion);
        $.ajax({
            url: urlGetDiscussion,
            type: 'GET',
            dataType: 'json',
            success: function (data2) {
                console.log(data2);
                for (var i = 0; i < data2.length; i++) {
                    //discussionGroupe.textContent += data2[i].TitreDiscussion;
                    var x = document.createElement("div");
                    x.setAttribute("class", "blocDiscussion")
                    var b = document.createElement("button");
                    b.setAttribute("id", data2[i].TokenDiscussion);
                    b.textContent = data2[i].TitreDiscussion;
                    b.setAttribute("class", "titreDiscussion");
                    b.setAttribute("onClick", "afficherMessages(" + "'" + data2[i].TokenDiscussion + "'" + ",2)");


                    discussionGroupe.appendChild(x);
                    x.appendChild(b);
                }


            },

            error: function (data2) {
                console.log("Erreur:"+data2);
            }

        });
    }    
    

afficherDiscussions();
afficherContacts();

function afficherContacts() {
    document.getElementById("listeContacts").innerHTML = "";
    var listeContacts = document.getElementById("listeContacts");
    
    
    
    var urlGetContact = "http://localhost:61994/api/Discussions?token=" + document.getElementById("utilisateurToken").value + "&dif=1";
    $.ajax({
        url: urlGetContact,
        dataType: 'json',
        method: 'GET',
        success: function (data2) {
            if (data2 != null) {
                for (var i = 0; i < data2.length; i++) {
                    
                    var blocContact = document.createElement("div");
                    blocContact.setAttribute("class", "blocDiscussion");
                    var nomContact = document.createElement("button");
                    nomContact.setAttribute("class", "titreDiscussion");
                    nomContact.innerText = data2[i].NomContact;
                    blocContact.appendChild(nomContact);
                    token = data2[i].TokenDiscussion;
                    nomContact.setAttribute("onclick", "afficherMessages(" + "'" + data2[i].TokenDiscussion + "'" + ",1)");
                    /*blocContact.addEventListener("click", function () {
                        afficherMessages(token);

                    });*/
                    listeContacts.appendChild(blocContact);
                }
            }
            
            
        }


        ,

        error: function () {
            console.log("érreure");
        }

    });
}



















//envoyer le message dans la discussion correspondante

function envoyerMessage() {

    console.log("message envoyé");
    tokenDiscussion = document.getElementById("discussionToken").value;
    var message = document.getElementById("messageEnCours").value;
    var urlPostMessage = "http://localhost:61994/api/Messages?tokenUtilisateur=" + tokenUtilisateur.value + "&tokenDiscussion=" + tokenDiscussion + "&texteMessage="+message+"";
    console.log(urlPostMessage);
    $.ajax({
        url: urlPostMessage,
        dataType: 'json',
        method:'POST',
        success: function (data2) {
            
            document.getElementById("messageEnCours").value = "";
            afficherMessages(tokenDiscussion);
        }


        ,

        error: function () {
            console.log("érreure");
        }

    });
    
}











function creerModal() {
    console.log("modal créee");
    var modal = document.createElement("div");
    var labelTitre = document.createElement("label");
    labelTitre.textContent = "Titre de votre discussion";
    var inputTitre = document.createElement("input");
    inputTitre.setAttribute("class", "form-control");
    inputTitre.setAttribute("id", "titreDiscussionCreee");
    var labelDescription = document.createElement("label");
    labelDescription.textContent = "description de la discussion";
    var taDescription = document.createElement("textarea");
    taDescription.setAttribute("class", "form-control");
    taDescription.setAttribute("id", "tadescription");
    var btnCreationDiscussion = document.createElement("input");
    btnCreationDiscussion.setAttribute("value", "créer");
    btnCreationDiscussion.setAttribute("type", "submit");
    btnCreationDiscussion.setAttribute("class", "btn btn-succes");
    btnCreationDiscussion.setAttribute("onclick", "creationDiscussion()");
    var btnAnnulerDiscussion = document.createElement("input");
    btnAnnulerDiscussion.setAttribute("type", "submit");
    btnAnnulerDiscussion.setAttribute("class", "btn btn-alert");
    btnAnnulerDiscussion.setAttribute("onclick", "fermerModal()");
    btnAnnulerDiscussion.setAttribute("value","annuler")
    modal.appendChild(labelTitre);
    modal.appendChild(inputTitre);
    modal.appendChild(labelDescription);
    modal.appendChild(taDescription);
    modal.appendChild(btnCreationDiscussion);
    modal.appendChild(btnAnnulerDiscussion);
    modal.setAttribute("style", "background-color:white;height:40%;width:40%;position;position:fixed;top:20vh;left:20vw;border-radius:20px;padding:50px;");
    modal.setAttribute("class", "modalDiscussion");
    document.body.appendChild(modal);
}

function fermerModal() {
    console.log("discussion annnuler")
    var modals = document.getElementsByClassName("modalDiscussion");
    for (var i = 0; i < modals.length; i++) {
        modals[i].setAttribute("style", "display:none;");
    }
    document.getElementById("titreDiscussionCreee").setAttribute("id", "none");
    document.getElementById("tadescription").setAttribute("id", "none");
}

element.scrollTop = element.scrollHeight;



//créer une discussion


function creationDiscussion() {
    var titre = document.getElementById("titreDiscussionCreee").value;
    var description = document.getElementById("tadescription").value;
    var createur = document.getElementById("utilisateurToken").value;
    if (titre.length > 20 || description.length > 300) {
        var modals = document.getElementsByClassName("modalDiscussion");
        for (var i = 0; i < modals.length; i++) {
            var messageErreure = document.createElement("p");
            messageErreure.innerText = "titre 20 caractères max, description 300 caractères max";
            messageErreure.setAttribute("style", "color:red;");
            
            modals[i].appendChild(messageErreure);
        }

    } else {
        var urlPostDiscussion = "http://localhost:61994/api/Discussions?titre=" + titre + "&description=" + description + "&tokenUtilisateur=" + createur;
        $.ajax({
            url: urlPostDiscussion,
            dataType: 'json',
            method: 'POST',
            success: function (data) {
                var urlPostUtilisateurDiscussion = "http://localhost:61994/api/UtilisateurDiscussions?utilisateurToken=" + createur;
                $.ajax({
                    url: urlPostUtilisateurDiscussion,
                    dataType: 'json',
                    method: 'POST',
                    success: function (data) {
                        fermerModal();
                        afficherDiscussions();

                    }


                    ,

                    error: function () {
                        console.log("érreure");
                    }
                })

            }


            ,

            error: function () {
                console.log("érreure");
            }
        })
    }
   
    
    

   
}


function rechercheParticipant(cible) {
    
    document.getElementById(cible).innerHTML = "";
    var textetest = document.createElement("p");
    textetest.innerText = "test";
    if (cible=="rechercheContact") {
        var texteRecherche = document.getElementById("barreDeRechercheContact").value;
    } else {
        var texteRecherche = document.getElementById("barreDeRecherche").value;
    }
    
    var urlRecherche = "http://localhost:61994/api/Utilisateurs?entree="+texteRecherche;
    console.log(urlRecherche);
    $.ajax({
        url: urlRecherche,
        dataType: 'json',
        method: 'GET',
        success: function (data5) {
            
            
            for (var i = 0; i < data5.length; i++) {
                var resultRecherche = document.createElement("div");
                var pseudoResult = (document.createElement("p"));
                
                pseudoResult.innerHTML = data5[i].PseudoUtilisateur;
                var emailResult = (document.createElement("p"));
                emailResult.innerText = data5[i].EmailUtilisateur;
                resultRecherche.appendChild(pseudoResult);
                resultRecherche.appendChild(emailResult);
                resultRecherche.setAttribute("onclick", "creationModalNotif("+"'" +data5[i].EmailUtilisateur+"'"+",'groupe')");
                resultRecherche.setAttribute("class", "resultRecherche");
                resultRecherche.setAttribute("id", data5[i].EmailUtilisateur);
                document.getElementById(cible).appendChild(resultRecherche);

                
            }
            

        }


        ,

        error: function () {
            console.log("erreure");
        }
    })
    
}






function rechercheContact(cible) {
    document.getElementById(cible).innerHTML = "";
    var textetest = document.createElement("p");
    textetest.innerText = "test";
    if (cible == "rechercheContact") {
        var texteRecherche = document.getElementById("barreDeRechercheContact").value;
    } else {
        var texteRecherche = document.getElementById("barreDeRecherche").value;
    }

    var urlRecherche = "http://localhost:61994/api/Utilisateurs?entree=" + texteRecherche;
    console.log(urlRecherche);
    $.ajax({
        url: urlRecherche,
        dataType: 'json',
        method: 'GET',
        success: function (data5) {


            for (var i = 0; i < data5.length; i++) {
                var resultRecherche = document.createElement("div");
                var pseudoResult = (document.createElement("p"));

                pseudoResult.innerHTML = data5[i].PseudoUtilisateur;
                var emailResult = (document.createElement("p"));
                emailResult.innerText = data5[i].EmailUtilisateur;
                resultRecherche.appendChild(pseudoResult);
                resultRecherche.appendChild(emailResult);
                resultRecherche.setAttribute("onclick", "creationModalNotif(" + "'" + data5[i].EmailUtilisateur + "'" + ",'contact')");
                resultRecherche.setAttribute("class", "resultRecherche");
                resultRecherche.setAttribute("id", data5[i].EmailUtilisateur);
                document.getElementById(cible).appendChild(resultRecherche);


            }


        }


        ,

        error: function () {
            console.log("erreure");
        }
    })
}




function effacerRecherche(cible) {
    setTimeout(function () { document.getElementById(cible).innerHTML = ""; }, 500);
    
}

function creationModalNotif(email, typeInvitation) {
    if (typeInvitation == "groupe") {
        document.getElementById("modaleEnvoiNotif").innerHTML = "";
        document.getElementById("modaleEnvoiNotif").setAttribute("style", "background-color:white;height:auto;width:20%;position:fixed;top:20vh;left:40vw;border-radius:20px;padding:50px;overflow:auto;borde:1px solid grey;display:inline;")
        var texteEmail = document.createElement("p");
        texteEmail.innerText = "voulez-vous inviter " + email + " à participer à cette discussion?";
        var btnValiderNotif = document.createElement("input");
        btnValiderNotif.setAttribute("type", "submit");
        btnValiderNotif.setAttribute("class", "form-control");
        btnValiderNotif.setAttribute("value", "valider");
        btnValiderNotif.setAttribute("onclick", "envoyerNotif(" + "'" + email + "'" + ")");
        var btnAnnulerNotif = document.createElement("input");
        btnAnnulerNotif.setAttribute("type", "submit");
        btnAnnulerNotif.setAttribute("class", "form-control");
        btnAnnulerNotif.setAttribute("value", "annuler");
        btnAnnulerNotif.setAttribute("onclick", "fermerNotif()");
        document.getElementById("modaleEnvoiNotif").appendChild(texteEmail);
        document.getElementById("modaleEnvoiNotif").appendChild(btnValiderNotif);
        document.getElementById("modaleEnvoiNotif").appendChild(btnAnnulerNotif);
    } else {
        document.getElementById("modaleEnvoiNotif").innerHTML = "";
        document.getElementById("modaleEnvoiNotif").setAttribute("style", "background-color:white;height:auto;width:20%;position:fixed;top:20vh;left:40vw;border-radius:20px;padding:50px;overflow:auto;borde:1px solid grey;display:inline;")
        var texteEmail = document.createElement("p");
        texteEmail.innerText = "voulez-vous inviter " + email + " à faire partie de vos contacts?";
        var btnValiderNotif = document.createElement("input");
        btnValiderNotif.setAttribute("type", "submit");
        btnValiderNotif.setAttribute("class", "form-control");
        btnValiderNotif.setAttribute("value", "valider");
        btnValiderNotif.setAttribute("onclick", "envoyerDemandeContact(" + "'" + email + "'" + ")");
        var btnAnnulerNotif = document.createElement("input");
        btnAnnulerNotif.setAttribute("type", "submit");
        btnAnnulerNotif.setAttribute("class", "form-control");
        btnAnnulerNotif.setAttribute("value", "annuler");
        document.getElementById("modaleEnvoiNotif").appendChild(texteEmail);
        document.getElementById("modaleEnvoiNotif").appendChild(btnValiderNotif);
        document.getElementById("modaleEnvoiNotif").appendChild(btnAnnulerNotif);
    }
    

}

function envoyerNotif(mail) {
    var urlPostNotif = "http://localhost:61994/api/Notifications?tokenUtilisateur=" + document.getElementById("utilisateurToken").value + "&emailDestinataire=" + mail + "&tokenDiscussion=" + document.getElementById("discussionToken").value;
    console.log(urlPostNotif);
    $.ajax({
        url: urlPostNotif,
        dataType: 'json',
        method: 'POST',
        success: function (data) {
            fermerNotif();
            

        }


        ,

        error: function () {
            console.log("érreure");
            fermerNotif();
        }
    })
           

}

function fermerNotif() {
    document.getElementById("modaleEnvoiNotif").innerHTML = "";
    document.getElementById("modaleEnvoiNotif").setAttribute("style", "background-color:white;height:auto;width:20%;position:fixed;top:20vh;left:40vw;border-radius:20px;padding:50px;display:none;box-shadow: 5px 5px 20px;");
}


function afficherNotifications() {
    console.log("notifications affichées");
    document.getElementById("modaleAffichageNotif").innerHTML = "";
    document.getElementById("modaleAffichageNotif").setAttribute("style", "background-color: white; height: auto;max-height:500px; width: 20%; position: fixed; top: 20vh; left: 40vw; border - radius: 20px; padding: 50px;overflow:auto;border-radius:20px;border:1px solid grey;display:inline");
    var btnFermeture = document.createElement("button");
    btnFermeture.innerHTML="fermer";
    btnFermeture.setAttribute("class", "btn btn-danger")
    btnFermeture.setAttribute("style", "color:white;right:10px;top:10px;position:absolute;box-shadow: 5px 5px 20px;");
    btnFermeture.setAttribute("onclick", "fermerAffichageNotif()");
    document.getElementById("modaleAffichageNotif").appendChild(btnFermeture);
    var urlGetNotif = "http://localhost:61994/api/Notifications?tokenUtilisateur=" + document.getElementById("utilisateurToken").value;
    $.ajax({
        url: urlGetNotif,
        dataType: 'json',
        method: 'GET',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].TitreDiscussion == null) {
                    var blocNotification = document.createElement("div");
                    blocNotification.setAttribute("id", "blocNotification" + i);
                    var texteNotif = data[i].EmailCreateur + " veut vous ajouter à ses contacts";
                    var pNotif = document.createElement("p");
                    pNotif.innerText = texteNotif;
                    var btnAccepterInvitation = document.createElement("input");
                    btnAccepterInvitation.setAttribute("type", "submit");
                    btnAccepterInvitation.setAttribute("class", "form-control");
                    btnAccepterInvitation.setAttribute("value", "accepter");
                    btnAccepterInvitation.setAttribute("onclick", "accepterContact(" + "'" + data[i].TokenNotification + "'" + "," + "'" + "blocNotification" + i + "'" + ")");
                    var btnAnnuler = document.createElement("input");
                    btnAnnuler.setAttribute("type", "submit");
                    btnAnnuler.setAttribute("class", "form-control");
                    btnAnnuler.setAttribute("value", "décliner");
                    btnAnnuler.setAttribute("onclick", "declinerInvitation(" + "'" + data[i].TokenNotification + "'" + "," + "'" + "blocNotification" + i + "'" + ")");
                    blocNotification.appendChild(pNotif)
                    blocNotification.appendChild(btnAccepterInvitation);
                    blocNotification.appendChild(btnAnnuler);
                    document.getElementById("modaleAffichageNotif").appendChild(blocNotification);
                } else {
                    var blocNotification = document.createElement("div");
                    blocNotification.setAttribute("id", "blocNotification" + i);
                    var texteNotif = data[i].EmailCreateur + " vous invite à rejoindre la discussion " + data[i].TitreDiscussion;
                    var pNotif = document.createElement("p");
                    pNotif.innerText = texteNotif;
                    var btnAccepterInvitation = document.createElement("input");
                    btnAccepterInvitation.setAttribute("type", "submit");
                    btnAccepterInvitation.setAttribute("class", "form-control");
                    btnAccepterInvitation.setAttribute("value", "accepter");
                    btnAccepterInvitation.setAttribute("onclick", "accepterInvitation(" + "'" + data[i].TokenNotification + "'" + "," + "'" + "blocNotification" + i + "'" + ")");
                    var btnAnnuler = document.createElement("input");
                    btnAnnuler.setAttribute("type", "submit");
                    btnAnnuler.setAttribute("class", "form-control");
                    btnAnnuler.setAttribute("value", "décliner");
                    btnAnnuler.setAttribute("onclick", "declinerInvitation(" + "'" + data[i].TokenNotification + "'" + "," + "'" + "blocNotification" + i + "'" + ")");
                    blocNotification.appendChild(pNotif)
                    blocNotification.appendChild(btnAccepterInvitation);
                    blocNotification.appendChild(btnAnnuler);
                    document.getElementById("modaleAffichageNotif").appendChild(blocNotification);
                }
                
            }


        }


        ,

        error: function () {
            console.log("érreure");
        }
    })

    

}
function accepterInvitation(tokenNotif, idblocNotif) {
    document.getElementById(idblocNotif).innerHTML = "";
    var urlPostUtilisateurDiscussion = "http://localhost:61994/api/UtilisateurDiscussions?utilisateurToken=" + document.getElementById("utilisateurToken").value + "&tokenNotif=" + tokenNotif;
    $.ajax({
        url: urlPostUtilisateurDiscussion,
        dataType: 'json',
        method: 'POST',
        success: function (data) {
            console.log("invitation acceptée");
            effacerNotif(tokenNotif, idblocNotif);
            fermerNotif();
            afficherDiscussions();

        }


        ,

        error: function () {
            console.log("érreure");
            fermerNotif();
        }
    });
   
    
    
}

function declinerInvitation(tokenNotif, idblocNotif) {
    document.getElementById(idblocNotif).innerHTML = "";
    console.log("invitation déclinée");
    effacerNotif(tokenNotif, idblocNotif);
}

function fermerAffichageNotif() {
    
    document.getElementById("modaleAffichageNotif").innerHTML = "";
    document.getElementById("modaleAffichageNotif").setAttribute("style", "background-color: white; height: auto;max-height:500px; width: 200px; position: fixed; top: 20vh; left: 40vw; border - radius: 20px; padding: 50px;display:none");
}

function effacerNotif(tokenNotif, idblocNotif) {
    
    var urlPostSuppressionNotification = "http://localhost:61994/api/Notifications?tokenNotification=" + tokenNotif;
    $.ajax({
        url: urlPostSuppressionNotification,
        dataType: 'json',
        method: 'POST',
        success: function (data) {
            fermerNotif();


        }


        ,

        error: function () {
            console.log("érreure");
            fermerNotif();
        }
    });
}

function envoyerDemandeContact(mail) {
    var urlDemandeContact = "http://localhost:61994/api/Notifications?tokenUtilisateur=" + document.getElementById("utilisateurToken").value + "&emailDestinataire=" + mail;
    $.ajax({
        url: urlDemandeContact,
        dataType: 'json',
        method: 'POST',
        success: function (data) {
            fermerNotif();


        }


        ,

        error: function () {
            console.log("érreure!");
            fermerNotif();
        }
    });
}


function accepterContact(tokenNotif,idBlocNotif) {
    document.getElementById(idBlocNotif).innerHTML = "";
    var urlPostDiscussionContact = "http://localhost:61994/api/Discussions?tokenNotification=" + tokenNotif;
    $.ajax({
        url: urlPostDiscussionContact,
        dataType: 'json',
        method: 'POST',
        success: function (data) {
            console.log("discussion créee")
            var urlPostUtilisateurDiscussion = "http://localhost:61994/api/UtilisateurDiscussions?utilisateurToken=" + document.getElementById("utilisateurToken").value + "&tokenNotif=" + tokenNotif + "&contact=1";
            $.ajax({
                url: urlPostUtilisateurDiscussion,
                dataType: 'json',
                method: 'POST',
                success: function (data) {
                    effacerNotif(tokenNotif, idBlocNotif);
                    fermerNotif();
                    afficherContacts();

                }


                ,

                error: function () {
                    console.log("érreure bordel");
                    fermerNotif();
                }
            });
    /*var urlPostUtilisateurDiscussion1 = "http://localhost:61994/api/UtilisateurDiscussions?utilisateurToken=" + document.getElementById("utilisateurToken").value + "&tokenNotif=" + tokenNotif + "&contact1=1";
        $.ajax({
            url: urlPostUtilisateurDiscussion1,
            dataType: 'json',
            method: 'POST',
            success: function (data) {
                console.log("premier utilisateurDiscussion crée")
    
    
            }
    
    
            ,
    
            error: function () {
                console.log("érreure");
               
            }
        });
    
       
       
        var urlPostUtilisateurDiscussion2 = "http://localhost:61994/api/UtilisateurDiscussions?utilisateurToken=" + document.getElementById("utilisateurToken").value + "&tokenNotif=" + tokenNotif + "&contact1=2";
        $.ajax({
            url: urlPostUtilisateurDiscussion2,
            dataType: 'json',
            method: 'POST',
            success: function (data) {
                console.log("deuxième utilisateurDiscussion crée")
                console.log("contact acceptée");
    
    
            }
    
    
            ,
    
            error: function () {
                console.log("érreure");
                fermerNotif();
            }
        });*/

        }


        ,

        error: function () {
            console.log("érreure");
            
        }
    });

   

    
   
    
}


function afficherListeDonnees(){
    console.log("liste de données affichée");
}


/*document.getElementById("modaleAffichageNotif").addEventListener("blur", function () {
    fermerAffichageNotif();
})*/

