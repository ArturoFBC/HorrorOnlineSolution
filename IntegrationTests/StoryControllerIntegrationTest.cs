using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using HtmlAgilityPack;
using Xunit;
using Xunit.Abstractions;


namespace IntegrationTests
{
    public class StoryControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly ITestOutputHelper _testOutputHelper;

        public StoryControllerIntegrationTest(CustomWebApplicationFactory factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("/Story/SearchForm", "form")]
        [InlineData("/Story/Create", "form")]
        [InlineData("/Story/Details", "div.story-card")]
        [InlineData("/Story/Index", "div.story-card")]
        public async Task Actions_ToReturnView(string route, string expectedElement)
        {
            //Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(route);

            // Assert
            Assert.True(response.IsSuccessStatusCode);

            string responseString = await response.Content.ReadAsStringAsync();

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(responseString);

            var document = html.DocumentNode;

            _testOutputHelper.WriteLine(document.InnerHtml);

            document.QuerySelectorAll(expectedElement).Should().NotBeEmpty();
        }
    }
}
