using MCafe.Cashier.Infrastructure.Data;
using MCafe.Shared;
using MCafe.Shared.Interfaces;

namespace MCafe.Cashier;

internal interface ICashierRepository : IRepository;
internal interface IReadOnlyCashierRepository : IReadOnlyRepository;

internal class ReadOnlyCashierRepository(CashierDbContext dbContext)
    : ReadOnlyRepository<CashierDbContext>(dbContext), IReadOnlyCashierRepository;

internal class CashierRepository(CashierDbContext dbContext)
    : Repository<CashierDbContext>(dbContext), ICashierRepository;
