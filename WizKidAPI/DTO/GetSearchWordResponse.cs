using System.Collections.Generic;
using WizKidAPI.Database.DBModel;

namespace WizKidAPI.DTO
{
    public class GetSearchWordResponse
    {

        public string[]? ValuePredictionService { get; set; }
        public List<WordsDto>? ValueCustomDictionary { get; set; }




    }
}
