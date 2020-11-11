function verModal(titulo,texto) {
   return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'No',
        confirmButtonText: 'Si',
        timer: 8000,
        closeOnConfirm: false
    })
}
//function Imprimir(table,titulo,col1,col2,col3,col4,col5) {
//    //<table><thead></thead><tbody></tbody></table>
//    var head = document.getElementById("head").outerHTML;
//    table = table.replace(head, "");
//    var columnas = "";
//    if (col1 != "") columnas += col1+" ";
//    if (col2 != "") columnas += col2 + " ";
//    if (col3 != "") columnas += col3 + " ";
//    if (col4 != "") columnas += col4 + " ";
//    if (col5 != "") columnas += col5 + " ";
//    var tabla = "<h1>Reporte de " + titulo + " </h1>"
//        + "<h3>"+columnas+"</h3>"
//        + table.outerHTML;
//        //+ "<h3>Especialidad Id          Especialidad</h3>"
//        //+ table.outerHTML;
//        //+ document.getElementById(table).outerHTML;
//        //+ document.getElementById("TbEspecial").outerHTML;
//    var pagina = window.document.body;
//    var ventana = window.open("");
//    ventana.document.write(tabla);
//    ventana.print();
//    ventana.close();
//    window.document.body.pagina;
//}
function pintarPantallaConsulta(url, campos, tbody) {
    var contenido = "";
    var nombreCampo;
    var objetoActual;
    contenido += "<tr style='background-color:#111110;color:white;'>";
    for (j = 0; j < campos.length; j++) {
        var cabecera = campos[j];
        cabecera = cabecera.charAt(0).toUpperCase() + cabecera.slice(1);
        contenido += "<td>" + cabecera + "</td>";
    }
    contenido += "</tr>";

    $.get(url, function (data) {
        for (var i = 0; i < data.length; i += 1) {
            contenido += "<tr>";
            for (j = 0; j < campos.length; j++) {
                nombreCampo = campos[j];
                objetoActual = data[i];
                contenido += "<td style='text-align:left;'>" + objetoActual[nombreCampo] + "</td>";
            }
            contenido += "</tr>";
        }
        tbody.innerHTML = contenido;
    })
}
function pintarPantallaCRUD(url, campos, propiedadId, nombreController,
    tbody) {
    var contenido = "";
    var nombreCampo;
    var objetoActual;
    $.get(url, function (data) {
        for (var i = 0; i < data.length; i += 1) {
            contenido += "<tr>";
            for (j = 0; j < campos.length; j++) {
                nombreCampo = campos[j];
                objetoActual = data[i];
                contenido += "<td>" + objetoActual[nombreCampo] + "</td>";
            }
            contenido += `<td>
                        <i class="fa fa-trash btn btn-danger" aria-hidden="true"
                                  onclick = "EliminarEspecialidad(${data[i].EspecialidadId})" >
                                  </i >
                        <a class="fa fa-info-circle btn btn-primary" aria-hidden="true"
                           asp-controller="Especialidad" asp-action="Details"
                                      asp-route-id="${data[i].EspecialidadId})"></a>
            
                        <a href="${nombreController}/Edit" asp-route-id="${objetoActual[propiedadId]}">                 
                          <i class="fa fa-pencil btn btn-info" aria-hidden="true"></i>
                        </a>
                    </td>`
            contenido += "</tr>";
        }
        tbody.innerHTML = contenido;
        $('#tbDatos').DataTable();
    })
}

function correcto(title = "Listo!") {
    Swal.fire({
        
        position: 'center',
        icon: 'success',
        title: title,
        showConfirmButton: true,
        timer: 3500
    })
}
function error(title = "Error!") {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: title,
        timer: 3500
    })
}
function pintar(url, campos, propiedadId, nombreController,
    popup = false, opciones = true, id = "tbDatos", idTabla = "table", propiedadMostrar = "") {
    var contenido = "";
    if (id == null || id == undefined || id == "") {
        var tbody = document.getElementById("tbDatos");
    } else {
        var tbody = document.getElementById(id);
    }
    var nombreCampo;
    var objetoActual;
    $.get(url, function (data) {

        for (var i = 0; i < data.length; i++) {
            contenido += "<tr>";
            for (var j = 0; j < campos.length; j++) {
                nombreCampo = campos[j];
                objetoActual = data[i];
                contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
            }
            //contenido += "<td>" + data[i].iidespecialidad + "</td>";
            // data[i][iidespecialidad]
            //contenido += "<td>" + data[i].nombre + "</td>";
            //contenido += "<td>" + data[i].descripcion + "</td>";
            //[a,b,c]
            var propiedadIdNueva = propiedadId.split('/');

            var idpropiedad = propiedadIdNueva[0];
            var exiEditar = propiedadIdNueva[1] * 1;
            var exiEliminar = propiedadIdNueva[2] * 1;
            if (opciones == true) {
                contenido += `
					<td>
							${exiEliminar == -1 ? '' :
                        ` <i class="fa fa-trash btn btn-danger" aria-hidden="true"
						   onclick="Eliminar(${objetoActual[idpropiedad]})">

						</i>  `
                    }
						`;
                if (popup == false) {
                    contenido += `
						${exiEditar == -1 ? ''
                            :
                            ` 
                         <a
						   href="${nombreController}/Editar/${objetoActual[idpropiedad]}"
						  >
								<i class="fa fa-edit btn btn-primary"
										aria-hidden="true"
                                  ></i>	
						</a>
                          `
                        }
						

					
                 `
                } else {

                    contenido += `

						${exiEditar == -1 ? '' :
                            ` 
                         <i class="fa fa-edit btn btn-primary" aria-hidden="true"
						   data-toggle="modal"  data-target="#exampleModal"
						   onclick="Abrir(${objetoActual[idpropiedad]})">

						</i>
                            `
                        }
	                  
                        `;
                }
                contenido += `</td>`;
            } else {
                contenido += `
					<td>

						<i class="fa fa-check btn btn-success" aria-hidden="true"
						   onclick="AsignarNombre(${objetoActual[idpropiedad]},
                              '${objetoActual[propiedadMostrar]}')">

						</i>
					</td>`;
            }
            contenido += "</tr>";
        }
        if (idTabla == null || idTabla == undefined || idTabla == "") {
            $('#table').DataTable().clear();
            $('#table').DataTable().destroy();
            tbody.innerHTML = contenido;
            $('#table').DataTable();
        } else {
            $('#' + idTabla).DataTable().clear();
            $('#' + idTabla).DataTable().destroy();
            tbody.innerHTML = contenido;
            $('#' + idTabla).DataTable();
        }
    })
}
