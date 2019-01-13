const ROOT_API_USER = "/api/User/";

// UserController is an object that contains actions the will be executed on
// get, save (create or update), delete
var UserController =
    function ($table, $modal, $modalAddUser) {

        return {

            getUser: function (userID) {
                $.ajax({ url: ROOT_API_USER + userID, cache: false })
                    .done( function (json) {
                        $modal.loadJSON(json);
                    })
                    .fail(function () {
                        document.getElementById("EditUserForm").reset();
                        toastr.error('An error occured while trying to get the User.');
                    });
            },

            saveUser: function (userID, user) {
                $.ajax({
                    method: (userID === null) ? "POST" : "PUT",
                    url: ROOT_API_USER + (userID||""),
                    // processData: false,
                    contentType: "application/json",
                    dataType: 'json',
                    data: user,
                    // async: true,
                    success: function () {
                        toastr.success("User is successfully saved!");
                        $table.ajax.reload(null, false);
                        if (userID === null)
                            $modalAddUser.modal('hide');
                        else
                            $modal.modal('hide');
                    },
                    error: function (msg) {
                        for (var error in msg.responseJSON) {
                            // var id = userID == null? (error.startsWith('Input.')? `error${error.substring(6, error.lengthyyyyy)}` : `error${error}`) : (error.startsWith('Input.')? `_error${error.substring(6, error.lengthyyyyy)}` : `_error${error}`);
                            var id = error.startsWith('Input.')? `error${error.substring(6, error.length)}` : `error${error}`;
                            var query = userID == null? (jQuery("#" + id, $modalAddUser).length > 0 ? jQuery("#" + id, $modalAddUser) : null) : (jQuery("#" + id, $modal).length > 0 ? jQuery("#" + id, $modal) : null);
                            if(query != null){
                                var element = jQuery(query, $modal);
                                var value = id == "errorAll"? '<li>' + msg.responseJSON[error] + '</li>' : msg.responseJSON[error];
                                $(element).html(value);
                                // $(element).css();
                            }
                        }
                        // toastr.error('An error occured while trying to save the user.');
                    }
                });
            },

            deleteUser: function (userID) {
                $.ajax({
                    method: "DELETE",
                    url: ROOT_API_USER + userID,
                    success: function () {
                        toastr.success("User is successfully deleted!");
                        $table.ajax.reload(null, false);
                    },
                    error: function (msg) {
                        toastr.error('An error occured while trying to delete the User. ' + msg.responseText, 'User cannot be deleted!');
                    }
                });
            }
        }
    };

$(document).ready(function () {

    var $table = $('#example').DataTable();

    // Bootstrap modal setup
    $modalEditUser = $('#modalEditUser');
    $modalEditUser.on('hide.bs.modal', function () {
        $(this).find("input[type!=checkbox],textarea,select").val('').end();
        $(this).find("input:checkbox").prop('checked', false);
    });
    
    $("#cancelEditButton", $modalEditUser).on("click", function () {
        $modalEditUser.modal('hide');
    });

    $modalAddUser = $('#modalAddUser');
    $modalAddUser.on('hide.bs.modal', function () {
        $(this).find("input[type!=checkbox],textarea,select").val('').end();
        $(this).find("input:checkbox").prop('checked', false);
    });

    $("#cancelAddButton", $modalAddUser).on("click", function () {
        $modalAddUser.modal('hide');
    });
    // end modal setup

    var ctrl = UserController($table, $modalEditUser, $modalAddUser);

    $table.on("click", "button.edit",
        function () {
            ctrl.getUser(this.attributes["data-id"].value);
        });

    $table.on("click", "button.delete",
        function () {
            ctrl.deleteUser(this.attributes["data-id"].value);
        });

    $('body').on("click", "#submitEditButton",
        function (e) {
            e.preventDefault();
            var $form = $("#EditUserForm");
            var UserId = $("#Id", $form).val();
            var User = JSON.stringify($form.serializeJSON({checkboxUncheckedValue: "false", parseAll: true }));
            ctrl.saveUser(UserId, User);
    });
    
    $('body').on("click", "#submitAddButton",
    function (e) {
        e.preventDefault();
        var $form = $("#AddUserForm");
        var User = JSON.stringify($form.serializeJSON({checkboxUncheckedValue: "false", parseAll: true }));
        ctrl.saveUser(null, User);
    });
    
    $('body').on("click", "#submitAddButton2",
    function (e) {
        e.preventDefault();
        var $form = $("#AddUserForm");
        $form.submit();
    });
});