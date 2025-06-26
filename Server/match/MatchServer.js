const WebSocket = require('ws');
const wss = new WebSocket.Server({port: 7779},() => {
    console.log('MATCH SERVER ON');
})

//make connection to game server
const gameSocket = new WebSocket('ws://localhost:7778');
gameSocket.on('open',() => {
    const greetingMessage = JSON.stringify({type: 'greeting', payload: 'hello'});
    gameSocket.send(greetingMessage);
})

gameSocket.on('error', () => {
    console.log('connection error');
})



//match server
wss.on('connection', function connection(ws) {
    

});
