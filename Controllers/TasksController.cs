using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Data;
using TaskManagement.API.DTOs;
using TaskManagement.API.Models;
using System.Security.Claims;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpPost]
    public IActionResult CreateTask(TaskDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            UserId = GetUserId()
        };

        _context.Tasks.Add(task);
        _context.SaveChanges();

        return Ok(task);
    }

    [HttpGet]
    public IActionResult GetTasks(bool? completed)
    {
        var userId = GetUserId();

        var tasks = _context.Tasks
            .Where(t => t.UserId == userId);

        if (completed.HasValue)
            tasks = tasks.Where(t => t.IsCompleted == completed);

        return Ok(tasks.ToList());
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, TaskDto dto)
    {
        var task = _context.Tasks.Find(id);

        if (task == null) return NotFound();

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Priority = dto.Priority;

        _context.SaveChanges();

        return Ok(task);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var task = _context.Tasks.Find(id);

        if (task == null) return NotFound();

        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return Ok("Deleted");
    }
}