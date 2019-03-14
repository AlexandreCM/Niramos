class Objet {
  constructor(idObjet, pointSpawn, typeArme) {
    this.idObjet = idObjet;

    this.pointSpawn = pointSpawn;
    this.typeArme = typeArme;
    
    this.dispo = true;
  }
};

module.exports = Objet;
