using System.Linq.Expressions;

namespace Customer.Service.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Models.Customer>> GetAll();
    Task<Models.Customer?> GetBy(Expression<Func<Models.Customer, bool>> predicate);
    Task<Models.Customer?> Create(Models.Customer customer);
    Task<Models.Customer?> Update(Models.Customer customer);
    Task<bool> Delete(int id);
    Task<bool> Exists(Expression<Func<Models.Customer, bool>> predicate);
}