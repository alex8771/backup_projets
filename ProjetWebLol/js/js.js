$(document).ready(function(){



    $("#Persos").fadeOut();
    $("#Mode").fadeOut();
    $("#Items").fadeOut();
    $("#Shop").fadeOut();

    $("#LolBouton").on("click", function (){
        $("#Persos").fadeOut(function(){
            $("#Mode").fadeOut();
            $("#Items").fadeOut();
            $("#Shop").fadeOut();
            $("#Index").fadeIn('1000');
            var body=document.getElementsByTagName("BODY")[0].style.overflow="Hidden";
        });  
    }); 

    $("#ItemBouton").on("click", function (){
        $("#Index").fadeOut(function(){
            $("#Persos").fadeOut();
            $("#Mode").fadeOut();
            $("#Shop").fadeOut();
            $("#Items").fadeIn('1000');
            var body=document.getElementsByTagName("BODY")[0].style.overflow="scroll";

        });  
    }); 

    $("#ModeBouton").on("click", function (){
        $("#Index").fadeOut(function(){
            $("#Persos").fadeOut();
            $("#Items").fadeOut();
            $("#Shop").fadeOut();
            $("#Mode").fadeIn('1000');
            var body=document.getElementsByTagName("BODY")[0].style.overflow="Hidden";
        });  
    }); 

    $("#ShopBouton").on("click", function (){
        $("#Index").fadeOut(function(){
            $("#Persos").fadeOut();
            $("#Mode").fadeOut();
            $("#Items").fadeOut();
            $("#Shop").fadeIn('1000');
            var body=document.getElementsByTagName("BODY")[0].style.overflow="scroll";
        });  
    }); 
    $("#PersoBouton").on("click", function (){  
        Personnage();
    });
})
var listImg = [152];
var listNom=["Aatrox","Ahri","Akali","Alistar","Amumu","Anivia","Annie","Aphelios","Ashe","Aurelion sol","Azir","Bard","Blitzcrank","Brand","Braum","Caitlyn","Camille","Cassiopeia","Cho'gath","Corki","Darius","Diana","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","",];

function Personnage(){
    var page=document.getElementById("Index").style.display="none";
    var page=document.getElementById("Persos").style.display="none";
    var page=document.getElementById("Mode").style.display="none";
    var page=document.getElementById("Items").style.display="none";
    var page=document.getElementById("Shop").style.display="none";



    for (var o = 0; o < 152; o++)
    {
        listImg[o] = `
<img src="Personnages/${o}/${o}.jpg" class=" img card-img-top image" id="${o}">`;
    }

    var numeroImg = 0;
    var numeroNom=0;

    var i=0;
    var table_body = '<table border="1">';

    for(var i=0;i<19;i++){
        table_body+='<tr>';
        for(var j=0;j<8;j++){
            table_body +='<th>';
            table_body +=(listImg[numeroImg]);
            table_body +='</th>';
            numeroImg++;
        }
        table_body+='</tr>';
        table_body+='<tr>';
        for(var j=0;j<8;j++){   
            table_body +='<th>';
            table_body+='<div class="d-flex justify-content-center">';
            table_body+='<button class="btn btn-dark">';
            table_body+=(listNom[numeroNom]);
            table_body+='</button>';
            table_body+='</div>';
            table_body +='</th>';
            numeroNom++;
        }

        table_body+='</tr>';
    } 


    table_body+='</table>';
    $('#tableDiv').html(table_body);
    $(".img").css({"height": "200px", "width" : "235px"});

    var page=document.getElementById("Persos").style.display="Block";
    var body=document.getElementsByTagName("BODY")[0].style.overflow="scroll";
}


