const WebSocket = require('ws');
const {makePacket} = require('../common/Packet');
const { json } = require('express');

const matchServerId = 'MATCHSERVER';

class Matcher {
    constructor() {
        this.matchQueue = [];
        this.game1ws = this.connectToGameServer();
    }

    connectToGameServer() {
        //게임 서버 연결
        const game1ws = new WebSocket('ws://localhost:7778');

        game1ws.on('open',() => {
            console.log('connection to game 1 server successful.');
            game1ws.send(makePacket('auth',matchServerId));
        });

        game1ws.on('message',(message) => {
            const {type, payload} = JSON.parse(message);
        });

        game1ws.on('close', (code, reason) => {

        });

        game1ws.on('error', (err) => {

        });

        return game1ws;
    }

    findMatch(ws) {
        //플레이어를 매칭 큐에 추가.
        //임시 매칭으로 두명 차면 바로 세션 생성.
        
        if(this.matchQueue.length >= 1) {
            console.log('matching complete. requesting session create.');
            this.requestSessionCreate(ws, this.matchQueue.pop());
        } else {
            this.matchQueue.push(ws);
        }
    }

    requestSessionCreate(ws1, ws2) {
        this.game1ws.send(makePacket('create_session',{ids: [ws1.id, ws2.id]}));
        //해당 유저들에게 세션 생성이 요청되었음을 알림
        //해당 유저는 매칭 성공 알림을 받고 게임 서버 접속 시도
        ws1.send(makePacket('match_success', ''));
        ws2.send(makePacket('match_success', ''));
        
    }

};

module.exports = {Matcher};