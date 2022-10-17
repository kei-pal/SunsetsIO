using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SunsetsIO.Data;
using SunsetsIO.Models;

namespace SunsetsIO.Pages.Rate
{
    public class IndexModel : PageModel
    {
        private readonly SunsetsIO.Data.ApplicationDbContext _context;

        public IndexModel(SunsetsIO.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Rating> Rating { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Rating != null)
            {
                Rating = await _context.Rating
                .Include(r => r.User).ToListAsync();
            }
        }
    }
}
