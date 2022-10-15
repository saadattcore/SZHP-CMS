$.cachedScript("../Scripts/Jquery/jquery.validate.min.js").done(function () {

"use strict";

//Forgot Password Form Validation
$("#ForgotPasswordForm").validate({
    rules: {
        email: {
            email: true
        }

    },
    messages: {
        email: "Invalid email address"
      
    }
});


$.validator.methods.email = function (value, element) {
    return this.optional(element) || /[A-Za-z0-9_.]+@[a-z]+\.[a-z]+/.test(value);
}

$.validator.addMethod("pwcheck", function (value) {
    return /^[A-Za-z0-9\d=!\-@._*]*$/.test(value) // consists of only these
        && /[A-Z]/.test(value) // has a lowercase letter
        && /\d/.test(value) // has a digit
});

$.validator.methods.Cphone = function (value, element) {
    return this.optional(element) || /[0-9]/.test(value);
}
});

function randomString() {
    var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOP1234567890";
    var string_length = 10;
    var randomstring = '';
    for (var i = 0; i < string_length; i++) {
        var rnum = Math.floor(Math.random() * chars.length);
        randomstring += chars.substring(rnum, rnum + 1);
    }
    document.signUp.randomPassA.value = randomstring;
    document.signUp.randomPassB.value = randomstring;
}
function CancelClick() {
    window.location.reload();
}

