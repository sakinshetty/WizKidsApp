using WizKidAPI.DTO;

namespace WizKidAPI.Service
{
    public interface IWordPredictionService
    {

        public Task<GetSearchWordResponse> GetSearchWords(string SeachString);
    }
}
