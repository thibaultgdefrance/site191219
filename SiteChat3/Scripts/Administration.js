function afficherListeDonnees(url) {
    $.ajax({
        url: url,
        dataType: 'Html',
        method: 'GET',
        success: function (data) {
            document.getElementById("affichageListeDonnees").innerHTML = data;

        }


        ,

        error: function () {
            console.log("érreure");
        }
    })
}

console.log("ok");