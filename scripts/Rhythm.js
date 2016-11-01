var Rhythm = {};
var extend = require('util')._extend;

/*  BuildUrl
 *  Combines a base url string with an object containing query parameters, and returns the resulting url as a string.
 */
Rhythm.BuildUrl = function (base, query) {
    var result = base;
    if (query) {
        result += "?"
        for (var item in query) {
            result += item + "=" + query[item] + "&"
        }
        result = result.substring(0, result.length - 1);
    }
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

    return params;
}

/*  ExtendStyle
 *  Extends a style object with another one. Calls GetStyleObject first to handle string inputs.
 */
Rhythm.ExtendStyle = function (base, extension) {
    base = Rhythm.GetStyleObject(base ? base : {});
    extension = Rhythm.GetStyleObject(extension ? extension : {});
    return extend(base, extension);
}

/*  ExtendAttributes
 *  Extends a tag parameters object with another one. Also calls ImplyStyles on each parameter before combining them, and merges the resulting styles objects.
 */
Rhythm.ExtendAttributes = function (base, extension) {
    base = Rhythm.ImplyStyles(base ? base : {});
    extension = Rhythm.ImplyStyles(extension ? extension : {});

    extension.style = Rhythm.ExtendStyle(base.style, extension.style);
    var result = extend(base, extension);

    return result;
}

module.exports = Rhythm;