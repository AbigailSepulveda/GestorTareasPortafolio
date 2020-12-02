var documents;

$(document).ready(function () {
    initTable();
    documents = [];
    loadTasks();
});

function initTable() {
    $('#tableTasks').DataTable({
        "data": [],
        "language": {
            "url": baseUrl + "/Content/DataTables/Spanish.json"
        },
        "columnDefs": [
            { width: '10%', targets: 0 },
            { width: '20%', targets: 1 },
            { width: '20%', targets: 2 },
            { width: '15%', targets: 3 },
            { width: '15%', targets: 4 },
            { width: '10%', targets: 5 },
            { width: '10%', targets: 6 }
        ],
        "aoColumns": [
            {
                title: "Estado", className: 'text-center',
                "render": function (data, type, row) {
                    var alert = '<div>';

                    if (row.alert === 1) {
                        alert = '<h5 class="card-header" style="background-color: #C70039; color:white;text-align:center;">Vencida</h5>';
                    } else if (row.alert === 2) {
                        alert = '<h5 class="card-header" style="background-color: #FFC300; color:white;text-align:center;">Por Vencer</h5>';
                    } else if (row.alert === 3) {
                        alert = '<h5 class="card-header" style="background-color: #007C35; color:white;text-align:center;">Al Día</h5>';
                    }
                    return alert + '</div>';
                }, sortable: false
            },
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
                $("#id").val(resultData.list.id);
                $("#nombre").val(resultData.list.name);
                $("#descripcion").val(resultData.list.description);
                $("#padre").val(resultData.list.fatherTaksId);
                $("#estado").val(resultData.list.taskStatusId);
                $("#fechaTermino").val(resultData.list.sDateEnd);
                $("#fechaInicio").val(resultData.list.sDateStart);
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

function editTask() {
    var id = $("#id").val();
    var estado = $("#estado").val();
    var asignado = $("#asignado").val();

    $.ajax({
        url: baseUrl + '/Process/editTask',
        method: 'POST',
        data: {
            id: id,
            estado: estado,
            asignado: asignado
        },
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                loadProcess();
                edit(id);
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
                title: "ERROR AL CARGAR",
                text: "Contacte la Administrador",
                type: "error",
                showCancelButton: false,
                confirmButtonText: "CERRAR"
            });
        }
    });
}

function loadTasks() {
    $.ajax({
        url: baseUrl + '/Tasks/getTasksByUserId',
        method: 'GET',
        data: { },
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