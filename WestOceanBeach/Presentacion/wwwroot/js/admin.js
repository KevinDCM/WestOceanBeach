





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

function updateFacilities() {


    var text = document.getElementById('facilities');
    var facilidades = text.value;



    if (trimfield(facilidades) == '') {


        var aswer = document.getElementById('answer');
        aswer.innerHTML = "Complete el campo no puede ir vacio";
        var modal = document.getElementById("myModal");
        modal.style.display = "block";

    } else {



        $.ajax({

            type: 'POST',
            dataType: 'json',
            url: '/Admin/editarFacilidades',
            data: { facilidades: facilidades },


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

}// editar facilidades


function updateHome() {


    var text = document.getElementById('homeedit');
    var home = text.value;



    if (trimfield(home) == '') {


        var aswer = document.getElementById('answer');
        aswer.innerHTML = "Complete el campo no puede ir vacio";
        var modal = document.getElementById("myModal");
        modal.style.display = "block";

    } else {

        $.ajax({

            type: 'POST',
            dataType: 'json',
            url: '/Admin/EditarHome',
            data: { home: home },


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
//Editar Home

function trimfield(str) {
    return str.replace(/^\s+|\s+$/g, '');
}


function cancelarupdateAboutUs() {


    var text = document.getElementById('aboutUs');
    text.value = "";
    var modal = document.getElementById('id01');
    modal.style.display = "none";

    window.location = "https://localhost:44343/Admin";


}

function cancelarupdateFacilities() {


    var text = document.getElementById('facilities');
    text.value = "";
    var modal = document.getElementById('id02');
    modal.style.display = "none";
    window.location = "https://localhost:44343/Admin";


}

function cancelarupdateHome() {


    var text = document.getElementById('homeedit');
    text.value = "";
    var modal = document.getElementById('id03');
    modal.style.display = "none";
    window.location = "https://localhost:44343/Admin";
 
}