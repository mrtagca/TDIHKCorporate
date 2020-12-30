
function GetMembers(dropdownId) {
    
    var callParams = {
        endPoint: "/Management/JobOffer/GetMembers",
        requestType: "POST"
    }

    $("#" + dropdownId).html(""); 

    RequestAjax(callParams, null, function (response) {
        response = JSON.parse(response);

        var dropdown = $("#" + dropdownId);

        $.each(response, function () {

            var option = document.createElement("option");
            option.value = this.MemberID;
            option.innerText = this.MemberTitle;

            dropdown.append(option);

        });

    });
}

function SaveJobOffer()
{
    var callParams = {
        endPoint: "/Management/JobOffer/SaveJobOffer",
        requestType: "POST"
    }

    var jobOffer = new FormData();

    var isMember = document.getElementById("IsMember");

    var memberID = null;
    var logoPath = null;

    var dataParams = {};

    if (isMember.checked === true)
    {
        memberID = document.getElementById("Members");

        logoPath
    }

    dataParams.IsMember = isMember.checked;




}