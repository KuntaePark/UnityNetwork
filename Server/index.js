const WebSocket = require('ws')
const Physics = require('./physics.js')
const wss = new WebSocket.Server({ port: 7777 },()=>{
    console.log('SERVER START')
})

const maxPlayerCount = 10;
const idLength = 16;
const players = {}
const exitQueue = [];

const deltaTime = 0.05;
const speed = 5.0;

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
      
      const now = new Date(Date.now());
      console.log("[" + now.toUTCString()+"]" + " incoming message, type: ", dataobj.type);
      if(dataobj.type === "input") {
         //update position based on input
         const inputs = JSON.parse(dataobj.payload);
         players[id].inputH = inputs.x;
         players[id].inputV = inputs.y;
         
      }
   })
   
   ws.on('close', () =>{
      console.log("connection closed on " + id);
      exitQueue.push(id);
      delete players[id];
   })
})

wss.on('listening',()=>{
   console.log('listening on 7777')
})

//for each frame, 60fps
setInterval(() => {
   while(exitQueue.length !== 0) {
      console.log("broadcast exit");
      const id = exitQueue.pop();
      const packet = JSON.stringify({type: 'playerExit', payload: id})
      wss.clients.forEach((client) => {
         if (client.readyState === WebSocket.OPEN) {
            client.send(packet);
         }
      });
   }

   //broadcast member location to all members
   const updatedPositions = {};

   //position update
   for (let id in players) {
   const p = players[id];
   const newX = p.x + p.inputH * deltaTime * speed;
   const newY = p.y + p.inputV * deltaTime * speed;

   //check collision
   let colliding = false;

   for(let others in players) {
      if(others === id) continue;
      
      const o = players[others];
      const myLoc = new Physics.Vector2(newX, newY);
      const otherLoc = new Physics.Vector2(o.x, o.y);
      
      if(Physics.checkCollision(myLoc, otherLoc)) {
         console.log("colliding");
         colliding = true; break;
      }
   }

   
   if(!colliding)
      {
         //update only when not colliding
         p.x = newX;
         p.y = newY;
      }
   //out of boundary
   if(p.x >= 9 - Physics.colliderRadius) {
      p.x = 8.99 - Physics.colliderRadius;
   }
   if(p.x <= -9 + Physics.colliderRadius) {
      p.x = -8.99 + Physics.colliderRadius;
   }
   if(p.y >= 5 - Physics.colliderRadius) {
      p.y = 4.99 - Physics.colliderRadius;
   }
   if(p.y <= -5 + Physics.colliderRadius) {
      p.y = -4.99 + Physics.colliderRadius;
   }


   // console.log(p.x, p.y)
   updatedPositions[id] = { x: p.x, y: p.y };
   p.inputH = 0; p.inputV = 0;
  }

  // 전체 위치 브로드캐스트
   const packet = JSON.stringify({ type: 'playerUpdate', payload: JSON.stringify(updatedPositions) });
   //console.log(packet);
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