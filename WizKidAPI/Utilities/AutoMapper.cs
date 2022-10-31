using AutoMapper;
using WizKidAPI.Database.DBModel;
using WizKidAPI.DTO;

namespace WizKidAPI.Utilities
{
    public class APIMapper : Profile
    {
        public APIMapper()
        {
            CreateMap<WordsDB, WordsDto>();
        }
    }
}
