

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
      chatLength : 0
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
        banner.innerHTML = "It\'s <strong>your turn</strong>";
        banner.className = "alert alert-success";
      }
      else{
        banner.innerHTML = "Waiting for your <strong>opponents turn</strong>";
        banner.className = "alert alert-info";
      }
      if(data.chat != null){
        // Append chat
      }
      // Update movecounter
      const movecounter = document.getElementById("movecounter")
      movecounter.innerText = data.moveCount + " Moves Made.";
    });
}
