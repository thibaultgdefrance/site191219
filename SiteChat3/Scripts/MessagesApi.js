var element = document.getElementById("affichageMessages");

//afficher les messages quand on clic sur une discussion
function afficherMessages(idDiscussion) {
    var messagesVisibles = document.getElementsByClassName("contourMessage");
    for (var i = 0; i < messagesVisibles.length; i++) {
        messagesVisibles[i].setAttribute("style", "height:0px;");
        messagesVisibles[i].setAttribute("style", "display:none;");

    }

    
    console.log("IdDiscussion=" + idDiscussion);
    $.ajax({
        url: 'http://localhost:61994/api/Messages?IdDiscussion=7',
        type: 'GET',
        dataType: 'json',
        success: function (data3) {
            for (var i = 0; i < data3.length; i++) {
                //discussionGroupe.textContent += data2[i].TitreDiscussion;
                var contourMessage = document.createElement("div");
                contourMessage.setAttribute("class","contourMessage")
                var blocMessage = document.createElement("div");
                blocMessage.setAttribute("class", "blocMessage")
                var texteMessage = document.createElement("p");
                var dateEnvoi = document.createElement("p");
                dateEnvoi.textContent = data3[i].PseudoUtilisateur+" "+data3[i].DateEnvoi;
                dateEnvoi.setAttribute("class", "dateEnvoi");
                texteMessage.setAttribute("id", data3[i].IdMessage);
                texteMessage.setAttribute("class","texteMessages");
                texteMessage.textContent = data3[i].TexteMessage;
                document.getElementById("affichageMessages").appendChild(contourMessage);
                contourMessage.appendChild(blocMessage);
                blocMessage.appendChild(texteMessage);
                blocMessage.appendChild(dateEnvoi);
                var retour = document.createElement("br");
                document.getElementById("affichageMessages").appendChild(retour);
            }

            element.scrollTop = element.scrollHeight;
 
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
    
    
   

    $.ajax({
        url: 'http://localhost:61994/api/Discussions?token=7',
        type: 'GET',
        dataType: 'json',
        success: function (data2) {
            for (var i = 0; i < data2.length; i++) {
                //discussionGroupe.textContent += data2[i].TitreDiscussion;
                var x = document.createElement("div");
                x.setAttribute("class", "blocDiscussion")
                var b = document.createElement("button");
                b.setAttribute("id", data2[i].IdDiscussion);
                b.textContent = data2[i].TitreDiscussion;
                b.setAttribute("class", "titreDiscussion");
                b.setAttribute("onClick", "afficherMessages("+data2[i].IdDiscussion+")");
               
               
                discussionGroupe.appendChild(x);
                x.appendChild(b);
            }
            
            
        },

        error: function () {

        }

    });

});

//envoyer le message dans la discussion corespondante

function envoyerMessage(){
    console.log("message envoyé");
    $.ajax({
        url: '',
        type: 'Post',
        dataType: 'json',
        success: function (data2) {
           
        }


        ,

        error: function () {

        }

    });
}


element.scrollTop = element.scrollHeight;