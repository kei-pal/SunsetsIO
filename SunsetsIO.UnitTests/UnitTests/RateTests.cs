using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using SunsetsIO.Data;
using SunsetsIO.Models;
using SunsetsIO.Pages.Rate;
using SunsetsIO.UnitTests;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;

namespace SunsetsIO.UnitTests.Pages
{
    public class RateTests
    {
        [Fact]
        public async Task OnPost_ValidModel_ReturnRedirectToIndex()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase("SunsetsIO");
            var _dbContext = new AppDbContext(optionsBuilder.Options);

            var userId = new Guid().ToString();
            // mock user manager
            var mockedUserManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);


            var userClaim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));

            var user = new User { Id = userId };

            var httpContext = new DefaultHttpContext()
            {
                User = userClaim
            };
            
            mockedUserManager.Setup(x => x.GetUserAsync(userClaim)).ReturnsAsync(user);

            // need these for the page context
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };
            var model = new CreateModel(_dbContext, mockedUserManager.Object)
            {
                Rating = new Rating(),
                PageContext = pageContext
            };
            //model.ModelState.AddModelError("Rating", "Required");
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
        }

        [Fact]
        public async Task OnPost_InvalidModel_ReturnsPage()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase("SunsetsIO");
            var _dbContext = new AppDbContext(optionsBuilder.Options);

            var userId = new Guid().ToString();
            // mock user manager
            var mockedUserManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);


            var userClaim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));

            var user = new User { Id = userId };

            var httpContext = new DefaultHttpContext()
            {
                User = userClaim
            };

            mockedUserManager.Setup(x => x.GetUserAsync(userClaim)).ReturnsAsync(user);

            // need these for the page context
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };
            var model = new CreateModel(_dbContext, mockedUserManager.Object)
            {
                Rating = new Rating(),
                PageContext = pageContext
            };
            
            // Act
            model.ModelState.AddModelError("Rating", "Required");
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsType<PageResult>(result);
        }
    }
}