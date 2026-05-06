using ServiceFlow.CRM.Models;

namespace ServiceFlow.CRM.Services;

public interface ICustomerService
{
    List<Customer> GetAll();

    Customer? GetById(Guid id);

    void Add(Customer customer);

    void Update(Customer customer);

    void Delete(Guid id);
}