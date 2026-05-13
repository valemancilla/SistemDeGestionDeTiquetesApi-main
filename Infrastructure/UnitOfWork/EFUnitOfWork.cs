using Application.Abstractions;
using Domain.Common;
using Domain.Entities.Addresses;
using Domain.Entities.Aircraft;
using Domain.Entities.Airports;
using Domain.Entities.Auth;
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
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Aircraft;
using Infrastructure.Repositories.Airlines;
using Infrastructure.Repositories.Auth;
using Infrastructure.Repositories.CheckIn;
using Infrastructure.Repositories.Clients;
using Infrastructure.Repositories.Flights;
using Infrastructure.Repositories.Invoices;
using Infrastructure.Repositories.Payments;
using Infrastructure.Repositories.Reservations;
using Infrastructure.Repositories.Routes;
using Infrastructure.Repositories.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork;

public sealed class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _ctx;
    private readonly Dictionary<Type, object> _genericRepos = new();

    private static readonly Dictionary<Type, Func<AppDbContext, object>> _repoFactories = new()
    {
        [typeof(Country)]     = ctx => new EfRepositoryWithIncludes<Country>(ctx,     q => q.Include(x => x.Continente)),
        [typeof(Region)]      = ctx => new EfRepositoryWithIncludes<Region>(ctx,      q => q.Include(x => x.Pais)),
        [typeof(City)]        = ctx => new EfRepositoryWithIncludes<City>(ctx,        q => q.Include(x => x.Region)),
        [typeof(Address)]     = ctx => new EfRepositoryWithIncludes<Address>(ctx,     q => q.Include(x => x.TipoVia).Include(x => x.Ciudad)),
        [typeof(Person)]      = ctx => new EfRepositoryWithIncludes<Person>(ctx,      q => q.Include(x => x.TipoDocumento)),
        [typeof(PersonEmail)] = ctx => new EfRepositoryWithIncludes<PersonEmail>(ctx, q => q.Include(x => x.DominioEmail)),
        [typeof(PersonPhone)] = ctx => new EfRepositoryWithIncludes<PersonPhone>(ctx, q => q.Include(x => x.CodigoTelefono)),
        [typeof(AircraftManufacturer)] = ctx => new EfRepositoryWithIncludes<AircraftManufacturer>(ctx, q => q.Include(x => x.Pais)),
        [typeof(AircraftModel)] = ctx => new EfRepositoryWithIncludes<AircraftModel>(ctx, q => q.Include(x => x.Fabricante!).ThenInclude(f => f.Pais)),
        [typeof(CabinConfiguration)] = ctx => new EfRepositoryWithIncludes<CabinConfiguration>(ctx, q => q.Include(x => x.Aeronave).Include(x => x.TipoCabina)),
        [typeof(Airport)]     = ctx => new EfRepositoryWithIncludes<Airport>(ctx,     q => q.Include(x => x.Ciudad)),
        [typeof(AirportAirline)] = ctx => new EfRepositoryWithIncludes<AirportAirline>(ctx, q => q.Include(x => x.Aeropuerto).Include(x => x.Aerolinea)),
        [typeof(Domain.Entities.Staff.Staff)] = ctx => new EfRepositoryWithIncludes<Domain.Entities.Staff.Staff>(ctx, q => q.Include(x => x.Persona).Include(x => x.Cargo).Include(x => x.Aerolinea).Include(x => x.Aeropuerto)),
        [typeof(StaffAvailability)] = ctx => new EfRepositoryWithIncludes<StaffAvailability>(ctx, q => q.Include(x => x.EstadoDisponibilidad)),
        [typeof(RouteStop)]   = ctx => new EfRepositoryWithIncludes<RouteStop>(ctx,   q => q.Include(x => x.AeropuertoEscala)),
        [typeof(Fare)]        = ctx => new EfRepositoryWithIncludes<Fare>(ctx,        q => q.Include(x => x.TipoCabina).Include(x => x.TipoPasajero).Include(x => x.Temporada)),
        [typeof(FlightStatusTransition)] = ctx => new EfRepositoryWithIncludes<FlightStatusTransition>(ctx, q => q.Include(x => x.EstadoOrigen).Include(x => x.EstadoDestino)),
        [typeof(FlightAssignment)] = ctx => new EfRepositoryWithIncludes<FlightAssignment>(ctx, q => q.Include(x => x.Personal).ThenInclude(p => p!.Persona).Include(x => x.RolVuelo)),
        [typeof(FlightSeat)]  = ctx => new EfRepositoryWithIncludes<FlightSeat>(ctx,  q => q.Include(x => x.TipoCabina).Include(x => x.TipoUbicacion)),
        [typeof(FlightHistory)] = ctx => new EfRepositoryWithIncludes<FlightHistory>(ctx, q => q.Include(x => x.EstadoAnterior).Include(x => x.EstadoNuevo)),
        [typeof(Passenger)]   = ctx => new EfRepositoryWithIncludes<Passenger>(ctx,   q => q.Include(x => x.Persona).Include(x => x.TipoPasajero)),
        [typeof(ReservationStatusTransition)] = ctx => new EfRepositoryWithIncludes<ReservationStatusTransition>(ctx, q => q.Include(x => x.EstadoOrigen).Include(x => x.EstadoDestino)),
        [typeof(ReservationFlight)] = ctx => new EfRepositoryWithIncludes<ReservationFlight>(ctx, q => q.Include(x => x.Vuelo)),
        [typeof(ReservationPassenger)] = ctx => new EfRepositoryWithIncludes<ReservationPassenger>(ctx, q => q.Include(x => x.Pasajero).ThenInclude(p => p!.Persona)),
        [typeof(InvoiceItem)] = ctx => new EfRepositoryWithIncludes<InvoiceItem>(ctx, q => q.Include(x => x.TipoItem)),
        [typeof(PaymentMethod)] = ctx => new EfRepositoryWithIncludes<PaymentMethod>(ctx, q => q.Include(x => x.TipoMedioPago).Include(x => x.TipoTarjeta).Include(x => x.EmisorTarjeta)),
        [typeof(RolePermission)] = ctx => new EfRepositoryWithIncludes<RolePermission>(ctx, q => q.Include(x => x.Rol).Include(x => x.Permiso)),
        [typeof(Session)]     = ctx => new EfRepositoryWithIncludes<Session>(ctx,     q => q.Include(x => x.Usuario)),
        [typeof(AircraftMaintenance)] = ctx => new EfRepositoryWithIncludes<AircraftMaintenance>(ctx, q => q.Include(x => x.Aeronave).Include(x => x.TipoMantenimiento)),
    };

    private IFlightRepository? _flights;
    private IAirlineRepository? _airlines;
    private IAircraftRepository? _aircraft;
    private IRouteRepository? _routes;
    private IClientRepository? _clients;
    private IReservationRepository? _reservations;
    private IPaymentRepository? _payments;
    private ITicketRepository? _tickets;
    private ICheckInRepository? _checkIns;
    private IInvoiceRepository? _invoices;
    private IUserRepository? _users;

    public EfUnitOfWork(AppDbContext ctx) => _ctx = ctx;

    public IFlightRepository Flights => _flights ??= new FlightRepository(_ctx);
    public IAirlineRepository Airlines => _airlines ??= new AirlineRepository(_ctx);
    public IAircraftRepository Aircraft => _aircraft ??= new AircraftRepository(_ctx);
    public IRouteRepository Routes => _routes ??= new RouteRepository(_ctx);
    public IClientRepository Clients => _clients ??= new ClientRepository(_ctx);
    public IReservationRepository Reservations => _reservations ??= new ReservationRepository(_ctx);
    public IPaymentRepository Payments => _payments ??= new PaymentRepository(_ctx);
    public ITicketRepository Tickets => _tickets ??= new TicketRepository(_ctx);
    public ICheckInRepository CheckIns => _checkIns ??= new CheckInRepository(_ctx);
    public IInvoiceRepository Invoices => _invoices ??= new InvoiceRepository(_ctx);
    public IUserRepository Users => _users ??= new UserRepository(_ctx);

    public IRepository<T> Repository<T>() where T : BaseEntity<int>
    {
        var type = typeof(T);
        if (!_genericRepos.TryGetValue(type, out var repo))
        {
            repo = _repoFactories.TryGetValue(type, out var factory)
                ? factory(_ctx)
                : new EfRepository<T>(_ctx);
            _genericRepos[type] = repo;
        }
        return (IRepository<T>)repo;
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _ctx.SaveChangesAsync(ct);

    public async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken ct = default)
    {
        await using var tx = await _ctx.Database.BeginTransactionAsync(ct);
        try
        {
            await operation(ct);
            await _ctx.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
        }
        catch
        {
            await tx.RollbackAsync(ct);
            throw;
        }
    }
}
