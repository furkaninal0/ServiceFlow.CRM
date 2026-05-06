using ServiceFlow.CRM.Data;
using ServiceFlow.CRM.Models;

namespace ServiceFlow.CRM.Services;

public class CustomerService(ApplicationDbContext dbContext) : ICustomerService
{
    public List<Customer> GetAll()
    {
        return dbContext.Customers
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }

    public Customer? GetById(Guid id)
    {
        return dbContext.Customers
            .FirstOrDefault(x => x.Id == id);
    }

    public void Add(Customer customer)
    {
        customer.CreatedAt = DateTime.Now;

        dbContext.Customers.Add(customer);
        dbContext.SaveChanges();
    }

    public void Update(Customer customer)
    {
        var existing = dbContext.Customers.FirstOrDefault(x => x.Id == customer.Id);

        if (existing == null)
            return;

        existing.FullName = customer.FullName;
        existing.Email = customer.Email;
        existing.Phone = customer.Phone;
        existing.CompanyName = customer.CompanyName;
        existing.Notes = customer.Notes;

        dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var customer = dbContext.Customers.FirstOrDefault(x => x.Id == id);

        if (customer == null)
            return;

        dbContext.Customers.Remove(customer);
        dbContext.SaveChanges();
    }
}