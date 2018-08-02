﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Intranet.Areas.Composants.Models.Elements;
using Intranet.Areas.Elements_Communautaires.Controllers.Dal;
using Intranet.Areas.Elements_Communautaires.Controllers.Parent;
using Intranet.Areas.Elements_Communautaires.Models.Ressources;
using Intranet.Areas.Elements_Communautaires.ViewModels.Creer;
using Intranet.Areas.Elements_Generaux.Models;

namespace Intranet.Areas.Elements_Communautaires.Controllers
{
    public class ModifierController : Element_Communautaire_Objet_Controller
    {
        private IDal_Element_Communautaire_Objet_Controller dalController = new Dal_Element_Communautaire_Objet_Controller();

        // GET: Elements_Communautaires/Modifier
        public ActionResult Index()
        {
            return View();
        }

        #region Ressource
        // GET: Modifier/Ressource
        public ActionResult Ressource(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Element elementLieElementTrouve = dalElementCommunautaire.RetournerElementLie(id);
            if (elementLieElementTrouve == null)
            {
                return HttpNotFound();
            }
            else
            {
                Ressource ressourceTrouvee = dalElementCommunautaire.RetournerElementCommunautaireTrouve(ressource, id);
                RessourceViewModel model = new RessourceViewModel
                {
                    Titre = ressourceTrouvee.Titre,
                    Description = ressourceTrouvee.Description,
                    ListeMediasAssocies = ressourceTrouvee.ListeMediasAssocies,
                    ListeThemesSelectionnes = ressourceTrouvee.Element.ListeThemesAssocies,
                    CategorieSelectionnee = ListeCategories(ressourceTrouvee.Categorie.Id),
                    Categorie = ressourceTrouvee.Categorie.Id
                };
                model.Themes = ListeThemes(model.ListeThemesSelectionnes);
                return View(model);
                // Position de la catégorie courante dans la liste déroulante
            }




            //if (dalElementGeneral.Lister(categorie) != null && dalElementGeneral.Lister(theme) != null)
            //{
            //    RessourceViewModel model = new RessourceViewModel
            //    {
            //        CategorieSelectionnee = ListeCategories(null)
            //    };
            //    model.Themes = ListeThemes(model.ListeThemesSelectionnes);

            //    return dalController.Creer(model);
            //}
            //else
            //{
            //    return View("Error");
            //}
        }

        // POST: Modifier/Ressource
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ressource(RessourceViewModel ressourceACreer, int? id)
        {
            
            












            //Initialisation des listes de thèmes et catégories
            if (ressourceACreer.Themes.Count() > 0)
            {
                foreach (var themeACoche in ressourceACreer.Themes)
                {
                    if (themeACoche.EstCoche)
                    {
                        Theme themeAAjouter = dalElementGeneral.RetournerElementGeneralTrouve(theme, themeACoche.ID);
                        themeACoche.Element = themeAAjouter;
                        ressourceACreer.ListeThemesSelectionnes.Add(themeAAjouter);
                    }
                }
            }
            ressourceACreer.CategorieSelectionnee = ListeCategories(null);
            ressourceACreer.Themes = ListeThemes(ressourceACreer.ListeThemesSelectionnes);

            //Traitement
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Le formulaire comporte des erreurs.");
                return View(ressourceACreer);
            }
            else
            {
                return dalController.Creer(ressourceACreer, ressource);
            }
        }
        #endregion

        #region Media
        //// GET: Creer/Media
        //public ActionResult Media()
        //{
        //    return dalController.Creer(media.GetType().Name, media);
        //}

        //// POST: Creer/Fraction
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Media([Bind(Include = "Id,Libelle,Element")] Media mediaACreer)
        //{
        //    return dalController.Creer(mediaACreer);
        //}
        #endregion
    }
}