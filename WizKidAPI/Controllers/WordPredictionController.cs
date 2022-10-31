using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizKidAPI.Service;


namespace WizKidAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WordPredictionController : ControllerBase
    {
        public readonly IWordPredictionService _wordPredService;
        public WordPredictionController(IWordPredictionService wordPredictionService)
        {
            _wordPredService = wordPredictionService;
        }
        
        [HttpGet]
        [Route("searchword/{searchString}")]
        [ProducesResponseType(StatusCodes.Status200OK)]       
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> SearchWord(string searchString)
        {
           var  matchWordsResp = await _wordPredService.GetSearchWords(searchString);            
            if (matchWordsResp == null)
            {
                return NotFound();
            }            
            return  Ok(matchWordsResp);
        }
    }
}
