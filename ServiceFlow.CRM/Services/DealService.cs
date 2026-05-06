using Microsoft.EntityFrameworkCore;
using ServiceFlow.CRM.Data;
using ServiceFlow.CRM.Models;

namespace ServiceFlow.CRM.Services;

public class DealService(ApplicationDbContext dbContext) : IDealService
{
    public List<SalesDeal> GetAll()
    {
        return dbContext.Deals
            .Include(x => x.Customer)
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }

    public SalesDeal? GetById(Guid id)
    {
        return dbContext.Deals
            .Include(x => x.Customer)
            .FirstOrDefault(x => x.Id == id);
    }

    public void Add(SalesDeal deal)
    {
        deal.CreatedAt = DateTime.Now;

        dbContext.Deals.Add(deal);
        dbContext.SaveChanges();
    }

    public void Update(SalesDeal deal)
    {
        var existing = dbContext.Deals.FirstOrDefault(x => x.Id == deal.Id);

        if (existing == null)
            return;

        existing.Title = deal.Title;
        existing.Amount = deal.Amount;
        existing.Status = deal.Status;
        existing.CustomerId = deal.CustomerId;

        dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var deal = dbContext.Deals.FirstOrDefault(x => x.Id == id);

        if (deal == null)
            return;

        dbContext.Deals.Remove(deal);
        dbContext.SaveChanges();
    }
}