
$(document).ready(function () {
    // DataTable setup
    $('#example').DataTable({
        // "method": "GET",
        "ajax": "/api/User/", 
        "dataSrc": "json",
        "columns": [
            { "data": "fullName"},
            { "data": "username", "defaultContent": "" },
            { "data": "email", "defaultContent": "" },
            { "data": "phoneNumber","defaultContent": "" },
            {
                "data": "id",
                "sortable": false,
                "searchable": false,
                "render": function (data) {
                    return  '<div class="text-center">' +
                                '<button data-id="' + data + '" class="btn btn-primary btn-sm edit mx-1" data-toggle="modal" data-target="#modalEditUser">' + 
                                    '<i class="far fa-edit"></i>' +
                                '</button>' +
                                '<button data-id="' + data + '" class="btn btn-danger btn-sm delete mx-1">'+
                                    '<i class="far fa-trash-alt"></i>' +
                                '</button>' +
                            '</div>';
                    
                }
            }
        ]
    });// end DataTable setup

});