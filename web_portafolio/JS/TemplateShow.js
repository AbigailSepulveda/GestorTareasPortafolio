$(document).ready(function () {
    initTable();
    loadData();
});

function initTable() {
    $('#tableProcess').DataTable({
        "data": [],
        "language": {
            "url": baseUrl + "/Content/DataTables/Spanish.json"
        },
        "columnDefs": [
            { width: '30%', targets: 0 },
            { width: '50%', targets: 1 },
            { width: '10%', targets: 2 },
            { width: '10%', targets: 3 }
        ],
        "aoColumns": [
            { title: "Nombre", mData: 'name', sortable: true },
            { title: "Descripción", mData: 'description', sortable: false },
            { title: "N. de Tareas", mData: 'n_tasks', sortable: false },
            {
                title: "Acciones", className: 'text-center',
                "render": function (data, type, row) {
                    var icons = '<button class="btn btn-light" style="width:100%" onclick="startProcess(\'' + row.id + '\');"> Ejecutar </button>';
                    return icons;
                }, sortable: false
            }
        ],
        "processing": true,
        "bDestroy": true,
        "filter": true,
        "orderMulti": false,
        "ordering": false,
        "info": false,
        "scrollX": false,
        "lengthChange": false,
        "fixedColumns": true,
        "searching": true,
        "autoWidth": false
    });
}

function startProcess(id) {
    $.ajax({
        url: baseUrl + '/Template/startProcess?id=' + id,
        method: 'POST',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                Swal.fire({
                    title: "REALIZADO",
                    text: resultData.msg,
                    type: "success",
                    showCancelButton: false,
                    confirmButtonText: "ACEPTAR"
                });
            } else {
                Swal.fire({
                    title: "PROBLEMA DETECTADO",
                    text: resultData.msg,
                    type: "error",
                    showCancelButton: false,
                    confirmButtonText: "CERRAR"
                });
            }
        }, error: function (jqXHR, error, errorThrown) {
            Swal.fire({
                title: "ERROR AL CARGAR LA GRILLA",
                text: "Contacte la Administrador",
                type: "error",
                showCancelButton: false,
                confirmButtonText: "CERRAR"
            });
        }
    });
}

function loadData() {
    $.ajax({
        url: baseUrl + '/Template/getAllByUnitId',
        method: 'GET',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                var datatable = $('#tableProcess').DataTable();
                datatable.clear();
                datatable.rows.add(resultData.list);
                datatable.draw();
            } else {
                Swal.fire({
                    title: "PROBLEMA DETECTADO",
                    text: resultData.msg,
                    type: "error",
                    showCancelButton: false,
                    confirmButtonText: "CERRAR"
                });
            }
        }, error: function (jqXHR, error, errorThrown) {
            Swal.fire({
                title: "ERROR AL CARGAR LA GRILLA",
                text: "Contacte la Administrador",
                type: "error",
                showCancelButton: false,
                confirmButtonText: "CERRAR"
            });
        }
    });
}