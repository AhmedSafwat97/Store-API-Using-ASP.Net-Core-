using AutoMapper;
using Talabat.API.Dtos;
using Talabat.Core.Enities;

namespace Talabat.API.Helper

    //this Helperprofile Class Must Implement Profile THat in Auto Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // we use ToReverse() if the Dto Make Create or update or Delete
            //to map the object to string We use ForMember That take the Dustination and the 
            //Strint that we want to map from it using MapFrom Method
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Brand, O => O.MapFrom(S => S.Brand.Name))
                .ForMember(d => d.Category, O => O.MapFrom(S => S.Category.Name))
                // We Make Class Named ProductPictureUrlResolver And Emplement Interface
                // IValueResolver 
                // We Using the Genaric MapFrom 
                // We amke this to return the full Url In the Json Response
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());


            CreateMap<AddProductDto, Product>();


        }

    }
}
