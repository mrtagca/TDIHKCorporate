function GenerateFriendlyUrl(elementId, targetElementId, lang) {

    var callParams = {

        requestType: "POST"
    };

    if (lang === 'tr') {
        callParams.endPoint = "/Management/Url/GenerateFriendlyUrlTurkish";
    }
    else {
        callParams.endPoint = "/Management/Url/GenerateFriendlyUrlGerman";
    }

    var dataParams = {}
    var element = document.getElementById(elementId);
    dataParams.text = element.value;

    RequestAjax(callParams, dataParams, function (response) {

        var target = document.getElementById(targetElementId);
        target.value = response;
    });


}