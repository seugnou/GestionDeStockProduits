namespace Produits
{
    public enum Etat
    {
        DISPONIBLE,
        RUPTURE,
        REAPPROVISIONNEMENT

    }
    class Produit
    {
        public int Id
        {set; get;}

        public string NomProduit
        {set; get;}

        public int Quantite
        {set; get;}

        public Etat State
        {set; get;}

        // Constructeur sans paramètres
        public Produit()
        {
            Id = 0;
            NomProduit = "";
            Quantite = 0;
            State = Etat.RUPTURE;
        }

        // Constructeur avec paramètres
        public Produit(int id, string nom, int quantite, Etat state)
        {
            Id = id;
            NomProduit = nom;
            Quantite = quantite;
            State = state;
        }

        public void afficher()
        {
            Console.WriteLine("id: " + this.Id + ", Nom: "+ this.NomProduit + ", Quantite: "+ this.Quantite + ", Etat: " +this.State);
        }
    }
}