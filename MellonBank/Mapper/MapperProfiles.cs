using AutoMapper;
using MellonBank.Models;
using MellonBank.ViewModels;

namespace MellonBank.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<CreateCustomerViewModel, User>();
            CreateMap<User, CreateCustomerViewModel>();

            CreateMap<ChangePasswordViewModel, User>();
            CreateMap<User, ChangePasswordViewModel>();

            CreateMap<CustomerDetailsViewModel, User>();
            CreateMap<User, CustomerDetailsViewModel>();

            CreateMap<BankAccountViewModel, AccountData>();
            CreateMap<AccountData, BankAccountViewModel>();

            CreateMap<EditViewModel, User>();
            CreateMap<User, EditViewModel>();

            CreateMap<AccountDataViewModel, AccountData>();
            CreateMap<AccountData, AccountDataViewModel>();

        }
    }
}
