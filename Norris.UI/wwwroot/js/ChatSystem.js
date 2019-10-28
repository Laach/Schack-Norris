function SendMessage(GameID) {
    var message = document.getElementById("ChatBoxInput").value
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
    document.getElementById("ChatBoxInput").value = "";
}