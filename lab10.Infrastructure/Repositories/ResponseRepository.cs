using lab10.Application.Repositories;
using lab10.Domain.Entities;
using lab10.Infrastructure.Persistence;

namespace lab10.Infrastructure.Repositories;

public class ResponseRepository : GenericRepository<Response>, IResponseRepository
{
    public ResponseRepository(AppDbContext context) : base(context)
    {
        
    }
}