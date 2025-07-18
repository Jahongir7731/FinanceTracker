using AutoMapper;
using FinanceTracker.Application.DTOs.Income;
using FinanceTracker.Domain.Entities;


namespace FinanceTracker.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Income, IncomeDto>().ReverseMap();
            CreateMap<CreateIncomeDto, Income>();
            CreateMap<UpdateIncomeDto, Income>();

        }
    }
}
