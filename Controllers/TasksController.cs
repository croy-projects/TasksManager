using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksManager.Data;
using TasksManager.Models;

namespace TasksManager.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TasksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? categoryId, int? priorityId, bool? completed, string? search)
        {
            var userId = _userManager.GetUserId(User);

            // var query = _context.Tasks
            //     .Where(t => t.UserId == userId)
            //     .Include(t => t.Category)
            //     .Include(t => t.Priority);

            IQueryable<TaskItem> query = _context.Tasks
                .Include(t => t.Category)
                .Include(t => t.Priority)
                .Where(t => t.UserId == userId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Title.Contains(search) || t.Description.Contains(search));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(t => t.CategoryId == categoryId.Value);
            }

            if (priorityId.HasValue)
            {
                query = query.Where(t => t.PriorityId == priorityId.Value);
            }

            if (completed.HasValue)
            {
                query = query.Where(t => t.IsCompleted == completed.Value);
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Priorities = await _context.Priorities.ToListAsync();
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SelectedPriorityId = priorityId;
            ViewBag.SelectedCompleted = completed;
            ViewBag.search = search;

            var tasks = await query.OrderBy(t => t.DueDate).ToListAsync();

            // var tasks = await _context.Tasks
            //     .Where(t => t.UserId == userId)
            //     .OrderBy(t => t.DueDate)
            //     .ToListAsync();

            return View(tasks);
        }
        public async Task<IActionResult> Dashboard()
        {
            var userId = _userManager.GetUserId(User);

            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            var totalTasks = tasks.Count;
            var completedTasks = tasks.Count(t => t.IsCompleted);
            var pendingTasks = totalTasks - completedTasks;
            var overdueTasks = tasks.Count(t => !t.IsCompleted && t.DueDate < DateTime.Today);

            ViewBag.TotalTasks = totalTasks;
            ViewBag.CompletedTasks = completedTasks;
            ViewBag.PendingTasks = pendingTasks;
            ViewBag.OverdueTasks = overdueTasks;

            return View();
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Priorities = await _context.Priorities.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = _userManager.GetUserId(User)!;
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Priorities = await _context.Priorities.ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null) return NotFound();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Priorities = await _context.Priorities.ToListAsync();
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskItem model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var existingTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

                if (existingTask == null) return NotFound();

                existingTask.Title = model.Title;
                existingTask.Description = model.Description;
                existingTask.IsCompleted = model.IsCompleted;
                existingTask.DueDate = model.DueDate;
                existingTask.CategoryId = model.CategoryId;
                existingTask.PriorityId = model.PriorityId;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Priorities = await _context.Priorities.ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null) return NotFound();

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}