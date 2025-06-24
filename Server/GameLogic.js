//행동 종류
const actions = {
    attack: {
        behavior: 'attack'    
    },
    defense: {
        behavior: 'defense',
        unitshieldRate: 0.08 //단위 데미지 감소율
    },
    heal : {
        behavior: 'heal',
        unitHeal : 3, //단위 힐량
    },
    strengthen: { //공버프
        behavior : 'strengthen',
        unitAtkIncRate: 0.15 //단위 공격력 증가율
    }
}
Object.freeze(skills);

//특수 스킬 마다 행동
const behaviors = {
    //(user, target,skill)
    attack: (user, target, skill) => {
        //강도 * 공격력 
        const attackDamage = user.strengthLevel * user.atk;
        const realDamage = attackDamage * (1 - target.shieldRate); //데미지 감소율 적용
        target.shieldRate = 0; //뎀감은 한턴 적용
    },
    defense: (user, _, skill) => {
        user.shieldRate = user.strengthLevel * skill.unitshieldRate;
    },
    heal: (user, _, skill) => {
        user.hp += skill.unitHeal * user.strengthLevel;
    }
}

class Player {
    //게임중 플레이어 데이터를 저장하는 클래스
    constructor() {
        //기본 수치
        this.hp = 100;           //체력
        this.mp = 0;             //마나
        this.atk = 2;            //기초공
        this.strengthLevel = 0;  //현재 행동 강도
        this.currentAction = ""; //현재 선택 행동
        this.skillId = "heal";   //선택 스킬 종류, 기본값 회복

        //단어 관련
        this.currentWord = "";   //현재 단어
        this.options = [];       //현재 단어에 대한 뜻 제시 선택지, 4개
        this.correctIdx = -1;    //뜻 선택지 중 맞는 답의 인덱스

        //특수 플래그
        this.shieldRate = 0.0;   //데미지 감소율
    }

}

class GameLogic {
    //실제 게임 로직 계산
    constructor() {
        //게임 전체 정보
        this.startTime = 0; //게임시작 시간
        this.usedWords = [];

        //각 사용자 정보
        this.players = [new Player(), new Player()];
    }

    //게임 시작
    gameStart() {
        
    }

    //해당 플레이어의 행동 발동
    actionBy(playerIdx, actionType) 
    {
        const action = actions[actionType];
        if(!skill) {
            console.log("no such action");
            return;
        }
        const behaviorFunc = behaviors[action.behavior];
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