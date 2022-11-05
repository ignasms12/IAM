using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IAM.Models;
using IAM.Services;

namespace IAM.Controllers;

[ApiController]
[Route("users")]

public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    private IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    // GET: users
    [HttpGet]
    public IEnumerable<User> GetUsers()
    {
        //if (_context.Users == null)
        //{
        //    return NotFound();
        //}

        return _userService.GetUsers();

        //return await _context.Users.Select(x => ItemToDTO(x)).ToListAsync();
    }

    //// GET: users/5
    //[HttpGet("{id}")]
    //public async Task<ActionResult<UserDTO>> GetUser(long id)
    //{
    //    if (_context.Users == null)
    //    {
    //        return NotFound();
    //    }
    //    var user = await _context.Users.FindAsync(id);

    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    return ItemToDTO(user);
    //}

    //// PUT: users/5
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutUser(long id, UserDTO userDTO)
    //{
    //    if (id != userDTO.Id)
    //    {
    //        return BadRequest();
    //    }

    //    var user = await _context.Users.FindAsync(id);

    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    user.Username = userDTO.Username;
    //    user.Role = userDTO.Role;

    //    _context.Entry(user).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException) when (!UserExists(id))
    //    {

    //        return NotFound();

    //    }

    //    return NoContent();
    //}

    //// POST: users
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPost]
    //public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDTO)
    //{

    //    var user = new User
    //    {
    //        Username = userDTO.Username,
    //        Role = userDTO.Role
    //    };

    //    _context.Users.Add(user);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction(
    //        nameof(GetUser),
    //        new { id = user.Id },
    //        ItemToDTO(user)
    //    );
    //}

    //// DELETE: users/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteUser(long id)
    //{
    //    if (_context.Users == null)
    //    {
    //        return NotFound();
    //    }
    //    var user = await _context.Users.FindAsync(id);
    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Users.Remove(user);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    //private bool UserExists(long id)
    //{
    //    return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
    //}

    //private static UserDTO ItemToDTO(User user) =>
    //    new UserDTO
    //    {
    //        Id = user.Id,
    //        Username = user.Username,
    //        Role = user.Role
    //    };
}
