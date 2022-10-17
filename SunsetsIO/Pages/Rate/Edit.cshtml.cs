using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SunsetsIO.Data;
using SunsetsIO.Models;

namespace SunsetsIO.Pages.Rate
{
    public class EditModel : PageModel
    {
        private readonly SunsetsIO.Data.ApplicationDbContext _context;

        public EditModel(SunsetsIO.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rating Rating { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating =  await _context.Rating.FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }
            Rating = rating;
           ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(Rating.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RatingExists(int id)
        {
          return _context.Rating.Any(e => e.Id == id);
        }
    }
}
