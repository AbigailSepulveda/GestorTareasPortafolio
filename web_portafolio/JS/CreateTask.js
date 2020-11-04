$(document).ready(function () {
    $('#fechaInicio').datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        clearBtn: true,
        language: "es",
        autoclose: true
    });
    $('#fechaTermino').datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        clearBtn: true,
        language: "es",
        autoclose: true
    });
});

function cleanInputs() {
    $("#nombre").val('');
    $("#descripcion").val('');
    $("#padre").val('-1');
    $("#proceso").val('-1');
    $("#responsable").val('-1');
    $("#fechaInicio").val('');
    $("#fechaTermino").val('');
    document.getElementById('FileUpload1').value = '';
}

function createTask() {
    var nombre = $("#nombre").val();
    var descripcion = $("#descripcion").val();
    var responsableId = $("#responsable").val();
    var processId = $("#proceso").val();
    var taskId = $("#padre").val();
    var state = $("#estado").val();
    var start = $("#fechaInicio").val();
    var end = $("#fechaTermino").val();

    var formData = new FormData();
    var file1 = document.getElementById("FileUpload1").files[0];
    formData.append("FileUpload1", file1);

    $.ajax({
        async: true,
        beforeSend: function () {
            showModalLoading('PROCESANDO INFORMACIÓN');
        },
        url: baseUrl + '/Tasks/createTask?' +
            'nombre=' + nombre +
            '&descripcion=' + descripcion +
            '&responsableId=' + responsableId +
            '&process=' + processId +
            '&taskId=' + taskId +
            '&state=' + state +
            '&start=' + start +
            '&end=' + end
        ,
        method: 'POST',
        contentType: false,
        processData: false,
        data: formData,
        success: function (resultData) {
            setTimeout(hideModalLoading, 800);
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