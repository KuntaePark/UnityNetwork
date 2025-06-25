/*
 * 게임 1 전용 서버.
 */

const WebSocket = require('ws');
const { deltaTime, skills, skillBehaviors, Player, GameLogic} 
= require('./GameLogic');
const wss = new WebSocket.Server({ port: 7778 },()=>{
    console.log('GAME1 SERVER START');
});

/* [sessionId] : [Session] */
const sessions = {}; //게임 세션

class Session {
    constructor(sessionId, playerId1, playerId2) {
        this.sessionId = sessionId;
        this.players = new Map(); // socket.id → { socket, playerData }
    }

    addPlayer(socket) {
        this.players.set(socket.id, socket);

        socket.on('message', (msg) => this.handleInput(socket, msg));
        socket.on('close', () => this.removePlayer(socket));
    }

    removePlayer(socket) {
        this.players.delete(socket.id);
    }

    broadcast(type, payload) {
        for (const socket of this.players.values()) {
            socket.send(JSON.stringify({ type, payload }));
        }
    }

    tick() {
        // 플레이어 상태 갱신
    }
}


//매칭 서버에 의해 해당 유저들의 세션이 이미 생성되었다고 가정
//todo - 매칭 서버에 의한 세션 생성 요청 및 수락 구현
wss.on('connection', function connection(ws) {


    ws.on('message', (data) => {
        const dataobj = JSON.parse(data);
        console.log("[" + UTCTimeStamp()+"]" + " incoming message, type: ", dataobj.type);
        handlePacket[dataobj.type](ws, dataobj.payload);
        
    })

    ws.on('close', () => {
        console.log("connection closed");
    })
});

//수신 패킷 처리
const handlePacket = {
    greeting : (ws, payload) => {
        ws.send(JSON.stringify({
                    type: 'greeting',
                    payload: 'Hello!'
                }));
    },
    input : (_, payload) => {
        handleInput(payload);
    }
}




//초당 60번 게임 로직 수행 및 업데이트
setInterval(() => {
    //각 플레이어의 플래그 체크 -> 활성 플래그 있을 시 해당 행동 수행
    for(sessionId in sessions) {
        const session = sessions[sessionId];
        const gameLogic = session.gameLogic;
        for(player in gameLogic.players) {
            const flags = player.actionFlag;
        }
    }
}, deltaTime * 1000);



function handleInput(payload) {
    const inputData = JSON.parse(payload);
    switch(inputData.type) {
        case 'chargeMana':
            console.log('chargeMana');
        break;
        default:
            console.log('unknown input');
    }

}

//********기타 편의성 함수*********//
function UTCTimeStamp() {return new Date(Date.now()).toUTCString();}