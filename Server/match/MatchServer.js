const WebSocket = require('ws');
const express = require('express');
const http = require('http');

const app = express();

const port = 8080;

app.listen(port, function() {
    console.log(`match server started on ${port}`);
});

//match server

//매칭 요청 
app.post("/match/join/:id", function (req, res) {
    //id 인증 확인 필요, 임시
    const id = req.params.id;
    console.log('request join');
    
});

const RequestHandler = {
    'GET': () => {},
    'POST': () => {},
    'PUT': () => {},
    'DELETE': () => {},
};
