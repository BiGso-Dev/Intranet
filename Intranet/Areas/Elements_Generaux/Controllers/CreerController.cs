﻿using System.Web.Mvc;
using Intranet.Areas.Elements_Generaux.Models;

namespace Intranet.Areas.Elements_Generaux.Controllers
{
    public class CreerController : Element_General_Objet_Controller
    {
        // GET: Elements_Generaux/Creer
        public ActionResult Index()
        {
            return View();
        }

        #region Categorie
        // GET: Elements_Generaux/Creer/Categorie
        public ActionResult Categorie()
        {
            return View();
        }
        
        // POST: Elements_Generaux/Creer/Categorie
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Categorie([Bind(Include = "Id,Libelle,Element")] Categorie categorieACreer)
        {
            if (ModelState.IsValid)
            {
                dal.Creer(categorieACreer);
                return RedirectToAction("Categories","Afficher",null);
            }
            return View(categorieACreer);
        }
        #endregion

        #region Fraction
        // GET: Elements_Generaux/Creer/Fraction
        public ActionResult Fraction()
        {
            return View();
        }

        // POST: Elements_Generaux/Creer/Fraction
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Fraction([Bind(Include = "Id,Libelle,Element")] Fraction fractionACreer)
        {
            if (ModelState.IsValid)
            {
                dal.Creer(fractionACreer);
                return RedirectToAction("Fractions", "Afficher", null);
            }
            return View(fractionACreer);
        }
        #endregion

        #region Theme
        // GET: Elements_Generaux/Creer/Theme
        public ActionResult Theme()
        {
            return View();
        }

        // POST: Elements_Generaux/Creer/Theme
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Theme([Bind(Include = "Id,Libelle,Element")] Theme themeACreer)
        {
            if (ModelState.IsValid)
            {
                dal.Creer(themeACreer);
                return RedirectToAction("Themes","Afficher",null);
            }
            return View(themeACreer);
        }
        #endregion
    }
}