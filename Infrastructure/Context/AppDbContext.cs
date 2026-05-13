using Domain.Common;
using Domain.Entities.Addresses;
using Domain.Entities.Aircraft;
using Domain.Entities.Airlines;
using Domain.Entities.Airports;
using Domain.Entities.Auth;
using Domain.Entities.CheckIn;
using Domain.Entities.Clients;
using Domain.Entities.Fares;
using Domain.Entities.Flights;
using Domain.Entities.Geography;
using Domain.Entities.Invoices;
using Domain.Entities.Maintenance;
using Domain.Entities.Passengers;
using Domain.Entities.Payments;
using Domain.Entities.People;
using Domain.Entities.Reservations;
using Domain.Entities.Routes;
using Domain.Entities.Staff;
using Domain.Entities.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public sealed class AppDbContext : DbContext
{
    // Geography
    public DbSet<Continent> Continents { get; set; } = default!;
    public DbSet<Country> Countries { get; set; } = default!;
    public DbSet<Region> Regions { get; set; } = default!;
    public DbSet<City> Cities { get; set; } = default!;

    // Addresses
    public DbSet<RoadType> RoadTypes { get; set; } = default!;
    public DbSet<Address> Addresses { get; set; } = default!;

    // People
    public DbSet<DocumentType> DocumentTypes { get; set; } = default!;
    public DbSet<Person> People { get; set; } = default!;
    public DbSet<EmailDomain> EmailDomains { get; set; } = default!;
    public DbSet<PhoneCode> PhoneCodes { get; set; } = default!;
    public DbSet<PersonEmail> PersonEmails { get; set; } = default!;
    public DbSet<PersonPhone> PersonPhones { get; set; } = default!;

    // Clients
    public DbSet<Client> Clients { get; set; } = default!;

    // Airlines & Aircraft
    public DbSet<Airline> Airlines { get; set; } = default!;
    public DbSet<AircraftManufacturer> AircraftManufacturers { get; set; } = default!;
    public DbSet<AircraftModel> AircraftModels { get; set; } = default!;
    public DbSet<Aircraft> Aircraft { get; set; } = default!;
    public DbSet<CabinType> CabinTypes { get; set; } = default!;
    public DbSet<CabinConfiguration> CabinConfigurations { get; set; } = default!;

    // Airports & Operations
    public DbSet<Airport> Airports { get; set; } = default!;
    public DbSet<AirportAirline> AirportAirlines { get; set; } = default!;

    // Staff
    public DbSet<StaffRole> StaffRoles { get; set; } = default!;
    public DbSet<Domain.Entities.Staff.Staff> Staff { get; set; } = default!;
    public DbSet<AvailabilityStatus> AvailabilityStatuses { get; set; } = default!;
    public DbSet<StaffAvailability> StaffAvailabilities { get; set; } = default!;

    // Routes
    public DbSet<FlightRoute> Routes { get; set; } = default!;
    public DbSet<RouteStop> RouteStops { get; set; } = default!;

    // Fares
    public DbSet<Season> Seasons { get; set; } = default!;
    public DbSet<PassengerType> PassengerTypes { get; set; } = default!;
    public DbSet<Fare> Fares { get; set; } = default!;

    // Flights
    public DbSet<FlightState> FlightStates { get; set; } = default!;
    public DbSet<FlightStatusTransition> FlightStatusTransitions { get; set; } = default!;
    public DbSet<Flight> Flights { get; set; } = default!;
    public DbSet<FlightRole> FlightRoles { get; set; } = default!;
    public DbSet<FlightAssignment> FlightAssignments { get; set; } = default!;
    public DbSet<SeatLocationType> SeatLocationTypes { get; set; } = default!;
    public DbSet<FlightSeat> FlightSeats { get; set; } = default!;
    public DbSet<FlightHistory> FlightHistories { get; set; } = default!;

    // Passengers
    public DbSet<Passenger> Passengers { get; set; } = default!;

    // Reservations
    public DbSet<ReservationStatus> ReservationStatuses { get; set; } = default!;
    public DbSet<ReservationStatusTransition> ReservationStatusTransitions { get; set; } = default!;
    public DbSet<Reservation> Reservations { get; set; } = default!;
    public DbSet<ReservationFlight> ReservationFlights { get; set; } = default!;
    public DbSet<ReservationPassenger> ReservationPassengers { get; set; } = default!;

    // Tickets
    public DbSet<TicketStatus> TicketStatuses { get; set; } = default!;
    public DbSet<Ticket> Tickets { get; set; } = default!;

    // CheckIn
    public DbSet<CheckInStatus> CheckInStatuses { get; set; } = default!;
    public DbSet<Domain.Entities.CheckIn.CheckIn> CheckIns { get; set; } = default!;

    // Invoices
    public DbSet<InvoiceItemType> InvoiceItemTypes { get; set; } = default!;
    public DbSet<Invoice> Invoices { get; set; } = default!;
    public DbSet<InvoiceItem> InvoiceItems { get; set; } = default!;

    // Payments
    public DbSet<PaymentState> PaymentStates { get; set; } = default!;
    public DbSet<PaymentMethodType> PaymentMethodTypes { get; set; } = default!;
    public DbSet<CardType> CardTypes { get; set; } = default!;
    public DbSet<CardIssuer> CardIssuers { get; set; } = default!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
    public DbSet<Payment> Payments { get; set; } = default!;

    // Auth
    public DbSet<SystemRole> SystemRoles { get; set; } = default!;
    public DbSet<Permission> Permissions { get; set; } = default!;
    public DbSet<RolePermission> RolePermissions { get; set; } = default!;
    public DbSet<AppUser> Users { get; set; } = default!;
    public DbSet<Session> Sessions { get; set; } = default!;

    // Maintenance (legacy, kept)
    public DbSet<MaintenanceType> MaintenanceTypes { get; set; } = default!;
    public DbSet<AircraftMaintenance> AircraftMaintenances { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
