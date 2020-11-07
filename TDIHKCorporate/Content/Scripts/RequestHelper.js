function RequestAjax(callParams, dataParams, callback) {

    $.ajax({
        url: callParams.endPoint,
        type: callParams.requestType,
        contentType: callParams.contentType,
        dataType: callParams.dataType,
        cache: callParams.cache,
        async: callParams.async,
        data: dataParams,
        success: function (data) {

            callback(data);

        },
        error: function (error) {

            alert(error.responseText);
            return;

        }
    })
        .done(function (data) {
        })
        .fail(function (error) {
            alert(error.responseText);
        });

}
