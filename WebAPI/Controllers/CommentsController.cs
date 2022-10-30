using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentLogic commentLogic;

    public CommentsController(ICommentLogic commentLogic)
    {
        this.commentLogic = commentLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> CreateAsync(CommentCreationDto creationDto)
    {
        try
        {
            Comment comment = await commentLogic.CreateAsync(creationDto);
            return Created($"/comments/{comment.Id}", comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAsync([FromQuery] int? postId)
    {
        try
        {
            IEnumerable<Comment> comments = await commentLogic.GetAsync(postId);
            return Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("/SubComments/")]

    public async Task<ActionResult<IEnumerable<Comment>>> GetSubCommentsAsync([FromQuery] int id)
    {
        try
        {
            IEnumerable<Comment> comments = await commentLogic.GetSubCommentsAsync(id);
            return Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("/Comment/")]
    public async Task<ActionResult<Comment>> GetByIdAsync([FromQuery] int id)
    {
        try
        {
            Comment comment = await commentLogic.GetByIdAsync(id);
            return Ok(comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] CommentUpdateDto commentUpdateDto)
    {
        try
        {
            await commentLogic.UpdateAsync(commentUpdateDto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync([FromQuery] int id)
    {
        try
        {
            await commentLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}