function GetEventCategories(dropdownId,language) {
    var callParams = {
        endPoint: "/Management/Event/GetEventCategories",
        requestType: "GET",
        async: true
    }

    dataParams = {};
    dataParams.language = language;

    RequestAjax(callParams, dataParams, function (response) {
        response = JSON.parse(response);

        $("#" + dropdownId).html("");

        var documentTypeDropdown = $("#" + dropdownId);

        $.each(response, function () {

            var option = document.createElement("option");
            option.value = this.ID;
            option.innerText = this.EventCategoryName;

            documentTypeDropdown.append(option);
        });
    });
}

function AddEvent() {

    var callParams = {
        endPoint: "/Management/Event/AddEvent",
        requestType: "POST"
    }

    var styleCss = "border:solid #ff0000 1px;";

    var dataParams = {}
    dataParams.Language = $("#EventLanguage").val();
    dataParams.EventCategoryID = $("#EventCategories").val();
    dataParams.EventTitle = $("#EventTitle").val();

    var editor = $("#editor").data("kendoEditor");
    var eventContent = editor.value();
    var index = eventContent.indexOf("<!--HtmlHeaderEnd|-->");
    eventContent = eventContent.substring(0, index);
    dataParams.EventContent = eventContent;
     

    dataParams.EventSeoLink = $("#EventSeoLink").val();
    dataParams.EventDescription = $("#EventDescription").val();
    dataParams.EventSeoKeywords = $("#SeoKeywords").val();
    dataParams.EventDate = $("#EventDate").val();
    dataParams.EventTime = $("#EventTime").val();
    dataParams.EventQuota = $("#EventQuota").val();
    dataParams.EventCriticalQuota = $("#EventCriticalQuota").val();
    dataParams.EventTags = $("#SeoTags").val();

    var identifierElement = document.getElementById("IdentifierCheckbox");

    if (identifierElement.checked === true) {
        dataParams.EventIdentifier = $("#EventIdentifierDropdown").val();
    }
    else {
        dataParams.EventIdentifier = null;
    }

    var eventImagePath = document.getElementById("EventImage");
    dataParams.EventImagePath = eventImagePath.getAttribute("image-path");


    if (eventContent === '' || typeof (eventContent) === 'undefined' || eventContent === null) {
        alert("Event Content cannot be null!");
        return;
    }

    if (dataParams.EventTitle === '' || typeof (dataParams.EventTitle) === 'undefined' || dataParams.EventTitle === null) {
        alert("Event Title cannot be null!");
        var element = document.getElementById("EventTitle");
        element.style = styleCss;
        return;
    }

    if (dataParams.Language === '' || typeof (dataParams.Language) === 'undefined' || dataParams.Language === null) {
        alert("Please select a language!");
        var element = document.getElementById("EventLanguage");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventCategoryID === '' || typeof (dataParams.EventCategoryID) === 'undefined' || dataParams.EventCategoryID === null) {
        alert("Please select a page category!");
        var element = document.getElementById("EventCategories");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventSeoLink === '' || typeof (dataParams.EventSeoLink) === 'undefined' || dataParams.EventSeoLink === null) {
        alert("Seo link cannot be null!");
        var element = document.getElementById("EventSeoLink");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventDescription === '' || typeof (dataParams.EventDescription) === 'undefined' || dataParams.EventDescription === null) {
        alert("Event Description cannot be null!");
        var element = document.getElementById("EventDescription");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventSeoKeywords === '' || typeof (dataParams.EventSeoKeywords) === 'undefined' || dataParams.EventSeoKeywords === null) {
        alert("Seo keywords cannot be null!");
        var element = document.getElementById("SeoKeywords");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventTags === '' || typeof (dataParams.EventTags) === 'undefined' || dataParams.EventTags === null) {
        alert("Seo tags cannot be null!");
        var element = document.getElementById("SeoTags");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventDate === '' || typeof (dataParams.EventDate) === 'undefined' || dataParams.EventDate === null) {
        alert("Event Date cannot be null!");
        var element = document.getElementById("EventDate");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventTime === '' || typeof (dataParams.EventTime) === 'undefined' || dataParams.EventTime === null) {
        alert("Event Time cannot be null!");
        var element = document.getElementById("EventDate");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventQuota === '' || typeof (dataParams.EventQuota) === 'undefined' || dataParams.EventQuota === null) {
        alert("Event Quota cannot be null!");
        var element = document.getElementById("EventQuota");
        element.style = styleCss;
    }
    else {
        if (dataParams.EventQuota < 1) {
            alert("Event Quota value cannot be smaller than 1");
            var element = document.getElementById("EventQuota");
            element.style = styleCss;
            return;
        }
    }

    if (dataParams.EventCriticalQuota === '' || typeof (dataParams.EventCriticalQuota) === 'undefined' || dataParams.EventCriticalQuota === null) {
        alert("Critical Quota cannot be null!");
        var element = document.getElementById("EventQuota");
        element.style = styleCss;
    }
    else {
        if (dataParams.EventCriticalQuota < 1) {
            alert("Critical Quota value cannot be smaller than 1");
            var element = document.getElementById("EventCriticalQuota");
            element.style = styleCss;
            return;
        }
    }

    if (dataParams.EventImagePath === '' || typeof (dataParams.EventImagePath) === 'undefined' || dataParams.EventImagePath === null) {
        alert("Please select a page image!");
        var element = document.getElementById("EventImage");
        element.style = styleCss;
        return;
    }


    RequestAjax(callParams, dataParams, function (response) {

        debugger

        response = JSON.parse(response);

        if (response === true) {
            alert("Event Create successfull!");
            location.reload();
        }
        else {
            alert("Fail!");
        }
    });


}

function EditEvent(eventID) {

    var callParams = {
        endPoint: "/Management/Event/EditEvent",
        requestType: "POST"
    }

    var styleCss = "border:solid #ff0000 1px;";

    var dataParams = {}
    dataParams.EventID = eventID;
    dataParams.Language = $("#EventLanguage").val();
    dataParams.EventCategoryID = $("#EventCategories").val();
    dataParams.EventTitle = $("#EventTitle").val();

    var editor = $("#editor").data("kendoEditor");
    var eventContent = editor.value();
    var index = eventContent.indexOf("<!--HtmlHeaderEnd|-->");
    eventContent = eventContent.substring(0, index);
    dataParams.EventContent = eventContent;

    dataParams.EventSeoLink = $("#EventSeoLink").val();
    dataParams.EventDescription = $("#EventDescription").val();
    dataParams.EventSeoKeywords = $("#SeoKeywords").val();
    dataParams.EventIdentifier = $("#EventIdentifierDropdown").val();
    dataParams.EventDate = $("#EventDate").val();
    dataParams.EventTime = $("#EventTime").val();
    dataParams.EventQuota = $("#EventQuota").val();
    dataParams.EventCriticalQuota = $("#EventCriticalQuota").val();
    dataParams.EventTags = $("#SeoTags").val();

    var eventImagePath = document.getElementById("EventImage");
    dataParams.EventImagePath = eventImagePath.getAttribute("image-path");


    if (eventContent === '' || typeof (eventContent) === 'undefined' || eventContent === null) {
        alert("Event Content cannot be null!");
        return;
    }

    if (dataParams.EventTitle === '' || typeof (dataParams.EventTitle) === 'undefined' || dataParams.EventTitle === null) {
        alert("Event Title cannot be null!");
        var element = document.getElementById("EventTitle");
        element.style = styleCss;
        return;
    }

    if (dataParams.Language === '' || typeof (dataParams.Language) === 'undefined' || dataParams.Language === null) {
        alert("Please select a language!");
        var element = document.getElementById("EventLanguage");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventCategoryID === '' || typeof (dataParams.EventCategoryID) === 'undefined' || dataParams.EventCategoryID === null) {
        alert("Please select a page category!");
        var element = document.getElementById("EventCategories");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventSeoLink === '' || typeof (dataParams.EventSeoLink) === 'undefined' || dataParams.EventSeoLink === null) {
        alert("Seo link cannot be null!");
        var element = document.getElementById("EventSeoLink");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventDescription === '' || typeof (dataParams.EventDescription) === 'undefined' || dataParams.EventDescription === null) {
        alert("Event Description cannot be null!");
        var element = document.getElementById("EventDescription");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventSeoKeywords === '' || typeof (dataParams.EventSeoKeywords) === 'undefined' || dataParams.EventSeoKeywords === null) {
        alert("Seo keywords cannot be null!");
        var element = document.getElementById("SeoKeywords");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventTags === '' || typeof (dataParams.EventTags) === 'undefined' || dataParams.EventTags === null) {
        alert("Seo tags cannot be null!");
        var element = document.getElementById("SeoTags");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventDate === '' || typeof (dataParams.EventDate) === 'undefined' || dataParams.EventDate === null) {
        alert("Event Date cannot be null!");
        var element = document.getElementById("EventDate");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventTime === '' || typeof (dataParams.EventTime) === 'undefined' || dataParams.EventTime === null) {
        alert("Event Time cannot be null!");
        var element = document.getElementById("EventDate");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventQuota === '' || typeof (dataParams.EventQuota) === 'undefined' || dataParams.EventQuota === null) {
        alert("Event Quota cannot be null!");
        var element = document.getElementById("EventQuota");
        element.style = styleCss;
    }
    else {
        if (dataParams.EventQuota < 1) {
            alert("Event Quota value cannot be smaller than 1");
            var element = document.getElementById("EventQuota");
            element.style = styleCss;
            return;
        }
    }

    if (dataParams.EventCriticalQuota === '' || typeof (dataParams.EventCriticalQuota) === 'undefined' || dataParams.EventCriticalQuota === null) {
        alert("Critical Quota cannot be null!");
        var element = document.getElementById("EventQuota");
        element.style = styleCss;
    }
    else {
        if (dataParams.EventCriticalQuota < 1) {
            alert("Critical Quota value cannot be smaller than 1");
            var element = document.getElementById("EventCriticalQuota");
            element.style = styleCss;
            return;
        }
    }

    if (dataParams.EventImagePath === '' || typeof (dataParams.EventImagePath) === 'undefined' || dataParams.EventImagePath === null) {
        alert("Please select a page image!");
        var element = document.getElementById("EventImage");
        element.style = styleCss;
        return;
    }

    if (dataParams.EventIdentifier === '' || typeof (dataParams.EventIdentifier) === 'undefined' || dataParams.EventIdentifier === null) {
        alert("Please select a event identifier!");
        var element = document.getElementById("EventIdentifier");
        element.style = styleCss;
        return;
    }


    RequestAjax(callParams, dataParams, function (response) {

        debugger

        response = JSON.parse(response);

        if (response === true) {
            alert("Event Edit successfull!");
        }
        else {
            alert("Fail!");
        }
    });


}

function GetEventIdentifier(identifierDropdown, lang) {
    var callParams = {
        endPoint: "/Management/Event/GetEventIdentifiers",
        requestType: "POST",
        async: true
    }

    dataParams = {};
    dataParams.lang = lang;

    RequestAjax(callParams, dataParams, function (response) {
        response = JSON.parse(response);

        $("#" + identifierDropdown).html("");

        var identifyDropdown = $("#" + identifierDropdown);

        $.each(response, function () {

            var option = document.createElement("option");
            option.value = this.IdentifierName;
            option.innerText = this.EventTitle;

            identifyDropdown.append(option);
        });
    });
}
