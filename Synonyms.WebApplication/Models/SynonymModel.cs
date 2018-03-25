using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Synonyms.WebApplication.Models
{
    public class SynonymModel
    {
        [Required]
        public string Term { get; set; }
        [Required]
        public string Synonyms { get; set; }
    }
}