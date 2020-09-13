function abrirModalCrear() {
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

function abrirModalEditar() {
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


    $(document).ready(function () {
        $('#TbEspecial').DataTable(
            {
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                }
            });
        })
    function EliminarEspecialidad(EspecialidadId) {
        document.getElementById("txtEspecialidadId").value = EspecialidadId;
        verModal('Eliminar especialidad',
            '¿Desea eliminar la especialidad código '
            + EspecialidadId + '?').then((result) => {
                if (result.value) {
                    var viewEliminar = document.getElementById("viewEliminar");
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

    //function EditarEspecialidad(EspecialidadId) {
    //    document.getElementById("txtEspecialidadId").value = EspecialidadId;
    //    verModal('Editar especialidad',
    //        '¿Desea modificar la especialidad código '
    //        + EspecialidadId + '?').then((result) => {
    //            if (result.value) {
    //                var viewEditar = document.getElementById("viewEditar");
    //                viewEditar.submit();
    //                Swal.fire(
    //                    'Modificación!',
    //                    'La especialidad' + EspecialidadId + 'fue modificada!.',
    //                    'success'
    //                )
    //            }
    //        })
    //}


    

    

