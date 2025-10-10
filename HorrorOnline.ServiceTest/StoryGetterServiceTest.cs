using AutoFixture;
using FluentAssertions;
using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Core.DTO;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.Services.Stories;
using Moq;
using System.Diagnostics.Metrics;
using Xunit;
using Xunit.Abstractions;

namespace HorrorOnline.ServiceTest
{
    public class StoryGetterServiceTest
    {
        private readonly IStoryGetterService _storyGetterService;
        private readonly Mock<IStoryRepository> _storyRepositoryMock;
        private readonly Fixture _fixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public StoryGetterServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            _storyRepositoryMock = new Mock<IStoryRepository>();
            _storyGetterService = new StoryGetterService(_storyRepositoryMock.Object);

            _fixture = new Fixture();
        }

        #region GetAllStories
        [Fact]
        public async Task GetAllStories_NoStories_ReturnEmptyList()
        {
            _storyRepositoryMock.Setup(repository => repository.GetAllStories()).ReturnsAsync(new List<Story>());

            IEnumerable<StoryResponse> storyList = await _storyGetterService.GetAllStories();

            storyList.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllStories_SomeStories_ReturnStories()
        {
            List<Story> stories = GetTestStories();

            IEnumerable<StoryResponse> expectedResponse = stories.Select(item => item.ToStoryResponse());

            _testOutputHelper.WriteLine("Expected:");
            expectedResponse.ToList().ForEach(
                item => _testOutputHelper.WriteLine(item.ToString()));

            _storyRepositoryMock.Setup(repository => repository.GetAllStories()).ReturnsAsync(stories);

            IEnumerable<StoryResponse> realStoryList = await _storyGetterService.GetAllStories();

            _testOutputHelper.WriteLine("Real:");
            realStoryList.ToList().ForEach(
                item => _testOutputHelper.WriteLine(item.ToString()));

            realStoryList.Should().BeEquivalentTo(expectedResponse);
        }
        #endregion

        #region GetStoryByID
        [Fact]
        public async Task GetStoryByID_NullID_ReturnNull()
        {
            StoryResponse? response = await _storyGetterService.GetStoryByID(null);

            response.Should().BeNull();
        }

        [Fact]
        public async Task GetStoryByID_IDInexistant_ReturnNull()
        {
            _storyRepositoryMock.Setup(repository => repository.GetStoryByID(It.IsAny<Guid>())).ReturnsAsync(null as Story);

            StoryResponse? response = await _storyGetterService.GetStoryByID(Guid.NewGuid());

            response.Should().BeNull();
        }

        [Fact]
        public async Task GetStoryByID_IDExists_ReturnCorrectStory()
        {
            Story story = _fixture.Build<Story>()
                      .With(temp => temp.Author, null as ApplicationUser)
                      .With(temp => temp.Reviews, null as ICollection<Review>)
                      .With(temp => temp.Tags, null as ICollection<Tag>).Create();

            _storyRepositoryMock.Setup(repository => repository.GetStoryByID(story.StoryId)).ReturnsAsync(story);

            StoryResponse expectedResponse = story.ToStoryResponse();

            _testOutputHelper.WriteLine("Expected:");
            _testOutputHelper.WriteLine(expectedResponse.ToString());

            StoryResponse? realResponse = await _storyGetterService.GetStoryByID(story.StoryId);

            _testOutputHelper.WriteLine("Real:");
            _testOutputHelper.WriteLine(realResponse?.ToString());

            realResponse.Should().BeEquivalentTo(expectedResponse);
        }
        #endregion

        #region GetSelectedStories

        #endregion

        private List<Story> GetTestStories()
        {
            List<Story> stories = new List<Story>();

            for (int i = 0; i < 3; i++)
            {
                stories.Add(
                    _fixture.Build<Story>()
                      .With(temp => temp.Author, null as ApplicationUser)
                      .With(temp => temp.Reviews, null as ICollection<Review>)
                      .With(temp => temp.Tags, null as ICollection<Tag>).Create()
                );
            };

            return stories;
        }
    }
}
