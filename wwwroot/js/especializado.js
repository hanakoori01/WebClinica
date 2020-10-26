
/*--------------------MODALES CREAR--------------------*/
function abrirModalCrearEspecialidad() {
    verModal('Agregar especialidad', '¿Desea guardar la especialidad?').then((result) => {
        if (result.value) {
            var viewAgregar = document.getElementById("viewAgregar");
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
            var viewAgregar = document.getElementById("viewAgregar");
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
            var viewAgregar = document.getElementById("viewAgregar");
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
            var viewAgregar = document.getElementById("viewAgregar");
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
                    'La especialidad' + EspecialidadId + 'fue eliminada!.',
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
                    'El medico' + MedicoId + 'fue eliminado!.',
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
                    'El paciente' + PacienteId + 'fue eliminado!.',
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
                    'La enfermedad' + EnfermedadId + 'fue eliminada!.',
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

/*--------------------METODOS ELIMINAR--------------------*/
/*--------------------METODOS GLOBALES--------------------*/


function agregar() {
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
                }

            }
        }
    }
}

/* BUSCAR POR NOMBRE CONSULTAS*/

function buscarConsulta() {
    let titulo = document.title;
    if (titulo == "Consulta Especialidad") {
        BuscarEspecialidad();
    } else {
        if (titulo == "Medico") {
            BuscarMedico();
        } else {
            if (titulo == "Paciente") {
                BuscarPaciente();
            } else {
                if (titulo == "Consulta Enfermedad") {
                    BuscarEnfermedad();
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
    var url = "Enfermedad/BuscarEnfermedad/?nombreEnfermedad=" + nombre;
    var tbody = document.getElementById("tbDatos");
    var campos = new Array("enfermedadId", "nombre", "descripcion");
    pintarPantallaConsulta(url, campos, tbody);
}
/* BUSCAR POR NOMBRE CONSULTA PACIENTE*/
function BuscarPaciente() {
    var nombre = document.getElementById("nombre").value;
    var url = "Enfermedad/BuscarEnfermedad/?nombreEnfermedad=" + nombre;
    var tbody = document.getElementById("tbDatos");
    var campos = new Array("enfermedadId", "nombre", "descripcion");
    pintarPantallaConsulta(url, campos, tbody);
}
/* BUSCAR POR NOMBRE CONSULTA ENFERMEDAD*/
function BuscarEnfermedad() {
    var nombre = document.getElementById("nombre").value;
    var url = "ConsultaEnfermedad/BuscarEnfermedad/?nombreEnfermedad=" + nombre;
    var tbody = document.getElementById("tbDatos");
    var campos = new Array( "nombre", "descripcion");
    pintarPantallaConsulta(url, campos, tbody);
}

/* RESETEAR CONSULTAS*/

function Resetear() {
    document.getElementById("nombre").value = "";
    buscarConsulta();
}



$(document).ready(function () {
    $('#TbEspecial').DataTable(
        {
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            }
        });
})

function verificar() {
    var value = $('.dataTables_filter input').val();
    alert(value);
}