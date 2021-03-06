﻿function GetNewsCategories(dropdownId) {
    var callParams = {
        endPoint: "/Management/News/GetNewsCategories",
        requestType: "GET"
    }


    dataParams = {};
    dataParams.Language = $("#NewsLanguage").val();

    RequestAjax(callParams, dataParams, function (response) {
        response = JSON.parse(response);

        $("#" + dropdownId).html("");
        var documentTypeDropdown = $("#" + dropdownId);

        $.each(response, function () {

            var option = document.createElement("option");
            option.value = this.ID;
            option.innerText = this.NewsCategoryName;

            documentTypeDropdown.append(option);
        });
    });
}

function AddNews() {

    var callParams = {
        endPoint: "/Management/News/AddNews",
        requestType: "POST"
    }

    var styleCss = "border:solid #ff0000 1px;";

    var dataParams = {}
    dataParams.Language = $("#NewsLanguage").val();
    dataParams.NewsTitle = $("#NewsTitle").val();
    dataParams.NewsCategoryID = $("#NewsCategories").val();

    var editor = $("#editor").data("kendoEditor");
    var pageContent = editor.value();
    var index = pageContent.indexOf("<!--HtmlHeaderEnd|-->");
    pageContent = pageContent.substring(0, index);
    dataParams.NewsContent = pageContent;

    dataParams.NewsSeoLink = $("#NewsSeoLink").val();
    dataParams.NewsSeoKeywords = $("#NewsSeoKeywords").val();
    dataParams.NewsDescription = $("#NewsDescription").val();
    dataParams.CreatedDate = $("#NewsDate").val();
     

    var identifierElement = document.getElementById("IdentifierCheckbox");

    if (identifierElement.checked === true) {
        dataParams.NewsIdentifier = $("#NewsIdentifierDropdown").val();
    }
    else {
        dataParams.NewsIdentifier = null;
    }

    var newsImagePath = document.getElementById("NewsImage");
    dataParams.NewsImagePath = newsImagePath.getAttribute("image-path");
    dataParams.NewsThumbnailPath = newsImagePath.getAttribute("thumbnail-path");

    var isEventNew = document.getElementById("IsEventNew");

    if (isEventNew.checked === true) {
        dataParams.IsEventNew = true;
    }
    else {
        dataParams.IsEventNew = false;
    }


    if (pageContent === '' || typeof (pageContent) === 'undefined' || pageContent === null) {
        alert("News Content cannot be null!");
        return;
    }

    if (dataParams.NewsTitle === '' || typeof (dataParams.NewsTitle) === 'undefined' || dataParams.NewsTitle === null) {
        alert("News Title cannot be null!");
        var element = document.getElementById("NewsTitle");
        element.style = styleCss;
        return;
    }

    if (dataParams.Language === '' || typeof (dataParams.Language) === 'undefined' || dataParams.Language === null) {
        alert("Please select a language!");
        var element = document.getElementById("NewsLanguage");
        element.style = styleCss;
        return;
    }


    if (dataParams.NewsCategoryID === '' || typeof (dataParams.NewsCategoryID) === 'undefined' || dataParams.NewsCategoryID === null) {
        alert("Please select a news category!");
        var element = document.getElementById("NewsCategories");
        element.style = styleCss;
        return;
    }

    if (dataParams.NewsSeoLink === '' || typeof (dataParams.NewsSeoLink) === 'undefined' || dataParams.NewsSeoLink === null) {
        alert("Seo link cannot be null!");
        var element = document.getElementById("NewsSeoLink");
        element.style = styleCss;
        return;
    }

    if (dataParams.NewsSeoKeywords === '' || typeof (dataParams.NewsSeoKeywords) === 'undefined' || dataParams.NewsSeoKeywords === null) {
        alert("Seo keywords cannot be null!");
        var element = document.getElementById("NewsSeoKeywords");
        element.style = styleCss;
        return;
    }

    if (dataParams.NewsImagePath === '' || typeof (dataParams.NewsImagePath) === 'undefined' || dataParams.NewsImagePath === null) {
        alert("Please select a news image!");
        var element = document.getElementById("NewsImage");
        element.style = styleCss;
        return;
    }

    if (dataParams.NewsDescription === '' || typeof (dataParams.NewsDescription) === 'undefined' || dataParams.NewsDescription === null) {
        alert("News description cannot be null!");
        var element = document.getElementById("NewsDescription");
        element.style = styleCss;
        return;
    }

    if (dataParams.CreatedDate === '' || typeof (dataParams.CreatedDate) === 'undefined' || dataParams.CreatedDate === null) {
        alert("News Date cannot be null!");
        var element = document.getElementById("NewsDate");
        element.style = styleCss;
        return;
    }


    RequestAjax(callParams, dataParams, function (response) {

        response = JSON.parse(response);

        if (response === true) {
            alert("News Create successfull!");
            location.reload();
        }
        else {
            alert("Fail!");
        }
    });


}

function GetNews(dropdownId, languageDropdownId) {

    var callParams = {
        endPoint: "/Management/News/GetNews",
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
            option.value = this.NewsID;
            option.innerText = this.NewsTitle;

            dropdown.append(option);
        });
    });
}

function EditNews(newsID, newsIdentifier) {

    var callParams = {
        endPoint: "/Management/News/EditNews",
        requestType: "POST"
    }

    var styleCss = "border:solid #ff0000 1px;";

    var dataParams = {}
    dataParams.NewsID = newsID;
    dataParams.Language = $("#NewsLanguage").val();
    dataParams.NewsCategoryID = $("#NewsCategories").val();
    dataParams.NewsTitle = $("#NewsTitle").val();

    var editor = $("#editor").data("kendoEditor");
    var pageContent = editor.value();
    var index = pageContent.indexOf("<!--HtmlHeaderEnd|-->");
    pageContent = pageContent.substring(0, index);
    dataParams.NewsContent = pageContent;
    dataParams.NewsSeoLink = $("#NewsSeoLink").val();
    dataParams.NewsSeoKeywords = $("#NewsSeoKeywords").val();

    dataParams.CreatedDate = $("#NewsDate").val();

    var newsImagePath = document.getElementById("NewsImage");
    dataParams.NewsImagePath = newsImagePath.getAttribute("image-path");
    dataParams.NewsThumbnailPath = newsImagePath.getAttribute("thumbnail-path");

    dataParams.NewsDescription = $("#NewsDescription").val();

    var isEventNew = document.getElementById("IsEventNew");

    if (isEventNew.checked === true) {
        dataParams.IsEventNew = true;
    }
    else {
        dataParams.IsEventNew = false;
    }


    var identifierElement = document.getElementById("IdentifierCheckbox");

    if (identifierElement.checked === true) {
        dataParams.NewsIDentifier = $("#NewsIdentifierDropdown").val();
    }
    else {
        dataParams.NewsIDentifier = $("#NewsIdentifierTxt").val();
    }

    if (dataParams.NewsIDentifier === '' || typeof (dataParams.NewsIDentifier) === 'undefined' || dataParams.NewsIDentifier === null) {
        dataParams.NewsIDentifier = newsIdentifier;
    }


    if (pageContent === '' || typeof (pageContent) === 'undefined' || pageContent === null) {
        alert("News Content cannot be null!");
        return;
    }

    if (dataParams.NewsTitle === '' || typeof (dataParams.NewsTitle) === 'undefined' || dataParams.NewsTitle === null) {
        alert("News Title cannot be null!");
        var element = document.getElementById("NewsTitle");
        element.style = styleCss;
        return;
    }

    if (dataParams.Language === '' || typeof (dataParams.Language) === 'undefined' || dataParams.Language === null) {
        alert("Please select a language!");
        var element = document.getElementById("NewsLanguage");
        element.style = styleCss;
        return;
    }

    

    if (dataParams.NewsCategoryID === '' || typeof (dataParams.NewsCategoryID) === 'undefined' || dataParams.NewsCategoryID === null) {
        alert("Please select a news category!");
        var element = document.getElementById("NewsCategories");
        element.style = styleCss;
        return;
    }

    if (dataParams.CreatedDate === '' || typeof (dataParams.CreatedDate) === 'undefined' || dataParams.CreatedDate === null) {
        alert("News Date cannot be null!");
        var element = document.getElementById("NewsDate");
        element.style = styleCss;
        return;
    }


    if (dataParams.NewsSeoLink === '' || typeof (dataParams.NewsSeoLink) === 'undefined' || dataParams.NewsSeoLink === null) {
        alert("Seo link cannot be null!");
        var element = document.getElementById("NewsSeoLink");
        element.style = styleCss;
        return;
    }

    if (dataParams.NewsSeoKeywords === '' || typeof (dataParams.NewsSeoKeywords) === 'undefined' || dataParams.NewsSeoKeywords === null) {
        alert("Seo keywords cannot be null!");
        var element = document.getElementById("NewsSeoKeywords");
        element.style = styleCss;
        return;
    }

    if (dataParams.NewsImagePath === '' || typeof (dataParams.NewsImagePath) === 'undefined' || dataParams.NewsImagePath === null) {
        alert("Please select a news image!");
        var element = document.getElementById("NewsImage");
        element.style = styleCss;
        return;
    }

    if (dataParams.NewsDescription === '' || typeof (dataParams.NewsDescription) === 'undefined' || dataParams.NewsDescription === null) {
        alert("News description cannot be null!");
        var element = document.getElementById("NewsDescription");
        element.style = styleCss;
        return;
    }

    RequestAjax(callParams, dataParams, function (response) {
        debugger
        response = JSON.parse(response);

        if (response === true) {
            alert("News Edit successfull!");
            location.reload();
        }
        else {
            alert("Fail!");
        }
    });


}

function GetNewsIdentifier(identifierDropdown, lang) {
    var callParams = {
        endPoint: "/Management/News/GetNewsIdentifiers",
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
            option.innerText = this.NewsTitle;

            identifyDropdown.append(option);
        });
    });
}

function PassiveNews(newsID) {

    if (confirm("Are you sure for delete this news?")) {
        var callParams = {
            endPoint: "/Management/News/PassiveNews",
            requestType: "POST"
        }

        var dataParams = {};
        dataParams.newsID = newsID;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("News is passive now.");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });
    }


}

function ActivateNews(newsID) {

    if (confirm("Are you sure for activate this news?")) {
        var callParams = {
            endPoint: "/Management/News/ActivateNews",
            requestType: "POST"
        }

        var dataParams = {};
        dataParams.newsID = newsID;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("News is active now.");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });
    }


}