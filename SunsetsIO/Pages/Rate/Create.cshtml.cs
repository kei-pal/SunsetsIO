using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SunsetsIO.Controllers;
using SunsetsIO.Data;
using SunsetsIO.Models;

namespace SunsetsIO.Pages.Rate
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public CreateModel(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Rating Rating { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var user = _userManager.GetUserAsync(User).Result;
            Rating.UserId = user.Id;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Rating.Add(Rating);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<OpenWeatherMapPayload> GetWeatherForecastAsync(double latitude, double longitude)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&appid={Environment.GetEnvironmentVariable("OPEN_WEATHER_MAP_API_KEY")}");
            var responseString = await response.Content.ReadAsStringAsync();
            var weatherForecast = JsonConvert.DeserializeObject<OpenWeatherMapPayload>(responseString);
            return weatherForecast;
        }
    }
}
