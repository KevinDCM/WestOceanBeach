﻿





function updateAboutUs() {
   

    var text = document.getElementById('aboutUs');
    var sobreNosostros = text.value;
    


    if (trimfield(sobreNosostros) == '') {
      
      
        var aswer = document.getElementById('answer');
        aswer.innerHTML = "Complete el campo no puede ir vacio";
        var modal = document.getElementById("myModal");
        modal.style.display = "block";
        
    } else {
        
        

       $.ajax({

            type: 'POST',
            dataType: 'json',
            url: '/Admin/EditarSobreNosotros',
           data: { sobreNosotros: sobreNosostros },
           

           success: function (result) {

               

              
               var aswer = document.getElementById('answer');
               aswer.innerHTML = result.message;
               var modal = document.getElementById("myModal");
               modal.style.display = "block";


               
             
            },
            error:
                function (response) {
                    alert("Error en el llamado: " + response);
                }
        });
  
    }

}

function trimfield(str) {
    return str.replace(/^\s+|\s+$/g, '');
}


function cancelarupdateAboutUs() {


    var text = document.getElementById('aboutUs');
    text.value = "";
    var modal = document.getElementById('id01');
    modal.style.display = "none";


}
