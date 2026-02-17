using AutoMapper;
using Exam.App.Domain;
using Exam.App.Dtos;

namespace Exam.App.Mappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ProfileDto>();
            CreateMap<Patient, PatientDTO>()
                .ForMember(
                    dest => dest.AnymalType,
                    opt => opt.MapFrom(src => src.AnimalType != null
                    ? src.AnimalType.Name
                    : string.Empty)
                )
                .ForMember(
                    dest => dest.OwnerName,
                    opt => opt.MapFrom(src => src.Owner !=null
                    ? src.Owner.Name
                    : string.Empty)
                )
                .ForMember(
                    dest => dest.VeterinarianName,
                    opt => opt.MapFrom(src => src.Veterinarian != null
                    ? src.Veterinarian.Name
                    : string.Empty)
                )
                .ForMember(
                    dest=>dest.Age,
                    opt=>opt.Ignore()
                );
        }
    }
}
