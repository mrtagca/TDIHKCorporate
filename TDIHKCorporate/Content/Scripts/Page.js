function GetPageCategories(dropdownId) {
    var callParams = {
        endPoint: "../Page/GetPageCategories",
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
        endPoint: "../Page/AddPage",
        requestType: "POST"
    }

    var styleCss = "border:solid #ff0000 1px;";

    var dataParams = {}
    dataParams.Language = $("#PageLanguage").val();
    dataParams.PageCategoryID = $("#PageCategories").val();
    dataParams.PageTitle = $("#PageTitle").val();

    debugger

    var pageContent = document.getElementsByClassName("ql-editor");
    dataParams.PageContent = pageContent[0].innerHTML;


    dataParams.PageSeoLink = $("#PageSeoLink").val();
    dataParams.PageSeoKeywords = $("#SeoKeywords").val();


    if (pageContent[0].innerText === '' || typeof (pageContent[0].innerText) === 'undefined' || pageContent[0].innerText === null) {
        alert("Page Content cannot be null!");
        var element = document.getElementById("editor");
        element.style = "background-color:#ffffff;" + styleCss;
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

    if (dataParams.PageCategoryID === '' || typeof (dataParams.PageCategoryID) === 'undefined' || dataParams.PageCategoryID === null) {
        alert("Please select a page category!");
        var element = document.getElementById("PageCategories");
        element.style = styleCss;
        return;
    }

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

        if (response.success === true) {
            alert(response.message);
            location.reload();
        }
        else {
            alert(response.message);
        }
    });


}