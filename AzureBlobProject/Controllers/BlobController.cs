using AzureBlobProject.Models;
using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System;

namespace AzureBlobProject.Controllers;

public class BlobController(IBlobService blobService) : Controller
{
    // GET
    [HttpGet]
    public async Task<IActionResult> Manage(string containerName)
    {
        containerName = containerName.ToLowerInvariant();
        var blobObj = await blobService.GetAllBlobs(containerName);
        ViewData["ContainerName"] = containerName;
        return View(blobObj);
    }
    
    [HttpGet]
    public async Task<IActionResult> AddFile(string containerName)
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddFile(string containerName, IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            return View();
        }
        var baseName = Regex.Replace(Path.GetFileNameWithoutExtension(file.FileName), @"[^a-zA-Z0-9\-_\.]", "_");
        var fileName = baseName + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
        var result = await blobService.CreateBlob(fileName, containerName, file, new BlobModel());
        if (result)
            return RedirectToAction("Manage",new {containerName});
 
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> ViewFile(string name, string containerName)
    {
        containerName = containerName.ToLowerInvariant();
        name = Uri.UnescapeDataString(name);
        return Redirect(await blobService.GetBlob(containerName, name));
    }
    
    [HttpGet]
    public async Task<IActionResult> DeleteFile(string name, string containerName)
    {
        containerName = containerName.ToLowerInvariant();
        name = Uri.UnescapeDataString(name);
        var result = await blobService.DeleteBlob(name, containerName);
        if (result)
        {
            TempData["Message"] = "File deleted successfully.";
        }
        else
        {
            TempData["Error"] = "Failed to delete the file.";
        }
        return RedirectToAction("Manage", new { containerName });
    }
}