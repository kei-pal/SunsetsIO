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
    public class DetailsModel : PageModel
    {
        private readonly SunsetsIO.Data.ApplicationDbContext _context;

        public DetailsModel(SunsetsIO.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Rating Rating { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }
            else 
            {
                Rating = rating;
            }
            return Page();
        }
    }
}
