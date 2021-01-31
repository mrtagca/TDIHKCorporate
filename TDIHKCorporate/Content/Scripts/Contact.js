function AddContact() {

    var callParams = {
        endPoint: "/AboutUs/AddContact",
        requestType: "POST"
    }


    var privacyPolicyCheck = document.getElementById("PrivacyPolicyCheck");


    if (privacyPolicyCheck.checked === false) {
        alert("Please check privacy policy!");
        return;
    }

    var name = document.getElementById("name");
    var surname = document.getElementById("surname");
    var email = document.getElementById("EmailAdress");
    var message = document.getElementById("Message");

    var dataParams = {}
    dataParams.Name = name.value;
    dataParams.Surname = surname.value;
    dataParams.Message = message.value;
    dataParams.EmailAdress = email.value;

    if (dataParams.Name === '' || typeof (dataParams.Name) === 'undefined' || dataParams.Name === null) {
        alert("Name cannot be null!");
        return;
    }

    if (dataParams.Surname === '' || typeof (dataParams.Surname) === 'undefined' || dataParams.Surname === null) {
        alert("Surname cannot be null!");
        return;
    }

    if (dataParams.EmailAdress === '' || typeof (dataParams.EmailAdress) === 'undefined' || dataParams.EmailAdress === null) {
        alert("EmailAdress cannot be null!");
        return;
    }

    if (dataParams.Message === '' || typeof (dataParams.Message) === 'undefined' || dataParams.Message === null) {
        alert("Message cannot be null!");
        return;
    }

    RequestAjax(callParams, dataParams, function (response) {

        response = JSON.parse(response);

        if (response === true) {
            alert("Your message is sent!");
            location.reload();
        }
        else {
            alert("Fail!");
        }
    });
}