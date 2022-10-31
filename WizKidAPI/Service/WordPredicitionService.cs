using AutoMapper;
using WizKidAPI.Client;
using WizKidAPI.Database.DBModel;
using WizKidAPI.DTO;
using WizKidAPI.Repository;
using HttpClientFactory = WizKidAPI.Client.HttpClientFactory;

namespace WizKidAPI.Service
{
    public class WordPredicitionService : IWordPredictionService
    {
        public readonly IGenericRepository<WordsDB> _genericRepository;
        public readonly ILogger<WordPredicitionService> _logger;
        public readonly IMapper _mapper;
        public HttpClientFactory _httpClientFactory;



        public WordPredicitionService(IGenericRepository<WordsDB> genericRepository,
            ILogger<WordPredicitionService> logger,
            IMapper mapper,
            HttpClientFactory httpClientFactory)
        {
            _genericRepository = genericRepository;
            _logger = logger;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetSearchWordResponse> GetSearchWords(string SeachString)
        {
            try
            {
                var matchWords = await _genericRepository.Get(x => x.Value.ToUpper().StartsWith(SeachString.ToUpper()));               
                var getPredictionWord = await _httpClientFactory.OnGet("en-GB", SeachString);          

                if(matchWords.Count != 0 || getPredictionWord.Length !=0)
                {
                    var searchWordResp = new GetSearchWordResponse()
                    {
                        ValuePredictionService = getPredictionWord,
                        ValueCustomDictionary = _mapper.Map<List<WordsDB>, List<WordsDto>>((List<WordsDB>)matchWords.ToList())
                    };
                    return searchWordResp;
                }
                return null;                
            }         
            catch (Exception ex)
            {
                _logger.LogError($"ExceptionHandler: {ex}");
                throw;
            }

        }
    }
}

