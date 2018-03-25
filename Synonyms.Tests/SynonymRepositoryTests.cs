using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Synonyms.DataAccess.Interfaces;
using Synonyms.DataAccess.Models;
using Synonyms.DataAccess.Repositories;

namespace Synonyms.Tests
{
    [TestClass]
    public class SynonymRepositoryTests
    {
        private IRepository<Synonym> synonymRepository;

        public SynonymRepositoryTests()
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
                    Term = "beautiful",
                    SynonymList = "pretty,attractive"
                }
            };

            var synonymMockRepository = new Mock<IRepository<Synonym>>();
            synonymMockRepository.Setup(sr => sr.GetAll()).Returns(testSynonyms);
            synonymMockRepository.Setup(sr => sr.Insert(It.IsAny<Synonym>())).Callback((Synonym s) => testSynonyms.Add(s));


            synonymRepository = synonymMockRepository.Object;

        }

        [TestMethod]
        public void CanRetrieveAllSynonyms()
        {
            var testSynonyms = synonymRepository.GetAll();

            Assert.IsNotNull(testSynonyms);
            Assert.AreEqual(2, testSynonyms.Count);
        }

        [TestMethod]
        public void CanInsertSynonym()
        {
            var preInsertSynonymsCount = synonymRepository.GetAll().Count;

            synonymRepository.Insert(new Synonym
            {
                Term = "outgoing",
                SynonymList = "sociable,extroverted"
            });

            var synonymsCount = synonymRepository.GetAll().Count;

            Assert.AreEqual(preInsertSynonymsCount + 1, synonymsCount);

        }
    }
}
