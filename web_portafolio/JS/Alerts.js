$(document).ready(function () {
    initTable();
});

function initTable() {
    $('#tableTasks1').DataTable({
        "data": [],
        "language": {
            "url": baseUrl + "/Content/DataTables/Spanish.json"
        },
        "columnDefs": [
            { width: '33%', targets: 0 },
            { width: '33%', targets: 1 },
            { width: '33%', targets: 2 }
        ],
        "aoColumns": [
            { title: "Nombre", mData: 'name', sortable: true },
            { title: "Fecha Limite", mData: 'sDateEnd', sortable: false },
            { title: "Responsable", mData: 'creatorUser.name', sortable: false }
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
    $('#tableTasks2').DataTable({
        "data": [],
        "language": {
            "url": baseUrl + "/Content/DataTables/Spanish.json"
        },
        "columnDefs": [
            { width: '33%', targets: 0 },
            { width: '33%', targets: 1 },
            { width: '33%', targets: 2 }
        ],
        "aoColumns": [
            { title: "Nombre", mData: 'name', sortable: true },
            { title: "Fecha Limite", mData: 'sDateEnd', sortable: false },
            { title: "Responsable", mData: 'creatorUser.name', sortable: false }
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
    $('#tableTasks3').DataTable({
        "data": [],
        "language": {
            "url": baseUrl + "/Content/DataTables/Spanish.json"
        },
        "columnDefs": [
            { width: '33%', targets: 0 },
            { width: '33%', targets: 1 },
            { width: '33%', targets: 2 }
        ],
        "aoColumns": [
            { title: "Nombre", mData: 'name', sortable: true },
            { title: "Fecha Limite", mData: 'sDateEnd', sortable: false },
            { title: "Responsable", mData: 'creatorUser.name', sortable: false }
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
    loadData1();
    loadData2();
    loadData3();
    window.setInterval(function () {
        console.log("holi");
        loadData1();
        loadData2();
        loadData3();
    }, 60000);
}

function loadData1() {
    $.ajax({
        url: baseUrl + '/Tasks/getTaskRed',
        method: 'GET',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                var datatable = $('#tableTasks1').DataTable();
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
function loadData2() {
    $.ajax({
        url: baseUrl + '/Tasks/getTaskYellow',
        method: 'GET',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                var datatable = $('#tableTasks2').DataTable();
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
function loadData3() {
    $.ajax({
        url: baseUrl + '/Tasks/getTaskGreen',
        method: 'GET',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                var datatable = $('#tableTasks3').DataTable();
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