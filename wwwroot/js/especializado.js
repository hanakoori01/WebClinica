﻿
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
/*------------------------------------------------------*/
/*--------------------MODALES EDITAR--------------------*/

function abrirModalEditarEspecialidad() {
    verModal('Modificar especialidad', '¿Desea modificar la especialidad?').then((result) => {
        if (result.value) {
            var viewEditar = document.getElementById("viewEditar");
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
            var viewEditar = document.getElementById("viewEditar");
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
            var viewEditar = document.getElementById("viewEditar");
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
        '¿Desea eliminar el medico de código '
        + MedicoId + '?').then((result) => {
            if (result.value) {
                var viewEliminar = document.getElementById("viewEliminarMedico");
                viewEliminar.submit();
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
            }
        }
    }
}

function editar() {
    let titulo = document.title;
    if (titulo == "Editar especialidad") {
        abrirModalEditarEspecialidad();
    } else {
        if (titulo == "Editar medico") {
            abrirModalEditarMedico();
        } else {
            if (titulo == "Editar paciente") {
                abrirModalEditarPaciente();
            }
        }
    }
}

function eliminar(id) {
    let titulo = document.title;
    if (titulo == "Especialidad") {
        EliminarEspecialidad(id);
    } else {
        if (titulo == "Medico") {
            EliminarMedico(id);
        } else {
            if (titulo == "Paciente") {
                EliminarPaciente(id);
            }
        }
    }
}

function Resetear() {
    document.getElementById("nombre").value = "";
    document.getElementById("frmFilter").submit();
}

function ExportarPDF() {
    var aux = document.getElementById("viewExportarPDF");
    aux.submit();
}

function ExportarExcel() {
    var aux = document.getElementById("viewExportarExcel");
    aux.submit();
}

$(document).ready(function () {
    $('#TbEspecial').DataTable(
        {
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            }
        });
})









