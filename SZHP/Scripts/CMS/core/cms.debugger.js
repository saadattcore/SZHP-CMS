CMS.namespace("Debugger");

CMS.Debugger = (function () {
    "use strict";
    return {
        log: function (message) {
            try {
                if (console)
                    console.log(message);
            } catch (exception) {
                return;
            }
        }
    };
}());