function GetPageCategories(dropdownId) {
    var callParams = {
        endPoint: "/Management/Page/GetPageCategories",
        requestType: "GET"
    }

    RequestAjax(callParams, null, function (response) {
        response = JSON.parse(response);

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

function EditPage(pageID, pageIdentifier) {

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
    dataParams.PageIdentifier = pageIdentifier;


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


    RequestAjax(callParams, dataParams, function (response) {
        debugger
        response = JSON.parse(response);

        if (response === true) {
            alert("Page Edit successfull!");
            location.reload();
        }
        else {
            alert("Fail!");
        }
    });


}
 
