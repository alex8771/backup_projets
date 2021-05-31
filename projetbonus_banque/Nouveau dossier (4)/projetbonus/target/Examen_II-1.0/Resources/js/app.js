$(document).ready(function () {

    function randomNumber()
    {
        var rnd = Math.floor(Math.random() * 10);

        return rnd;
    }


    $("#Generateur").on("click", function () {
        var identifiant;
        identifiant = randomNumber(9).toString();
        identifiant += randomNumber(9).toString();
        identifiant += randomNumber(9).toString();
        identifiant += randomNumber(9).toString();
        identifiant += randomNumber(9).toString();

        $("#identifiantIns").val(identifiant);
    });







  


});


