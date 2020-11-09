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
                    var icons = '<button class="btn btn-success" style="width:100%" onclick="acceptTask(\'' + row.id + '\',\'' + row.name + '\');"> Aceptar </button>';
                    icons = icons + '<br/>';
                    icons = icons + ' <button class="btn btn-danger" style="width:100%" onclick="refuse(\'' + row.id + '\',\'' + row.name + '\');"> Rechazar </button>';
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

function refuse(id, name) {
    $("#modalID").val(id);
    $("#modalNombre").val(name);
    $("#modalMessage").val('');
    $("#modalRefuse").modal();
}

function hideModal() {
    $("#modalID").val('');
    $("#modalNombre").val('');
    $("#modalMessage").val('');
    $('#modalRefuse').modal('hide');
}

function acceptTask(id) {
    $.ajax({
        url: baseUrl + '/Tasks/acceptTask?id=' + id
        ,
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

function refuseTask() {
    var id = $("#modalID").val();
    var message = $("#modalMessage").val();
    $.ajax({
        url: baseUrl + '/Tasks/refuseTask?id=' + id +
            '&message=' + message
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
        url: baseUrl + '/Tasks/getAssignTasksByUser',
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