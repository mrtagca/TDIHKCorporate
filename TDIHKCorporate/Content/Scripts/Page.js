function GetPageCategories(dropdownId) {
    var callParams = {
        endPoint: "/Management/Page/GetPageCategories",
        requestType: "GET"
    }

    dataParams = {};
    dataParams.Language = $("#PageLanguage").val();

    RequestAjax(callParams, dataParams, function (response) {
        response = JSON.parse(response);

        $("#" + dropdownId).html("");
        var documentTypeDropdown = $("#" + dropdownId);

        $.each(response, function () {

            var option = document.createElement("option");
            option.value = this.ID;
            option.innerText = this.PageCategoryName;

            documentTypeDropdown.append(option);
        });
    });
}

function AddPage() {

    var callParams = {
        endPoint: "/Management/Page/AddPage",
        requestType: "POST"
    }

    var styleCss = "border:solid #ff0000 1px;";

    var dataParams = {}
    dataParams.Language = $("#PageLanguage").val();
    dataParams.PageTitle = $("#PageTitle").val();

    var editor = $("#editor").data("kendoEditor");
    var pageContent = editor.value();
    var index = pageContent.indexOf("<!--HtmlHeaderEnd|-->");
    pageContent = pageContent.substring(0, index);
    dataParams.PageContent = pageContent;

    dataParams.PageSeoLink = $("#PageSeoLink").val();
    dataParams.PageSeoKeywords = $("#SeoKeywords").val();

    var identifierElement = document.getElementById("IdentifierCheckbox");

    if (identifierElement.checked === true) {
        dataParams.PageIdentifier = $("#PageIdentifierDropdown").val();
    }
    else {
        dataParams.PageIdentifier = $("#PageIdentifierTxt").val();
    }

    var pageImagePath = document.getElementById("PageImage");
    dataParams.PageImagePath = pageImagePath.getAttribute("image-path");


    if (pageContent === '' || typeof (pageContent) === 'undefined' || pageContent === null) {
        alert("Page Content cannot be null!");
        return;
    }

    if (dataParams.PageTitle === '' || typeof (dataParams.PageTitle) === 'undefined' || dataParams.PageTitle === null) {
        alert("Page Title cannot be null!");
        var element = document.getElementById("PageTitle");
        element.style = styleCss;
        return;
    }

    if (dataParams.Language === '' || typeof (dataParams.Language) === 'undefined' || dataParams.Language === null) {
        alert("Please select a language!");
        var element = document.getElementById("PageLanguage");
        element.style = styleCss;
        return;
    }

    if (dataParams.PageIdentifier === '' || typeof (dataParams.PageIdentifier) === 'undefined' || dataParams.PageIdentifier === null) {
        alert("Please select or enter Identifier!");
        return;
    }

    //if (dataParams.PageCategoryID === '' || typeof (dataParams.PageCategoryID) === 'undefined' || dataParams.PageCategoryID === null) {
    //    alert("Please select a page category!");
    //    var element = document.getElementById("PageCategories");
    //    element.style = styleCss;
    //    return;
    //}

    if (dataParams.PageSeoLink === '' || typeof (dataParams.PageSeoLink) === 'undefined' || dataParams.PageSeoLink === null) {
        alert("Seo link cannot be null!");
        var element = document.getElementById("PageSeoLink");
        element.style = styleCss;
        return;
    }

    if (dataParams.PageSeoKeywords === '' || typeof (dataParams.PageSeoKeywords) === 'undefined' || dataParams.PageSeoKeywords === null) {
        alert("Seo keywords cannot be null!");
        var element = document.getElementById("SeoKeywords");
        element.style = styleCss;
        return;
    }

    if (dataParams.PageImagePath === '' || typeof (dataParams.PageImagePath) === 'undefined' || dataParams.PageImagePath === null) {
        alert("Please select a page image!");
        var element = document.getElementById("PageImage");
        element.style = styleCss;
        return;
    }


    RequestAjax(callParams, dataParams, function (response) {

        response = JSON.parse(response);

        if (response === true) {
            alert("Page Create successfull!");
            location.reload();
        }
        else {
            alert("Fail!");
        }
    });


}

function GetPages(dropdownId, languageDropdownId) {

    var callParams = {
        endPoint: "/Management/Page/GetPages",
        requestType: "POST"
    }

    $("#" + dropdownId).html("");

    dataParams = {};
    dataParams.Language = $("#" + languageDropdownId).val();

    RequestAjax(callParams, dataParams, function (response) {
        response = JSON.parse(response);
        debugger
        var dropdown = $("#" + dropdownId);



        $.each(response, function () {

            var option = document.createElement("option");
            option.value = this.PageID;
            option.innerText = this.PageTitle;

            dropdown.append(option);
        });
    });
}

function EditPage(pageID) {

    var callParams = {
        endPoint: "/Management/Page/EditPage",
        requestType: "POST"
    }

    var styleCss = "border:solid #ff0000 1px;";

    var dataParams = {}
    dataParams.PageID = pageID;
    dataParams.Language = $("#PageLanguage").val();
    //dataParams.PageCategoryID = $("#PageCategories").val();
    dataParams.PageTitle = $("#PageTitle").val();

    var editor = $("#editor").data("kendoEditor");
    var pageContent = editor.value();
    var index = pageContent.indexOf("<!--HtmlHeaderEnd|-->");
    pageContent = pageContent.substring(0, index);
    dataParams.PageContent = pageContent;
    dataParams.PageSeoLink = $("#PageSeoLink").val();
    dataParams.PageSeoKeywords = $("#SeoKeywords").val();

    dataParams.PageIdentifier = $("#PageIdentifierDropdown").val();

    var pageImagePath = document.getElementById("PageImage");
    dataParams.PageImagePath = pageImagePath.getAttribute("image-path");

    if (pageContent === '' || typeof (pageContent) === 'undefined' || pageContent === null) {
        alert("Page Content cannot be null!");
        return;
    }

    if (dataParams.PageTitle === '' || typeof (dataParams.PageTitle) === 'undefined' || dataParams.PageTitle === null) {
        alert("Page Title cannot be null!");
        var element = document.getElementById("PageTitle");
        element.style = styleCss;
        return;
    }

    if (dataParams.Language === '' || typeof (dataParams.Language) === 'undefined' || dataParams.Language === null) {
        alert("Please select a language!");
        var element = document.getElementById("PageLanguage");
        element.style = styleCss;
        return;
    }

    if (dataParams.PageIdentifier === '' || typeof (dataParams.PageIdentifier) === 'undefined' || dataParams.PageIdentifier === null) {
        alert("Please select or enter Identifier!");
        return;
    }

    //if (dataParams.PageCategoryID === '' || typeof (dataParams.PageCategoryID) === 'undefined' || dataParams.PageCategoryID === null) {
    //    alert("Please select a page category!");
    //    var element = document.getElementById("PageCategories");
    //    element.style = styleCss;
    //    return;
    //}

    if (dataParams.PageSeoLink === '' || typeof (dataParams.PageSeoLink) === 'undefined' || dataParams.PageSeoLink === null) {
        alert("Seo link cannot be null!");
        var element = document.getElementById("PageSeoLink");
        element.style = styleCss;
        return;
    }

    if (dataParams.PageSeoKeywords === '' || typeof (dataParams.PageSeoKeywords) === 'undefined' || dataParams.PageSeoKeywords === null) {
        alert("Seo keywords cannot be null!");
        var element = document.getElementById("SeoKeywords");
        element.style = styleCss;
        return;
    }

    if (dataParams.PageImagePath === '' || typeof (dataParams.PageImagePath) === 'undefined' || dataParams.PageImagePath === null) {
        alert("Please select a page image!");
        var element = document.getElementById("PageImage");
        element.style = styleCss;
        return;
    }


    RequestAjax(callParams, dataParams, function (response) {
        debugger
        response = JSON.parse(response);

        if (response === true) {
            alert("Page Edit successfull!");
        }
        else {
            alert("Fail!");
        }
    });


}

function GetPageIdentifier(identifierDropdown, lang) {
    var callParams = {
        endPoint: "/Management/Page/GetPageIdentifiers",
        requestType: "POST"
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
            option.innerText = this.PageTitle;

            identifyDropdown.append(option);
        });
    });
}

function PassivePage(pageID) {

    if (confirm("Are you sure for delete this page?")) {
        var callParams = {
            endPoint: "/Management/Page/PassivePage",
            requestType: "POST"
        }

        var dataParams = {};
        dataParams.pageID = pageID;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Page is passive now.");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });
    }


}

function ActivatePage(pageID) {

    if (confirm("Are you sure for activate this page?")) {
        var callParams = {
            endPoint: "/Management/Page/ActivatePage",
            requestType: "POST"
        }

        var dataParams = {};
        dataParams.pageID = pageID;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Page is active now.");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });
    }


}

