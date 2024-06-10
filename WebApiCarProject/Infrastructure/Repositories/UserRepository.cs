using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApiCarProject.Infrastructure.DatabseContexts;
using WebApiCarProject.Infrastructure.Entities;

namespace WebApiCarProject.Infrastructure.Repositories;

public class UserRepository : IGenericRepository<User>, IDisposable
{
    private readonly CarDbContext _context;


    private bool disposed;

    public UserRepository(CarDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public User GetById(int id)
    {
        return _context.Users.Find(id);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public void Remove(User sender)
    {
        if (_context.Entry(sender).State == EntityState.Detached)
            _context.Users.Attach(sender);
        
        _context.Users.Remove(sender);
    }

    public void Add(in User sender)
    {
        _context.Add(sender).State = EntityState.Added;
    }

    public void Update(in User sender)
    {
        _context.Entry(sender).State = EntityState.Modified;
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public User Select(Expression<Func<User, bool>> predicate)
    {
        return _context.Users
            .Where(predicate).FirstOrDefault()!;
    }

    public async Task<User> SelectAsync(Expression<Func<User, bool>> predicate)
    {
        return (await _context.Users
            .Where(predicate).FirstOrDefaultAsync())!;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
            if (disposing)
                _context.Dispose();
        disposed = true;
    }
}