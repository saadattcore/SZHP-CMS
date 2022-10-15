

CMS.namespace("UI.News.Index");

CMS.UI.News.Index = (function () {
    "use strict";

    var _isInitialized = false;

    function initialiseControls() {
        if (_isInitialized == false) {
            _isInitialized = true;
            BindEvents();

            //$('#btnEnglish').click(function () {

            //    // alert(5);
            //    SetLanguage(1);

            //    var selText = $(this).text();
            //    $(this).parents('.language').find('.dropdown-toggle').html(selText + ' <span class="caret crt_pading"></span>');
            //    return false;
            //    // $.cookie("LanguageId", 1);


            //});

            //$('#btnArabic').click(function () {

            //    //  alert(6);
            //    SetLanguage(2);
            //    var selText = $(this).text();
            //    $(this).parents('.language').find('.dropdown-toggle').html(selText + ' <span class="caret crt_pading"></span>');

            //    return false;
            //    // $.cookie("LanguageId", 2);

            //});
        }

    }

    function BindEvents() {

        $('#dataTable').dataTable();

    } // End of BindEvents()

    //====Lanaguage
    function SetLanguage(langId) {

        //=============== Saving Language to Cookies ============
        // alert(langId);
        var url = "UserData/SetLanguage?landId=" + langId;

        CMS.Globals.MakeAjaxCall("GET", url, [], function (result) {
            if (result.success === true) {

                $.cookie("LanguageId", result.GetLangId.LanguageId, { path: '/' });
                $.cookie("URLName", result.GetLangId.URLName, { path: '/' });
                // alert(url);
                CMS.UI.showRoasterMessage(CMS.Resources.Msg_Change_Lang, Enums.MessageType.Info);

                window.location.href = "/User/Login";
                //location.reload();

            } else {
                CMS.UI.showRoasterMessage(result.error, Enums.MessageType.Error, 5000);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            //debugger;
            CMS.UI.showRoasterMessage(CMS.Resources.Msg_Prob_Authen_Cred, Enums.MessageType.Error);
        });
        //=========================================================
    }

    function ClearFields() {

    }  // End of ClearFields()

    function resetPage() {


        initialiseControls();

    }

    return {

        initialisePage: function () {
            initialiseControls();
        },

        readyMain: function () {
            $(function () {
                CMS.UI.News.Index.initialisePage();
            });//End of Ready Function
        },

        resetPage: function () {
            resetPage();
        }
    };
}());
