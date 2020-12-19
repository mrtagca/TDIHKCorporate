function EditInstitution(institutionId) {

    var callParams = {
        endPoint: "/Management/Institution/Edit",
        requestType: "POST"
    };


    var dataParams = {};
    dataParams.Language = $("#Language").val();
    dataParams.InstitutionTitle = $("#InstitutionTitle").val();
    dataParams.InstitutionDescription = $("#InstitutionDescription").val();
    dataParams.InstitutionDate = $("#InstitutionDate").val();
    dataParams.ID = institutionId;


    RequestAjax(callParams, dataParams, function (response) {
        debugger
        response = JSON.parse(response);

        if (response === true) {
            alert("Institution Edit successfull!");
            location.reload();
        }
        else {
            alert("Fail!");
        }
    });


}

function DeleteInstitution(institutionId) {

    if (confirm("Are you sure delete this institution?")) {
        var callParams = {
            endPoint: "/Management/Institution/Delete",
            requestType: "POST"
        };

        var dataParams = {};
        dataParams.ID = institutionId;


        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Institution delete successfull!");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });
    }


}

function ActivateInstitution(institutionId) {

    if (confirm("Are you sure active this institution?")) {
        var callParams = {
            endPoint: "/Management/Institution/Activate",
            requestType: "POST"
        };

        var dataParams = {};
        dataParams.ID = institutionId;


        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Institution activate successfull!");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });

    }

}