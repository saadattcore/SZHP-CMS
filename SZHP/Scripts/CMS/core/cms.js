var CMS = CMS || {};

CMS.namespace = function (ns_string) {
    "use strict";
    var parts = ns_string.split('.'),
        parent = CMS,
        i;

    // strip redundant leading global
    if (parts[0] === "CMS") {
        parts = parts.slice(1);
    }

    for (i = 0; i < parts.length; i += 1) {
        // create a property if it doesn't exist
        if (typeof parent[parts[i]] === "undefined") {
            parent[parts[i]] = {};
        }
        parent = parent[parts[i]];
    }
    return parent;
};