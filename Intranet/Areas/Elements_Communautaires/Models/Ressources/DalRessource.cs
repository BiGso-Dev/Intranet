﻿using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Areas.Composants.Models.BDD;
using Intranet.Areas.Composants.Models.Elements;
using Intranet.Areas.Elements_Generaux.Models;
using Intranet.Areas.Elements_Communautaires.Models.Medias;
using Intranet.Models;

namespace Intranet.Areas.Elements_Communautaires.Models.Ressources
{
    public class DalRessource : IDisposable
    {
        private BddContext bdd;

        public DalRessource()
        {
            bdd = new BddContext();
        }

        public void CreerRessource(string titre, string description)
        {
            // Recherche de la fraction "Ressource"
            Fraction rechercheRessourceDansFractions = bdd.Fractions.FirstOrDefault(fraction => fraction.Libelle.Contains("Ressource"));
            // Création de l'élément de type Ressource
            Element_Communautaire element = bdd.ElementsCommunautaires.Add(new Element_Communautaire());

            if (rechercheRessourceDansFractions == null)
            {
                // Création de l'élément de type Fraction
                Element_General elementFraction = bdd.ElementsGeneraux.Add(new Element_General());
                // Création de la fraction "Média"
                Fraction fraction = bdd.Fractions.Add(new Fraction { Libelle = "Ressource", Element = elementFraction });
                //bdd.Operations.Add(new Operation { Element = elementFraction, Type_Operation = Operation.Operations.Création });
                bdd.SaveChanges();
            }

            // Nouvelle recherche de la fraction "Ressource"
            Fraction fractionRessourceTrouvee = bdd.Fractions.FirstOrDefault(f => f.Libelle.Contains("Ressource"));
            Categorie categorie = bdd.Categories.FirstOrDefault(c => c.Element.Id == 1);
            if (rechercheRessourceDansFractions != null || fractionRessourceTrouvee != null)
            {
                element.Fraction = fractionRessourceTrouvee;
                Ressource ressource = bdd.Ressources.Add(new Ressource { Titre = titre, Description = description, Element= element, Categorie = categorie});
                //bdd.Operations.Add(new Operation { Element = element, Type_Operation = Operation.Operations.Création });
                bdd.SaveChanges();

                //Recherche du dernier média créé
                List<Media> medias = bdd.Medias.ToList();
                Media dernierMediaCree = medias.LastOrDefault();

                //Ajout du dernier média créé à la ressource
                AjouterUnMediaAUneRessource(ressource.Element.Id, dernierMediaCree);
                bdd.SaveChanges();
            }
        }

        public void AjouterUnMediaAUneRessource(int id, Media media)
        {
            Ressource ressourceTrouvee = bdd.Ressources.FirstOrDefault(ressource => ressource.Element.Id == id);
            if (ressourceTrouvee != null)
            {
                ressourceTrouvee.ListeMediasAssocies.Add(media);
                bdd.SaveChanges();
            }
        }

        public void AjouterUnThemeAUneRessource(Ressource ressource, string theme)
        {
            Element elementAAjouter = ExtraireElement(ressource);
            List<Theme> themes = bdd.Themes.ToList();
            Theme themeAAjouter = themes.FirstOrDefault(t => t.Libelle == theme);

            if (themeAAjouter != null)
            {
                Element elmt = bdd.Elements.FirstOrDefault(a => a.Id == elementAAjouter.Id);
                elmt.ListeThemesAssocies.Add(themeAAjouter);
                bdd.SaveChanges();
            }
        }

        public void ModifierRessource(int id, string titre, string description, List<Media> listeMediasAssocies)
        {
            Ressource ressourceTrouvee = bdd.Ressources.FirstOrDefault(ressource => ressource.Element.Id == id);
            if (ressourceTrouvee != null)
            {
                ressourceTrouvee.Titre = titre;
                ressourceTrouvee.Description = description;
                ressourceTrouvee.ListeMediasAssocies = listeMediasAssocies;
                bdd.SaveChanges();
            }
        }

        public void SupprimerRessource(int id)
        {
            Ressource ressourceTrouvee = bdd.Ressources.FirstOrDefault(ressource => ressource.Element.Id == id);

            if (ressourceTrouvee != null)
            {
                foreach (Media media in ressourceTrouvee.ListeMediasAssocies)
                {
                    bdd.Medias.Remove(media);
                    ressourceTrouvee.ListeMediasAssocies.Remove(media);
                    bdd.Elements.Remove(media.Element);
                }

                if (ressourceTrouvee.ListeMediasAssocies == null)
                {
                    bdd.Ressources.Remove(ressourceTrouvee);
                    bdd.SaveChanges();
                }
            }
        }

        public List<Ressource> ListerToutesLesRessources()
        {
            return bdd.Ressources.ToList();
        }

        public Element ExtraireElement(Ressource ressource)
        {
            return ressource.Element;
        }

        public void Dispose()
        {
            bdd.Dispose();
        }
    }
}