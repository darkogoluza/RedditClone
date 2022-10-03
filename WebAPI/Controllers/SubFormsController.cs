using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SubFormsController : ControllerBase
{
    private readonly ISubForumLogic subForumLogic;

    public SubFormsController(ISubForumLogic subForumLogic)
    {
        this.subForumLogic = subForumLogic;
    }

    [HttpPost]
    public async Task<ActionResult<SubForum>> CreateAsync(SubForumCreationDto subForumCreationDto)
    {
        try
        {
            SubForum subForum = await subForumLogic.CreateAsync(subForumCreationDto);
            return Created($"/subForms/{subForum.Id}", subForum);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubForum>>> GetAsync()
    {
        try
        {
            IEnumerable<SubForum> subForums = await subForumLogic.GetAsync();
            return Ok(subForums);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}