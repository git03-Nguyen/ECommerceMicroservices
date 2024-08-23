using System.Linq.Expressions;
using Customer.Service.Data;
using Microsoft.EntityFrameworkCore;

namespace Customer.Service.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerContext _context;

    public CustomerRepository(CustomerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.Customer?>> GetAll()
    {
        return await _context.Customers.ToListAsync();
    }

    // Generic method to get a customer by a filter
    public async Task<Models.Customer?> GetBy(Expression<Func<Models.Customer, bool>> predicate)
    {
        return await _context.Customers.FirstOrDefaultAsync(predicate!);
    }

    public async Task<Models.Customer?> Create(Models.Customer customer)
    {
        var entity = await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<Models.Customer?> Update(Models.Customer customer)
    {
        var entity = _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer is null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    // Generic method to check if a customer exists
    public async Task<bool> Exists(Expression<Func<Models.Customer, bool>> predicate)
    {
        return await _context.Customers.AnyAsync(predicate!);
    }
}