
CMS.namespace("UI.Common");

CMS.UI.Common = (function () {
    "use strict";

    var callbacks = {

    };

    return {

        initialisePage: function () {

        }
        , registerCallbacks: function (cbacks) {
            callbacks = $.extend(callbacks, cbacks);
            return this;
        }
    };
}());

