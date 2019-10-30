function SendMessage(GameID) {
    var message = document.getElementById("chat-box-input").value
    if(message == null || message == ""){
      return;
    }
    fetch('/Game/SendMessage', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ message: message, GameID: GameID })
    })
        .then(data => {
            return data.text()
        })
        .then(text => {
            console.log(text);
        });
    document.getElementById("chat-box-input").value = "";
}