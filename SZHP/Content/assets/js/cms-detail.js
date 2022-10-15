

var ValidateExtension = function (fileUploader, extToValidate) {

    var arrExtensions = extToValidate.split(",");

    var fileCtr = $(fileUploader);

    var fileName = fileCtr.val();

    if (fileName == "") {


        fileCtr.parent().parent().find(".text-danger").remove();

        if ($('.text-danger').length == 0) {
            fileCtr.closest("form").find("input[type=submit]").prop('disabled', false);
        }

        return;
    }

    var dotIndex = fileName.indexOf(".");

    var extension = fileName.substring(dotIndex + 1);

    var found = false;

    $.each(arrExtensions, function (index, item) {

        if (item == extension) {

            found = true;
        }

    });

    if (!found) {

        fileCtr.closest("form").find("input[type=submit]").prop('disabled', true);

        var element = fileCtr.parent().parent().children(".text-danger").length;

        if (element > 0) {

            $(".text-danger").remove();
        }

        fileCtr.parent().parent().append('<div  class="text text-danger col-md-2 padding-top-10" style="font-size:12px;padding-left:0px;display:none" ><strong><span id="errExt" ><i class="fa fa-exclamation-triangle"></i>&nbsp;<label>' + extension + ' files are not allowed' + '</label></span></strong></div>');

        fileCtr.popover({ content: '  ' + extension + ' files are not allowed', placement: 'bottom', trigger: 'manual' }).popover('show');

        $('.popover-content').addClass("fa fa-exclamation-circle fa-5x");


        fileCtr.on('shown.bs.popover', function () {

            setTimeout(function () {
                fileCtr.popover('destroy');

            }, 2000);

        })



    }
    else {

        if ($('.text-danger').length > 1) {

            fileCtr.parent().parent().find(".text-danger").remove();

        }

        else {
            $('.text-danger').remove();


            fileCtr.closest("form").find("input[type=submit]").prop('disabled', false);
        }
    }
}

var RenderFormDocumentPartialView = function (anchor, isEmpty) {

    if (anchor === null && isEmpty) {
        var dataServer = JSON.stringify({ id: '', operation: $('#operation').val(), formID: $('#itemID').val() });
    }
    else if (anchor != null && !isEmpty)
    {
        var row = $(anchor).parent().parent();

        var itemID = row.children("td:first").children('input').val();

        var dataServer = JSON.stringify({ id: itemID, operation: $('#operation').val(), formID: $('#itemID').val() });
    }

    $.ajax({

        url: '/Archive/GetFormDocPartial',
        type: 'POST',
        dataType: 'html',
        data: dataServer,
        success: function (data) {

            $(".modal-body").html(data);

            $("#myModal").modal('show');

        }

    });

}

var RenderEmptyPartial = function () {


    var dataServer = JSON.stringify({ id: '', operation: $('#operation').val(), formID: $('#itemID').val() });


    $.ajax({

        url: '/Archive/GetFormDocPartial',
        type: 'POST',
        dataType: 'html',
        data: dataServer,
        success: function (data) {

            $(".modal-body").html(data);

            $("#myModal").modal('show');

        }

    });

}