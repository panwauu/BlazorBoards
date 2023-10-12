using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class LabelController : ControllerBase
{
    private readonly LabelService _labelService;

    public LabelController(LabelService labelService)
    {
        _labelService = labelService;
    }

    [HttpGet]
    public List<Models.LabelDB> GetAll()
    {
        return _labelService.GetLabels();
    }

    /*
    // GET: LabelController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: LabelController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: LabelController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: LabelController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: LabelController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: LabelController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: LabelController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
    */
}
