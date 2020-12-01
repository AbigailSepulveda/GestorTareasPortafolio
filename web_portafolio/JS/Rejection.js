$(document).ready(function () {
    initTable();
    loadData();
});

function initTable() {
    $('#tableTasks').DataTable({
        "data": [],
        "language": {
            "url": baseUrl + "/Content/DataTables/Spanish.json"
        },
        "columnDefs": [
            { width: '20%', targets: 0 },
            { width: '30%', targets: 1 },
            { width: '15%', targets: 2 },
            { width: '15%', targets: 3 },
            { width: '10%', targets: 4 },
            { width: '10%', targets: 5 }
        ],
        "aoColumns": [
            { title: "Nombre", mData: 'task.name', sortable: true },
            { title: "Descripcion", mData: 'task.description', sortable: false },
            { title: "Proceso", mData: 'task.process.name', sortable: false },
            { title: "Responsable", mData: 'task.creatorUser.name', sortable: false },
            { title: "Mensaje", mData: 'message', sortable: false },
            {
                title: "Acciones", className: 'text-center',
                "render": function (data, type, row) {
                    var icons = '<button class="btn btn-success" style="width:100%" onclick="resolveTask(\'' + row.task.id + '\',\'' + row.task.name + '\',\'' + row.message + '\');"> Resolver </button>';
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

function resolveTask(id, name, message) {
    $("#modalID").val(id);
    $("#modalNombre").val(name);
    $("#modalMessage").val(message);
    $("#asignado").val(-1);
    $("#modalRefuse").modal();
}

function hideModal() {
    $("#modalID").val('');
    $("#modalNombre").val('');
    $("#modalMessage").val('');
    $("#asignado").val(-1);
    $('#modalRefuse').modal('hide');
}

function assignTask() {
    var id = $("#modalID").val();
    var asignado = $("#asignado").val();
    $.ajax({
        url: baseUrl + '/Tasks/assignTask?id=' + id +
            '&asignado=' + asignado
        ,
        method: 'POST',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                hideModal();
                loadData();
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
        url: baseUrl + '/Tasks/getRejectionTasksByCreator',
        method: 'GET',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                var datatable = $('#tableTasks').DataTable();
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