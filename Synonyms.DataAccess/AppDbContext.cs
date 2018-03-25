using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Synonyms.DataAccess.Models;

namespace Synonyms.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Synonym> Synonyms { get; set; }

        public AppDbContext() : base("name=SynonymsConnection")
        {

        }
    }
}
