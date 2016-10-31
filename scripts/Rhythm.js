var Rhythm = {};
var extend = require('util')._extend;

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

Rhythm.MergeAttributes = function (base, extension) {
    base = base ? base : {};
    extension = extension ? extension : {};
    if (base.style && extension.style) {
        base.style = Rhythm.GetStyleObject(base.style);
        extension.style = Rhythm.GetStyleObject(extension.style);
        extension.style = extend(base.style, extension.style);
    }
    return extend(base, extension);
}

Rhythm.AppendStyle = function (base, extension) {
    base = Rhythm.GetStyleObject(base ? base : {});
    extension = Rhythm.GetStyleObject(extension ? extension : {});
    return extend(base, extension);
}

module.exports = Rhythm;
