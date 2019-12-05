﻿var element = document.getElementById("affichageMessages");
var tokenUtilisateur = document.getElementById("utilisateurToken");




//créer une nouvelle discussion de groupe
function creerDiscussion() {
    console.log("discussion créée!")
}




//afficher les messages quand on clic sur une discussion
function afficherMessages(discussionToken2) {
    var messagesVisibles = document.getElementsByClassName("contourMessage");
    
    for (var i = 0; i < messagesVisibles.length; i++) {
        messagesVisibles[i].setAttribute("style", "height:0px;");
        messagesVisibles[i].setAttribute("style", "display:none;");

    }
    console.log(discussionToken2);
    document.getElementById("discussionToken").value = discussionToken2;
    var tokenDiscussion = discussionToken2;
    var urlMessages = "http://localhost:61994/api/Messages?tokenDiscussion="+tokenDiscussion;
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
                    contourMessage.setAttribute("class", "contourMessage")
                    var blocMessage = document.createElement("div");
                    blocMessage.setAttribute("class", "blocMessage")
                    var texteMessage = document.createElement("p");
                    var dateEnvoi = document.createElement("p");
                    dateEnvoi.textContent = data3[i].PseudoUtilisateur + " " + data3[i].DateEnvoi;
                    dateEnvoi.setAttribute("class", "dateEnvoi");
                    texteMessage.setAttribute("id", data3[i].IdMessage);
                    texteMessage.setAttribute("class", "texteMessages");
                    texteMessage.textContent = data3[i].TexteMessage;
                    document.getElementById("affichageMessages").appendChild(contourMessage);
                    contourMessage.appendChild(blocMessage);
                    blocMessage.appendChild(texteMessage);
                    blocMessage.appendChild(dateEnvoi);
                    var retour = document.createElement("br");
                    document.getElementById("affichageMessages").appendChild(retour);
                }

                element.scrollTop = element.scrollHeight;
            }
            
 
        },
 
        error: function () {
            console.log("erreure");
        }
 
    });
}


//var a = document.getElementById("truc")
//var donnee = document.createElement("p");
var discussionGroupe = document.createElement("p");

//a.appendChild(donnee);
discussionsGroupe.appendChild(discussionGroupe);


//afficher les discussions de l'utilisateur au lancement de la page

$(document).ready(function () {
    

    var utilisateurToken = document.getElementById('utilisateurToken').value;
    var urlGetDiscussion = 'http://localhost:61994/api/Discussions?token='+utilisateurToken;
    console.log(urlGetDiscussion);
    $.ajax({
        url: urlGetDiscussion,
        type: 'GET',
        dataType: 'json',
        success: function (data2) {
            for (var i = 0; i < data2.length; i++) {
                //discussionGroupe.textContent += data2[i].TitreDiscussion;
                var x = document.createElement("div");
                x.setAttribute("class", "blocDiscussion")
                var b = document.createElement("button");
                b.setAttribute("id", data2[i].TokenDiscussion);
                b.textContent = data2[i].TitreDiscussion;
                b.setAttribute("class", "titreDiscussion");
                b.setAttribute("onClick", "afficherMessages(" +"'"+ data2[i].TokenDiscussion +"'"+ ")");
               
               
                discussionGroupe.appendChild(x);
                x.appendChild(b);
            }
            
            
        },

        error: function () {

        }

    });

});

//envoyer le message dans la discussion corespondante

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

function creationDiscussion() {
    fermerModal();
}
