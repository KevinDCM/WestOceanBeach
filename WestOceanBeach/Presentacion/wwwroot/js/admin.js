
$(document).ready(function () {

    console.log('hello world2');
    


    var now = new Date();
    var month = (now.getMonth() + 1);
    var day = now.getDate();
    if (month < 10)
        month = "0" + month;
    if (day < 10)
        day = "0" + day;
    var today = now.getFullYear() + '-' + month + '-' + day;
    $('#fecha').val(today);



}); 



function uploadFile() {

    var formData = new FormData();
    var uploadField = document.getElementById('filee');
    formData.append("file", uploadField.files[0]);

    $.ajax({
        type: 'POST',
        url: "/Admin/ChangeImageHome",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
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
}// uploadFile

function cambiarImgHabitacionEstandar() {

    var formData = new FormData();
    var uploadField = document.getElementById('fileEstandar');
    formData.append("file", uploadField.files[0]);

    $.ajax({
        type: 'POST',
        url: "/Admin/CambiarImgHabitacionEstandar",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {

        },
        error:
            function (response) {
                alert("Error en el llamado: " + response);
            }
    });
}// cambiar

function cambiarImgHabitacionJunior() {

    var formData = new FormData();
    var uploadField = document.getElementById('fileJunior');
    formData.append("file", uploadField.files[0]);

    $.ajax({
        type: 'POST',
        url: "/Admin/CambiarImgHabitacionJunior",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {

        },
        error:
            function (response) {
                alert("Error en el llamado: " + response);
            }
    });
}// cambiar

function cambiarImgHabitacionSuite() {

    var formData = new FormData();
    var uploadField = document.getElementById('fileSuite');
    formData.append("file", uploadField.files[0]);

    $.ajax({
        type: 'POST',
        url: "/Admin/CambiarImgHabitacionSuite",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {

        },
        error:
            function (response) {
                alert("Error en el llamado: " + response);
            }
    });
}// cambiar

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

function trimfield(str) {
    return str.replace(/^\s+|\s+$/g, '');
}

function cancelarupdateAboutUs() {

    var text = document.getElementById('aboutUs');
    text.value = "";
    var modal = document.getElementById('id01');
    modal.style.display = "none";
    window.location = "https://localhost:44343/Admin";
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


function updateComoLlegar() {

    var text = document.getElementById('comollegarr');
    var comollegar = text.value;

    if (trimfield(comollegar) == '') {

        var aswer = document.getElementById('answer');
        aswer.innerHTML = "Complete el campo no puede ir vacio";
        var modal = document.getElementById("myModal");
        modal.style.display = "block";

    } else {

        $.ajax({

            type: 'POST',
            dataType: 'json',
            url: '/Admin/EditarComoLLegar',
            data: { comollegar: comollegar },
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
}// editar cómon llegar

function updateHabitacionEstandar() {
   
    
    var descripcion = document.getElementById('descripcionESTANDAR').value;
    var tarifa = document.getElementById('tarifaESTANDAR').value;
    var nombre = "ESTANDAR";
    
        $.ajax({

            type: 'POST',
            dataType: 'json',
            url: '/Admin/EditarHabitacion',
            data: { tarifa: tarifa, descripcion: descripcion, nombre: nombre },
            success: function (result) {
              
                    alert("Listo: " + response);
                
            },
            error:
                function (response) {
                    alert("Error en el llamado: " + response);
                }
        });

    alert("LISTO");
}// updateHabitacion

function updateHabitacionJunior() {

    var descripcion = document.getElementById('descripcionJUNIOR').value;
    var tarifa = document.getElementById('tarifaJUNIOR').value;
    var nombre = "JUNIOR";

    $.ajax({

        type: 'POST',
        dataType: 'json',
        url: '/Admin/EditarHabitacion',
        data: { tarifa: tarifa, descripcion: descripcion, nombre: nombre },
        success: function (result) {

            alert("Listo: " + response);

        },
        error:
            function (response) {
                alert("Error en el llamado: " + response);
            }
    });

    alert("LISTO");
}// updateHabitacion

function updateHabitacionSuite() {

    var descripcion = document.getElementById('descripcionSUITE').value;
    var tarifa = document.getElementById('tarifaSUITE').value;
    var nombre = "SUITE";

    $.ajax({

        type: 'POST',
        dataType: 'json',
        url: '/Admin/EditarHabitacion',
        data: { tarifa: tarifa, descripcion: descripcion, nombre: nombre },
        success: function (result) {

            alert("Listo: " + response);

        },
        error:
            function (response) {
                alert("Error en el llamado: " + response);
            }
    });

    alert("LISTO");
}// updateHabitacion

jQuery(document).ready(function () {
    $(".oculto").hide();
    $(".inf").click(function () {
        var nodo = $(this).attr("href");

        if ($(nodo).is(":visible")) {
            $(nodo).hide();
            return false;
        } else {
            $(".oculto").hide("slow");
            $(nodo).fadeToggle("fast");
            return false;
        }
    });
});