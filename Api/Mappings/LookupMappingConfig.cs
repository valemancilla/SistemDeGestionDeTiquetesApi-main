using Api.Dtos.Common;
using Domain.Common;
using Domain.Entities.Addresses;
using Domain.Entities.Aircraft;
using Domain.Entities.Auth;
using Domain.Entities.CheckIn;
using Domain.Entities.Flights;
using Domain.Entities.Geography;
using Domain.Entities.Invoices;
using Domain.Entities.Payments;
using Domain.Entities.Reservations;
using Domain.Entities.Staff;
using Domain.Entities.Tickets;
using Mapster;

namespace Api.Mappings;

public sealed class LookupMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // All LookupEntity subclasses share the same mapping: Id + Nombre → LookupDto
        config.NewConfig<Continent, LookupDto>();
        config.NewConfig<RoadType, LookupDto>();
        config.NewConfig<CabinType, LookupDto>();
        config.NewConfig<FlightState, LookupDto>();
        config.NewConfig<FlightRole, LookupDto>();
        config.NewConfig<SeatLocationType, LookupDto>();
        config.NewConfig<StaffRole, LookupDto>();
        config.NewConfig<AvailabilityStatus, LookupDto>();
        config.NewConfig<ReservationStatus, LookupDto>();
        config.NewConfig<TicketStatus, LookupDto>();
        config.NewConfig<CheckInStatus, LookupDto>();
        config.NewConfig<InvoiceItemType, LookupDto>();
        config.NewConfig<PaymentState, LookupDto>();
        config.NewConfig<PaymentMethodType, LookupDto>();
        config.NewConfig<CardType, LookupDto>();
        config.NewConfig<CardIssuer, LookupDto>();
        config.NewConfig<SystemRole, LookupDto>()
            .Map(dest => dest.Nombre, src => src.Nombre);
        config.NewConfig<Permission, LookupDto>()
            .Map(dest => dest.Nombre, src => src.Nombre);
    }
}
