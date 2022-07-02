
$(document).ready(function () {

    console.log('hello world2');
    

    let tablaHabitaciones = document.getElementById("mytable");
    tablaHabitaciones.setAttribute("hidden", "hidden");
    var now = new Date();
    var month = (now.getMonth() + 1);
    var day = now.getDate();
    if (month < 10)
        month = "0" + month;
    if (day < 10)
        day = "0" + day;
    var today = now.getFullYear() + '-' + month + '-' + day;
    $('#fecha').val(today);

   

    $.ajax(
        {
            type: "POST",
            dataType:'JSON',
            url:  "/Admin/estadoActualHabitaciones",
            success: function (data) {

                $.each(data, function (key, item) {

                  
                    
                    
                    addrow(item.numeroHabitacion, item.tipoHabitacion, item.encuentra);

                });
               
            },
            error: function (xhr, statusText, err) {
                alert("error" + xhr.status);
            }

        });



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




function habitacionesPorFecha() {

    var date = $('#myDateLlegada').val().split('-');
    var day = date[2];
    var month = date[1];
    var year = date[0];

    var date2 = $('#myDateSalida').val().split('-');
    var day2 = date2[2];
    var month2 = date2[1];
    var year2 = date2[0];


    var fechaLlegada = year + "-" + month + "-" + day ;

    var fechaSalida = year2 + "-" + month2 + "-" + day2 ;


   
    var typeRoom = $("#typeRoom option:selected").text();


    //hacer validacion pruebas aceptacion

    //1.fecha llegada colocarla con el dia de hoy
    //2.fecha salida colocarla con el dia de ayer  al de hoy
    //3. fecha llegada no puede ser menor al dia actual
    //4.fecha salida no puede ser menor al de fecha llegada
    //5. fecha salida y llegada no pueden ir vacias
    //tipo no puede ir vacio


    var resultado = typeRoom.localeCompare("Todas");




    if (resultado == 0) {
        //llamar a las habitaciones disponibles todas sin importar el tipo 

        $.ajax(
            {
                type: 'POST',
                dataType: 'JSON',
                url: '/Habitacion/ObtenerHabitacionesRangoFecha',
                data: { fechaLlegada: fechaLlegada, fechaSalida: fechaSalida},
                success:
                    function (data) {
                        // Generate HTML table.  
                        $.each(data, function (key, item) {




                            addrow2(item.numeroHabitacion, item.tipoHabitacion, item.tarifaDiaria);
                            let element = document.getElementById("mytable");
                            element.removeAttribute("hidden");

                        });
                    },
                error:
                    function (response) {
                        alert("Error: " + response);
                    }
            });


    } else {
        //llama a las habitaciones pero por tipo

        $.ajax(
            {
                type: 'POST',
                dataType: 'JSON',
                url: '/Habitacion/ObtenerHabitacionesRangoFechaTipo',
                data: { fechaLlegada: fechaLlegada, fechaSalida: fechaSalida, typeRoom: typeRoom },
                success:
                    function (data) {

                        $.each(data, function (key, item) {




                            addrow2(item.numeroHabitacion, item.tipoHabitacion, item.tarifaDiaria);
                            let element = document.getElementById("mytable");
                            element.removeAttribute("hidden");

                        });
                        
                    },
                error:
                    function (response) {
                        alert("Error: " + response);
                    }
            });



    }



    



    


}

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

function addrow(numero, tipo, estado) {

    var contentRow = "<td>" + numero + "</td>" + "<td>" + tipo + "</td>" + "<td>" + estado + "</td>";
    var table = document.getElementById("estadoActual");
    var row = table.insertRow(table.rows.length);
    row.innerHTML = contentRow;
}
function addrow2(numero, tipo, tarifa) {

    var contentRow = "<td>" + numero + "</td>" + "<td>" + tipo + "</td>" + "<td>" + tarifa + "</td>";
    var table = document.getElementById("mytable");
    var row = table.insertRow(table.rows.length);
    row.innerHTML = contentRow;
}

function createPDF() {

    var sTable = document.getElementById('estadoActual').innerHTML;
console.log(sTable);

    var style = "<style>";
    style = style + "table {width: 100%;font: 17px Calibri;}";
    style = style + "table, th, td {border: solid 1px #DDD; border-collapse: collapse;";
    style = style + "padding: 2px 3px;text-align: center;}";
    style = style + "</style>";

console.log(style);


    // CREATE A WINDOW OBJECT.
    var win = window.open('', '', 'height=700,width=700');

    win.document.write('<html><head>');
    win.document.write('<title>Reporte del estado actual de las habitaciones</title>');   // <title> FOR PDF HEADER.
    win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
    win.document.write('</head>');
    win.document.write('<body>');
    win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
    win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
}
