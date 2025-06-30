/*
 * 게임 세션 정보 저장 클래스.
 */
const {Player} = require('./Player');
const {deltaTime} = require('./GameLogic');

class Session {
    static sessionCount = 0;

    constructor(playerId1, playerId2) {
        //매칭 서버로부터 두 플레이어의 아이디를 받아 세션 생성
        this.id = (Session.sessionCount++).toString();
        this.intervalId = null;

        //게임 상태 관련
        this.startTime = null; //게임 시작 시간

        //게임 상태: ready: 대기, countdown: 카운트다운, running: 게임중, end: 게임종료
        this.gameState = "ready";
        this.usedWords = [];   //해당 라운드에서 사용된 단어들
        
        //상대 판정 쉽게 하기 위해 배열로 저장
        this.players = [new Player(0,playerId1, this), new Player(1, playerId2, this)]; //2인
    }

    //게임 시작 시 호출
    gameStart() {
        console.log('game start');
        this.startTime = Date.now();
        this.intervalId = setInterval(() => this.tick(), deltaTime * 1000);
    }

    //프레임 당 연산
    tick() {
        let hasUpdate = false;

        //각 플레이어에 대해 input 체크 후 행동 수행
        for(const player of this.players) {
            if(player.hasInput()) {
                //행동 수행
                player.do(deltaTime);
                hasUpdate = true;
            }
        }

        //업데이트 존재 시 브로드캐스트
        if(hasUpdate) this.broadcastState();

        //게임 종료 체크
        this.checkGameEnd();
    }
    
    //각 플레이어에게 상태 브로드캐스트
    broadcastState() {
        for(const player of this.players) {
            const message = JSON.stringify({
                type: 'gameState',
                payload: JSON.stringify(this)
        })
            player.socket.send(message);
        }
    }

    //게임 종료 체크
    checkGameEnd() {

    }

    //해당 플레이어의 소켓 배정
    setPlayerConnection(ws) {
        const id = ws.id;
        let player = null;
        for(const p of this.players) {
            if(p.id === id) {
                player = p;
            }
        }
        if(player) {
            player.socket = ws;
        } else {
            console.log("no such player");
        }
    }

    //플레이어 아이디로 플레이어 검색
    getPlayer(playerId) {
        for(const player of this.players) {
            if(player.id === playerId) return player;
        }
        return null;
    }

    close() {
        clearInterval(this.intervalId);   
    }

    //게임 상태 전송용
    toJSON() {
        return {
            startTime: this.startTime,
            players: this.players
        }
    }
}

module.exports = {Session} ;