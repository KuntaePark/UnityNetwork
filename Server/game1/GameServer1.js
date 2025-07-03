/*
 * 게임 1 전용 서버.
 */

const WebSocket = require('ws');
const {Session} = require('./Session');
const {makePacket} = require('./Packet');
const wss = new WebSocket.Server({ port: 7778 },()=>{
    console.log('GAME1 SERVER START ON 7778');
});

//매칭 서버 연결 저장
let matchServerClient = null;

/* [sessionId] : [Session] */
const sessions = new Map(); //게임 세션

//임시 매칭 큐. 매칭 서버 구현 이후 제거.
const playerQueue = [];
const idSet = new Set();
const idLength = 16;

//매칭 서버에 의해 해당 유저들의 세션이 이미 생성되었다고 가정
//todo - 매칭 서버에 의한 세션 생성 요청 및 수락 구현
wss.on('connection', function connection(ws) {
    const id = generateId();
    ws["id"] = id;
    console.log(`user ${id} connected`);
    const message = makePacket('connect', id);
    //임시 매칭 큐에 저장
    playerQueue.push(ws);

    //임시 매칭, 2명 들어오는대로 세션 생성
    if(playerQueue.length === 2) {
        createSession();
    }
    
    ws.on('message', (data) => {
        const {type, payload} = JSON.parse(data);
        // console.log(`[${UTCTimeStamp()}] incoming message, type: ${type}`);
        (handlePacket[type] || (() => console.log('unknown packet type')))(ws, payload);
    })

    ws.on('close', () => {
        console.log("connection closed");
        //todo - 세션 퇴장 처리 필요
    })
});


//세션 생성 함수. 임시로 들어오는대로 세션 생성
function createSession() {
    const session = new Session(playerQueue.pop(), playerQueue.pop());
    sessions.set(session.id, session);
    console.log(`session ${session.id} created`);

    //테스트를 위해 바로 게임 시작
    session.gameStart();
}

//수신 패킷 처리
const handlePacket = {
    input : (ws, payload) => {
        if(ws.sessionId) handleInput(ws, payload);
    }
}

//인풋 처리
function handleInput(ws, payload) {
    const playerId = ws.id;
    const sessionId = ws.sessionId;
    // console.log(`input by playerId ${playerId} in session ${sessionId}`)
    const player = sessions.get(sessionId).getPlayer(playerId);
    const inputData = JSON.parse(payload);
    player.setInput(inputData);

}

//임시 아이디 생성 함수.
function generateId() {
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
         idSet.add(result);
         return result;
      }
   }
}

//********기타 편의성 함수*********//
function UTCTimeStamp() {return new Date(Date.now()).toUTCString();}
