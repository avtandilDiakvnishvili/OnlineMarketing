using AutoMapper;
using BusinessLogic.ViewModels;
using DataAccess.Models;
using OnlineMarketing.ViewModels;

namespace BusinessLogic.MapperProfiles
{
    internal class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            _ = CreateMap<ProductViewModel, Product>();
            _ = CreateMap<Product, ProductViewModel>();
            _ = CreateMap<PersonalDocument, PersonalDocsViewModel>();
            _ = CreateMap<PersonalDocsViewModel, PersonalDocument>();

            _ = CreateMap<PersonalContact, ContactViewModel>();
            _ = CreateMap<ContactViewModel, PersonalContact>();


            _ = CreateMap<Sale, SaleViewModel>()
                .ForMember(f => f.DistributorName,
                           opt => opt.MapFrom(src => src.Distributor != null ? $"{src.Distributor.FirstName} {src.Distributor.LastName}" : string.Empty))
                .ForMember(f => f.SaleProducts, opt => opt.MapFrom(src => src.ProductList));

            _ = CreateMap<SaleViewModel, Sale>()
                .ForMember(f => f.TotalPrice, opt => opt.MapFrom(src => src.SaleProducts != null ? src.SaleProducts.Sum(s => s.ProductSalePrice) : 0))
                .ForMember(f => f.ProductList, opt => opt.MapFrom(src => src.SaleProducts));


            _ = CreateMap<SaleProductsViewModel, SalesProduct>();
            _ = CreateMap<SalesProduct, SaleProductsViewModel>().
                ForMember(f => f.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
                .ForMember(f => f.Code, opt => opt.MapFrom(src => src.Product != null ? src.Product.Code : string.Empty));





            _ = CreateMap<AddressViewModel, DistributorAddress>();

            _ = CreateMap<DistributorAddress, AddressViewModel>();


            _ = CreateMap<Distributor, DistributorViewModel>()
                .ForMember(f => f.PersonalDocuments, opt => opt.MapFrom(src => src.PersonalDocuments))
                .ForMember(f => f.Contacts, opt => opt.MapFrom(src => src.PersonalContacts))
                .ForMember(f => f.Addresses, opt => opt.MapFrom(src => src.DistributorAddress));

            _ = CreateMap<DistributorViewModel, Distributor>()
                .ForMember(f => f.PersonalDocuments, opt => opt.MapFrom(src => src.PersonalDocuments))
                .ForMember(f => f.PersonalContacts, opt => opt.MapFrom(src => src.Contacts))
                .ForMember(f => f.DistributorAddress, opt => opt.MapFrom(src => src.Addresses));

        }
    }
}
