using AutoMapper;
using Data.ClientModels.CustomerService;
using Data.Models.OracleDb;

namespace Data.Repository
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<CTApplications_PendingChanges, PendingChange>()
                    .ForMember(dest => dest.PaxId, opt => opt.MapFrom(src => src.PAX_ID))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FIRST_NAME))
                    .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.FAMILY_NAME))
                    .ForMember(dest => dest.BookingType, opt => opt.MapFrom(src => src.BOOKING_USER_FIELD_1))
                    .ForMember(dest => dest.BookingNumber, opt => opt.MapFrom(src => src.BOOKING_NO))
                    .ForMember(dest => dest.ComponentNumber, opt => opt.MapFrom(src => src.COMPONENT_NO))
                    .ForMember(dest => dest.Season, opt => opt.MapFrom(src => src.SEASON))
                    .ForMember(dest => dest.ProdCode, opt => opt.MapFrom(src => src.PROD_CODE))
                    .ForMember(dest => dest.MarketId, opt => opt.MapFrom(src => src.MARKET_ID));

                config.CreateMap<CTBookings_Passenger, BookingPassenger>()
                    .ForMember(dest => dest.PaxId, opt => opt.MapFrom(src => src.PAX_ID))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FIRST_NAME))
                    .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.FAMILY_NAME))
                    .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MIDDLE_NAME))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.GENDER))
                    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DOB))

                    .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PSPT_NO))
                    .ForMember(dest => dest.PassportNationality, opt => opt.MapFrom(src => src.PSPT_NATIONALITY))
                    .ForMember(dest => dest.PassportBirthPlace, opt => opt.MapFrom(src => src.PSPT_BIRTH_PLACE))
                    .ForMember(dest => dest.PassportExpiryDate, opt => opt.MapFrom(src => src.PSPT_EXP_DATE))
                    .ForMember(dest => dest.PassportIssueDate, opt => opt.MapFrom(src => src.PSPT_ISS_DATE))

                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.PERSON_TITLE));

                config.CreateMap<CTPassengers_Passenger, BookingPassenger>()
                    .ForMember(dest => dest.PaxId, opt => opt.MapFrom(src => src.PAX_ID))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FIRST_NAME))
                    .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.FAMILY_NAME))
                    .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MIDDLE_NAME))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.GENDER))
                    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DOB))

                    .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PSPT_NO))
                    .ForMember(dest => dest.PassportNationality, opt => opt.MapFrom(src => src.PSPT_NATIONALITY))
                    .ForMember(dest => dest.PassportBirthPlace, opt => opt.MapFrom(src => src.PSPT_BIRTH_PLACE))
                    .ForMember(dest => dest.PassportExpiryDate, opt => opt.MapFrom(src => src.PSPT_EXP_DATE))
                    .ForMember(dest => dest.PassportIssueDate, opt => opt.MapFrom(src => src.PSPT_ISS_DATE))

                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.PERSON_TITLE));
            });
        }
    }
}
