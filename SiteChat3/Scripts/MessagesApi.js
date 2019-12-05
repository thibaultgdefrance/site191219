var element = document.getElementById("affichageMessages");
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

//créer une discussion de groupe
document.getElementById("creationDiscussion").addEventListener("click", function{
    console.log("discussion de groupe créee");

});



element.scrollTop = element.scrollHeight;


