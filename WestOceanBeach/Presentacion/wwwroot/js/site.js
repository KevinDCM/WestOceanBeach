// variables globales
var carrito = [];
var i = -1;

$(document).ready(function () {

    console.log('hello world');
    document.getElementById("divSelectHabitaciones").style.display = "none";
    document.getElementById("datosCliente").style.display = "none";
    carrito.length = 100;

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

		// 1. validar que ambas fechas sean superiores a hoy
		//    y
		// 2. validar que la fechaInicio sea menor a la fechaFinal

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

		document.getElementById("divSelectHabitaciones").style.display = "";

	}
}

function AgregarHabitacionAlCarrito(){
	
	// pendiente: por cada Habitación se debe poder escoger un rango de fechas distinto

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

	} else {
		// se descarta selección
	}
}

function VerDatosCliente(){
	document.getElementById("datosCliente").style.display = "";
    document.getElementById("divSelectHabitaciones").style.display = "none";
    document.getElementById("rangoFechas").style.display = "none";
}

function SolicitarReservacion(){
	// hacer post del objeto reserva
	// marcar en BD habitaciones esocogidas como no disponibles
	// hacer post del objeto cliente
	var nombre = $('#nombre').val();
	alert("Gracias " + nombre +", su reserva ha sido realizada con éxito!");
    document.getElementById("datosCliente").style.display = "none";
    document.getElementById("rangoFechas").style.display = "";
	// reset carrito
	carrito = [];
	i = -1;
}