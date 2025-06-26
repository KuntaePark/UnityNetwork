//======== 행동 정의 ========
//스킬 종류 및 스킬 관련 수치(기본 공격 및 방어 포함)
const actions = ['ATTACK', 'DEFENSE', 'SPECIAL'];

const skills = {
    attack: {
        behavior: 'attack'    
    },
    defense: {
        behavior: 'defense',
        unitShieldRate: 0.08 //단위 데미지 감소율
    },
    heal : {
        behavior: 'heal',
        unitHeal : 3, //단위 힐량
    },
    strengthen: { //공버프
        behavior : 'strengthen',
        unitAtkIncRate: 0.05 //단위 공격력 증가율
    }
}
Object.freeze(skills);

//스킬에 해당하는 행동
const skillBehaviors = {
    //(user, target,skill)
    attack: (user, target, skill) => {
        //강도 * 공격력 
        const attackDamage = user.strengthLevel * user.atk;
        const realDamage = attackDamage * (1 - target.shieldRate); //데미지 감소율 적용
        target.shieldRate = 0; //뎀감은 한턴 적용
    },
    defense: (user, _, skill) => {
        user.shieldRate = user.strengthLevel * skill.unitShieldRate;
    },
    heal: (user, _, skill) => {
        user.hp += skill.unitHeal * user.strengthLevel;
    }
}
Object.freeze(skillBehaviors);
//======== 행동 정의 ========

//======== 단어 관련 함수 ========
function getWord() {

}
//======== 단어 관련 함수 ========

const deltaTime = 0.016; //서버 틱 시간

class Player {
    //게임중 플레이어 데이터를 저장하는 클래스
    constructor() {
        // this.id = 

        //기본 수치
        this.hp = 100;           //체력
        this.mp = 0;             //마나
        this.unitMana = 1;       //초당 마나 회복량
        this.atk = 2;            //기초공
        this.strengthLevel = 0;  //현재 행동 강도
        this.actionIdx = -1; //현재 선택 행동
        this.isActionSelected = false; //행동 결정 여부
        this.skillId = "heal";   //선택 스킬 종류, 기본값 회복

        //단어 관련
        this.currentWord = "";   //현재 단어
        this.options = [];       //현재 단어에 대한 뜻 제시 선택지, 4개
        this.correctIdx = -1;    //뜻 선택지 중 맞는 답의 인덱스

        //특수 플래그
        this.shieldRate = 0.0;   //데미지 감소율
        this.actionFlag = {           //행동 플래그
            'chargeMana'   : false,
            'actionSelect' : false,
            'actionCancel' : false,
            'optionSelect' : false,
        }
    }
    //======== 사용자 입력에 따른 행동 함수 ========
    chargeMana(_) {
        console.log("charge mana");
        this.mp = Math.min(this.mp + deltaTime * this.unitMana, 10.0);
    }
    actionSelect(params) {
        //행동 설정
        const action = params.action;
        
    }

    actionCancel(params) {
        //현재 행동 취소 -> 행동 선택 단계로
    }

    optionSelect(params) {
        //단어 뜻 선택 -> 채점 후 맞는 거 따라 행동
    }

}

class GameLogic {
    //실제 게임 로직 계산
    constructor() {
        //게임 전체 정보
        this.startTime = 0; //게임시작 시간
        this.usedWords = [];
        this.unitMana = 1; //초당 충전되는 마나량

        //각 사용자 정보
        this.players = [new Player(), new Player()];

        //갱신 플래그
        this.hasUpdate = false;
    }

    //게임 시작
    gameStart() {
        
    }

    //해당 플레이어의 행동 발동
    actionBy(playerIdx, actionType) 
    {
        const skill = skills[actionType];
        if(!skill) {
            console.log("no such action");
            return;
        }
        const behaviorFunc = skillBehaviors[action.behavior];
        if(!behaviorFunc) {
            console.log("no such behavior");
            return;
        }
        const user = this.players[playerIdx]; 
        const target = this.players[1-playerIdx];
        behaviorFunc(user, target, action);
    }

    //게임 종료 호출, 양쪽의 체력 상황에 따라 게임 결과 산정
    gameFinish() {
    }

}

module.exports = {
    deltaTime, skills, skillBehaviors, 
    Player, GameLogic 
}
