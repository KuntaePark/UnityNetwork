const WebSocket = require('ws')
const wss = new WebSocket.Server({ port: 7777 },()=>{
    console.log('서버시작')
})

const maxPlayerCount = 10;
const idLength = 16;
const players = {}

const deltaTime = 0.016;

wss.on('connection', function connection(ws) {
   //initial setup. generate player id add to players 
   console.log('connection established.');
   const idSet = new Set(Object.keys(players));
   const id = generateId(idSet);
   console.log("id: " + id);
   //Announce id to client
   ws.send(JSON.stringify({
      type: 'playerId',
      payload: id
   }));
   players[id] = {
      x: 0.0, y: 0.0, inputH: 0.0, inputV: 0.0
   }

   ws.on('message', (data) => {
      //update location
      const dataobj = JSON.parse(data);
      console.log("incoming message, type: ", dataobj.type);
      if(dataobj.type === "input") {
         //update position based on input
         const inputs = JSON.parse(dataobj.payload);
         players[id].inputH = inputs.x;
         players[id].inputV = inputs.y;
         
      }
   })
   
   ws.on('close', () =>{
      console.log("connection closed on " + id);
      delete players[id];
   })
})

wss.on('listening',()=>{
   console.log('listening on 7777')
})

//for each frame, 60fps
setInterval(() => {
   //broadcast member location to all members
   const updatedPositions = {};

   for (let id in players) {
   const p = players[id];
   const newX = p.x + p.vx * deltaTime * 100;
   const newY = p.y + p.vy * deltaTime * 100;

   p.x = newX;
   p.y = newY;
   
   updatedPositions[id] = { x: p.x, y: p.y };
  }

  // 전체 위치 브로드캐스트
   const packet = JSON.stringify({ type: 'playerUpdate', payload: JSON.stringify(updatedPositions) });
   console.log(packet);
   wss.clients.forEach((client) => {
      if (client.readyState === WebSocket.OPEN) {
         client.send(packet);
      }
   });
},deltaTime * 1000)

function generateId(idSet) {
   const chars = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
   
   while(true)
   {
      //simple id redundancy checking. should use UUID or player login id later.
      let result = '';
      for (let i = 0; i < idLength; i++) {
         result += chars[Math.floor(Math.random() * chars.length)];
      }

      if(!idSet.has(result))
      {
         return result;
      }
   }
}