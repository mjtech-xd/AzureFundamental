using AzureBlobProject.Models;
using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobProject.Controllers;

public class ContainerController(IContainerService containerService) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        var allContainers = await containerService.GetAllContainers();
        return View(allContainers);
    }
    
    public async Task<IActionResult> Create()
    {
        return View(new ContainerModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(ContainerModel container)
    {
        await containerService.CreateContainer(container.Name);
        return  RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Delete(string containerName)
    {
        await containerService.DeleteContainer(containerName);
        return  RedirectToAction(nameof(Index));
    }
}