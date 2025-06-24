/*
 * 게임 1 전용 서버.
 */

const WebSocket = require('ws');
const GameLogic = require('./GameLogic.js');
const wss = new WebSocket.Server({ port: 7778 },()=>{
    console.log('GAME1 SERVER START');
});

const sessions = {}; //game sessions

wss.on('connection', function connection(ws) {
    //
});
