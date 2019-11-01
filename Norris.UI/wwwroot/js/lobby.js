

function refreshLobby(){
  fetch('/Home/GetLobbyPartial', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' }
  })
  .then(data => {return data.text()})
  .then(data => {
    const lobby = document.getElementById("lobby-partial");

    if(lobby.innerHTML != data){
      lobby.innerHTML = data;
    }
  });
}

setInterval(refreshLobby, 5000);