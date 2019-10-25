

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

        if(canMoveTo.includes(data.changedTiles[i])){
          highlight_img.setAttribute("src", "/images/pieces/highlight-green.png")
        } else if(canTakeAt.includes(data.changedTiles[i])){
          highlight_img.setAttribute("src", "/images/pieces/highlight-red.png")
        } else if(data.changedTiles[i] == selected){
          highlight_img.setAttribute("src", "/images/pieces/highlight-blue.png")
        } else{
          highlight_img.setAttribute("src", "/images/pieces/ee.png")
        }

        const tile = document.getElementById(file + rank);
        const img = tile.getElementsByClassName("piece")[0];
        img.setAttribute("src", "/images/pieces/" + piece + ".png")

        

      }


      // for(let i = 0; i < 8; i++){
      //   for(let j = 0; j < 8; j++){
      //     const piece = data.gameState.board[i][j];
      //     const file = intToFile(j);
      //     const rank = intToRank(i);
      //     const tile = document.getElementById(file + rank);
      //     const img = tile.getElementsByClassName("piece")[0];
      //     img.setAttribute("src", "/images/pieces/" + piece + ".png")
      //     // <img style="position:absolute" class="piece-image piece" src="~/images/pieces/ee.png" alt="@board[i, j]" />
      //   }
      // }

    });
}