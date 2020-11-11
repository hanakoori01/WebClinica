
/*--------------------Tablas--------------------*/
$(document).ready(function () {
    $('#TbEspecial').DataTable(
        {
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            }
        });
})
/*--------------------Tablas--------------------*/

/*--------------------Asigna Roles--------------------*/
function Guardar(url) {
    var tipousuarioid = document.getElementById("UserType").value;
    var bHabilitado = 1;
    var frm = new FormData;
    frm.append("tipousuarioid", tipousuarioid);
    frm.append("Bhabilitado", bHabilitado);
    var checks = document.getElementsByClassName("checkbox");
    var nchecks = checks.length
    for (var i = 0; i < nchecks; i++) {
        if (checks[i].checked == true) {
            frm.append("_Paginas[]", checks[i].id.replace("/", ""));
        }
    }
    $.ajax({
        type: "POST",
        url: url,
        data: frm,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data == "OK") {
                correcto("Se actualizó correctamente el rol segun tipo de usuario!");
                document.getElementById("frmEnviar").submit();
                document.getElementById("frmRegresar").submit();
            }
            else {
                error("Ocurrió un error, por favor verifique!");
            }
        }
        //},
        //error: alert("No se pudo procesar el registro")
    })
}

function ListarBotones() {
    $.get("PaginaTipoUsuario/listarBotones", function (data) {
        var contenido = "<table class='table'>";
        contenido += "<thead>";
        contenido += "<tr>";
        contenido += "<td></td>";
        contenido += "<td>Nombre Boton</td>";
        contenido += "</tr>";
        contenido += "</thead>";
        contenido += "<tbody>";
        for (var i = 0; i < data.length; i++) {
            contenido += "<tr>";
            contenido += "<td> <input type='checkbox' class='checkbox' id='chk" + data[i].iidboton + "' /> </td>";
            contenido += "<td>" + data[i].nombre + "</td>";
            contenido += "</tr>";
        }
        contenido += "</tbody>";
        contenido += "<table>";

        document.getElementById("divBotones").innerHTML = contenido;
        recuperar();
    })
}

function ListarPaginas() {
    $.get("AsignaRol/CargarPaginas", function (data) {
        var contenido = "<table class='table'>";
        contenido += "<thead>";
        contenido += "<tr>";
        contenido += "<td></td>";
        contenido += "<td>Nombre página</td>";
        contenido += "</tr>";
        contenido += "</thead>";
        contenido += "<tbody>";
        for (var i = 0; i < data.length; i++) {
            contenido += "<tr>";
            contenido += "<td> <input type='checkbox' class='checkbox' id='"
                + data[i].paginaid + "' /> </td>";
            contenido += "</tr>";
        }
        contenido += "</tbody>";
        contenido += "<table>";
        document.getElementById("divPaginas").innerHTML = contenido;
        recuperar();
    })
}

function recuperar() {
    var tipousuarioid = document.getElementById("UserType").value;
    $.get("AsignaRol/RecuperarPaginas/?tipousuarioid" + tipousuarioid, function (data) {
        for (var i = 0; i < data.length; i++) {
            var pagid = data[i].Paginaid;
            var idgene = pagid;
            document.getElementById(idgene).checked = true;
        }
    });
}
/*--------------------Asigna Roles--------------------*/

/*--------------------LOGIN--------------------*/
function Enviar() {
    var user = document.getElementById("name").value;
    var pass = document.getElementById("password").value;
    $.get("Login/_Login/?user=" + user + "&pass=" + pass, function (data) {
        if (data == "") {
            error("Usuario o contaseña incorrecto!");
        } else {
            correcto("Bienvenido!");
            document.location.href = "Home";
        }
    });
}

function Enviar2() {
    var user = document.getElementById("name").value;
    var pass = document.getElementById("password").value;
    $.ajax({
        type: "GET",
        url: "/Login/_Login",
        data: { "user": user, "pass": pass },
        success: function (data) {
            if (data == "") {
                error("Usuario o contaseña incorrecto!");
            } else {
                document.location.href = "/Home/Index"
            }
        },
        error: "Esta mierda no sirve"
    })
};

var button = document.getElementById('mainButton');

var openForm = function () {
    button.className = 'active';
};

var checkInput = function (input) {
    if (input.value.length > 0) {
        input.className = 'active';
    } else {
        input.className = '';
    }
};

var closeForm = function () {
    button.className = '';
};

document.addEventListener("keyup", function (e) {
    if (e.keyCode == 27 || e.keyCode == 13) {
        closeForm();
    }
});


/*--------------------MODALES CREAR--------------------*/
function abrirModalCrearEspecialidad() {
    verModal('Agregar especialidad', '¿Desea guardar la especialidad?').then((result) => {
        if (result.value) {
            var viewAgregar = document.getElementById("viewAgregarEspecialidad");
            viewAgregar.submit();
            Swal.fire(
                'Agregado!',
                'La especialidad fue agregada!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La especialidad no fue agregada!!!:)',
                'error'
            )
        }
    })
}
function abrirModalCrearMedico() {
    verModal('Agregar medico', '¿Desea guardar al medico?').then((result) => {
        if (result.value) {
            var viewAgregar = document.getElementById("viewAgregarMedico");
            viewAgregar.submit();
            Swal.fire(
                'Agregado!',
                'El medico fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El medico no fue agregado!!!:)',
                'error'
            )
        }
    })
}
function abrirModalCrearPaciente() {
    verModal('Agregar paciente', '¿Desea guardar al paciente?').then((result) => {
        if (result.value) {
            var viewAgregar = document.getElementById("viewAgregarPaciente");
            viewAgregar.submit();
            Swal.fire(
                'Agregado!',
                'El paciente fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El paciente no fue agregad!!!:)',
                'error'
            )
        }
    })
}
function abrirModalCrearEnfermedad() {
    verModal('Agregar enfermedad', '¿Desea guardar la enfermedad?').then((result) => {
        if (result.value) {
            var viewAgregar = document.getElementById("viewAgregarEnfermedad");
            viewAgregar.submit();
            Swal.fire(
                'Agregado!',
                'La enfermedad fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La enfermedad no fue agregada!!!:)',
                'error'
            )
        }
    })
}
function abrirModalCrearCita() {
    verModal('Agregar cita', '¿Desea guardar la cita?').then((result) => {
        if (result.value) {
            var viewAgregarCita = document.getElementById("viewAgregarCita");
            viewAgregarCita.submit();
            Swal.fire(
                'Agregado!',
                'La cita fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La cita no fue agregada!!!:)',
                'error'
            )
        }
    })
}
function abrirModalCrearUsuario() {
    verModal('Agregar usuario', '¿Desea guardar al usuario?').then((result) => {
        if (result.value) {
            var viewAgregarUsuario = document.getElementById("viewAgregarUsuario");
            viewAgregarUsuario.submit();
            Swal.fire(
                'Agregado!',
                'El usuario fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El usuario no fue agregado!!!:)',
                'error'
            )
        }
    })
}
function abrirModalCrearTipoUsuario() {
    verModal('Agregar tipo usuario', '¿Desea guardar el tipo de Usuario?').then((result) => {
        if (result.value) {
            var viewAgregarTipoUsuario = document.getElementById("viewAgregarTipoUsuario");
            viewAgregarTipoUsuario.submit();
            Swal.fire(
                'Agregado!',
                'El tipo de usuario fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El tipo usuario no fue agregado!!!:)',
                'error'
            )
        }
    })
}

/*------------------------------------------------------*/
/*--------------------MODALES EDITAR--------------------*/

function abrirModalEditarEspecialidad() {
    verModal('Modificar especialidad', '¿Desea modificar la especialidad?').then((result) => {
        if (result.value) {
            var viewEditar = document.getElementById("viewEditarEspecialidad");
            viewEditar.submit();
            Swal.fire(
                'Modificado!',
                'La especialidad fue modificada!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La especialidad no fue modificada!!!:)',
                'error'
            )
        }
    })
}

function abrirModalEditarMedico() {
    verModal('Modificar medico', '¿Desea modificar el medico?').then((result) => {
        if (result.value) {
            var viewEditar = document.getElementById("viewEditarMedico");
            viewEditar.submit();
            Swal.fire(
                'Modificado!',
                'El medico fue modificado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El medico no fue modificado!!!:)',
                'error'
            )
        }
    })
}

function abrirModalEditarPaciente() {
    verModal('Modificar paciente', '¿Desea modificar el paciente?').then((result) => {
        if (result.value) {
            var viewEditar = document.getElementById("viewEditarPaciente");
            viewEditar.submit();
            Swal.fire(
                'Modificado!',
                'El paciente fue modificado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El paciente no fue modificado!!!:)',
                'error'
            )
        }
    })
}
function abrirModalEditarEnfermedad() {
    verModal('Modificar enfermedad', '¿Desea modificar la enfermedad?').then((result) => {
        if (result.value) {
            var viewEditar = document.getElementById("viewEditarEnfermedad");
            viewEditar.submit();
            Swal.fire(
                'Modificada!',
                'La enfermedad fue modificada!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La enfermedad no fue modificada!!!:)',
                'error'
            )
        }
    })
}
function abrirModalEditarCita() {
    verModal('Modificar cita', '¿Desea modificar la cita?').then((result) => {
        if (result.value) {
            var viewEditarCita = document.getElementById("viewEditarCita");
            viewEditarCita.submit();
            Swal.fire(
                'Modificado!',
                'La cita fue modificado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La cita no fue modificado!!!:)',
                'error'
            )
        }
    })
}
function abrirModalEditarTipoUusuario() {
    verModal('Modificar tipo Usuario', '¿Desea modificar el tipo Usuario?').then((result) => {
        if (result.value) {
            var viewEditarTipoUsuario = document.getElementById("viewEditarTipoUsuario");
            viewEditarTipoUsuario.submit();
            Swal.fire(
                'Modificado!',
                'El tipo usuario fue modificado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El tipo usuario no fue modificado!!!:)',
                'error'
            )
        }
    })
}

/*--------------------MODALES EDITAR-----------------------*/
/*--------------------METODOS ELIMINAR--------------------*/
function EliminarEspecialidad(EspecialidadId) {
    document.getElementById("txtEspecialidadId").value = EspecialidadId;
    verModal('Eliminar especialidad',
        '¿Desea eliminar la especialidad código '
        + EspecialidadId + '?').then((result) => {
            if (result.value) {
                var viewEliminar = document.getElementById("viewEliminarEspecialidad");
                viewEliminar.submit();
                Swal.fire(
                    'Eliminación!',
                    'La especialidad ' + EspecialidadId + ' fue eliminada!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'La especialidad no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}

function EliminarMedico(MedicoId) {
    document.getElementById("txtMedicoId").value = MedicoId;
    verModal('Eliminar medico',
        '¿Desea eliminar el medico de código  '
        + ' ' + MedicoId + ' ?').then((result) => {
            if (result.value) {
                var viewEliminarMedico = document.getElementById("viewEliminarMedico");
                viewEliminarMedico.submit();
                Swal.fire(
                    'Eliminación!',
                    'El medico ' + MedicoId + ' fue eliminado!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'El medico no fue eliminado!!!:)',
                    'error'
                )
            }
        })
}

function EliminarPaciente(PacienteId) {
    document.getElementById("txtPacienteId").value = PacienteId;
    verModal('Eliminar paciente',
        '¿Desea eliminar el paciente código '
        + PacienteId + '?').then((result) => {
            if (result.value) {
                var viewEliminar = document.getElementById("viewEliminarPaciente");
                viewEliminar.submit();
                Swal.fire(
                    'Eliminación!',
                    'El paciente ' + PacienteId + ' fue eliminado!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'El paciente no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}
function EliminarEnfermedad(EnfermedadId) {
    document.getElementById("txtEnfermedadId").value = EnfermedadId;
    verModal('Eliminar enfermedad',
        '¿Desea eliminar la enfermedad con el código '
        + EnfermedadId + '?').then((result) => {
            if (result.value) {
                var viewEliminar = document.getElementById("viewEliminarEnfermedad");
                viewEliminar.submit();
                Swal.fire(
                    'Eliminación!',
                    'La enfermedad ' + EnfermedadId + ' fue eliminada!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'La enfermedad no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}
function EliminarUsuario(UsuarioId) {
    document.getElementById("txtUsuarioId").value = UsuarioId;
    verModal('Eliminar paciente',
        '¿Desea eliminar el usuario código '
        + UsuarioId + '?').then((result) => {
            if (result.value) {
                var viewEliminarUsuario = document.getElementById("viewEliminarUsuario");
                viewEliminarUsuario.submit();
                Swal.fire(
                    'Eliminación!',
                    'El usuario ' + UsuarioId + ' fue eliminado!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'El usuario no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}
function EliminarCita(CitaId) {
    document.getElementById("txtCitaId").value = CitaId;
    verModal('Eliminar cita',
        '¿Desea eliminar la cita con el código '
        + CitaId + '?').then((result) => {
            if (result.value) {
                var viewEliminarCita = document.getElementById("viewEliminarCita");
                viewEliminarCita.submit();
                Swal.fire(
                    'Eliminación!',
                    'La cita ' + CitaId + ' fue eliminada!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'La cita no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}
function EliminarTipoUsuario(TipoUsuarioId) {
    document.getElementById("txtTipoUsuarioId").value = TipoUsuarioId;
    verModal('Eliminar tipo de usuario',
        '¿Desea eliminar el tipo de usuario con el código '
        + TipoUsuarioId + '?').then((result) => {
            if (result.value) {
                var viewEliminarTipoUsuario = document.getElementById("viewEliminarTipoUsuario");
                viewEliminarTipoUsuario.submit();
                Swal.fire(
                    'Eliminación!',
                    'La el tipo de usuario ' + TipoUsuarioId + ' fue eliminado!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'El tipo de usuario no fue eliminado!!!:)',
                    'error'
                )
            }
        })
}


/*--------------------METODOS ELIMINAR--------------------*/
/*--------------------METODOS GLOBALES--------------------*/


function Agregar() {
    let titulo = document.title;
    if (titulo == "Agregar especialidad") {
        abrirModalCrearEspecialidad();
    } else {
        if (titulo == "Agregar medico") {
            abrirModalCrearMedico();
        } else {
            if (titulo == "Agregar paciente") {
                abrirModalCrearPaciente();
            } else {
                if (titulo == "Agregar enfermedad") {
                    abrirModalCrearEnfermedad();
                } else {
                    if (titulo == "Agregar usuario") {
                        abrirModalCrearUsuario();
                    } else {
                        if (titulo == "Agregar citas") {
                            abrirModalCrearCita();
                        } else {
                            if (titulo == "Agregar tipo Usuario") {
                                abrirModalCrearTipoUsuario();
                            }
                        }
                    }
                }
            }
        }
    }
}


function Editar() {
    let titulo = document.title;
    if (titulo == "Editar especialidad") {
        abrirModalEditarEspecialidad();
    } else {
        if (titulo == "Editar medico") {
            abrirModalEditarMedico();
        } else {
            if (titulo == "Editar paciente") {
                abrirModalEditarPaciente();
            } else {
                if (titulo == "Editar enfermedad") {
                    abrirModalEditarEnfermedad();
                } else {
                    if (titulo == "Editar usuario") {
                        abrirModalEditarUsuario();
                    } else {
                        if (titulo == "Editar cita") {
                            abrirModalEditarCita();
                        } else {
                            if (titulo == "Editar Tipo de Usuario"){
                                abrirModalEditarTipoUusuario();
                            }
                        }
                    }
                }
            }
        }
    }
}

function Eliminar(id) {
    let titulo = document.title;
    if (titulo == "Especialidad") {
        EliminarEspecialidad(id);
    } else {
        if (titulo == "Medico") {
            EliminarMedico(id);
        } else {
            if (titulo == "Paciente") {
                EliminarPaciente(id);
            } else {
                if (titulo == "Enfermedad") {
                    EliminarEnfermedad(id);
                } else {
                    if (titulo == "Citas") {
                        EliminarCita(id);
                    } else {
                        if (titulo == "Usuario") {
                            EliminarUsuario(id);
                        } else {
                            if (titulo == "Tipo Usuario") {
                                EliminarTipoUsuario(id);
                            }
                        }
                    }
                }
            }
        }
    }
}

/* BUSCAR POR NOMBRE CONSULTAS*/

function BuscarConsulta() {
    let titulo = document.title;
    if (titulo == "Consulta Especialidad") {
        BuscarEspecialidad();
    } else {
        if (titulo == "Consulta Medico") {
            BuscarMedico();
        } else {
            if (titulo == "Consulta Paciente") {
                BuscarPaciente();
            } else {
                if (titulo == "Consulta Enfermedad") {
                    BuscarEnfermedad();
                } else {
                    if (titulo == "Consulta Citas") {
                        BuscarCitas();
                    }
                }
            }
        }
    }

    /* BUSCAR POR NOMBRE CONSULTA ESPECIALIDAD*/
    function BuscarEspecialidad() {
        var nombre = document.getElementById("nombre").value;
        var url = "Especialidad/BuscarEspecialidad/?nombreEspecialidad=" + nombre;
        var tbody = document.getElementById("tbDatos");
        var campos = new Array("especialidadId", "nombre", "descripcion");
        pintarPantallaConsulta(url, campos, tbody);
    }
    /* BUSCAR POR NOMBRE CONSULTA MEDICO*/
    function BuscarMedico() {
        var nombre = document.getElementById("nombre").value;
        var url = "Medico/BuscarMedico/?nombreMedico=" + nombre;
        var tbody = document.getElementById("tbDatos");
        var campos = new Array("medicoId", "apellido", "nombre", "direccion", "telefonoFijo", "telefonoCelular", "especialidad");
        pintarPantallaConsulta(url, campos, tbody);
    }
    /* BUSCAR POR NOMBRE CONSULTA PACIENTE*/
    function BuscarPaciente() {
        var nombre = document.getElementById("nombre").value;
        var url = "Paciente/BuscarPaciente/?nombrePaciente=" + nombre;
        var tbody = document.getElementById("tbDatos");
        var campos = new Array("nombre", "apellidos", "direccion", "telefonoContacto");
        pintarPantallaConsulta(url, campos, tbody);
    }
    /* BUSCAR POR NOMBRE CONSULTA ENFERMEDAD*/
    function BuscarEnfermedad() {
        var nombre = document.getElementById("nombre").value;
        var url = "ConsultaEnfermedad/BuscarEnfermedad/?nombreEnfermedad=" + nombre;
        var tbody = document.getElementById("tbDatos");
        var campos = new Array("nombre", "descripcion");
        pintarPantallaConsulta(url, campos, tbody);
    }

    /* BUSCAR POR NOMBRE CONSULTA CITAS*/
    function BuscarEnfermedad() {
        var nombre = document.getElementById("nombre").value;
        var url = "ConsultaCitas/BuscarCitas/?nombreCitas=" + nombre;
        var tbody = document.getElementById("tbDatos");
        var campos = new Array("fechaCita", "diagnostico", "medico", "paciente", "especialidad");
        pintarPantallaConsulta(url, campos, tbody);
    }

    /* RESETEAR CONSULTAS*/

    function Resetear() {
        document.getElementById("nombre").value = "";
        BuscarConsulta();
    }

    

    function verificar() {
        var value = $('.dataTables_filter input').val();
        alert(value);
    }
}