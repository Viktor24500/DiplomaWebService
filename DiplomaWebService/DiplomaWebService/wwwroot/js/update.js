//User
function FillUserForm(id)
{
    debugger;
    let userId = document.getElementsByClassName("UserIdInTable")[id].textContent.trim();
    let username = document.getElementsByClassName("UserNameInTable")[id].textContent.trim();
    let lastName = document.getElementsByClassName("LastNameInTable")[id].textContent.trim();
    let firstName = document.getElementsByClassName("FirstNameInTable")[id].textContent.trim();
    let phone = document.getElementsByClassName("Phone")[id].textContent.trim();
    let email = document.getElementsByClassName("Email")[id].textContent.trim();
    let isActive = document.getElementsByClassName("Status")[id].textContent.trim();
    let roleId = document.getElementsByClassName("RoleId")[id].textContent.trim();
    let comment = document.getElementsByClassName("Comment")[id].textContent.trim();

    let form = document.getElementById("addUserForm");
    document.getElementById("submitButton").setAttribute('type', 'button');
    document.getElementById("submitButton").onclick = UpdateUser;

    document.getElementById("userIdForm").value = userId;
    document.getElementById("lastNameForm").value = lastName;
    document.getElementById("firstNameForm").value = firstName;
    document.getElementById("usernameForm").value = username;
    document.getElementById("emailForm").value = email;
    document.getElementById("phoneNumberForm").value = phone;
    document.getElementById("roleIdForm").value = roleId;
    if (isActive == "True") {
        document.getElementById("isActiveForm").value = "true";
    }
    else
    {
        document.getElementById("isActiveForm").value = "false";
    }
    document.getElementById("commentForm").value = comment;

    document.getElementById("userPasswordForm").disabled = true;
    document.getElementById("usernameForm").disabled = true
    document.getElementById("userPasswordForm").readOnly = true;
    document.getElementById("usernameForm").readOnly = true;

    document.getElementById("addUserForm").style.display = "block";
} 

function UpdateUser() {
    let userId = document.getElementById("userIdForm").value;
    let username = document.getElementById("usernameForm").value;
    let lastName = document.getElementById("lastNameForm").value;
    let firstName = document.getElementById("firstNameForm").value;
    let phone = document.getElementById("phoneNumberForm").value;
    let email = document.getElementById("emailForm").value;
    let isActive = document.getElementById("isActiveForm").value;
    let roleId = parseInt(document.getElementById("roleIdForm").value);
    let comment = document.getElementById("commentForm").value;
    if (isActive == "true") {
        isActive = true;
    }
    else {
        isActive = false;
    }
    let Http = new XMLHttpRequest();
    let url = `/users/${userId}`
    Http.open("PUT", url);
    Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
    let data = {
        email: email,
        firstName: firstName,
        lastName: lastName,
        comment: comment,
        isActive: isActive,
        phoneNumber: phone,
        roleId: roleId
    }
    Http.send(JSON.stringify(data));

    Http.onreadystatechange = function () {
        if (Http.readyState === 4 && Http.status === 200) {
            console.log(Http.responseText);
            if (Http.status === 200) {
                window.location.href = '/users';
            }
        }
    };
} 

function DisableUser(id) {
    let userId = document.getElementsByClassName("UserIdInTable")[id].textContent.trim();
    let lastName = document.getElementsByClassName("LastNameInTable")[id].textContent.trim();
    let firstName = document.getElementsByClassName("FirstNameInTable")[id].textContent.trim();
    let phone = document.getElementsByClassName("Phone")[id].textContent.trim();
    let email = document.getElementsByClassName("Email")[id].textContent.trim();
    let roleId = parseInt(document.getElementsByClassName("RoleId")[id].textContent.trim());
    let comment = document.getElementsByClassName("Comment")[id].textContent.trim();
    let isActive = false;

    let Http = new XMLHttpRequest();
    let url = `/users/${userId}`
    Http.open("PUT", url);
    Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
    let data = {
        email: email,
        firstName: firstName,
        lastName: lastName,
        comment: comment,
        isActive: isActive,
        phoneNumber: phone,
        roleId: roleId
    }
    Http.send(JSON.stringify(data));

    Http.onreadystatechange = function () {
        if (Http.readyState === 4 && Http.status === 200) {
            console.log(Http.responseText);
            if (Http.status === 200) {
                window.location.href = '/users';
            }
        }
    };
} 

//Unit

function FillUnitForm(id) {
    debugger;
    let unitId = document.getElementsByClassName("unitId")[id].textContent.trim();
    let unitName = document.getElementsByClassName("unitName")[id].textContent.trim();
    let unitDescription = document.getElementsByClassName("unitDescription")[id].textContent.trim();

    let form = document.getElementById("addUnitForm");
    document.getElementById("submitButton").setAttribute('type', 'button');
    document.getElementById("submitButton").onclick = UpdateUnit;

    document.getElementById("unitIdForm").value = unitId;
    document.getElementById("unitNameFrom").value = unitName;
    document.getElementById("unitDescriptionForm").value = unitDescription;
    document.getElementById("addUnitForm").style.display = "block";
}

function UpdateUnit() {
    let unitId = document.getElementById("unitIdForm").value;
    let unitName = document.getElementById("unitNameFrom").value;
    let unitDescription = document.getElementById("unitDescriptionForm").value;

    let Http = new XMLHttpRequest();
    let url = `/units/${unitId}`
    Http.open("PUT", url);
    Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
    let data = {
        name: unitName,
        description: unitDescription
    }
    Http.send(JSON.stringify(data));

    Http.onreadystatechange = function () {
        if (Http.readyState === 4 && Http.status === 200) {
            console.log(Http.responseText);
            if (Http.status === 200) {
                window.location.href = '/units';
            }
        }
    };
} 

//Sector

function FillSectorForm(id) {
    debugger;
    let sectorId = document.getElementsByClassName("sectorId")[id].textContent.trim();
    let Name = document.getElementsByClassName("Name")[id].textContent.trim();
    let ShortName = document.getElementsByClassName("ShortName")[id].textContent.trim();

    let form = document.getElementById("addSectorForm");
    document.getElementById("submitButton").setAttribute('type', 'button');
    document.getElementById("submitButton").onclick = UpdateSector;

    document.getElementById("sectorIdForm").value = sectorId;
    document.getElementById("sectorNameForm").value = Name;
    document.getElementById("shortSectorNameForm").value = ShortName;
    document.getElementById("addSectorForm").style.display = "block";
}

function UpdateSector() {
    let sectorId = document.getElementById("sectorIdForm").value;
    let sectorName = document.getElementById("sectorNameForm").value;
    let shortSectorName = document.getElementById("shortSectorNameForm").value;

    let Http = new XMLHttpRequest();
    let url = `/sectors/${sectorId}`
    Http.open("PUT", url);
    Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
    let data = {
        name: sectorName,
        shortName: shortSectorName
    }
    Http.send(JSON.stringify(data));

    Http.onreadystatechange = function () {
        if (Http.readyState === 4 && Http.status === 200) {
            console.log(Http.responseText);
            if (Http.status === 200) {
                window.location.href = '/sectors';
            }
        }
    };
} 

//Item

function FillItemForm(id) {
    debugger;
    let itemId = document.getElementsByClassName("itemId")[id].textContent.trim();
    let itemName = document.getElementsByClassName("name")[id].textContent.trim();
    let nomenclatureNumber = document.getElementsByClassName("inventoryNumber")[id].textContent.trim();
    let sectorId = document.getElementsByClassName("sectorId")[id].textContent.trim();
    let unitId = document.getElementsByClassName("unitId")[id].textContent.trim();
    let weight = document.getElementsByClassName("weight")[id].textContent.trim();
    let requiredQuantity = document.getElementsByClassName("requiredQuantity")[id].textContent.trim();
    let description = document.getElementsByClassName("description")[id].textContent.trim();

    let form = document.getElementById("addItemForm");
    document.getElementById("submitButton").setAttribute('type', 'button');
    document.getElementById("submitButton").onclick = UpdateItem;

    document.getElementById("itemIdForm").value = itemId;
    document.getElementById("itemNameForm").value = itemName;
    document.getElementById("nomenclatureNumberForm").value = nomenclatureNumber;
    document.getElementById("sectorIdForm").value = sectorId;
    document.getElementById("unitIdForm").value = unitId;
    document.getElementById("weightForm").value = weight;
    document.getElementById("requiredQuantityForm").value = requiredQuantity;
    document.getElementById("itemDescriptionForm").value = description;
    document.getElementById("addItemForm").style.display = "block";
}

function UpdateItem() {
    let itemId = document.getElementById("itemIdForm").value;
    let itemName = document.getElementById("itemNameForm").value;
    let nomenclatureNumber = document.getElementById("nomenclatureNumberForm").value;
    let sectorId = parseInt(document.getElementById("sectorIdForm").value);
    let unitId = parseInt(document.getElementById("unitIdForm").value);
    let weight = parseFloat(document.getElementById("weightForm").value);
    let requiredQuantity = parseFloat(document.getElementById("requiredQuantityForm").value);
    let description = document.getElementById("itemDescriptionForm").value;

    if (isNaN(weight)) {
        weight = null;
    }

    if (isNaN(requiredQuantity)) {
        requiredQuantity = null;
    }

    let Http = new XMLHttpRequest();
    let url = `/items/${itemId}`
    Http.open("PUT", url);
    Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
    let data = {
        itemName: itemName,
        nomenclatureNumber: nomenclatureNumber,
        sectorId: sectorId,
        unitId: unitId,
        weight: weight,
        requiredQuantity: requiredQuantity,
        description: description
    }
    Http.send(JSON.stringify(data));

    Http.onreadystatechange = function () {
        if (Http.readyState === 4 && Http.status === 200) {
            console.log(Http.responseText);
            if (Http.status === 200) {
                window.location.href = '/items';
            }
        }
    };
} 

//Contragent

function FillContragentForm(id) {
    let contragentId = document.getElementsByClassName("contragentIdInTable")[id].textContent.trim();
    let contragentName = document.getElementsByClassName("contragentNameInTable")[id].textContent.trim();
    let parentId = parseInt(document.getElementsByClassName("parentIdInTable")[id].textContent.trim());
    let contragentDescription = document.getElementsByClassName("contragentDescriptionInTable")[id].textContent.trim();
    let isActive = document.getElementsByClassName("Status")[id].textContent.trim();

    let form = document.getElementById("addContragentForm");
    document.getElementById("submitButton").setAttribute('type', 'button');
    document.getElementById("submitButton").onclick = UpdateContragent;

    document.getElementById("contragentIdForm").value = contragentId;
    document.getElementById("contragentNameForm").value = contragentName;
    if (isNaN(parentId)) {
        document.getElementById("parentIdForm").value = null;
    }
    else {
        document.getElementById("parentIdForm").value = parentId;
    }
    document.getElementById("contragentDescriptionForm").value = contragentDescription;
    if (isActive == "True") {
        document.getElementById("isActiveForm").value = true;
    }
    else {
        document.getElementById("isActiveForm").value = false;
    }
    document.getElementById("addContragentForm").style.display = "block";
}

function UpdateContragent() {
    let contragentId = document.getElementById("contragentIdForm").value;
    let parentId = parseInt(document.getElementById("parentIdForm").value);
    let contragentName = document.getElementById("contragentNameForm").value;
    let isActive = document.getElementById("isActiveForm").value;
    let contragentDescription = document.getElementById("contragentDescriptionForm").value;

    if (isNaN(parentId)) {
        parentId = null;
    }

    if (isActive == "true") {
        isActive = true;
    }
    else {
        isActive = false;
    }

    let Http = new XMLHttpRequest();
    let url = `/contragents/${contragentId}`
    Http.open("PUT", url);
    Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
    let data = {
        parentId: parentId,
        contragentName: contragentName,
        isActive: isActive,
        contragentDescription: contragentDescription
    }
    Http.send(JSON.stringify(data));

    Http.onreadystatechange = function () {
        if (Http.readyState === 4 && Http.status === 200) {
            console.log(Http.responseText);
            if (Http.status === 200) {
                window.location.href = '/contragents';
            }
        }
    };
} 

function DisableContragent(id) {
    let contragentId = document.getElementsByClassName("contragentIdInTable")[id].textContent.trim();
    let contragentName = document.getElementsByClassName("contragentNameInTable")[id].textContent.trim();
    let parentId = parseInt(document.getElementsByClassName("parentIdInTable")[id].textContent.trim());
    let contragentDescription = document.getElementsByClassName("contragentDescriptionInTable")[id].textContent.trim();
    let isActive = false;

    if (isNaN(parentId)) {
        parentId = null;
    }

    let Http = new XMLHttpRequest();
    let url = `/contragents/${contragentId}`
    Http.open("PUT", url);
    Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
    let data = {
        contragentId: contragentId,
        parentId: parentId,
        contragentName: contragentName,
        isActive: isActive,
        contragentDescription: contragentDescription
    }
    Http.send(JSON.stringify(data));

    Http.onreadystatechange = function () {
        if (Http.readyState === 4 && Http.status === 200) {
            console.log(Http.responseText);
            if (Http.status === 200) {
                window.location.href = '/contragents';
            }
        }
    };
} 