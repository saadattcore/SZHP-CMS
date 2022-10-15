

CMS.namespace("Globals");

CMS.Globals = (function () {
    "use strict";
    var counter = 0;

    var webapiurlbase = window.ApplicationBasePath + "Api/";
    var normalurlbase = window.ApplicationBasePath;
    var userFullName = '';
    $.ajaxSetup({
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        cache: false
    });

    function ShowSpinner() {
        var opts = {
            lines: 11, // The number of lines to draw
            length: 3, // The length of each line
            width: 3, // The line thickness
            radius: 24, // The radius of the inner circle
            corners: 0, // Corner roundness (0..1)
            rotate: 30, // The rotation offset
            color: '#FFF', // #rgb or #rrggbb
            speed: 0.6, // Rounds per second
            trail: 64, // Afterglow percentage
            shadow: true, // Whether to render a shadow
            hwaccel: false, // Whether to use hardware acceleration
            className: 'spinner', // The CSS class to assign to the spinner
            zIndex: 2e9, // The z-index (defaults to 2000000000)
            top: 'auto', // Top position relative to parent in px
            left: 'auto' // Left position relative to parent in px
        };
        $("#divProgressStatus").spin(opts);
    }
    function HideSpinner() {
        $("#divProgressStatus").spin(false);
    }
    function FormatDateForClientSide(dateTime) {

        // the dateTime recieved from DB is in format of : 2014-01-09 i.e. yyyy-mm-dd
        var newDate = null;
        if (dateTime.indexOf('T') > 0)
            newDate = dateTime.substring(0, dateTime.indexOf('T'));
        else
            newDate = dateTime;
        newDate = newDate.substring(newDate.indexOf('-', 5) + 1) + '/' + newDate.substring(newDate.indexOf('-', 3) + 1, newDate.indexOf('-', 5)) + '/' + newDate.substring(0, newDate.indexOf('-', 3));
        return newDate;
    }

    function FormatMonthYearDateForClientSide(dateTime) {
        // the dateTime recieved from DB is in format of : 2014-01-09 i.e. yyyy-mm-dd
        var newDate = null;
        var monthInwords;
        if (dateTime.indexOf('T') > 0)
            newDate = dateTime.substring(0, dateTime.indexOf('T'));
        else
            newDate = dateTime;

        var year = newDate.substring(0, newDate.indexOf('-', 3));
        var month = newDate.substring(newDate.indexOf('-', 3) + 1, newDate.indexOf('-', 5));
        if (year == '1800') {
            newDate = "Present";
        } else {
            if (month == '01') {
                monthInwords = "January";
            } else if (month == '02') {
                monthInwords = "February";
            } else if (month == '03') {
                monthInwords = "March";
            } else if (month == '04') {
                monthInwords = "April";
            } else if (month == '05') {
                monthInwords = "May";
            } else if (month == '06') {
                monthInwords = "June";
            } else if (month == '07') {
                monthInwords = "July";
            } else if (month == '08') {
                monthInwords = "August";
            } else if (month == '09') {
                monthInwords = "September";
            } else if (month == '10') {
                monthInwords = "October";
            } else if (month == '11') {
                monthInwords = "November";
            } else if (month == '12') {
                monthInwords = "December";
            }

            // newDate = newDate.substring(newDate.indexOf('-', 5) + 1) + '/' + newDate.substring(newDate.indexOf('-', 3) + 1, newDate.indexOf('-', 5)) + '/' + newDate.substring(0, newDate.indexOf('-', 3));
            newDate = monthInwords + ' ' + year;
        }
        return newDate;
    }

    //This funciton will cast the date of client side to expected server side/jason format
    function FormatDateForServer(dateTime) {

        var convertedDate = dateTime.substring(dateTime.indexOf('/', 5) + 1, 10) + '-' + dateTime.substring(dateTime.indexOf('/', 2) + 1, 5) + '-' + dateTime.substring(0, 2) + 'T21:00:00.000Z';
        var sDate = new Date(convertedDate);
        return sDate;

    }

    function FormatMonthYearDateForServer(dateTime) {

        var monthInNumer;
        var year = dateTime.replace(/[^\d.]/g, '');

        var month = dateTime.replace(/\d+/g, '');
        month = month.replace(/^\s\s*/, '').replace(/\s*\s$/, '');
        if (month == 'January') {
            monthInNumer = '01';
        } else if (month == 'February') {
            monthInNumer = '02';
        } else if (month == 'March') {
            monthInNumer = '03';
        } else if (month == 'April') {
            monthInNumer = '04';
        } else if (month == 'May') {
            monthInNumer = '05';
        } else if (month == 'June') {
            monthInNumer = '06';
        } else if (month == 'July') {
            monthInNumer = '07';
        } else if (month == 'August') {
            monthInNumer = '08';
        } else if (month == 'September') {
            monthInNumer = '09';
        } else if (month == 'October') {
            monthInNumer = '10';
        } else if (month == 'November') {
            monthInNumer = '11';
        } else if (month == 'December') {
            monthInNumer = '12';
        } else if (month == 'Present') {
            monthInNumer = '01';
            year = '1800';
        }


        var convertedDate = year + '-' + monthInNumer + '-' + '01' + 'T21:00:00.000Z';
        var sDate = new Date(convertedDate);
        return sDate;

    }

    //This funciton will check the each field(error class implemented on field) of form for validation 
    function validateFormFields(dateTime) {

        var ValidationFlag, flagEmptyFieldStatus, flagValidateWebURL, flagvalidateAlphabets, flagvalidateNumeric, flagvalidateAlphaNumeric, flagvalidateEmail, flagvalidateEmptyddl, flagValiddateofBirth, flagvalidatedateformat, flagvalidatePassword, flagValidateSamePassword, flagValidateNumber;

        ValidationFlag = 1;
        flagEmptyFieldStatus = 1;
        flagValidateWebURL = 1;
        flagvalidateAlphabets = 1;
        flagvalidateNumeric = 1;
        flagvalidateAlphaNumeric = 1;
        flagvalidateEmail = 1;
        flagvalidateEmptyddl = 1;
        flagValiddateofBirth = 1;
        flagvalidatedateformat = 1;
        flagvalidatePassword = 1;
        flagValidateNumber = 1;
        $(".validateRequired").each(function (index, value) {
            var $ID = '#' + $(this).attr("id");
            flagEmptyFieldStatus = Utilities.ValidateForm($ID, Enums.ValidationType.validateRequired, "This field is mandatory.");

            if (flagEmptyFieldStatus == 0) {
                ValidationFlag = 0;
            }

            if (flagEmptyFieldStatus == 1) {
                if ($($ID).hasClass("ValidateWebUrl")) {
                    flagValidateWebURL = Utilities.ValidateForm($ID, Enums.ValidationType.ValidateWebUrl, "Not a valid website URL");
                    if (flagValidateWebURL == 0) {
                        ValidationFlag = 0;
                    }
                } else if ($($ID).hasClass("validateAlphabets")) {
                    flagvalidateAlphabets = Utilities.ValidateForm($ID, Enums.ValidationType.validateAlphabets, "Only alphabets are allowed");
                    if (flagvalidateAlphabets == 0) {
                        ValidationFlag = 0;
                    }

                } else if ($($ID).hasClass("validateNumeric")) {
                    flagvalidateNumeric = Utilities.ValidateForm($ID, Enums.ValidationType.validateNumeric, "Only numeric data allowed");
                    if (flagvalidateNumeric == 0) {
                        ValidationFlag = 0;
                    }
                } else if ($($ID).hasClass("validateAlphaNumeric")) {
                    flagvalidateAlphaNumeric = Utilities.ValidateForm($ID, Enums.ValidationType.validateAlphaNumeric, "Only alphnumeric characters are allowed");
                    if (flagvalidateAlphaNumeric == 0) {
                        ValidationFlag = 0;
                    }
                } else if ($($ID).hasClass("validateEmail")) {
                    flagvalidateEmail = Utilities.ValidateForm($ID, Enums.ValidationType.validateEmail, "Enter a valid email address");
                    if (flagvalidateEmail == 0) {
                        ValidationFlag = 0;
                    }
                }

                else if ($($ID).hasClass("validateDateFormat")) {
                    flagvalidatedateformat = Utilities.ValidateForm($ID, Enums.ValidationType.validateDateFormat, "Date is not corrrect");
                    if (flagvalidatedateformat == 1) {

                        if ($($ID).hasClass("validateDateofBirth")) {
                            flagValiddateofBirth = Utilities.ValidateForm($ID, Enums.ValidationType.validateDateofBirth, "Age must be greater then 18 Years");

                            if (flagValiddateofBirth == 0) {
                                ValidationFlag = 0;
                            }
                        }


                    }
                    else {
                        ValidationFlag = 0;
                    }
                }


                else if ($($ID).hasClass("validatePassword")) {
                    flagvalidatePassword = Utilities.ValidateForm($ID, Enums.ValidationType.validatePassword, "Password must be min 6 Character");
                    if (flagvalidatePassword == 0) {
                        ValidationFlag = 0;
                    }
                }

                //else if ($($ID).hasClass("ValidateSamePassword")) {
                //    flagValidateSamePassword = Utilities.ValidateForm($ID, Enums.ValidationType.ValidateSamePassword, "Passwrod are not same");
                //    if (flagValidateSamePassword == 0) {
                //        ValidationFlag = 0;
                //    }
                //}

            }

        });

        $(".validateNumeric").each(function (index, value) {
            var $ID = '#' + $(this).attr("id");
            flagValidateNumber = Utilities.ValidateForm($ID, Enums.ValidationType.validateNumeric, "Only Number is allowed");

            if (flagValidateNumber == 0) {
                ValidationFlag = 0;
            }



        });
        $(".validateEmptyddl").each(function (index, value) {
            var $ID = '#' + $(this).attr("id");
            flagvalidateEmptyddl = Utilities.ValidateForm($ID, Enums.ValidationType.validateEmptyddl, "Please select an option.");
            if (flagvalidateEmptyddl == 0) {
                ValidationFlag = 0;
            }
        });

        if (ValidationFlag == 0) {
            if ($('.errorMessage').first().parent().children().first().hasClass('validateEmptyddl')) {

                $('#' + $('.errorMessage').first().parent().children().first().attr('id')).trigger('chosen:activate');
            }
            else {
                $('.errorMessage').first().parent().children().first().focus();
            }


            return false;
        } else {

            return true;
        }

    }


    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }


    return {
        baseURL: normalurlbase
        , ShowSpinner: function (bShowOverlay) {
            $('#divProgressStatus').show();
            if (bShowOverlay) {
                $('#divProgressOverlay').show();
            }
            ShowSpinner();
        }
        , HideSpinner: function () {
            $('#divProgressStatus').hide();
            $('#divProgressOverlay').hide();
            HideSpinner();
        }
        , MakeAjaxCallToApi: function (httpmethod, URL, data, successCallback, failureCallback, aynch, showProgress) {


            if (typeof aynch == 'undefined')
                aynch = true;


            if (typeof showProgress == 'undefined') {


                if (URL.indexOf('CommonLookupsData') >= 0) {
                    showProgress = false;
                }
                else {
                    showProgress = true;
                }
            }


            if (showProgress) {
                counter++;
                if (counter > 0) {
                    $('#divProgressOverlay').show();
                    $('#divProgressStatus').show();
                    ShowSpinner();
                }
            }

            var urltocall = webapiurlbase + URL;

            var defObj = $.ajax({
                type: httpmethod, //"POST"
                url: urltocall,
                data: data,
                async: aynch,
                beforeSend: function (jqXHR, settings) {

                },
                success: function (resp) {

                    try {
                        var result = JSON.parse(resp);
                        if (successCallback)
                            successCallback(result);
                    } catch (err) {
                    }
                },
                error: function (err, type, httpStatus) {
                    if (err.status == 406) {

                        var retPath = "";
                        if (window.location.pathname) {
                            retPath = "?ReturnURL=" + window.location.pathname;
                        }
                        window.location.href = CMS.Resources.Views.LoginURL;/// + retPath;

                        return;
                    }
                    if (failureCallback)
                        failureCallback(err, type, httpStatus);
                    else {
                    }

                },
                complete: function () {
                    if (showProgress) {
                        counter--;
                        if (counter <= 0) {
                            $('#divProgressOverlay').hide();
                            $('#divProgressStatus').hide();
                            HideSpinner();
                        }
                    }
                }
            });

            return defObj;
        } //End of MakeAjaxCallToApi
       ,
        MakeAjaxCallToController: function (httpmethod, URL, data, successCallback, failureCallback, aynch, showProgress) {

            if (typeof aynch == 'undefined')
                aynch = true;

            if (typeof showProgress == 'undefined')
                showProgress = true;

            if (showProgress) {
                counter++;
                if (counter > 0) {
                    $('#divProgressOverlay').show();
                    $('#divProgressStatus').show();
                    ShowSpinner();
                }
            }

            var urltocall = normalurlbase + URL;

            var defObj = $.ajax({
                type: httpmethod, //"POST"
                url: urltocall,
                data: data,
                async: aynch,
                beforeSend: function (jqXHR, settings) {

                },
                success: function (resp) {

                    try {
                        var result = JSON.parse(resp);
                        if (successCallback)
                            successCallback(result);
                    } catch (err) {
                    }
                },
                error: function (err, type, httpStatus) {
                    if (err.status == 406) {

                        var retPath = "";
                        if (window.location.pathname) {
                            retPath = "?ReturnURL=" + window.location.pathname;
                        }
                        window.location.href = CMS.Resources.Views.LoginURL + retPath;

                        return;
                    }
                    if (failureCallback)
                        failureCallback(err, type, httpStatus);
                    else {
                    }

                },
                complete: function () {
                    if (showProgress) {
                        counter--;
                        if (counter <= 0) {
                            $('#divProgressOverlay').hide();
                            $('#divProgressStatus').hide();
                            HideSpinner();
                        }
                    }
                }
            });

            return defObj;
        } //End of MakeAjaxCallToController
        , GetCurrentUserInfo: function () {
            //var cookieValue = $.cookie("UserFullName", { path: '/' });
            return 'test';
        }
        , GetCurrentUserIsEmployer: function () {
            //var cookieValue = $.cookie("IsEmployerUser", { path: '/' });
            return 'test';
        }
        , GetControllerPath: function (actionName, controllerName) {
            return "".format("{0}{1}/{2}", CMS.Globals.baseURL, controllerName, actionName);
        }
        , FormatDateForClientSide: function (datetime) {
            return FormatDateForClientSide(datetime);
        }
        , FormatDateForServerSide: function (datetime) {
            return FormatDateForServer(datetime);
        }
        , FormatMonthYearDateForServerSide: function (datetime) {
            return FormatMonthYearDateForServer(datetime);
        }
          , FormatMonthYearDateForClientSide: function (datetime) {
              return FormatMonthYearDateForClientSide(datetime);
          }
        , GetParameterByName: function (name) {
            return getParameterByName(name);
        },
        ValidateForm: function (name) {
            return validateFormFields(name);
        }
    };
}());

