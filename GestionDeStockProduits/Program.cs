using System;
using Produits;

namespace Gestion
{
    class Program
    {     
        private const int taillePermiseNomProduit = 44;
        private const int taillePermiseQuantite = 13;
        private const int taillePermiseEtat = 24;
        static void Main(String[] args)
        {
            int n = 1;
            List<Produit> produits = chargerProduitsDuFichier("./produits.txt");
            do{
                affichage(produits);

                // On entend que l'utilisatuer entre un id au clavier.
                //Console.WriteLine("Type something: ");
                var valeur = Console.ReadKey();
                
                int productIdToUpdateQuantite;
                bool parsable = int.TryParse(valeur.KeyChar.ToString(), out productIdToUpdateQuantite);
                if (parsable == true) {
                    // if n has been convert to int, We will update the stock of product which id equals n.
                    foreach(var p in produits)
                    {
                        if(p.Id == productIdToUpdateQuantite)
                        {
                            p.Quantite--;
                            if (p.Quantite <= 0) p.Quantite = 0;
                            p.State  = setProductState(p.Quantite);
                        }
                    }
                    if (productIdToUpdateQuantite == 0)
                    {
                        Environment.Exit(0);
                    }
                } 
                Console.Clear();
            } while(n != 0);
        }

         /**
            * Cette méthode est nécessaire lors de 
            * l'affichage des produits dans le tableau.
        */
        static void completerAffichage(int n)
        {
            for (int i = 0; i<n; i++) Console.Write(" ");
        }

        /**
            * Cette fonction retourne une liste contenant les ids des produits en cours.
            * Afin de mettre à jour les stocks des différents produits en juste saisissant
            * l'id d'un produit.
        */
        static List<int> registerAvailableProduitIds(List<Produit> products)
        {
            var ids = new List<int>();

            foreach (var item in products)
            {
                ids.Add(item.Id);
            }
            return ids;
        }

        /**
            * Cette méthode permet de charger les enregistrements contenus dans
            * le fichier produits.txt. Un enregistrement représente un produit.  
        */        
        static List<Produit> chargerProduitsDuFichier(string filePath)
        {
            var lineProduitsArray = File.ReadAllLines(filePath).ToList();
            var produits = new List<Produit>();

            int id;
            int stock;
            string name;
            Etat etat;
            string[] productDataArray;

            foreach( var line in lineProduitsArray)
            {
                productDataArray = line.Split(":");

                int.TryParse(productDataArray[0], out id);
                
                name = productDataArray[1];

                int.TryParse(productDataArray[2], out stock);
                
                etat = setProductState(stock);

                Produit p = new Produit(id, name, stock, etat);
               
                produits.Add(p);
            }
            return produits;
        }

        /**
            * Cette méthode prend en paramètre un objet Etat et
            * retourne sa valeur en chaine de caractères.
        */
        static string matchEtatToString(Etat etat)
        {
            string state;

            switch (etat)
            {
                case Etat.DISPONIBLE:
                    state = "Disponible";
                break;
                case Etat.RUPTURE:
                    state = "Rupture";
                break;
                case Etat.REAPPROVISIONNEMENT:
                    state = "Réapprovisionnement";
                break;
                default: state = "Rupture";
                break;
            }
            return state;
        }

        /**
            * Cette méthode réalise l'affichage 
            * des produits dans le tableau. 
        */
        static void affichage(List<Produit> produits)
        {
            Console.WriteLine("\n");
            Console.WriteLine("+---+--------------------------------------------+---------------+-------------------------+");
            Console.WriteLine("| # | Produit(s)                                 |  Quantite(s)  |           Etat          |");
            Console.WriteLine("+---+--------------------------------------------+---------------+-------------------------+");

            foreach (var item in produits)
            {
                ConsoleColor foreGroundColorToPrintWith = ConsoleColor.Black;
                if (item.Quantite == 0)
                {
                    foreGroundColorToPrintWith = ConsoleColor.Red;
                }
                
                if (item.Quantite > 0 && item.Quantite <= 5)
                {
                    foreGroundColorToPrintWith = ConsoleColor.DarkYellow;
                }
                Console.BackgroundColor = foreGroundColorToPrintWith;
                string id = "";
                if(item.Id<10) {
                    id ="| "+item.Id+" | ";
                }else {
                    id ="|"+item.Id+" | ";
                }
                Console.Write(id);  
                
                Console.Write(item.NomProduit);
                completerAffichage(taillePermiseNomProduit - item.NomProduit.Length - 1);
                Console.Write("| ");
                
                string qteStr = item.Quantite.ToString();
                Console.Write(qteStr+" ");
                completerAffichage(taillePermiseQuantite - qteStr.Length);
                Console.Write("| ");
                string etatQuantite = matchEtatToString(item.State);
                Console.Write(etatQuantite);
                completerAffichage(taillePermiseEtat - etatQuantite.Length);
                Console.Write("|\n");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("+---+--------------------------------------------+---------------+-------------------------+");
        }

        /**
            * n fonction du stock d'un produit, 
            * son état exact est retourné.
        */
        static Etat setProductState(int quantite)
        {
            Etat state = Etat.RUPTURE;
            
            if (quantite > 5) state = Etat.DISPONIBLE;
            if (quantite <= 5 && quantite > 0) state = Etat.REAPPROVISIONNEMENT;
            if (quantite <= 0) state = Etat.RUPTURE;
            
            return state;
        }
    }   
}