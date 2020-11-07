function GenerateFriendlyUrl(elementId,targetElementId) {

        var callParams = {
            endPoint: "../Url/GenerateFriendlyUrl",
            requestType: "POST"
    }


    var dataParams = {}
    var element = document.getElementById(elementId);
    dataParams.text = element.value;

        RequestAjax(callParams, dataParams, function (response) {

            debugger

            var target = document.getElementById(targetElementId);
            target.value = response;
        });
    

}