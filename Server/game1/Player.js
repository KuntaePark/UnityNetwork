/*
 * 게임1에서의 각 플레이어에 대한 정보를 저장하는 클래스.
 */
class Player {
    constructor(playerId, session) {
        this.id = playerId;
        this.socket = null;
        this.session = session;
        this.playerData = new PlayerData();

        this.inputData = null;
    }

    do(deltaTime) {
        const type = this.inputData.type;
        inputActions[type](this, deltaTime);
        this.inputData = null;
    }

    setInput(inputData) {
        if(!!this.inputData) return; //처리 안된 입력 있을 시 스킵
        else this.inputData = inputData;        
    }

    hasInput() {return !!this.inputData;}
}

/*
 * 게임 로직 관련 데이터만 저장하는 클래스.
 */
class PlayerData {
    constructor() {
        //기본 수치
        this.hp = 100;           //체력
        this.mp = 0;             //마나
        this.unitMana = 1;       //초당 마나 회복량
        this.atk = 2;            //기초공
        this.strengthLevel = 0;  //현재 행동 강도
        this.actionIdx = -1;     //현재 선택 행동
        this.skillId = "heal";   //선택 스킬 종류, 기본값 회복
        
        //상태 관련
        this.isActionSelected = false; //행동 결정 여부
        this.currentAction = 'ATTACK' // 현재 행동

        //단어 관련
        this.currentWord = "";   //현재 단어
        this.options = [];       //현재 단어에 대한 뜻 제시 선택지, 4개
        this.correctIdx = -1;    //뜻 선택지 중 맞는 답의 인덱스

        //특수 플래그
        this.shieldRate = 0.0;   //데미지 감소율
    }
    
    //클라이언트에게 전송할 데이터만 JSON으로 변환
    toJSON() {
        return {
            hp: this.hp,
            mp: this.mp,
            strengthLevel: this.strengthLevel,
            isActionSelected: this.isActionSelected,
            currentWord: this.currentWord,
            options: this.options,
            correctIdx: this.correctIdx
        }
    }
}

/*
 * 인풋에 해당하는 행동 함수
 */
const inputActions = {
    'chargeMana': (user, deltaTime) => {
        const playerData = user.playerData;
        playerData.mp = Math.min(playerData.mp + deltaTime * playerData.unitMana,10);
        console.log(`user ${user.id} charged mana : ${playerData.mp}`)
    },
    'actionSelect': (user, _) => {
        console.log(`user ${user.id} do actionSelect`);
    },
    'actionCancel': (user, _) => {
        console.log(`user ${user.id} do actionCancel`);
    },
    'wordSelect': (user, _) => {
        console.log(`user ${user.id} do wordSelect`);
    }

}

module.exports = {Player};