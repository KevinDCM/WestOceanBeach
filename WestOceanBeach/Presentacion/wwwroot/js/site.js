
// variables globales
var IdOfertaSeleccionada = 0;
var TemporadaActual = "";  
var DescuentoTemporada = 0;
var respuestaHabitacionDisponible = "";
var precioTotal = 0;
var precioFinal = 0;
var HabitOfert = 0;	

$(document).ready(function () {

	// ocultar el formulario de datos del cliente antes de consultar disponibilidad de habitaciones
    document.getElementById("datosCliente").style.display = "none";
	document.getElementById("buttonReiniciar").style.display = "none";


	// cargar las habitaciones Junior por defecto, en la reserva
	var CodigoTipoHabitacion = "1";

	$.ajax({
				
		url: "/Habitacion/ObtenerHabitacionesDisponibles",
		data: {"CodigoTipoHabitacion": CodigoTipoHabitacion},
		type: "GET",
		contentType: "application/json;charset=utf-8",
		dataType: "json",
		success: function (result) {
					
			var html = '';
			$.each(result, function (key, item) {
				html += '<option>' + item.numeroHabitacion + '</option>';
			});

			$('.sbody').html(html);

		},
		error: function (errorMessage) {
			alert(errorMessage.responseText);
		}

	});


	// cargar la fecha de hoy, por defecto, en la Fecha de Inicio de una reservación
    var now = new Date();
    var month = (now.getMonth() + 1);               
    var day = now.getDate();
    if (month < 10) 
        month = "0" + month;
    if (day < 10) 
        day = "0" + day;
    var today = now.getFullYear() + '-' + month + '-' + day;
    $('#fechaI').val(today);


	// cargar la fecha de mañana, por defecto, en la Fecha Final de una reservación
    var now2 = new Date();
    var month2 = (now2.getMonth() + 1);               
    var day2 = (now2.getDate()+1);
    if (month2 < 10) 
        month2 = "0" + month2;
    if (day2 < 10) 
        day2 = "0" + day2;
    var tomorrow = now2.getFullYear() + '-' + month2 + '-' + day2;
    $('#fechaF').val(tomorrow);


	// obtener temporada actual	y su descuento
	$.ajax({
				
		url: "/Reserva/GetTemporadaActual",
		type: "GET",
		contentType: "application/json;charset=utf-8",
		dataType: "json",
		success: function (result) {
					
			TemporadaActual = result.tipo_temporada;
			DescuentoTemporada = result.descuento_temporada;

		},
		error: function (errorMessage) {
			alert(errorMessage.responseText);
		}
	});


}); 

function onChangeTipoHabitacion(){

	// si el combo del tipoHabitacion cambia, el combo con los numeros de habitacion respectivos tambien

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
		data: {"CodigoTipoHabitacion": CodigoTipoHabitacion},
		type: "GET",
		contentType: "application/json;charset=utf-8",
		dataType: "json",
		success: function (result) {
					
			var html = '';
			$.each(result, function (key, item) {
				html += '<option>' + item.numeroHabitacion + '</option>';
			});

			$('.sbody').html(html);

		},
		error: function (errorMessage) {
			alert(errorMessage.responseText);
		}
	});
}

function parseDate(s) {
	var b = s.toString().split(/\D/); 
	return new Date(b[0], --b[1], b[2]);
}

function ValidarReserva(){

	var fechaI = $('#fechaI').val(); 
	var fechaF = $('#fechaF').val(); 

	if( fechaF === ''){
		alert("Por favor indique el rango de fechas correctamente.");
		return;

	} else {

		// obtener fecha actual

		var today = new Date();
		var dd = String(today.getDate()).padStart(2, '0');
		var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
		var yyyy = today.getFullYear();

		today = yyyy + '/' + mm + '/' + dd;
        var hoy = parseDate(today);

		var Inicio_date = parseDate(fechaI); 
		var Final_date = parseDate(fechaF);

		if( Inicio_date < hoy || Final_date < hoy ){
			alert('Las fechas seleccionadas son inválidas! Deben ser superiores a la fecha actual.');
			return;
		}

		if( Inicio_date >= Final_date ){
			alert('La fecha de inicio debe ser anterior a la fecha final!');
			return;

		} else {

			// una vez que se valida el rango de fechas, se consulta la disponibilidad de habitaciones
			// validar que ese numero de habitacion este disponible en el rango de fechas indicado 
			
			habitacionDisponible();			

		}
	}

}

function habitacionDisponible(){

	var fechaI = $('#fechaI').val(); 
	var fechaF = $('#fechaF').val(); 
	var NumHabitacion = parseInt($('#numsHabitacion').val());

	// responde "Sí" o "No"	de acuerdo a disponibilidad según fechas
	$.ajax({
        type: "POST", 
        url: "/Habitacion/ValidarHabitacionDisponible",
		data: JSON.stringify({ 
			"NumeroHabitacion": NumHabitacion, 
			"fechaIS" : fechaI, 
			"fechaFS" : fechaF
		}),
        contentType: "application/json",
        success: function (result) {

			respuestaHabitacionDisponible = result;

			if (respuestaHabitacionDisponible === "\"Sí\""){

				alert("Habitación disponible.");

				// saber el tipo de habitación escogido
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

				// determinar cantidad de días según fechas indicadas
				let date_1 = parseDate(fechaF);
				let date_2 = parseDate(fechaI);

				const days = (date_1, date_2) => {
					let difference = date_1.getTime() - date_2.getTime();
					let TotalDays = Math.ceil(difference / (1000 * 3600 * 24));
					return TotalDays;
				}
				
				var cantDias = days(date_1, date_2);

				$.ajax({
				
					url: "/Habitacion/ObtenerTarifaDiaria",
					data: {"CodigoTipoHabitacion": CodigoTipoHabitacion},
					type: "GET",
					contentType: "application/json;charset=utf-8",
					dataType: "json",
					success: function (result) {
					
						var tarifaDiaria = parseFloat(result);

						precioTotal = tarifaDiaria * cantDias;
						precioFinal = precioTotal - DescuentoTemporada;	
						// este caso no lleva oferta

						// calcular precio total (cargar en formulario)
						var precioTotalS = "Precio Total: $";	  
						let resultPrecioTotal = precioTotalS.concat(precioTotal.toString());
						document.getElementById("precioT").innerHTML = resultPrecioTotal;

						// calcular precio final (cargar en formulario)
						var precioFinalS = "Precio Final: $";	  
						let resultPrecioFinal = precioFinalS.concat(precioFinal.toString());
						document.getElementById("precioF").innerHTML = resultPrecioFinal;

						// descuento según temporada (cargar en formulario)
						var descuentoS = "Descuento: $";
						let resultDescuento = descuentoS.concat(DescuentoTemporada.toString());
						document.getElementById("descuento").innerHTML = resultDescuento;

						// cantidad de días (cargar en formulario)
						var cantDiasStr = "Cantidad de noches: ";
						let resultCantDias = cantDiasStr.concat(cantDias.toString());
						document.getElementById("cantNoches").innerHTML = resultCantDias;

						// tarifa diaria (cargar en formulario)
						var tDS = "Tarifa por noche: $";
						let resulttDS = tDS.concat(tarifaDiaria.toString());
						document.getElementById("tarifaDiaria").innerHTML = resulttDS;

						
						respuestaHabitacionDisponible =	"";

						$('#fechaF').prop('disabled', true); 
						$('#fechaI').prop('disabled', true);
						$('#numsHabitacion').prop('disabled', true);
						$('#tipoHabitacion').prop('disabled', true);
						document.getElementById('buttonC').style.visibility = 'hidden';
						document.getElementById('titleR').style.visibility = 'hidden';
						document.getElementById('buttonOf').style.visibility = 'hidden';
						document.getElementById('lblnumOferta').style.visibility = 'hidden';
						document.getElementById('numOferta').style.visibility = 'hidden';


						document.getElementById("buttonReiniciar").style.display = ""; 
						document.getElementById("datosCliente").style.display = "";

					},
					error: function (errorMessage) {
						alert(errorMessage.responseText);
					}
				});

			} else {
				alert("Esta habitación no está disponible en el rango de fechas especificado.");
			}

        },
        error: function (result, status) {
			console.log(result);
		}
	});

}

function ReiniciarReserva(){

	document.getElementById("buttonReiniciar").style.display = "none";
	document.getElementById("datosCliente").style.display = "none";

	document.getElementById('titleR').style.visibility = 'visible';
	$('#fechaF').prop('disabled', false); 
	$('#fechaI').prop('disabled', false);
	$('#numsHabitacion').prop('disabled', false);
	$('#tipoHabitacion').prop('disabled', false);
	document.getElementById('buttonC').style.visibility = 'visible';
	document.getElementById('lblnumOferta').style.visibility = 'visible';
	document.getElementById('numOferta').style.visibility = 'visible';

	document.getElementById('buttonOf').style.visibility = 'visible';
	document.getElementById('tipoHabitacion').style.visibility = 'visible';
	document.getElementById('numsHabitacion').style.visibility = 'visible';

	document.getElementById('titleTH').style.visibility = 'visible';
	document.getElementById('titleNH').style.visibility = 'visible';

	respuestaHabitacionDisponible = "";
}

function SolicitarReservacion(){

    if (document.getElementById("cedula").value.length == 0
        || document.getElementById("nombre").value.length == 0
        || document.getElementById("PrimerApellido").value.length == 0
        || document.getElementById("SegundoApellido").value.length == 0
        || document.getElementById("correo").value.length == 0 
        || document.getElementById("Telefono").value.length == 0
        || document.getElementById("edad").value.length == 0
		|| document.getElementById("Direccion").value.length == 0
		|| document.getElementById("Nacionalidad").value.length == 0
		|| document.getElementById("numeroTarjeta").value.length == 0
		|| document.getElementById("cvv").value.length == 0
	){
        alert("Complete todos los datos solicitados, por favor!");
        return;
    }

	if (document.getElementById("cedula").value.length < 9 || document.getElementById("cedula").value.length > 9){

		alert("La cédula debe contener 9 digitos.");
        return;
	}

	if (document.getElementById("Telefono").value.length < 8 || document.getElementById("Telefono").value.length > 8){

		alert("El tléfono debe contener 8 digitos.");
        return;
	}


	if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(document.getElementById("correo").value)) {

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
		var IdOferta = IdOfertaSeleccionada;
		var Temporada = TemporadaActual; 
		var NumHabitacion = parseInt($('#numsHabitacion :selected').text());
		if	(IdOferta != 0) { 
			NumHabitacion = HabitOfert;
		}
		var PrecioFinal = precioFinal;
		var PrecioTotal = precioTotal;
		var Descuento = DescuentoTemporada;
	
		$.ajax({
			type: "POST", 
			url: "/Reserva/RealizarReserva",
			data: JSON.stringify({ 
				"Cedula": Cedula, 
				"Nombre" : Nombre,
				"PrimerApellido" : PrimerApellido,
				"SegundoApellido" : SegundoApellido,
				"Correo" : Correo,
				"Telefono" : Telefono,
				"Edad" : Edad,
				"Direccion" : Direccion,
				"Nacionalidad" : Nacionalidad,
				"fechaIS" : fechaIS, 
				"fechaFS" : fechaFS,
				"NumHabitacion" : NumHabitacion,
				"IdOferta" : IdOferta,
				"Temporada" : Temporada,
				"PrecioFinal": PrecioFinal,
				"PrecioTotal": PrecioTotal,
				"Descuento": Descuento
			}),
			contentType: "application/json",
			success: function (result) {

				alert("Gracias " + Nombre + " " + PrimerApellido + ", " + result);

				respuestaHabitacionDisponible = "";
				IdOfertaSeleccionada = 0;
				PrecioFinal = 0;
				PrecioTotal = 0;
				DescuentoTemporada = 0;
				HabitOfert = 0;
				ReiniciarReserva();

				// clear inputs
				$('#cedula').val('');
				$('#nombre').val('');
				$('#PrimerApellido').val('');
				$('#SegundoApellido').val('');
				$('#correo').val('');
				$('#edad').val('');
				$('#Telefono').val('');
				$('#Nacionalidad').val('');
				$('#Direccion').val('');
				$('#numeroTarjeta').val('');
				$('#cvv').val('');

				Email.send({
					Host: "smtp.mailtrap.io",
					Username: "2ea82bed3cdd10", //  mailtrap credentials 
					Password: "4024f0dc586c86",
					To: Correo,  
					From: "bwestocean@gmail.com",  //  "the organization's email"
					Subject: "Bienvenido a Hotel West Ocean Beach Resort!",
					Body: "Reserva realizada!",
				})
				.then(function (message) {
					//alert("mail sent successfully")
				});

				location.reload();

					
			},
			error: function (result, status) {
				console.log(result);
			}
		});


	} else {

		alert("El correo electrónico ingresado es inválido");
		return;
	}

}

function ValidarOferta(){



	if (document.getElementById("numOferta").value.length == 0){
		alert("Especifique el código de oferta!");
		return;
	}

	document.getElementById("tipoHabitacion").style.display = "none";
	document.getElementById("numsHabitacion").style.display = "none";
	document.getElementById("titleTH").style.display = "none";
	document.getElementById("titleNH").style.display = "none";


	IdOfertaSeleccionada = parseInt($('#numOferta').val());
	var Id = IdOfertaSeleccionada;

	$.ajax({
		type: "GET", 
		url: "/Reserva/ReservaConOferta",
		data: {"Id": Id},
		contentType: "application/json;charset=utf-8",
		dataType: "json",
		success: function (result) {

			var numeroHabitAsignada = parseInt(result);
			HabitOfert = numeroHabitAsignada;// num habitación
			console.log(numeroHabitAsignada);

			var now2 = new Date();
			var month2 = (now2.getMonth() + 1);               
			var day2 = (now2.getDate()+1);
			if (month2 < 10) 
				month2 = "0" + month2;
			if (day2 < 10) 
				day2 = "0" + day2;
			var tomorrow = now2.getFullYear() + '-' + month2 + '-' + day2;

			var now3 = new Date();
			var month3 = (now3.getMonth() + 1);               
			var day3 = (now3.getDate()+2);
			if (month3 < 10) 
				month3 = "0" + month3;
			if (day3 < 10) 
				day3 = "0" + day3;
			var ptomorrow = now3.getFullYear() + '-' + month3 + '-' + day3;

			$('#fechaI').val(tomorrow);
			$('#fechaF').val(ptomorrow);


			var Inicio_date = parseDate(tomorrow); 
			var Final_date = parseDate(ptomorrow);

			var cantidadDias = 2; // fechaInicio mañana y fechaFin pasado mañana

			var CodigoTipoHabitacion = "";

			if(	numeroHabitAsignada >= 1 && numeroHabitAsignada <=7 )
			{
				CodigoTipoHabitacion = "1";	
			}

			if(	numeroHabitAsignada >= 8 && numeroHabitAsignada <=14 )
			{
				CodigoTipoHabitacion = "2";	
			}

			if(	numeroHabitAsignada >= 15 && numeroHabitAsignada <=21 )
			{
				CodigoTipoHabitacion = "3";	
			}
			
	 		$.ajax({
				
				url: "/Habitacion/ObtenerTarifaDiaria",
				data: {"CodigoTipoHabitacion": CodigoTipoHabitacion},
				type: "GET",
				contentType: "application/json;charset=utf-8",
				dataType: "json",
				success: function (result) {
					
					var tarifaDiaria = parseFloat(result);

					$.ajax({			
						url: "/Oferta/GetDescuentoOferta",
						data: {"Id": Id},
						type: "GET",
						contentType: "application/json;charset=utf-8",
						dataType: "json",
						success: function (result) {

							var descuentoTotal = DescuentoTemporada + parseFloat(result);
							precioTotal = tarifaDiaria * cantidadDias;
							precioFinal = precioTotal - descuentoTotal;	
							// este caso lleva oferta

							// calcular precio total (cargar en formulario)
							var precioTotalS = "Precio Total: $";	  
							let resultPrecioTotal = precioTotalS.concat(precioTotal.toString());
							document.getElementById("precioT").innerHTML = resultPrecioTotal;

							// calcular precio final (cargar en formulario)
							var precioFinalS = "Precio Final: $";	  
							let resultPrecioFinal = precioFinalS.concat(precioFinal.toString());
							document.getElementById("precioF").innerHTML = resultPrecioFinal;

							// descuento según temporada (cargar en formulario)
							var descuentoS = "Descuento: (Incluye oferta) $";
							let resultDescuento = descuentoS.concat(descuentoTotal.toString());
							document.getElementById("descuento").innerHTML = resultDescuento;

							// cantidad de días (cargar en formulario)
							var cantDiasStr = "Cantidad de noches: ";
							let resultCantDias = cantDiasStr.concat(cantidadDias.toString());
							document.getElementById("cantNoches").innerHTML = resultCantDias;

							// tarifa diaria (cargar en formulario)
							var tDS = "Tarifa por noche: $";
							let resulttDS = tDS.concat(tarifaDiaria.toString());
							document.getElementById("tarifaDiaria").innerHTML = resulttDS;

			
							respuestaHabitacionDisponible =	"";

							$('#fechaF').prop('disabled', true); 
							$('#fechaI').prop('disabled', true);
							$('#numsHabitacion').prop('disabled', true);
							$('#tipoHabitacion').prop('disabled', true);
							document.getElementById('buttonC').style.visibility = 'hidden';
							document.getElementById('titleR').style.visibility = 'hidden';
							document.getElementById('buttonOf').style.visibility = 'hidden';
							document.getElementById('lblnumOferta').style.visibility = 'hidden';
							document.getElementById('numOferta').style.visibility = 'hidden';

							document.getElementById("buttonReiniciar").style.display = ""; 
							document.getElementById("datosCliente").style.display = "";

							alert("Se le ha asigado la habitación # " + numeroHabitAsignada + ", del " + tomorrow + " hasta " + ptomorrow);	
	
						},
						error: function (errorMessage) {
							alert(errorMessage.responseText);
						}
					});

				},
				error: function (errorMessage) {
					alert(errorMessage.responseText);
				}
			});

		},
		error: function (result, status) {
			console.log(result);
		}
	});
}