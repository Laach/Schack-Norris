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

let activeGamesOpen    = true;
let onlineFriendsOpen  = true;
let offlineFriendsOpen = true;

function refreshSidebar() {
  const id = document.getElementById("activeGame").innerText;
  fetch('/Home/Sidebar', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ GameID: id })
  })
  .then(data => {
      return data.json()
  })
  .then(data => {
    if(activeGamesOpen){
      const games = document.getElementById("activegames");
      const temp = document.createElement("div");
      temp.innerHTML = data.activeGames;
      if(games.innerText.replace(/\s/g,'') != temp.innerText.replace(/\s/g,'').replace(id, "")){
        console.log("Games")
        games.innerHTML = data.activeGames;
      }
    }
    if(onlineFriendsOpen){
      const onlinefriends = document.getElementById("online-friends");
      const temp = document.createElement("div");
      temp.innerHTML = data.onlineFriends;
      if(onlinefriends.innerText.replace(/\s/g,'') != temp.innerText.replace(/\s/g,'').replace(id,"")){
        onlinefriends.innerHTML = data.onlineFriends;
      }
    }
    if(offlineFriendsOpen){
      const offlinefriends = document.getElementsByClassName("offline-friends")[0];
      const temp = document.createElement("div");
      temp.innerHTML = data.offlineFriends;
      if(offlineFriends.innerText.replace(/\s/g,'') != temp.innerText.replace(/\s/g,'').replace(id,"")){
        offlinefriends.innerHTML = data.offlineFriends;
      }
    }
  });

}

setInterval(refreshSidebar, 1000);