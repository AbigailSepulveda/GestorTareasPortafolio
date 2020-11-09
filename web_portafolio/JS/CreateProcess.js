function cleanInputs() {
    $("#nombre").val('');
    $("#descripcion").val('');
}

function createProcess() {
    var nombre = $("#nombre").val();
    var descripcion = $("#descripcion").val();

    $.ajax({
        async: true,
        beforeSend: function () {
            showModalLoading('PROCESANDO INFORMACIÓN');
        },
        url: baseUrl + '/Process/createProcess'
        ,
        method: 'POST',
        data: {
            name: nombre,
            description: descripcion
        },
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