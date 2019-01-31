var express = require('express');
var app = express();

var server = require('http').createServer(app);
var io = require('socket.io').listen(server);

app.set('port', process.env.PORT || 3000);

var clients = [];

io.on("connection", function (socket) {
    var currentUser;
    socket.on("USER_CONNECT", function () {
        console.log('Un joueur vient de se connecter');
        for (var i = 0; i < clients.length; i++) {
            socket.emit("USER_CONNECTED", { name: clients[i].name, position: clients[i].position });
        }
        //Arrêt ici
        console.log("Joueur : "+clients[i].name)
    });
});


server.listen(app.get('port'), function () {
    console.log("=====SERVEUR EN COURS D'EXECUTION=====");
});