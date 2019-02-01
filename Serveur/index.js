var express = require('express');
var app = express();

var server = require('http').createServer(app);
var io = require('socket.io').listen(server);

app.set('port', process.env.PORT || 3000);

// Liste de tout les joueurs connectés au serveur
var clients = [];

//io est une variable de socket.io regroupant des évènements
io.on("connection", function (socket) {
    var joueurCourant;

    // Quand un joueur se connecte
    socket.on("USER_CONNECT", function () {
        console.log('Un joueur vient de se connecter');
        for (var i = 0; i < clients.length; i++) {
            socket.emit("USER_CONNECTED", { nom: clients[i].nom, position: clients[i].position });
        }
        console.log("Joueur : " + clients[i].nom + " est connecté.")
    });

    // Quand un joueur veut jouer
    socket.on("PLAY", function (data) {
        joueurCourant = {
            nom: data.nom,
            position: data.position
        }

        clients.push(joueurCourant);
        socket.emit("PLAY", joueurCourant);
        socket.broadcast.emit("USER_CONNECTED", joueurCourant);
    });

    // Quand un joueur se déplace
    socket.on("MOVE", function (data) {
        joueurCourant.position = data.position;
        socket.emit("MOVE", joueurCourant);
        socket.broadcast.emit("MOVE", joueurCourant);
        console.log(joueurCourant.nom + "se déplace vers " + joueurCourant.position);
    })

    // Quand un joueur se déconnecte
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


// Démarrage du serveur
server.listen(app.get('port'), function () {
    console.log("=====SERVEUR EN COURS D'EXECUTION=====");
});