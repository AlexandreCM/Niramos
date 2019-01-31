var express = require('express');
var app = express();

var server = require('http').createServer(app);
var io = require('socket.io').listen(server);

app.set('port', process.env.PORT || 3000);

var clients = [];

//io est une variable de socket.io regroupant des évènements
io.on("connection", function (socket) {
    var joueurCourant;
    socket.on("USER_CONNECT", function () {
        console.log('Un joueur vient de se connecter');
        for (var i = 0; i < clients.length; i++) {
            socket.emit("USER_CONNECTED", { nom: clients[i].nom, position: clients[i].position });
        }
        console.log("Joueur : " + clients[i].nom + " est connecté.")
    });

    socket.on("PLAY", function (data) {
        joueurCourant = {
            nom: data.nom,
            position: data.position
        }

        clients.push(joueurCourant);
        socket.emit("PLAY", joueurCourant);
        socket.broadcast.emit("USER_CONNECTED", joueurCourant);
    });

    socket.on("MOVE", function (data) {
        joueurCourant.position = data.position;
        socket.emit("MOVE", joueurCourant);
        socket.broadcast.emit("MOVE", joueurCourant);
        console.log(joueurCourant.nom + "se déplace vers " + joueurCourant.position);
    })

    socket.on("disconnect", function () {
        socket.broadcast.emit("USER_DISCONNECTED", joueurCourant);
        for (var i = 0; i < clients.length; i++) {
            if (clients[i].nom == joueurCourant.nom) {
                console.log("Joueur " + clients[i].nom + " s'est déconnecté.");
                clients.splice(i, 1);
            }
        }
    })
});



server.listen(app.get('port'), function () {
    console.log("=====SERVEUR EN COURS D'EXECUTION=====");
});