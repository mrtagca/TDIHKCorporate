function SaveMember() {

    var digitalSignature = document.getElementById("DigitalSignature");
    var privacyPolicy = document.getElementById("PrivacyPolicy");

    if (digitalSignature.checked === false) {
        alert("You must be check Digital Signature!");
        digitalSignature.setAttribute("style", "border:solid 1px red");
        return;
    }

    if (privacyPolicy.checked === false) {
        alert("You must be check Privacy Policy!");
        privacyPolicy.setAttribute("style", "border:solid 1px red");
        return;
    }


    if (digitalSignature.checked === true && privacyPolicy.checked === true) {
        dataParams = {};
        dataParams.MemberType = "StandardMitgliedschaft";


        var isCorporate = document.getElementById("IsCorporate");

        var isPrivate = document.getElementById("IsPrivate");

        debugger

        if (isCorporate.checked === true)
        {
            dataParams.IsCorporate = true; 
        }
        else if (isPrivate.checked === true)
        {
            dataParams.IsCorporate = false; 
        }
        else {
            alert("Please select member type!");
            var memberType = document.getElementById("MemberTypeArea");
            memberType.setAttribute("style", "border:solid 1px red");

            return;
        }

        var name = document.getElementById("Name");

        if (name.value === null || name.value === '' || typeof (name.value) === 'undefined') {

            alert(name.getAttribute("placeholder") + " cannot be null!");
            return;
        }
        else {
            dataParams.Name = name.value;
        }

        var street = document.getElementById("Street");

        if (street.value === null || street.value === '' || typeof (street.value) === 'undefined') {
            alert(street.getAttribute("placeholder") + " cannot be null!");
            return;
        }
        else {
            dataParams.Street = street.value;
        }

        var homePhone = document.getElementById("HomePhone");

        if (homePhone.value === null || homePhone.value === '' || typeof (homePhone.value) === 'undefined') {
            alert(homePhone.getAttribute("placeholder") + " cannot be null!");
            return;
        }
        else {
            dataParams.HomePhone = homePhone.value;
        }


        var postalCode = document.getElementById("PostalCode");

        if (postalCode.value === null || postalCode.value === '' || typeof (postalCode.value) === 'undefined') {
            alert(postalCode.getAttribute("placeholder") + " cannot be null!");
            return;
        }
        else {
            dataParams.PostalCode = postalCode.value;
        }

        var city = document.getElementById("City");

        if (city.value === null || city.value === '' || typeof (city.value) === 'undefined') {
            alert(city.getAttribute("placeholder") + " cannot be null!");
            return;
        }
        else {
            dataParams.City = city.value;
        }

        var email = document.getElementById("Email");

        if (email.value === null || email.value === '' || typeof (email.value) === 'undefined') {
            alert(email.getAttribute("placeholder") + " cannot be null!");
            return;
        }
        else {
            dataParams.Email = email.value;
        }

        var phoneNumber = document.getElementById("PhoneNumber");

        if (phoneNumber.value === null || phoneNumber.value === '' || typeof (phoneNumber.value) === 'undefined') {
            alert(phoneNumber.getAttribute("placeholder") + " cannot be null!");
            return;
        }
        else {
            dataParams.PhoneNumber = phoneNumber.value;
        }

        var webSite = document.getElementById("WebSite");

        if (webSite.value === null || webSite.value === '' || typeof (webSite.value) === 'undefined') {
            alert(webSite.getAttribute("placeholder") + " cannot be null!");
            return;
        }
        else {
            dataParams.WebSite = webSite.value;
        }

        var fax = document.getElementById("Fax");

        if (fax.value === null || fax.value === '' || typeof (fax.value) === 'undefined') {
            alert(fax.getAttribute("placeholder") + " cannot be null!");
            return;
        }
        else {
            dataParams.Fax = fax.value;
        }

        var responsiblePersonel = document.getElementById("ResponsiblePersonel");

        if (responsiblePersonel.value === null || responsiblePersonel.value === '' || typeof (responsiblePersonel.value) === 'undefined') {
            alert(responsiblePersonel.getAttribute("placeholder") + " cannot be null!");
            return;
        }
        else {
            dataParams.ResponsiblePersonel = responsiblePersonel.value;
        }

        var personelPosition = document.getElementById("PersonelPosition");

        if (personelPosition.value === null || personelPosition.value === '' || typeof (personelPosition.value) === 'undefined') {
            alert(personelPosition.getAttribute("placeholder") + " cannot be null!");
            return;
        }
        else {
            dataParams.PersonelPosition = personelPosition.value;
        }

        debugger

        var memberPacket1 = document.getElementById("MemberPacket1");
        var memberPacket2 = document.getElementById("MemberPacket2");
        var memberPacket3 = document.getElementById("MemberPacket3");

        if (memberPacket1.checked === true) {

            dataParams.CorporationIncome = memberPacket1.value;
        }
        else if (memberPacket2.checked === true) {
            dataParams.CorporationIncome = memberPacket2.value;
        }
        else if (memberPacket3.checked === true) {
            dataParams.CorporationIncome = memberPacket3.value;
        }
        else {
            alert("Please select member packet type!");
            var memberType = document.getElementById("MemberPacketArea");
            memberType.setAttribute("style", "border:solid 1px red");

            return;
        }

        dataParams.CorporationLogoPath = document.getElementById("FileUpload").getAttribute("image-path");

        dataParams.MemberSuggestion1 = document.getElementById("MemberSuggestion1").value;
        dataParams.MemberSuggestion2 = document.getElementById("MemberSuggestion2").value;
        dataParams.MemberSuggestion3 = document.getElementById("MemberSuggestion3").value;
        dataParams.SuggestionInfo = document.getElementById("SuggestionInfo").value;
        dataParams.SuggestionLocationAndTime = document.getElementById("LocationAndTime").value;

        var callParams = {
            endPoint: "/MemberShip/StandardMitgliedschaftAntragsformular",
            requestType: "POST"
        }

        RequestAjax(callParams, dataParams, function (response) {
            response = JSON.parse(response);

            if (response === true) {

                alert("Success!");
                location.reload();
            }
            else {
                alert("Fail");
            }

        });
    }
}

