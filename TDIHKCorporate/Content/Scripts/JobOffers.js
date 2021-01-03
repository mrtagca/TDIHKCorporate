
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
    var corpName = document.getElementById("CorporationName");
    var language = document.getElementById("JobOfferLanguage");
    var url = document.getElementById("JobOfferURL");

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
    jobOffer.append("CorporationName", corpName.value);
    jobOffer.append("Language", language.value);
    jobOffer.append("JobOfferURL", url.value);

    $.ajax({
        url: '/Management/JobOffer/AddJobOffer',
        type: "POST",
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        data: jobOffer,
        success: function (result) {
            alert("Job offer create successfull!");
            location.reload();
        },
        error: function (err) {
            alert(err.statusText);
        }
    });


}

function PassiveJobOffer(jobOfferID) {

    if (confirm("Are you sure for delete this job offer?")) {
        var callParams = {
            endPoint: "/Management/JobOffer/PassiveJobOffer",
            requestType: "POST"
        }

        var dataParams = {};
        dataParams.jobOfferID = jobOfferID;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Job offer is passive now.");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });
    }


}

function ActivateJobOffer(jobOfferID) {

    if (confirm("Are you sure for activate this job offer?")) {
        var callParams = {
            endPoint: "/Management/JobOffer/ActivateJobOffer",
            requestType: "POST"
        }

        var dataParams = {};
        dataParams.jobOfferID = jobOfferID;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Job offer is active now.");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });
    }


}

function EditJobOffer(jobOfferID) {


    var jobOffer = new FormData();

    var isMember = document.getElementById("IsMember");
    var position = document.getElementById("Position");
    var positionDescription = document.getElementById("PositionDescription");
    var location = document.getElementById("Location");
    var corpName = document.getElementById("CorporationName");
    var language = document.getElementById("JobOfferLanguage");
    var url = document.getElementById("JobOfferURL");

    var memberID = null;
    var logoPath = null;

    if (isMember.checked === true) {
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
    jobOffer.append("CorporationName", corpName.value);
    jobOffer.append("JobOfferID", jobOfferID);
    jobOffer.append("Language", language.value);
    jobOffer.append("JobOfferURL", url.value);

    $.ajax({
        url: '/Management/JobOffer/EditJobOffer',
        type: "POST",
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        data: jobOffer,
        success: function (result) {
            alert("Job offer update successfull!");
            location.reload();
        },
        error: function (err) {
            alert(err.statusText);
        }
    });


}