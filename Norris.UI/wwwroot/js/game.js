

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


function updateBoard(gameid, clickedtile, selectedtile, canmove, cantake) {
    fetch('/game/clickedtile', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify( { 
          ClickedTile : clickedtile,
          GameID : gameid,
          SelectedTile : selectedtile,
          CanMove : canmove,
          CanTake : cantake
        })
    })
    .then(data => {
        return data.json()
    })
    .then(data => {
      console.log(data);




    });
}