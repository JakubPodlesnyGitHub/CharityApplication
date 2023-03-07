using Moq;
using System.Net;
using System.Text.Json;
using UnitTests.Model;
using Xunit;

namespace UnitTests
{
    public class UnitTestEndpoints
    {
        public HttpClient HttpClient { get; set; } = new HttpClient();

        [Theory]
        [InlineData("http://s20540-001-site1.ctempurl.com/api/Module/GetModules")]
        [InlineData("http://s20540-001-site1.ctempurl.com/api/Group/GetVisibleGroups")]
        [InlineData("http://s20540-001-site1.ctempurl.com/api/Event/GetEvents")]
        [InlineData("http://s20540-001-site1.ctempurl.com/api/Badge/GetBadges")]
        public async Task TestGetReturnsOk(string url)
        {
            //Arrange
            HttpStatusCode correctStatus = HttpStatusCode.OK;
            //Act
            var httpResponse = await HttpClient.GetAsync(url);
            //Assert
            Assert.NotNull(httpResponse);
            Assert.Equal(httpResponse.StatusCode, correctStatus);
        }

        [Fact]
        public async Task TestGetReturnedDataModuleController()
        {
            //Arrange
            List<string> modulesNames = new List<string>() { "Location", "Online Event", "Foundraiser", "QrCode", "Assesment Form", "Attendance List" };
            //Act
            var httpResponse = await HttpClient.GetAsync("http://s20540-001-site1.ctempurl.com/api/Module/GetModules");
            var deserializedResponse = JsonSerializer.Deserialize<List<ModuleModel>>(await httpResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //Assert
            Assert.NotNull(httpResponse);
            Assert.Equal(modulesNames, deserializedResponse.Select(x => x.ModuleName).ToList());
            Assert.Equal(modulesNames.Count, deserializedResponse.Select(x => x.ModuleName).ToList().Count);
        }
        [Fact]
        public async Task TestGetReturnedDataBadgesController()
        {
            //Arrange
            List<string> badgesNames = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                badgesNames.Add($"Badge Level {i}");
            }
            //Act
            var httpResponse = await HttpClient.GetAsync("http://s20540-001-site1.ctempurl.com/api/Badge/GetBadges");
            var deserializedResponse = JsonSerializer.Deserialize<List<BadgeModel>>(await httpResponse.Content.ReadAsStringAsync(), 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //Assert
            Assert.NotNull(httpResponse);
            Assert.Equal(badgesNames, deserializedResponse.Select(x => x.Name).ToList());
            Assert.Equal(badgesNames.Count, deserializedResponse.Select(x => x.Name).ToList().Count);
        }

        [Theory]
        [InlineData("http://s20540-001-site1.ctempurl.com/api/Status/GetEventStatutes")]
        [InlineData("http://s20540-001-site1.ctempurl.com/api/EventAnnouncement/GetEventsAnnouncements")]
        [InlineData("http://s20540-001-site1.ctempurl.com/api/ContactForm/GetContactForms")]
        [InlineData("http://s20540-001-site1.ctempurl.com/api/CompanyAddress/GetCompaniesAddresses")]
        public async Task TestGetAuthorizeAttribuite(string url)
        {
            //Arrange
            HttpStatusCode correctStatus = HttpStatusCode.Unauthorized;
            //Act
            var httpResponse = await HttpClient.GetAsync(url);
            //Assert
            Assert.NotNull(httpResponse);
            Assert.Equal(correctStatus, httpResponse.StatusCode);
        }
    }
}