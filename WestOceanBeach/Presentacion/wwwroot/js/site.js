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


    var now2 = new Date();
    var month2 = (now2.getMonth() + 1);               
    var day2 = (now2.getDate()+1);
    if (month2 < 10) 
        month2 = "0" + month2;
    if (day2 < 10) 
        day2 = "0" + day2;
    var tomorrow = now2.getFullYear() + '-' + month2 + '-' + day2;
    $('#fechaF').val(tomorrow);


}); 


function parseDate(s) {
  var b = s.toString().split(/\D/); 
  return new Date(b[0], --b[1], b[2]);
}

function ObtenerHabitacionesDisponibles(){

	var fechaI = $('#fechaI').val(); 
	var fechaF = $('#fechaF').val(); 

	if( fechaF === ''){
		alert("Por favor indique el rango de fechas correctamente.");
		return;

	} else {

		// obtener fecha actual
		var hoyStr = new Date().toJSON().slice(0,10).replace(/-/g,'-');
        var hoy = parseDate(hoyStr);

		var Inicio_date = parseDate(fechaI); 
		var Final_date = parseDate(fechaF);

		if( Inicio_date < hoy || Final_date < hoy ){
			alert('Las fechas seleccionadas son inválidas! Deben ser superiores a la fecha actual.');
			return;
		}

		if( Inicio_date >= Final_date ){
			alert('La fecha de inicio debe ser menor a la fecha final!');
			return;
		} else {

			// una vez que se valida el rango de fechas, se consulta la disponibilidad de habitaciones

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

	// validar que carrito no esté vacío
	if(carrito.length === 0){	 
		alert("Aún no se han agregado habitaciones.");
	} else {

		document.getElementById("datosCliente").style.display = "";
		document.getElementById("divSelectHabitaciones").style.display = "none";
		document.getElementById("rangoFechas").style.display = "none";
	
		// aqui se debe solicitar el precio final de la reserva llamando a ReservaController/GetMontoTotal(param carrito)
		//document.getElementById("precio").innerHTML = returnedValue.toString();
	}

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