var documents;

$(document).ready(function () {
    initTable();
    loadProcess();
    documents = [];
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
            { title: "Nombre", mData: 'name', sortable: true },
            { title: "Descripcion", mData: 'description', sortable: false },
            { title: "Proceso", mData: 'process.name', sortable: false },
            { title: "Fecha Limite", mData: 'sDateEnd', sortable: false },
            {
                title: "Documentos", "render": function (data, type, row) {
                    var icons = "";
                    row.documents.forEach(element => {
                        if (icons === "") {
                            icons = '<a href="' + element.url + element.path + element.name + '" target="_blank">' + element.name + '</a>';
                        } else {
                            icons = icons + " <br/>" + '<a href="' + element.url + element.path + '" target="_blank">' + element.name + '</a>';
                        }
                    });
                    return icons;
                }, sortable: false
            },
            {
                title: "Acciones", className: 'text-center',
                "render": function (data, type, row) {
                    var icons = '<button class="btn btn-light" style="width:100%" onclick="edit(\'' + row.id + '\');"> Ver </button>';
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
    $('#tableProcess').DataTable({
        "data": [],
        "language": {
            "url": baseUrl + "/Content/DataTables/Spanish.json"
        },
        "columnDefs": [
            { width: '20%', targets: 0 },
            { width: '20%', targets: 1 },
            { width: '20%', targets: 2 },
            { width: '15%', targets: 3 },
            { width: '15%', targets: 4 },
            { width: '10%', targets: 5 }
        ],
        "aoColumns": [
            { title: "Nombre", mData: 'name', sortable: true },
            { title: "Fecha Termino", mData: 'endDate', sortable: false },
            { title: "Realizadas", mData: 'task_ready', sortable: false },
            { title: "Pendientes", mData: 'task_peding', sortable: false },
            {
                title: "Avance", "render": function (data, type, row) {
                    return row.progress + ' %';
                }, sortable: false
            },
            {
                title: "Seleccionar", className: 'text-center',
                "render": function (data, type, row) {
                    var icons = '<button class="btn btn-light" style="width:100%" onclick="selectProcess(\'' + row.id + '\');"> Revisar </button>';
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
    $('#tableDocuments').DataTable({
        "data": [],
        "language": {
            "url": baseUrl + "/Content/DataTables/Spanish.json"
        },
        "columnDefs": [
            { width: '80%', targets: 0 },
            { width: '20%', targets: 1 }
        ],
        "aoColumns": [
            { title: "Nombre", mData: 'name', sortable: true },
            {
                title: "Descargar", className: 'text-center',
                "render": function (data, type, row) {
                    var icons = '<a class="btn btn-light" style="width:100%" href="' + row.url + row.path + row.name + '" target="_blank">Descargar</a>';
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

function showModal() {
    var datatable = $('#tableDocuments').DataTable();
    datatable.clear();
    datatable.rows.add(documents);
    datatable.draw();
    $("#modalDocuments").modal();
}
function edit(id) {
    $.ajax({
        url: baseUrl + '/Process/getTasksById',
        method: 'GET',
        data: {
            id: id
        },
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                console.log(resultData.list);
                $("#nombre").val(resultData.list.name);
                $("#descripcion").val(resultData.list.description);
                $("#padre").val(resultData.list.fatherTaksId);
                $("#estado").val(resultData.list.taskStatusId);
                $("#fechaTermino").val(resultData.list.sDateEnd);
                $("#asignado").val(resultData.list.assingId);
                documents = resultData.list.documents;
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

function loadProcess() {
    $.ajax({
        url: baseUrl + '/Process/getProcessByUnit',
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

function selectProcess(id) {
    $.ajax({
        url: baseUrl + '/Process/getTasksByProcessId',
        method: 'GET',
        data: {
            id: id
        },
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