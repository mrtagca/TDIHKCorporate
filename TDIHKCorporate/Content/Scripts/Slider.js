function GetSliderList(dropdownId, widthHeightElement) {
    var callParams = {
        endPoint: "../Slider/GetSliderList",
        requestType: "GET"
    }

    RequestAjax(callParams, null, function (response) {
        response = JSON.parse(response);

        var documentTypeDropdown = $("#" + dropdownId);

        $.each(response, function () {

            var option = document.createElement("option");
            option.value = this.SliderID;
            option.innerText = '['+this.Language+'] '+this.SliderName + ' (' + this.ImageWidth + 'x' + this.ImageHeight + ')';
            documentTypeDropdown.append(option);
        });
    });
}
