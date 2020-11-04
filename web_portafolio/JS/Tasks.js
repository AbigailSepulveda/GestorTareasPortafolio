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
            { width: '30%', targets: 1 },
            { width: '20%', targets: 2 }
        ],
        "aoColumns": [
            { title: "Informacion", mData: 'DESCRIPCION', sortable: true },
            { title: "Fechas", mData: 'FECHA', sortable: false },
            { title: "Estado", mData: 'ESTADO2', sortable: false }
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

function loadData() {

}