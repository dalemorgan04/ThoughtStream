Core = {};
Core.ResolveUrl = _ResolveUrl;
Core.SynchronousCallback = _synchronousCallback;

function _ResolveUrl(action, controller, param) {
    var url = "Javascript/ResolveUrl?action=" + action + "&controller=" + controller;
    if (param != null) {
        for (var propertyName in param)
            url += "&" + propertyName + "=" + param[propertyName];
    }
    return Core.SynchronousCallback(url);
}

function _synchronousCallback(url) {
    return $.ajax({
        type: "GET",
        url: url,
        cahce: false,
        async: false
    }).responseText;
}