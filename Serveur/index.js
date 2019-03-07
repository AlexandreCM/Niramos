var express = require('express');
var app = express();
var Objet = require('./Objet.js');

var server = require('http').createServer(app);
var io = require('socket.io').listen(server);

app.set('port', process.env.PORT || 3000);

// Liste de tout les joueurs connectés au serveur
var clients = [];

var session1 = [];
var session2 = [];
var session3 = [];
var session4 = [];

const MAX_JOUEURS_PAR_SESSION = 4;
var listeObjets = [new Objet(1, 1, 2, 3), new Objet(2, 1, 2, 3), new Objet(3, 1, 2, 3)];

//io est une variable de socket.io regroupant des évènements
io.on("connection", function (socket) {
    var joueurCourant;
    var objetCourant;

    // Quand un joueur se connecte
    socket.on("USER_CONNECT", function () {
        console.log('Un joueur vient de se connecter au serveur mais n\'est pas encore identifié.');
    });

    // Quand un joueur veut jouer
    socket.on("PLAY", function (data) {

        joueurCourant = { nom: data.nom, position: data.position, vie: 100 }

        // On attribu le joueur à une session, si aucun session n'est disponible on lui indique.
        if (!attributionSessionAJoueur(joueurCourant)) {
            socket.emit("AUCUNE_SESSION_DISPO");
        }

        console.log(joueurCourant.nom + " s'est identifié, il peut maintenant jouer.");

        clients.push(joueurCourant);
        socket.emit("PLAY", joueurCourant);
        socket.broadcast.emit("USER_CONNECTED", joueurCourant);

        console.log("Joueurs connectés : ");
        for (var i = 0; i < clients.length; i++) {
            socket.emit("USER_CONNECTED", { nom: clients[i].nom, position: clients[i].position });
            console.log(clients[i].nom + " est connecté.");
        }
    });

    // Quand un joueur se déplace
    socket.on("MOVE", function (data) {

        joueurCourant = { nom: data.nom, position: data.position, direction: data.direction }

        socket.broadcast.emit("MOVE", joueurCourant);
        //console.log(joueurCourant.nom + " se déplace vers " + joueurCourant.position);
    });

    socket.on("ITEM_PICKUP", function (data) {
        var reponse;
        var reponseBroadcast;
        for (var i = 0; i < listeObjets.length; i++) {
            if (listeObjets[i].id != data.idObjet)
                break;

            if (listeObjets[i].dispo) {
                console.log("L'objet est disponible");
                reponse = { idObjet: data.idObjet, disponible: "True" }
                reponseBroadcast = { nomJoueur: data.nom, idObjet: data.idObjet }
                listeObjets[i].dispo = false;
                socket.broadcast.emit("PLAYER_PICKUP_ITEM", reponseBroadcast);
            } else {
                console.log("L'objet n'est pas disponible");
                reponse = { idObjet: data.idObjet, disponible: "False" }
            }
        }
        socket.emit("ITEM_PICKUP_RESPONSE", reponse);
    });

    socket.on("HIT", function (data) {
        // Appelée quand un joueur reçoit des dégas pour metre les autres au courant
        console.log("Le joueur " + data.nomJoueur + " a prit " + data.degat + " points de dégats");
        socket.broadcast.emit("PLAYER_LOSE_HEALTH", data);
    });

    socket.on("MORT", function (data) {
        console.log("Le joueur " + data.nomJoueur + " est mort");
        socket.broadcast.emit("UN_JOUEUR_EST_MORT", data);

        // On attend 5 secondes avant de faire respawn le joueur
        setTimeout(function(){
            var reponseBroadcast = data.nomJoueur;
            socket.broadcast.emit("RESPAWN", reponseBroadcast)
        }, 5000);
    });

    socket.on("SUPER_ATTAQUE", function (data) {
        console.log(data.nomJoueur + " a lancé une super-attaque à la position (" + data.x + "," + data.y + "," + data.z + ")");
        var reponseBroadcast = { joueurEmiteur: data.nomJoueur, posistion_x: data.x, position_y: data.y, position_z: data.z }
        socket.broadcast.emit("SUPER_ATTAQUE_LANCE", reponseBroadcast);
    });

    socket.on("DROP", function (data) {
        console.log(data.nomJoueur + " a drop un item");
        var reponseBroadcast = { nomJoueur: data.nomJoueur }
        socket.broadcast.emit("DROP_RESPONSE", reponseBroadcast);
    });

    socket.on("FLIP", function (data) {
        console.log(data.nomJoueur + " a flip");

        var reponseBroadcast = { nomJoueur: data.nomJoueur, direction: data.direction }
        socket.broadcast.emit("FLIP_RESPONSE", reponseBroadcast);
    });

    //Sert à afficher les sessions disponibles au joueur
    // socket.on("SHOW_SESSIONS", function (data) {


    //     socket.emit("MOVE", joueurCourant);
    //     socket.broadcast.emit("MOVE", joueurCourant);
    //     console.log(joueurCourant.nom + " se déplace vers " + joueurCourant.position);
    // });

    // Quand un joueur se déconnecte
    socket.on("disconnect", function (data) {
        socket.broadcast.emit("USER_DISCONNECTED", joueurCourant);
        for (var i = 0; i < clients.length; i++) {
            if (clients[i].nom == joueurCourant.nom) {
                console.log("Joueur " + clients[i].nom + " s'est déconnecté.");
                clients.splice(i, 1);
            }
        }
    });
});

function attributionSessionAJoueur(joueurCourant) {
    if (session1.length < MAX_JOUEURS_PAR_SESSION) {
        session1.push(joueurCourant);
    } else if (session2.length < MAX_JOUEURS_PAR_SESSION) {
        session2.push(joueurCourant);
    }
    else if (session3.length < MAX_JOUEURS_PAR_SESSION) {
        session3.push(joueurCourant);
    } else if (session4.length < MAX_JOUEURS_PAR_SESSION) {
        session4.push(joueurCourant);
    } else {
        return false;
    }
    return true;
}

// Démarrage du serveur
server.listen(app.get('port'), function () {
    console.log("=====SERVEUR EN COURS D'EXECUTION SUR LE PORT D'ECOUTE " + app.get('port') + " =====");
});