class Objet {
  constructor(idObjet, x, y, z) {
    this.id = idObjet;

    this.x = x;
    this.y = y;
    this.z = z;

    this.dispo = true;
  }
};

module.exports = Objet;