using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceFlow.CRM.Data;
using ServiceFlow.CRM.Models;
using ServiceFlow.CRM.Services;

namespace ServiceFlow.CRM.Controllers;

[Authorize]
public class DealsController(IDealService dealService, ApplicationDbContext dbContext) : Controller
{
    public IActionResult Index()
    {
        var deals = dealService.GetAll();
        return View(deals);
    }

    public IActionResult Create()
    {
        ViewBag.Customers = new SelectList(
            dbContext.Customers.OrderBy(x => x.FullName).ToList(),
            "Id",
            "FullName"
        );

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(SalesDeal deal)
    {
        ModelState.Remove("Customer");

        if (!ModelState.IsValid)
        {
            ViewBag.Customers = new SelectList(
                dbContext.Customers.OrderBy(x => x.FullName).ToList(),
                "Id",
                "FullName",
                deal.CustomerId
            );

            return View(deal);
        }

        dealService.Add(deal);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(Guid id)
    {
        var deal = dealService.GetById(id);

        if (deal == null)
            return NotFound();

        ViewBag.Customers = new SelectList(
            dbContext.Customers.OrderBy(x => x.FullName).ToList(),
            "Id",
            "FullName",
            deal.CustomerId
        );

        return View(deal);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(SalesDeal deal)
    {
        ModelState.Remove("Customer");

        if (!ModelState.IsValid)
        {
            ViewBag.Customers = new SelectList(
                dbContext.Customers.OrderBy(x => x.FullName).ToList(),
                "Id",
                "FullName",
                deal.CustomerId
            );

            return View(deal);
        }

        dealService.Update(deal);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id)
    {
        dealService.Delete(id);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Pipeline()
    {
        var deals = dealService.GetAll();

        return View(deals);
    }
}