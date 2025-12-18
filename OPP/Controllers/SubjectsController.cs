using Microsoft.AspNetCore.Mvc;
using OPP.Contracts.Subjects;
using OPP.Domain.Services;

namespace OPP.Controllers;

[ApiController]
[Route("api/subjects")]
public class SubjectsController : Controller
{
    private readonly SubjectsService _subjectsService;

    public SubjectsController(SubjectsService subjectsService)
    {
        _subjectsService = subjectsService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateSubjectDto dto)
    {
       await _subjectsService.AddSubject(dto);
       return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _subjectsService.DeleteSubject(id);
        return Ok();
    }
    
    [HttpPatch]
    public async Task<IActionResult> Update(SubjectDto dto)
    {
        await _subjectsService.UpdateSubject(dto);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(Guid id)
    {
        await _subjectsService.GetSubjectById(id);
        return Ok();
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _subjectsService.GetAllSubjects());
    }
}