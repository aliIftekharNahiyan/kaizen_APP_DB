using System.Threading.Tasks;
using Kaizen.Models.TokenAuth;
using Kaizen.Web.Controllers;
using Shouldly;
using Xunit;

namespace Kaizen.Web.Tests.Controllers
{
    public class HomeController_Tests: KaizenWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}