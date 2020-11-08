
var tasks = [];

$(document).ready(function () {
    initTable();

    $('#fechaTerminoTarea').datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        clearBtn: true,
        language: "es",
        autoclose: true
    });
});


function initTable() {
    $('#tableTasks').DataTable({
        "data": [],
        "language": {
            "url": baseUrl + "/Content/DataTables/Spanish.json"
        },
        "columnDefs": [
            { width: '40%', targets: 0 },
            { width: '50%', targets: 1 },
            { width: '10%', targets: 2 }
        ],
        "aoColumns": [
            { title: "Nombre", mData: 'name', sortable: true },
            { title: "Descripcion", mData: 'description', sortable: false },
            { title: "Fecha Termino", mData: 'endDate', sortable: false }
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

function addTask() {

    var nombre = $("#nombreTarea").val();
    var descripcion = $("#descripcionTarea").val();
    var state = $("#estadoTarea").val();
    var end = $("#fechaTerminoTarea").val();

    var task = {
        name: nombre,
        description: descripcion,
        task_status_code: state,
        endDate: end
    };

    tasks.push(task);

    cleanTask();
    loadTasks();
}

function cleanTask() {
    $("#nombreTarea").val('');
    $("#descripcionTarea").val('');
    $("#estadoTarea").val('');
    $("#fechaTerminoTarea").val('');
}

function cleanInputs() {
    $("#nombreProceso").val('');
    $("#descripcionProceso").val('');
    tasks = [];
    var datatable = $('#tableTasks').DataTable();
    datatable.clear();
    datatable.rows.add(tasks);
    datatable.draw();
}


function loadTasks() {
    var datatable = $('#tableTasks').DataTable();
    datatable.clear();
    datatable.rows.add(tasks);
    datatable.draw();
}

function addProcess() {

    var nombre = $("#nombreProceso").val();
    var descripcion = $("#descripcionProceso").val();

    var template = {
        name: nombre,
        description: descripcion,
        tasks: tasks
    };

    $.ajax({
        async: true,
        beforeSend: function () {
            showModalLoading('PROCESANDO INFORMACIÓN');
        },
        url: baseUrl + '/Template/createProcess',
        data: {
            template: template
        },
        method: 'POST',
        dataType: 'json',
        success: function (resultData) {
            setTimeout(hideModalLoading, 800);
            if (resultData.isReady) {
                cleanInputs();
                Swal.fire({
                    title: "REALIZADO",
                    text: resultData.msg,
                    type: "success",
                    showCancelButton: false,
                    confirmButtonText: "ACEPTAR"
                });
            } else {
                Swal.fire({
                    title: "ERROR DETECTADO",
                    html: "<b>Detalle: </b> " + resultData.msg,
                    type: "warning",
                    showCancelButton: false,
                    confirmButtonText: "CERRAR"
                });
            }
        }, error: function (jqXHR, error, errorThrown) {
            Swal.fire({
                title: "ERROR",
                text: "Contacte la Administrador",
                type: "error",
                showCancelButton: false,
                confirmButtonText: "CERRAR"
            });
        }
    });
}