using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POC.Audit.Models;

namespace POC.Audit.Controllers
{
    public class AuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.AuditLogs.ToListAsync());
        }

        // GET: AuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditLog = await _context.AuditLogs
                .FirstOrDefaultAsync(m => m.AuditId == id);
            if (auditLog == null)
            {
                return NotFound();
            }

            return View(auditLog);
        }

      
        private bool AuditLogExists(int id)
        {
            return _context.AuditLogs.Any(e => e.AuditId == id);
        }
    }
}
