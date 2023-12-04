var isEdit = false;
var deleteCode = '';
var controllerName = '';
$(document).ready(function () {
    $('#example1').DataTable()
    $('#example2').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': false,
        'ordering': true,
        'info': true,
        'autoWidth': false
    });
    controllerName = $('span.hidden').attr('controllerName') || '';
});

function SaveData() {

    var formData = {
        Code: $('#AuditCode').val(),
        Name: $('#AuditName').val(),
        ParentCode: $('#AuditParentCode').val()
    };

    var toastMsg = isEdit ? `${addSpaceBetweenCapital(controllerName)} Updated Succesfully` : `${addSpaceBetweenCapital(controllerName)} Added Succesfully`
        , ajaxUrl = isEdit ? `/${controllerName}/Edit` : `/${controllerName}/Create`;

    $.ajax({
        type: 'POST',
        url: ajaxUrl,
        data: formData,
        success: function (response) {
            if (response.success) {
                toastr.success(toastMsg);
                window.setTimeout(function () {
                    location.reload();
                }, 1000);
            }
            else {

                if (response.errorMesage == undefined) {
                    toastr.error("Unexpected error has occured");
                }
                else { toastr.error(response.errorMesage); }
            }
        },
        error: function (xhr, status, error) {

            toastr.error("Unexpected error has occured");
        }
    });
}

function edit(code) {
    $.ajax({
        type: 'GET',
        url: `/${controllerName}/Edit`,
        data: { Code: code },
        success: function (response) {
            if (response.success) {
                toastr.success(`${addSpaceBetweenCapital(controllerName)} data loaded Succesfully`);
                OpenEditModal(response.data);
            }
            else {
                toastr.error(response.errorMesage || "Unexpected error has occured");
            }
        },
        error: function (xhr, status, error) {

            toastr.error("Unexpected error has occured");
        }
    });
}

function SaveDataAuditRiskItem() {

    var formData = {
        Code: $('#AuditCode').val(),
        Name: $('#AuditName').val(),
        ParentCode: $('#AuditParentCode').val(),
        PrimaryDepartment: $('#AuditPrimaryDepartment').val(),
        SecondaryDepartment: $('#AuditSecondaryDepartment').val()
    };

    var toastMsg = isEdit ? `${addSpaceBetweenCapital(controllerName)} Updated Succesfully` : `${addSpaceBetweenCapital(controllerName)} Added Succesfully`
        , ajaxUrl = isEdit ? `/${controllerName}/Edit` : `/${controllerName}/Create`;

    $.ajax({
        type: 'POST',
        url: ajaxUrl,
        data: formData,
        success: function (response) {
            if (response.success) {
                toastr.success(toastMsg);
                window.setTimeout(function () {
                    location.reload();
                }, 1000);
            }
            else {

                if (response.errorMesage == undefined) {
                    toastr.error("Unexpected error has occured");
                }
                else { toastr.error(response.errorMesage); }
            }
        },
        error: function (xhr, status, error) {

            toastr.error("Unexpected error has occured");
        }
    });
}




function deleteAudit(code) {
    deleteCode = code;
    showPopup();
}

function OpenAddModal() {
    $('#ModalTitle').text(`Add ${addSpaceBetweenCapital(controllerName)}`);
    $('#AuditCode').removeAttr('disabled');
    $('#AuditCode').val("");
    $('#AuditName').val("");
    $('#AuditParentCode').val(0);
    if ($('#AuditPrimaryDepartment') != undefined) {
        $('#AuditPrimaryDepartment').val(0);
    }
    if ($('#AuditSecondaryDepartment') != undefined) {
        $('#AuditSecondaryDepartment').val('');
    }
    $('#modal-default').modal('show');
    isEdit = false;
}

function OpenEditModal(data) {
    $('#ModalTitle').text(`Edit ${addSpaceBetweenCapital(controllerName)}`);

    $('#AuditCode').attr('disabled', 'disabled');

    $('#AuditCode').val(data.Code);
    $('#AuditName').val(data.Name);
    $('#AuditParentCode').val(data.ParentCode);
    if (data.PrimaryDepartment != undefined) {

        $('#AuditPrimaryDepartment').val('');
        $('#AuditPrimaryDepartment').val(data.PrimaryDepartment);
    }
    if (data.SecondaryDepartment != undefined) {
        //var selectElement = document.getElementById("AuditSecondaryDepartment");

        $('#AuditSecondaryDepartment').val('');
        $.each(data.SecondaryDepartment, function (i, e) {
            $("#AuditSecondaryDepartment option[value='" + e + "']").prop("selected", true);
        });
        $('.select2').select2()
    }
    $('#modal-default').modal('show');

    isEdit = true;
}



function confirmAction() {
    //delete
    $.ajax({
        type: 'POST',
        url: `/${controllerName}/Delete`,
        data: { code: deleteCode },
        success: function (response) {
            if (response.success) {
                toastr.success(`${addSpaceBetweenCapital(controllerName)} delete Succesfully`);
                window.setTimeout(function () {
                    location.reload();
                }, 1000);
            }
            else {
                //toastr.error(response.errorMesage || "Unexpected error has occured");
                toastr.error("first delete all dependant data");
            }
        },
        error: function (xhr, status, error) {

            toastr.error("Unexpected error has occured");
        }
    });

    closePopup();
}
