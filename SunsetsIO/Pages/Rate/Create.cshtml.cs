using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
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
        private readonly IConfiguration _config;

        public CreateModel(AppDbContext context, UserManager<User> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }

        public IActionResult OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return Page();
        }

        public class AjaxInput
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
        
        public async Task<IActionResult> OnPostAjax([FromBody] AjaxInput ajaxIn)
        {
            WeatherForecastController controller = new(_context, _config);
            var localWeather = await controller.GetLocalWeather(ajaxIn.Latitude, ajaxIn.Longitude);

            var sunsetOffset = localWeather.SunsetUtc.AddSeconds(localWeather.TimezoneOffsetSecs);
            
            return new JsonResult(sunsetOffset);
        }

        [BindProperty]
        public Rating Rating { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var user = _userManager.GetUserAsync(User).Result;
            Rating.UserId = user.Id;

            WeatherForecastController controller = new (_context, _config);

            var localWeather = await controller.GetLocalWeather(Rating.Latitude, Rating.Longitude);

            Rating.LocalWeatherId = localWeather.Id;

            //DateTime dateTimeRatedUtc = DateTime.UtcNow;
            //DateTime ratingDateTimeOffset = DateTime.UtcNow.AddSeconds(localWeather.TimezoneOffsetSecs);
            Rating.DateTimeRatedUtc = DateTime.UtcNow;
            DateTime ratingDateTimeOffset = Rating.DateTimeRatedUtc.AddSeconds(localWeather.TimezoneOffsetSecs);
            DateTime localWeatherDateTimeOffset = localWeather.SunsetUtc.AddSeconds(localWeather.TimezoneOffsetSecs);

            // need to adapt the below to suit the date of the user and not UTC
            if (!_context.Rating.Any(r => r.UserId == user.Id && ratingDateTimeOffset.Date == localWeatherDateTimeOffset.Date))
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _context.Rating.Add(Rating);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
