function searchUsers(str) {
    if (str.length == 0) {
        document.getElementById("searchResults").innerHTML = "";
        return;
    }
    else {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById("searchResults").innerHTML = this.responseText;
            }
        };
        
        xmlhttp.open("GET", "/Home/Search/?searchString=" + str, true);
        xmlhttp.send();
    }
}

function addUser(userID) {
    fetch('/Home/AddFriend', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ userID: userID })
    });

    deleteRow(userID);
}

function deleteRow(rowid) {
    var row = document.getElementById(rowid);
    row.parentNode.removeChild(row);
}


function changeIcon(iconID) {
    if (document.getElementById(iconID).className == "glyphicon glyphicon-minus") {
        document.getElementById(iconID).className = "glyphicon glyphicon-plus";
    } else {
        document.getElementById(iconID).className = "glyphicon glyphicon-minus";
    }
}