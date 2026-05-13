using Domain.Common;

namespace Application.Abstractions;

public interface IUnitOfWork
{
    // Core operational repos
    IFlightRepository Flights { get; }
    IAirlineRepository Airlines { get; }
    IAircraftRepository Aircraft { get; }
    IRouteRepository Routes { get; }
    IClientRepository Clients { get; }
    IReservationRepository Reservations { get; }
    IPaymentRepository Payments { get; }
    ITicketRepository Tickets { get; }
    ICheckInRepository CheckIns { get; }
    IInvoiceRepository Invoices { get; }
    IUserRepository Users { get; }

    // Generic repository for all other entities
    IRepository<T> Repository<T>() where T : BaseEntity<int>;

    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken ct = default);
}
