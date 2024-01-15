using Data;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace csharp_crud_api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase
{
    private readonly UserContext _context;

    public UserController(UserContext context)
    {
        _context=context;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    //GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user=await _context.Users.FindAsync(id);
        if (user==null)
        {
            return NotFound();
        }

        return user;
    }

    //POST: api/users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser),new {id = user.Id},user);
    }

    
}