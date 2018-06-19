﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intranet.Areas.Composants.Models.BDD;
using Intranet.Areas.Composants.Models.Collaborateurs;
using Intranet.Areas.Elements_Generaux.Models;
using Intranet.Areas.Elements_Generaux.Models.Fractions;
using Intranet.Areas.Elements_Generaux.Models.Themes;

namespace Intranet.Areas.Composants.Models.Elements
{
    public class Element
    {
        [Key]
        public int Id { get; set; }

        
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Non défini")]
        public Collaborateur Collaborateur { get; set; }

        public enum Etats { Publié, En_attente_de_traitement, Modifié, Masqué }
        public Etats Etat { get; set; }

        public virtual ICollection<Theme> ListeThemesAssocies { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Fraction")]
        public Fraction Fraction { get; set; }

        public Element()
        {
            this.Etat = Etats.Publié;
            this.ListeThemesAssocies = new HashSet<Theme>();
        }
    }
}