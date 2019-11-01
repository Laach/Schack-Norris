

function rankToInt(rank){
  return 8 - rank;
}

function fileToInt(file){
  switch(file){
    case 'a': return 0;
    case 'b': return 1;
    case 'c': return 2;
    case 'd': return 3;
    case 'e': return 4;
    case 'f': return 5;
    case 'g': return 6;
    case 'h': return 7;
  }
  return null;
}


let selected = null;

let canMoveTo = [];

let canTakeAt = [];

function updateBoard(gameid, clickedtile) {
    tryGetUpdates(gameid);
    data = { 
          ClickedTile : clickedtile,
          GameID : gameid,
          SelectedTile : selected,
          CanMove : canMoveTo,
          CanTake : canTakeAt
        };
    fetch('/game/clickedtile', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    })
    .then(data => {
        return data.json()
    })
    .then(data => {
      if(data == ""){
        return;
      }
      selected  = data.selectedTile;
      canMoveTo = data.canMoveToTiles;
      canTakeAt = data.canMoveToAndTakeTiles;
      setTiles(data);
    });
}

function setTiles(data){
  for(let i = 0; i < data.changedTiles.length; i++){
    if(data.changedTiles[i] == null){
      continue;
    }
    const file = data.changedTiles[i][0];
    const rank = data.changedTiles[i][1];
    const x = fileToInt(file);
    const y = rankToInt(rank);


    const piece = data.gameState.board[y][x];

    const highlight = document.getElementById(file + rank);
    const highlight_img = highlight.getElementsByClassName("highlight")[0];

    if(data.canMoveToTiles.includes(data.changedTiles[i])){
      highlight_img.setAttribute("src", "/images/pieces/highlight-green.png")
    } else if(data.canMoveToAndTakeTiles.includes(data.changedTiles[i])){
      highlight_img.setAttribute("src", "/images/pieces/highlight-red.png")
    } else if(data.changedTiles[i] == data.selectedTile){
      highlight_img.setAttribute("src", "/images/pieces/highlight-blue.png")
    } else{
      highlight_img.setAttribute("src", "/images/pieces/ee.png")
    }

    const tile = document.getElementById(file + rank);
    const img = tile.getElementsByClassName("piece")[0];
    img.setAttribute("src", "/images/pieces/" + piece + ".png")
  }
}


function tryGetUpdates(gameid) {

    data = { 
      GameID : gameid,
        };
        
    fetch('/game/GameRefresh', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    })
    .then(data => { return data.json() })
    .then(data => {
      const banner = document.getElementById("banner");
      if(data.game != null){
        // Refresh board
        setTiles(data.game);
        // Display your-turn banner
      }
      if(data.isActive && data.isMyTurn){
        // Active game, my turn
        banner.innerHTML = "It\'s <strong>your turn</strong>";
        banner.className = "alert alert-success";
      }
      else if(data.isMyTurn){
        // Archived game, my turn, I lost
        banner.innerHTML = "<strong>You lost</strong>";
        banner.className = "alert alert-danger";
      }
      else if(data.isActive && !data.isMyTurn){
        // Active game, not my turn.
        banner.innerHTML = "waiting for your <strong>opponents turn</strong>";
        banner.className = "alert alert-info";
      }
      else{
        banner.innerHTML = "<strong>You won</strong>";
        banner.className = "alert alert-success";
      }
      if(data.chat != null){
        // Append chat
      }
      // Update movecounter
      const movecounter = document.getElementById("lobbyInfoRight")
      movecounter.innerText = data.moveCount + " Moves Made.";
    });

    const messages = document.getElementsByClassName("msg");
    data2 = {
      GameID: gameid,
      chatLength : messages.length
    }

    fetch('/game/getchat', {
      method: 'post',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data2)
    })
    .then(data => {return data.text()})
    .then(data => {
      if(data != "\"\""){
        const chatwindow = document.getElementById("chat-list-group-item-override");
        chatwindow.innerHTML = data;

        scrollToBottom("chat");
      }
    });
}

function scrollToBottom(id) {
  const div = document.getElementById(id);
  div.scrollTop = div.scrollHeight - div.clientHeight;
}

function SendMessage(GameID) {
    const message = document.getElementById("chat-box-input").value
    if(message == null || message == ""){
      return;
    }
    fetch('/Game/SendMessage', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ message: message, GameID: GameID })
    })
    
    document.getElementById("chat-box-input").value = "";
    tryGetUpdates(GameID);
}