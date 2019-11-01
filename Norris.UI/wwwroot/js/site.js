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
  const node = document.getElementById("activeGame");
  let id = "";
  if(node != null){
    id = node.innerText;
  }
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
      if(offlinefriends.innerText.replace(/\s/g,'') != temp.innerText.replace(/\s/g,'').replace(id,"")){
        offlinefriends.innerHTML = data.offlineFriends;
      }
    }
  });

}

setInterval(refreshSidebar, 1000);


function createGame(userID){
  fetch('/Game/NewGame', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ UserID: userID })
  })
  .then(data => {return data.json()})
  .then(data => {
      console.log(data);
      if (data.gameID == "") {
          alert("You cannot have more than three games with the same player! Sign up for our unlimited chess plan for unlimited chess games!");
      } else {
          window.location.href = "/Game?gameId=" + data.gameID;
      }
  });
}