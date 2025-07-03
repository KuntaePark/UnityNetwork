const WebSocket = require('ws');

const wss = new WebSocket.Server({port: 7779}, () => {
    console.log('MATCH SERVER STARTED AT 7779');
})

wss.on('connection',function connection(ws) {
   
   
    

    ws.on('message', (message) => {
        //message
    }); 

    ws.on('close', () => {
        //close
    });
});