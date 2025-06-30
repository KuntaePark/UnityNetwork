const mysql = require('mysql2/promise');

class WordDB {
    constructor() {
        this.wordData = null;
        this.wordCount = 0;

        this.sentenceData = null;
        this.sentenceCount = 0;
    }

//전체 단어 데이터 조회
    async getWordData() {
        const connection = await mysql.createConnection({
            host : '210.219.170.244',
            port: '3306',
            user: 'abc',
            password: '1234',
            database: 'engjoy'
        });
            
        const wordData = await connection.query("select expr_id, word_text, meaning, difficulty from expression where expr_type = 'WORD'");
        const sentenceData = await connection.query("select expr_id, word_text, meaning, difficulty from expression where expr_type = 'SENTENCE'")
        
        this.wordData = wordData[0];
        this.sentenceData = sentenceData[0];
        this.wordCount = this.wordData.length;
        this.sentenceCount = this.sentenceData.length;
        await connection.end();        
    }

    pickRandom(difficulty) {
        let wordData = null;

        if(!difficulty) {
            //무작위 난이도
            wordData = this.wordData;
        } else {
            //해당 난이도
            wordData = this.wordData.filter((word) => word.difficulty === difficulty);
        }
        const wordCount = wordData.length;
        const idx = Math.floor(Math.random() * wordCount);
        return wordData[idx];
    }

    //4개의 같은 난이도의 단어를 랜덤으로 선택해 반환
    pick4 (difficulty) {
        const words = [];
        for(let i = 0; i < 4; i++) {
            words.push(this.pickRandom(difficulty));
        }
        return words;
    }
}

module.exports = {WordDB};