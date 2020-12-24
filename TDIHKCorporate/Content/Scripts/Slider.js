function GetSliderList(dropdownId, widthHeightElement) {
    var callParams = {
        endPoint: "/Management/Slider/GetSliderList",
        requestType: "GET"
    }

    RequestAjax(callParams, null, function (response) {
        response = JSON.parse(response);

        var documentTypeDropdown = $("#" + dropdownId);

        $.each(response, function () {

            var option = document.createElement("option");
            option.value = this.SliderID;
            option.innerText = '[' + this.Language + '] ' + this.SliderName + ' (' + this.ImageWidth + 'x' + this.ImageHeight + ')';
            documentTypeDropdown.append(option);
        });
    });
}


//function GetSliderItemsById(sliderId, tableBodyId, tableId) {
//    var callParams = {
//        endPoint: "/Management/Slider/GetSliderItems",
//        requestType: "POST",
//        cache: false
//    }

//    $("#" + tableBodyId).html("");

//    var dataParams = {}
//    dataParams.sliderID = sliderId;

//    RequestAjax(callParams, dataParams, function (response) {
//        response = JSON.parse(response);

//        var tableBody = $("#" + tableBodyId);

//        $.each(response, function () {

//            var row = document.createElement("tr");
//            row.setAttribute("role", "row");

//            var sliderContentId = document.createElement("td");
//            sliderContentId.innerText = this.SliderContentID;

//            var sliderContentTitle = document.createElement("td");
//            sliderContentTitle.innerText = this.SliderContentTitle;

//            var sliderContentLanguage = document.createElement("td");
//            sliderContentLanguage.innerText = this.Language;

//            var sliderContentPriority = document.createElement("td");
//            sliderContentPriority.innerText = this.SliderPriority;

//            var sliderContentURL = document.createElement("td");
//            sliderContentURL.innerText = this.SliderUrl;

//            var isActive = document.createElement("td");

//            var edit = document.createElement("a");
//            edit.setAttribute("class", "btn btn-primary");
//            edit.setAttribute("href", "/Management/Slider/EditSliderItem/" + this.SliderContentID);
//            var editIcon = document.createElement("i");
//            editIcon.setAttribute("class", "fa fa-edit");
//            editIcon.setAttribute("aria-hidden", "true");
//            edit.append(editIcon)

//            var actions = document.createElement("button");
//            var actionIcon = document.createElement("i");


//            if (this.IsActive) {
//                actions.setAttribute("class", "btn btn-danger");
//                actions.setAttribute("value", "Passive");
//                actions.setAttribute("onclick", "PassiveSliderItem('" + this.SliderContentID + "','" + sliderId + "','" + tableBodyId + "','" + tableId + "')");
//                actionIcon.setAttribute("class", "fa fa-trash");
//                actionIcon.setAttribute("aria-hidden", "true");
//                actions.append(actionIcon);
//            }
//            else {
//                actions.setAttribute("class", "btn btn-success");
//                actions.setAttribute("value", "Activate");
//                actions.setAttribute("onclick", "ActivateSliderItem('" + this.SliderContentID + "','" + sliderId + "','" + tableBodyId + "','" + tableId + "')");
//                actionIcon.setAttribute("class", "fa fa-check");
//                actionIcon.setAttribute("aria-hidden", "true");
//                actions.append(actionIcon);
//            }

//            isActive.append(edit);
//            isActive.append(actions);



//            row.append(sliderContentId, sliderContentTitle, sliderContentLanguage, sliderContentPriority, sliderContentURL, isActive)
//            tableBody.append(row);

//        });


//        datatable = $('#' + tableId).DataTable({

//            "oLanguage": {
//                "oPaginate": { "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>', "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>' },
//                "sInfo": "Showing page _PAGE_ of _PAGES_",
//                "sSearch": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
//                "sSearchPlaceholder": "Search...",
//                "sLengthMenu": "Results :  _MENU_",
//            },
//            "stripeClasses": [],
//            "lengthMenu": [7, 10, 20, 50],
//            "pageLength": 7,
//            "destroy": true,
//            "cache": true

//        });

//    });
//}

function PassiveSliderItem(sliderItemId) {

    if (confirm("Are you sure sliderItem Passive?")) {
        var callParams = {
            endPoint: "/Management/Slider/PassiveSliderItem",
            requestType: "POST"
        }

        dataParams = {};
        dataParams.sliderItemId = sliderItemId;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Slider item is passive now.")

                location.reload();
            }
            else {
                alert("Fail!");

            }


        });
    }
}

function ActivateSliderItem(sliderItemId) {

    if (confirm("Are you sure sliderItem activate?")) {
        var callParams = {
            endPoint: "/Management/Slider/ActivateSliderItem",
            requestType: "POST"
        }

        dataParams = {};
        dataParams.sliderItemId = sliderItemId;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Slider item is active now.")
                location.reload();
            }
            else {
                alert("Fail!");

            }


        });
    }
}