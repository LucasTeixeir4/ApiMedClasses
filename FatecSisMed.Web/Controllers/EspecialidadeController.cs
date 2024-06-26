﻿using FatecSisMed.Web.Models;
using FatecSisMed.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace FatecSisMed.Web.Controllers;

public class EspecialidadeController : Controller
{
    private readonly IEspecialidadeService _especialidadeService;

    public EspecialidadeController(IEspecialidadeService especialidadeService)
    {
        _especialidadeService = especialidadeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EspecialidadeViewModel>>> Index()
    {
        var result = await _especialidadeService.GetAllEspecialidades(await GetAcessToken());
        if (result == null) return View("Error");
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateEspecialidade()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateEspecialidade(EspecialidadeViewModel especialidadeViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _especialidadeService.CreateEspecialidade(especialidadeViewModel, await GetAcessToken());
            if (result is not null) return RedirectToAction(nameof(Index));
        }
        else
        {
            return BadRequest("Error");
        }
        return View(especialidadeViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateEspecialidade(int id)
    {
        var result = await _especialidadeService.FindEspecialidadeById(id, await GetAcessToken());
        if (result is null) return View("Error");
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEspecialidade(EspecialidadeViewModel especialidadeViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _especialidadeService.UpdateEspecialidade(especialidadeViewModel, await GetAcessToken());
            if (result is not null)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return View(especialidadeViewModel);
    }

    [HttpGet]
    public async Task<ActionResult<EspecialidadeViewModel>> DeleteEspecialidade(int id)
    {
        var result = await _especialidadeService.FindEspecialidadeById(id, await GetAcessToken());
        if (result is null) { return View("Error"); }
        return View(result);
    }

    [HttpPost(), ActionName("DeleteEspecialidade")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _especialidadeService.DeleteEspecialidadeById(id, await GetAcessToken());
        if (!result) { return View("Error"); }
        return RedirectToAction(nameof(Index));
    }
    private async Task<string> GetAcessToken()
    {
        return await HttpContext.GetTokenAsync("acess_token");
    }

}


