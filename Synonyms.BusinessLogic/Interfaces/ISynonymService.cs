using Synonyms.BusinessLogic.Dto;
using System.Collections.Generic;

namespace Synonyms.BusinessLogic.Interfaces
{
    public interface ISynonymService
    {
        List<SynonymDto> Merge();
        void Add(SynonymDto item);
    }
}
