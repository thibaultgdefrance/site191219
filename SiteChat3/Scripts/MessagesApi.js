var a = document.getElementById("truc")
var donnee = document.createElement("p");
var discussionGroupe = document.createElement("p");

a.appendChild(donnee);
discussionsGroupe.appendChild(discussionGroupe);


function afficherMessages(idDiscussion) {
    $.ajax({
        url: 'http://localhost:61994/api/Messages?IdDiscussion=' + idDiscussion,
        type: 'GET',
        dataType: 'json',
        success: function (data3) {
            for (var i = 0; i < data3.length; i++) {
                //discussionGroupe.textContent += data2[i].TitreDiscussion;
                var blocMessage = document.createElement("div");
                blocMessage.setAttribute("class", "blocMessage")
                var texteMessage = document.createElement("p");
                texteMessage.setAttribute("id", data3[i].IdMessage);
                texteMessage.textContent = data3[i].TexteMessage;
                affichageMessages.appendChild(blocMessage);
                blocMessage.appendChild(texteMessage);
            }


        },

        error: function () {

        }

    });
}

$(document).ready(function () {
    
    
   /*$.ajax({
       url: 'http://localhost:61994/api/Utilisateurs',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            donnee.textContent = data[1].NomUtilisateur;
            console.log(data);
        },

        error: function () {

        }

    });*/

    $.ajax({
        url: 'http://localhost:61994/api/Discussions?token=7',
        type: 'GET',
        dataType: 'json',
        success: function (data2) {
            for (var i = 0; i < data2.length; i++) {
                //discussionGroupe.textContent += data2[i].TitreDiscussion;
                var x = document.createElement("div");
                x.setAttribute("class", "blocDiscussion")
                var b = document.createElement("p");
                b.setAttribute("id", data2[i].IdDiscussion);
                b.textContent = data2[i].TitreDiscussion;
                b.setAttribute("class", "titreDiscussion");
                b.cllick=afficherMessages(data2[i].IdDiscussion);
                discussionGroupe.appendChild(x);
                x.appendChild(b);
            }
            
            
        },

        error: function () {

        }

    });

});