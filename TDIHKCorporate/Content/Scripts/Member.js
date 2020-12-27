

function PassiveMember(memberID) {

    if (confirm("Are you sure for delete this member?")) {
        var callParams = {
            endPoint: "/Management/MemberShip/PassiveMember",
            requestType: "POST"
        }

        var dataParams = {};
        dataParams.memberID = memberID;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Member is passive now.");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });
    }


}

function ActivateMember(memberID) {

    if (confirm("Are you sure for activate this member?")) {
        var callParams = {
            endPoint: "/Management/MemberShip/ActivateMember",
            requestType: "POST"
        }

        var dataParams = {};
        dataParams.memberID = memberID;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Member is active now.");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });
    }


}