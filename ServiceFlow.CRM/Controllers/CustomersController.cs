using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceFlow.CRM.Models;
using ServiceFlow.CRM.Services;

namespace ServiceFlow.CRM.Controllers;

[Authorize]
public class CustomersController(ICustomerService customerService) : Controller
{
    public IActionResult Index()
    {
        var customers = customerService.GetAll();

        return View(customers);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        customerService.Add(customer);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(Guid id)
    {
        var customer = customerService.GetById(id);

        if (customer == null)
            return NotFound();

        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        customerService.Update(customer);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id)
    {
        customerService.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}