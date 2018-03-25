using System.Collections.Generic;
using System.Linq;
using Synonyms.BusinessLogic.Dto;
using Synonyms.BusinessLogic.Interfaces;
using Synonyms.DataAccess.Interfaces;
using Synonyms.DataAccess.Models;

namespace Synonyms.BusinessLogic.Services
{
    public class SynonymService : ISynonymService
    {
        private IRepository<Synonym> _synonymReopsitory;
        private List<SynonymDto> mergedSynonyms;

        public SynonymService(IRepository<Synonym> synonymReopsitory)
        {
            _synonymReopsitory = synonymReopsitory;
            mergedSynonyms = new List<SynonymDto>();
        }

        public void Add(SynonymDto item)
        {
            _synonymReopsitory.Insert(new Synonym
            {
                Term = item.Term,
                SynonymList = string.Join(",", item.Synonyms) 
            });
        }

        public List<SynonymDto> Merge()
        {
            // Assuming that if A is synonym of B then B is synonym of B
            // but if A is synonym of B and C is synonym of B then C is NOT synonym of A
            // *source: Exersice screenshot 
            // ("laptop" and "notebook" are synonyms to "computer" but they are not synonyms to each others)
            
            var storedSynonyms = _synonymReopsitory.GetAll();

            foreach(var synonym in storedSynonyms)
            {
                var synonymList = synonym.SynonymList.Split(',').ToList();
                MergeSynonym(synonym.Term, synonymList);
                synonymList.ForEach(s => MergeSynonym(s, new List<string> { synonym.Term }));
                
            }

            return mergedSynonyms;
        }

        private void MergeSynonym(string term, List<string> synonyms)
        {
            var existingSynonym = mergedSynonyms.FirstOrDefault(ms => ms.Term == term);

            if(existingSynonym == null)
            {
                mergedSynonyms.Add(new SynonymDto
                {
                    Term = term,
                    Synonyms = synonyms
                });
            }
            else
            {
                existingSynonym.Synonyms = existingSynonym.Synonyms.Union(synonyms).ToList();
            }

        }
    }
}
