$(document).ready(function () {
    $('a').tooltip();
    $('#tblData').dataTable();
    $('#tblData_length select').removeClass('form-control input-sm');
    $('#chkBoxHeader').click(function () {

        $("input[name='chkBoxItem']").prop("checked", this.checked);
       

    });
    $('#btnAction').click(function () {

        var itemSelected = $("input[name='chkBoxItem']:checked").length;

        var ddlItemSelected = $('#ddlActions option:selected').index();

        //if (itemSelected == 0 || $('#ddlActions option:selected').index() == 0) {
        //    alert('Please select a row and action');
        //    return false;
        //}
        if (ddlItemSelected == 0) {

            if ($('.error').length > 0) {

                $('.error').remove();

            }

            if ($('div.success').length > 0)
            {
                $('div.success').remove();
            }

            $('h1').after('<div class="error">Select appropriate action to continue</div>');

            return false;
            

        }
        if (itemSelected == 0) {

            if ($('.error').length > 0) {

                $('.error').remove();

            }

            if ($('div.success').length > 0) {
                $('div.success').remove();
            }
           

            $('h1').after('<div class="error">Select at least one row to continue</div>');

            return false;
        }
        else {

            if ($('.error').length > 0) {

                $('.error').remove();

            }

            //var message = "Are you sure to " + $('#ddlActions option:selected').text() + " " + itemSelected + " items ?"

            //if (!confirm(message))
            //    return false;
        }


    });
});

function UpdateMenu(clickedAnchor, url) {

    

    var row = $(clickedAnchor).parent().parent();
    var column = row.children('td:first');
    var hidMenuID = column.find("input[type='checkbox']");

    if (hidMenuID.val() === undefined) {
        hidMenuID = column.find("input[type='hidden']");
    }

    //window.location = '/News/CreateUpdate/?itemID=' + hidMenuID.val() + '&operation=Update';
    window.location = url + '/?itemID=' + hidMenuID.val() + '&operation=Update';
}

function DeleteMenu(clickedAnchor, url) {

    if (!confirm('Are you sure to delete this record ?')) {
        return false;
    }

    var row = $(clickedAnchor).parent().parent();
    var hidMenuID = row.children('td:first').children('input');
    var postData = JSON.stringify({ id: hidMenuID.val() });
    

    $.ajax({

        url: url,
        type: "POST",
        data: postData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
           
            if (data.status == true) {
                
                row.remove();
                row.html('');

                var rowCount = $('#tblData tbody tr').length;

                if (rowCount == 0)
                    $("#tblData_info").text("No record left");

                else
                    $("#tblData_info").text('Showing 1 to ' + rowCount + ' of ' + rowCount + ' entries');
            }
            else {

                alert(data.message);
            }


        },
        error: function (status) {
            alert(status);
        }


    });






}
