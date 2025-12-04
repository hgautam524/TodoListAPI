using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListAPI.Data;
using TodoListAPI.Models;

namespace TodoListAPI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var lists = await _context.TodoLists
                .Include(t => t.Tasks)
                .Where(t => t.UserId == user.Id)
                .ToListAsync();

            return View(lists);
        }

        [HttpPost]
        public async Task<IActionResult> CreateList(string title)
        {
            var user = await _userManager.GetUserAsync(User);
            var list = new TodoList { Title = title, UserId = user.Id };
            _context.TodoLists.Add(list);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(int listId, string taskTitle)
        {
            var task = new TodoTask { Title = taskTitle, TodoListId = listId };
            _context.TodoTasks.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> MarkDone(int taskId)
        {
            var task = await _context.TodoTasks.FindAsync(taskId);
            if (task != null)
            {
                task.Status = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}

