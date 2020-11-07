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

    var dataParams = {}
    dataParams.PageCategoryID = $("#PageCategories").val();
    dataParams.PageTitle = $("#PageTitle").val();

    var pageContent = document.getElementsByClassName("ql-editor");
    dataParams.PageContent = pageContent[0].innerHTML;

    dataParams.PageSeoLink = "test";
    dataParams.PageSeoKeywords = "keyword1,keyword2";
    dataParams.PageTags = "tag1,tag2";


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