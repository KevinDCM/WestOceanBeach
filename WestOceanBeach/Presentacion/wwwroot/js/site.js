// variables globales
var carrito = [];
var i = -1;

$(document).ready(function () {

    console.log('hello world');
    document.getElementById("divSelectHabitaciones").style.display = "none";
    document.getElementById("datosCliente").style.display = "none";
    //carrito.length = 100;

    var now = new Date();
    var month = (now.getMonth() + 1);               
    var day = now.getDate();
    if (month < 10) 
        month = "0" + month;
    if (day < 10) 
        day = "0" + day;
    var today = now.getFullYear() + '-' + month + '-' + day;
    $('#fechaI').val(today);


}); 

function ObtenerHabitacionesDisponibles(){

	var fechaI = $('#fechaI').val(); 
	var fechaF = $('#fechaF').val(); 

	if( fechaF === ''){
		alert("Por favor indique el rango de fechas correctamente.");
		return;

	} else {
		alert("-");
		// 1. validar que ambas fechas sean superiores a hoy
		//    y
		// 2. validar que la fechaInicio sea menor a la fechaFinal, al menos un día

		document.getElementById('fechaI').readOnly = true;
		document.getElementById('fechaF').readOnly = true;

		var tipoHabitacionText = $('#tipoHabitacion :selected').text(); 

		var CodigoTipoHabitacion = "";

		if ( tipoHabitacionText === "Junior" ){ 
			CodigoTipoHabitacion = "1"; 
		}

		if ( tipoHabitacionText === "Estándar" ){ 
			CodigoTipoHabitacion = "2"; 
		}

		if ( tipoHabitacionText === "Suite" ){ 
			CodigoTipoHabitacion = "3"; 
		}

		$.ajax({
				
			url: "/Habitacion/ObtenerHabitacionesDisponibles",
			//data: JSON.stringify(Habitacion),
			data: {"CodigoTipoHabitacion": CodigoTipoHabitacion},
			type: "GET",
			contentType: "application/json;charset=utf-8",
			dataType: "json",
			success: function (result) {
					
				var html = '';
				$.each(result, function (key, item) {

					html += '<option>' + item.numeroHabitacion + '</option>';

				});

				document.getElementById('fechaI2').value =	$('#fechaI').val();
				document.getElementById('fechaF2').value = 	$('#fechaF').val();

				$('.sbody').html(html);

			},
			error: function (errorMessage) {
				alert(errorMessage.responseText);
			}
		});


		document.getElementById('fechaI2').readOnly = true;
		document.getElementById('fechaF2').readOnly = true;
		document.getElementById("divSelectHabitaciones").style.display = "";

	}
}

function AgregarHabitacionAlCarrito(){
	

	// se debería poder eliminar un item del carrito
	// en vez de cargar las escogidas como texto, cargarlas en un combo y agregar boton eliminar

	var answer = window.confirm("Seleccionar esta habitación?");
	var numHabitacion = $('#numsHabitacion :selected').text(); 
			
	if (answer) {

		i++;
		carrito[i] = numHabitacion;
		alert("Habitación #" + numHabitacion + " agregada al carrito!");
	
		var numsHabitacionTemp = [];
		var j = 0;

		$('select#numsHabitacion').find('option').each(function() {
			var currentValue = $(this).val();
			if(	currentValue ===  numHabitacion){
				// do nothing
			}  else {
				numsHabitacionTemp[j] = $(this).val();
				j++;
			}
		});

		var html = '';
		for (var r = 0, n = numsHabitacionTemp.length; r < n; r++) {
			html += '<option>' + numsHabitacionTemp[r] + '</option>';
		}
		$('.sbody').html(html);

		var parElement = document.getElementById("habitacionesEscogidas");
		var textToAdd = document.createTextNode(numHabitacion.toString() + "(" + $('#tipoHabitacion :selected').text() + ") " );
		parElement.appendChild(textToAdd);

	} else {
		// se descarta selección
	}
}


function ReiniciarReserva(){

    document.getElementById("datosCliente").style.display = "none";
    document.getElementById("divSelectHabitaciones").style.display = "none";

	document.getElementById('fechaI').readOnly = false;
	document.getElementById('fechaF').readOnly = false;

    document.getElementById("rangoFechas").style.display = "";
	// reset carrito
	carrito = [];
	i = -1;		

	document.getElementById("habitacionesEscogidas").innerHTML = "";	

}

function VerDatosCliente(){
	document.getElementById("datosCliente").style.display = "";
    document.getElementById("divSelectHabitaciones").style.display = "none";
    document.getElementById("rangoFechas").style.display = "none";
	
	// validar que carrito no esté vacío
	// aqui se debe solicitar el precio final de la reserva llamando a ReservaController/GetMontoTotal(param carrito)
}

function SolicitarReservacion(){

	// POST de la entidad Reserva
	// marca en BD habitaciones esocogidas (carrito) como no disponibles
	// hace insert del Cliente

	// usar carrito	como concatenacion de int's  (num1,num2,num3,numN)
	let listHabit = '';

	for (var r = 0, n = carrito.length; r < n; r++) {
		listHabit += carrito[r].toString() + ",";
	}

	var Cedula = $('#cedula').val();
	var Nombre = $('#nombre').val();
	var PrimerApellido = $('#PrimerApellido').val();
	var SegundoApellido = $('#SegundoApellido').val();
	var Correo = $('#correo').val();
	var Telefono = $('#Telefono').val();
	var Edad = parseInt($('#edad').val());
	var Direccion = $('#Direccion').val();
	var Nacionalidad = $('#Nacionalidad').val();
	var fechaIS = $('#fechaI').val();
	var fechaFS = $('#fechaF').val();
	var ListHabitaciones = listHabit;
	var IdOferta = 0;

	
    $.ajax({
        type: "POST", 
        url: "/Reserva/RealizarReserva",
		data: JSON.stringify({ "Cedula": Cedula, "Nombre" : Nombre,
			"PrimerApellido" : PrimerApellido,
			"SegundoApellido" : SegundoApellido,
			"Correo" : Correo,
			"Telefono" : Telefono,
			"Edad" : Edad,
			"Direccion" : Direccion,
			"Nacionalidad" : Nacionalidad,
			"fechaIS" : fechaIS, 
			"fechaFS" : fechaFS,
			"ListHabitaciones" : listHabit,
			"IdOferta" : 0
		}),
        contentType: "application/json",
        success: function (result) {

			alert(result);
		
        },
        error: function (result, status) {
			console.log(result);
		}
    });
	

}