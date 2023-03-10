using Microsoft.AspNetCore.Mvc;
using Xunit;
using ControllerBase = GlitchedCat.API.Controllers.Blog.ControllerBase;

namespace GlitchedCat.Test.APITests.Controllers.Blog
{
    public class ControllerBaseTests
    {
        [Fact]
        public void ControllerBase_ShouldInheritFromController()
        {
            // Arrange
            var controllerBase = new ControllerBase();

            // Assert
            Assert.IsAssignableFrom<Controller>(controllerBase);
        }
    }
}