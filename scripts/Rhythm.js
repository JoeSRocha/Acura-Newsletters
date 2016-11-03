var Rhythm = {};
var extend = require('util')._extend;

/*  BuildUrl
 *  Combines a base url string with an object containing query parameters, and returns the resulting url as a string.
 *  Example: Rhythm.BuildUrl({ url: "http://google.com", query: [ { test1: "test1" }, { test2: "test2"} ], fragment: "test3" })
 *  Example output: http://google.com?test1=test1&test2=test2#test3
 */
Rhythm.BuildUrl = function (pieces) { // additional parameters handled by parsing arguments array
    if (!pieces || !pieces.url)
        throw "Rhythm.BuildUrl: base url not found";

    var result = pieces.url;

    if (pieces.query) {
        var query = pieces.query;

        if (typeof query === "string")
            result += "?" + query;

        if (Array.isArray(query)) {
            var combinedQuery = {};
            for (var item in query) {
                combinedQuery = extend(combinedQuery, query[item]);
            }
            query = combinedQuery; // fall through to object handling
        }

        if (typeof query === "object") {
            result += "?"
            for (var item in query) {
                result += item + "=" + query[item] + "&"
            }
            result = result.substring(0, result.length - 1);
        }
    }

    if (pieces.fragment)
        result += "#" + pieces.fragment;

    return result;
}

/*  GetStyleObject
 *  Converts a style string into an object. Exits immediately if source parameter is already an object.
 */
Rhythm.GetStyleObject = function (source) {
    if (typeof source === 'object') return source;

    var items = source.split(";");
    var result = { };
    for (var item in items) {
        var itemPieces = items[item].split(":");
        if (itemPieces.length < 2) continue;
        result[itemPieces[0].trim()] = itemPieces[1].trim();
    }
    return result;
}

/*  ImplyStyles
 *  Checks a tag parameters object to make sure that the styles sub-object exists and is an object. Also populates alternate style properties.
 */
Rhythm.ImplyStyles = function (params) {
    params.style = params.style ? Rhythm.GetStyleObject(params.style) : {}

    if (params.width)
        params.style.width = params.width + (/^\d+$/.test(params.width) ? "px" : "");
        
    if (params.align)
        params.style["text-align"] = params.align;

    if (params.valign)
        params.style["vertical-align"] = params.valign;

    return params;
}

/*  ExtendStyle
 *  Extends a style object with one or more others. Calls GetStyleObject first to handle string inputs.
 */
Rhythm.ExtendStyle = function (base) { // additional parameters handled by parsing arguments array
    var result = extend({}, Rhythm.GetStyleObject(base ? base : {}));
    for (var i = 1; i < arguments.length; i++) {
        var current = Rhythm.GetStyleObject(arguments[i] ? arguments[i] : {});
        result = extend(result, current);
    }
    return result;
}

/*  ExtendAttributes
 *  Extends a tag parameters object with one or more others. Also calls ImplyStyles on each parameter before combining them, and merges the resulting styles objects.
 */
Rhythm.ExtendAttributes = function (base) { // additional parameters handled by parsing arguments array
    var result = Rhythm.ImplyStyles(extend({}, base ? base : {})); // extend to clone, and imply styles for basic setup
    for (var i = 1; i < arguments.length; i++) {
        var current = arguments[i] ? arguments[i] : {}; // process this right now to avoid null problems
        var combinedStyles = Rhythm.ExtendStyle({}, result.style, current.style); // extend doesn't do deep cloning; we need to get a merged version of the styles before we merge everything else
        result = extend(result, current);
        result.style = combinedStyles; // now drop in the extended styles
        result = Rhythm.ImplyStyles(result); // ... and imply styles on top of that pile
    }
    return result;
}

module.exports = Rhythm;