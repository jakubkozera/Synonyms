using Synonyms.DataAccess.Interfaces;
using Synonyms.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synonyms.DataAccess.Repositories
{
    public class SynonymRepository : IRepository<Synonym>
    {
        public IList<Synonym> GetAll()
        {
            using(var dbContext = new AppDbContext())
            {
                return dbContext.Synonyms.AsNoTracking().ToList();
            }
        }

        public void Insert(Synonym item)
        {
            using(var dbContext = new AppDbContext())
            {
                dbContext.Synonyms.Add(item);
                dbContext.SaveChanges();
            }
        }
    }
}
