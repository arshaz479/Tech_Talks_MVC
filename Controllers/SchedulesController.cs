using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Tech_Talks_MVC.Data;
using Tech_Talks_MVC.Models;

namespace Tech_Talks_MVC.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly Tech_Talks_DbContext _context;

        public SchedulesController(Tech_Talks_DbContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var tech_Talks_DbContext = _context.Schedule.Include(s => s.Discussion).Include(s => s.Speaker).Include(s => s.Sponsor);
            return View(await tech_Talks_DbContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Discussion)
                .Include(s => s.Speaker)
                .Include(s => s.Sponsor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }
        [Authorize]
        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["DiscussionId"] = new SelectList(_context.Discussion, "Id", "Id");
            ViewData["SpeakerId"] = new SelectList(_context.Set<Speaker>(), "Id", "Id");
            ViewData["SponsorId"] = new SelectList(_context.Set<Sponsor>(), "Id", "Id");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SpeakerId,SponsorId,DiscussionId,ScheduledTime")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscussionId"] = new SelectList(_context.Discussion, "Id", "Id", schedule.DiscussionId);
            ViewData["SpeakerId"] = new SelectList(_context.Set<Speaker>(), "Id", "Id", schedule.SpeakerId);
            ViewData["SponsorId"] = new SelectList(_context.Set<Sponsor>(), "Id", "Id", schedule.SponsorId);
            return View(schedule);
        }
        [Authorize]
        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["DiscussionId"] = new SelectList(_context.Discussion, "Id", "Id", schedule.DiscussionId);
            ViewData["SpeakerId"] = new SelectList(_context.Set<Speaker>(), "Id", "Id", schedule.SpeakerId);
            ViewData["SponsorId"] = new SelectList(_context.Set<Sponsor>(), "Id", "Id", schedule.SponsorId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SpeakerId,SponsorId,DiscussionId,ScheduledTime")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscussionId"] = new SelectList(_context.Discussion, "Id", "Id", schedule.DiscussionId);
            ViewData["SpeakerId"] = new SelectList(_context.Set<Speaker>(), "Id", "Id", schedule.SpeakerId);
            ViewData["SponsorId"] = new SelectList(_context.Set<Sponsor>(), "Id", "Id", schedule.SponsorId);
            return View(schedule);
        }
        [Authorize]
        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Discussion)
                .Include(s => s.Speaker)
                .Include(s => s.Sponsor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            _context.Schedule.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.Id == id);
        }
    }
}
