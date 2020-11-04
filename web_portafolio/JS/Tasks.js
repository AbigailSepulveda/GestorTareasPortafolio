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
            { width: '50%', targets: 0 },
            { width: '20%', targets: 1 },
            { width: '15%', targets: 2 },
            { width: '15%', targets: 3 }
        ],
        "aoColumns": [
            { title: "Informacion", mData: 'name', sortable: true },
            { title: "Fechas", mData: 'fechas', sortable: false },
            { title: "Estado", mData: 'estado', sortable: false },
            {
                title: "Acciones", className: 'text-center',
                "render": function (data, type, row) {
                    var icons = '<button class="btn btn-light" style="width:100%" onclick="edit(\'' + row.id + '\');"> Editar </button>';
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

function edit(id) {
    $.ajax({
        url: baseUrl + '/Tasks/getTaskById?id=' + id,
        method: 'GET',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                console.log(resultData.list);
                $("#nombre").val(resultData.list.name);
                $("#descripcion").val(resultData.list.description);
                $("#padre").val(resultData.list.fatherTaksId);
                $("#proceso").val(resultData.list.processId);
                $("#estado").val(resultData.list.taskStatusId);
                $("#asignado").val(resultData.list.assingId);
                $("#fechaInicio").val(resultData.list.sDateStart);
                $("#fechaTermino").val(resultData.list.sDateEnd);
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
        url: baseUrl + '/Tasks/getTasksByUserId',
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