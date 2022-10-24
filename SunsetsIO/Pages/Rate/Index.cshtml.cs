using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SunsetsIO.Data;
using SunsetsIO.Models;

namespace SunsetsIO.Pages.Rate
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Rating> Rating { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Rating != null)
            {
                Rating = await _context.Rating
                .Include(r => r.User)
                .Where(a => a.User.UserName == User.Identity.Name)
                .ToListAsync();
            }
        }
    }
}
