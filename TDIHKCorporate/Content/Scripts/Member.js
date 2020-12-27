function SearchMember() {

    var callParams = {

        endPoint: "../MemberShip/Mitgliederliste?search" + document.getElementById("MemberSearchText").value,
        requestType: "GET",
        async: false
    }

    dataParams = {};

    RequestAjax(callParams, null, function (response) {
        debugger

    });

}