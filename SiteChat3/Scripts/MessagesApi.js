var a = document.getElementById("truc")
var donnee = document.createElement("p");

a.appendChild(donnee);



//$(document).ready(function () {
    
    
    $.ajax({
        url: 'https://localhost:44372/api/Utilisateurs',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            donnee.textContent = data[1].NomUtilisateur;
            console.log(data);
        },

        error: function () {

        }

    });

    

//});