using ServiceFlow.CRM.Models;

namespace ServiceFlow.CRM.Services;

public interface IDealService
{
    List<SalesDeal> GetAll();

    SalesDeal? GetById(Guid id);

    void Add(SalesDeal deal);

    void Update(SalesDeal deal);

    void Delete(Guid id);
}