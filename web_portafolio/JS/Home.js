$(document).ready(function () {
    initTable();
    loadAlerts();
    loadTask();
    loadProcess();
    loadUnit();
});

function initTable() {
    $('#tableProgress').DataTable({
        "data": [],
        "language": {
            "url": baseUrl + "/Content/DataTables/Spanish.json"
        },
        "columnDefs": [
            { width: '25%', targets: 0 },
            { width: '25%', targets: 1 },
            { width: '25%', targets: 3 },
            { width: '25%', targets: 4 }
        ],
        "aoColumns": [
            { title: "Tareas", mData: 'name', sortable: true },
            { title: "Pendientes", mData: 'sDateEnd', sortable: false },
            { title: "Trabajando", mData: 'creatorUser.name', sortable: false },
            { title: "Realizadas", mData: 'creatorUser.name', sortable: false }
        ],
        "processing": false,
        "bDestroy": true,
        "filter": true,
        "paging": false,
        "orderMulti": false,
        "ordering": false,
        "info": false,
        "scrollX": false,
        "lengthChange": false,
        "fixedColumns": true,
        "searching": false,
        "autoWidth": false
    });
    window.setInterval(function () {
        console.log("holi");
        loadData1();
        loadData2();
        loadData3();
    }, 60000);
}

function loadAlerts() {
    $.ajax({
        url: baseUrl + '/Home/getReportAlerts',
        method: 'GET',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                $("#red").val(resultData.list.red);
                $("#yellow").val(resultData.list.yellow);
                $("#green").val(resultData.list.green);
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
function loadTask() {
    $.ajax({
        url: baseUrl + '/Home/getReportTask',
        method: 'GET',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                var datos = [];
                var obj = ["Pendientes", resultData.list.pending];
                datos.push(obj);
                obj = ["Trabajando", resultData.list.working];
                datos.push(obj);
                obj = ["Realizadas", resultData.list.done];
                datos.push(obj);

                Highcharts.getOptions().plotOptions.pie.colors = (function () {
                    var colors = [],
                        base = Highcharts.getOptions().colors[9],
                        i;

                    for (i = 0; i < 10; i += 1) {
                        // Start out with a darkened base color (negative brighten), and end
                        // up with a much brighter color
                        colors.push(Highcharts.Color(base).brighten((i - 2) / 7).get());
                    }
                    return colors;
                }());

                // Build the chart
                Highcharts.chart('container2', {
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie'
                    },
                    title: {
                        text: 'Total de Tareas: ' + resultData.list.tasks
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    exporting: { enabled: false },
                    credits: { enabled: false },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: true,
                                format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                style: {
                                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                                }
                            }
                        }
                    },
                    series: [{
                        name: 'Tareas',
                        data: datos
                    }]
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
function loadProcess() {
    $.ajax({
        url: baseUrl + '/Home/getReportProcess',
        method: 'GET',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                var datosX = [];
                var pending = [];
                var working = [];
                var done = [];
                $.each(resultData.list, function (i, item) {
                    console.log(item);
                    datosX.push(item.processName);
                    pending.push(item.pending);
                    working.push(item.working);
                    done.push(item.done);
                });

                var datosY = [];

                var obj = {
                    name: 'Pendientes',
                    data: pending
                };
                datosY.push(obj);
                obj = {
                    name: 'Trabajando',
                    data: working
                };
                datosY.push(obj);
                obj = {
                    name: 'Realizadas',
                    data: done
                };
                datosY.push(obj);

                Highcharts.chart('container1', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Estado de Tareas por Procesos'
                    },
                    xAxis: {
                        categories: datosX
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Total de Tareas'
                        }
                    },
                    tooltip: {
                        pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> ({point.percentage:.0f}%)<br/>',
                        shared: true
                    },
                    plotOptions: {
                        column: {
                            stacking: 'percent'
                        }
                    },
                    series: datosY,
                    exporting: { enabled: false },
                    credits: { enabled: false }
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
function loadUnit() {
    $.ajax({
        url: baseUrl + '/Home/getReportUnit',
        method: 'GET',
        data: {},
        dataType: 'json',
        success: function (resultData) {
            if (resultData.isReady) {
                var datosX = [];
                var pending = [];
                var working = [];
                var done = [];
                $.each(resultData.list, function (i, item) {
                    console.log(item);
                    datosX.push(item.unitName);
                    pending.push(item.pending);
                    working.push(item.working);
                    done.push(item.done);
                });

                var datosY = [];

                var obj = {
                    name: 'Pendientes',
                    data: pending
                };
                datosY.push(obj);
                obj = {
                    name: 'Trabajando',
                    data: working
                };
                datosY.push(obj);
                obj = {
                    name: 'Realizadas',
                    data: done
                };
                datosY.push(obj);

                Highcharts.chart('container3', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Estado de Tareas por Unidades'
                    },
                    xAxis: {
                        categories: datosX,
                        crosshair: true
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Tareas'
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            '<td style="padding:0"><b>{point.y} </b></td></tr>',
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    },
                    plotOptions: {
                        column: {
                            pointPadding: 0.2,
                            borderWidth: 0
                        }
                    },
                    exporting: { enabled: false },
                    credits: { enabled: false },
                    series: datosY
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