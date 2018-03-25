using Synonyms.BusinessLogic.Dto;
using Synonyms.BusinessLogic.Interfaces;
using Synonyms.BusinessLogic.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Synonyms.DataAccess.Interfaces;
using Synonyms.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace Synonyms.Tests
{
    [TestClass]
    public class SynonymServiceTests
    {
        private IRepository<Synonym> synonymRepository;
        private ISynonymService synonymService;

        public SynonymServiceTests()
        {
            var testSynonyms = new List<Synonym>()
            {
                new Synonym
                {
                    Term = "computer",
                    SynonymList = "laptop,notebook"
                },
                new Synonym
                {
                    Term = "laptop",
                    SynonymList = "dell,macbook"
                }
            };

            var synonymMockRepository = new Mock<IRepository<Synonym>>();
            synonymMockRepository.Setup(sr => sr.GetAll()).Returns(testSynonyms);
            synonymMockRepository.Setup(sr => sr.Insert(It.IsAny<Synonym>())).Callback((Synonym s) => testSynonyms.Add(s));

            synonymRepository = synonymMockRepository.Object;
            synonymService = new SynonymService(synonymRepository);

        }

        [TestMethod]
        public void CanMergeSynonyms()
        {
            var mergedSynonyms = synonymService.Merge();

            var testSynonyms = new List<SynonymDto>
            {
                new SynonymDto()
                {
                    Term = "computer",
                    Synonyms = new List<string>{ "laptop", "notebook" }
                },
                new SynonymDto()
                {
                    Term = "laptop",
                    Synonyms = new List<string>{ "computer", "dell", "macbook" }
                },
                new SynonymDto()
                {
                    Term = "notebook",
                    Synonyms = new List<string>{ "computer" }
                },
                new SynonymDto()
                {
                    Term = "dell",
                    Synonyms = new List<string>{ "laptop" }
                },
                new SynonymDto()
                {
                    Term = "macbook",
                    Synonyms = new List<string>{ "laptop" }
                }
            };

            Assert.AreEqual(testSynonyms.Count, mergedSynonyms.Count);

            var testTerms = testSynonyms.Select(s => s.Term).ToList();
            var mergedTerms = mergedSynonyms.Select(s => s.Term).ToList();

            CollectionAssert.AreEqual(testTerms, mergedTerms);
        }
    }
}
