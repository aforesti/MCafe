using MCafe.Admin.Infrastructure.Data;
using MCafe.Shared;
using MCafe.Shared.Interfaces;

namespace MCafe.Admin;

internal interface IAdminRepository : IRepository;
internal interface IReadOnlyAdminRepository : IReadOnlyRepository;

internal class ReadOnlyAdminRepository(AdminDbContext dbContext)
    : ReadOnlyRepository<AdminDbContext>(dbContext), IReadOnlyAdminRepository;

internal class AdminRepository(AdminDbContext dbContext)
    : Repository<AdminDbContext>(dbContext), IAdminRepository;
