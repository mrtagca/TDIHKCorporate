
function GetMenuItemsForMenuList(menuId, tableBodyId, tableId) {
    var callParams = {
        endPoint: "../Menu/GetMenuItems",
        requestType: "POST",
        cache: false
    }

    $("#" + tableBodyId).html("");

    var dataParams = {}
    dataParams.menuID = menuId;

    RequestAjax(callParams, dataParams, function (response) {
        response = JSON.parse(response);

        var tableBody = $("#" + tableBodyId);

        $.each(response, function () {

            var row = document.createElement("tr");
            row.setAttribute("role", "row");

            var pageTitle = document.createElement("td");
            pageTitle.innerText = this.PageTitle;

            var language = document.createElement("td");
            language.innerText = this.Language;

            var menuItemPriority = document.createElement("td");
            menuItemPriority.innerText = this.MenuItemPriority;

            var menuName = document.createElement("td");
            menuName.innerText = this.MenuName;

            var menuLevel = document.createElement("td");
            menuLevel.innerText = this.MenuLevel;

            var isSubMenu = document.createElement("td");

            if (this.IsSubMenu) {
                isSubMenu.innerText = "Sub Menu Item";
            }
            else {
                isSubMenu.innerText = "Main Menu Item";
            }

            var isActive = document.createElement("td");

            var actions = document.createElement("button");
            var actionIcon = document.createElement("i")
            
            if (this.IsActive) {
                actions.setAttribute("class", "btn btn-danger");
                actions.setAttribute("value", "Passive");
                actions.setAttribute("onclick", "PassiveMenuItem('" + this.ID + "','" + menuId + "','" + tableBodyId + "','" + tableId+"')");
                actionIcon.setAttribute("class", "fa fa-trash");
                actionIcon.setAttribute("aria-hidden", "true");
                actions.append(actionIcon);
            }
            else {
                actions.setAttribute("class", "btn btn-success");
                actions.setAttribute("value", "Activate");
                actions.setAttribute("onclick", "ActivateMenuItem('" + this.ID + "','" + menuId + "','" + tableBodyId + "','" + tableId +"')");
                actionIcon.setAttribute("class", "fa fa-check");
                actionIcon.setAttribute("aria-hidden", "true");
                actions.append(actionIcon);
            }

            isActive.append(actions);
            

            row.append(pageTitle, language, menuItemPriority, menuName, menuLevel, isSubMenu, isActive)
            tableBody.append(row);

        });

        $('#' + tableId).DataTable({
            "oLanguage": {
                "oPaginate": { "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>', "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>' },
                "sInfo": "Showing page _PAGE_ of _PAGES_",
                "sSearch": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
                "sSearchPlaceholder": "Search...",
                "sLengthMenu": "Results :  _MENU_",
            },
            "stripeClasses": [],
            "lengthMenu": [7, 10, 20, 50],
            "pageLength": 7,
            "destroy": true,
            "cache": false
        });

    });
}

function GetMenus(dropdownId,value) {
    var callParams = {
        endPoint: "/Management/Menu/GetAllMenus",
        requestType: "GET"
    }

    $("#" + dropdownId).html("");
     

    RequestAjax(callParams, null, function (response) {
        response = JSON.parse(response);

        var dropdown = $("#" + dropdownId);

        $.each(response, function () {

            var option = document.createElement("option");
            option.value = this.ID;
            option.innerText = this.MenuName;

            dropdown.append(option);

        });

    });
}

function GetMenuItems(dropdownId, languageDropdownId) {
    debugger
    var callParams = {
        endPoint: "/Management/Menu/GetAllMenuItems",
        requestType: "POST"
    }

    $("#" + dropdownId).html("");

    dataParams = {};
    dataParams.Language = $("#" + languageDropdownId).val();

    RequestAjax(callParams, dataParams, function (response) {
        response = JSON.parse(response);

        var dropdown = $("#" + dropdownId);

        $.each(response, function () {

            var option = document.createElement("option");
            option.setAttribute("menu-level", this.MenuLevel)
            option.value = this.ID;
            option.innerText = this.MenuName;

            dropdown.append(option);

        });

    });
}

function PassiveMenuItem(menuItemId, menuId, tableBodyId, tableId) {

    if (confirm("Are you sure menuItem Passive?"))
    {
        var callParams = {
            endPoint: "../Menu/PassiveMenuItem",
            requestType: "POST"
        }

        dataParams = {};
        dataParams.menuItemId = menuItemId;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true)
            {
                alert("Menu item is passive now.")

                $("#" + tableId).DataTable().destroy();
                GetMenuItemsForMenuList(menuId, tableBodyId, tableId);
            }
            else
            {
                alert("Fail!");

            }

         
        });
    }
}

function ActivateMenuItem(menuItemId,menuId, tableBodyId, tableId) {

    if (confirm("Are you sure menuItem activate?")) {
        var callParams = {
            endPoint: "../Menu/ActivateMenuItem",
            requestType: "POST"
        }

        dataParams = {};
        dataParams.menuItemId = menuItemId;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Menu item is active now.")

                $("#" + tableId).DataTable().destroy();
                GetMenuItemsForMenuList(menuId, tableBodyId, tableId);
            }
            else {
                alert("Fail!");

            }


        });
    }
}