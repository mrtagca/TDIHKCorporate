
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

    debugger

    var jobOffer = new FormData();

    var isMember = document.getElementById("IsMember");
    var position = document.getElementById("Position");
    var positionDescription = document.getElementById("PositionDescription");
    var location = document.getElementById("Location");

    var memberID = null;
    var logoPath = null;

    if (isMember.checked === true)
    {
        memberID = document.getElementById("Members").value;
        jobOffer.append("MemberID", memberID);
    }
    else {

        var fileUpload = $("#JobOfferLogo").get(0);
        var files = fileUpload.files;

        for (var i = 0; i < files.length; i++) {
            jobOffer.append("file", files[i]);
        }
    }

    jobOffer.append("IsMember", isMember.checked);
    jobOffer.append("Position", position.value);
    jobOffer.append("PositionDescription", positionDescription.value);
    jobOffer.append("Location", location.value);

    $.ajax({
        url: '/Management/JobOffer/AddJobOffer',
        type: "POST",
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        data: jobOffer,
        success: function (result) {

        },
        error: function (err) {
            alert(err.statusText);
        }
    });


}