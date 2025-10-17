using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.DTO;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.ServiceContracts.Tags;
using HorrorOnline.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HorrorOnline.UI.Controllers
{
    [Route("[controller]")]
    public class StoryController : Controller
    {
        private readonly IStoryAdderService _storyAdderService;
        private readonly IStoryGetterService _storyGetterService;
        private readonly IStoryDeleterService _storyDeleterService;

        private readonly ITagAdderService _tagAdderService;
        private readonly ITagGetterService _tagGetterService;

        public StoryController(IStoryAdderService storyAdderService, IStoryGetterService storyGetterService, IStoryDeleterService storyDeleterService, ITagAdderService tagAdderService, ITagGetterService tagGetterService)
        {
            _storyAdderService = storyAdderService;
            _storyGetterService = storyGetterService;
            _storyDeleterService = storyDeleterService;

            _tagAdderService = tagAdderService;
            _tagGetterService = tagGetterService;
        }

        [Route("/")]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            IEnumerable<StoryResponse> stories = await _storyGetterService.GetAllStories();

            return View(stories);
        }

        [Route("[action]/{storyID}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Details(Guid storyID)
        {
            StoryResponse? storyToDisplay = await _storyGetterService.GetStoryByID(storyID);

            if (storyToDisplay == null)
                return RedirectToAction("Index");

            return View(storyToDisplay);
        }

        [Route("[action]")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SearchForm()
        {
            return View();
        }

        [Route("[action]")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SearchResults(string searchTerm)
        {
            IEnumerable<StoryResponse> stories = await _storyGetterService.GetSelectedStories(searchTerm, nameof(StoryResponse.Title));

            return View(stories);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StoryWithTagsModel storyWithTagsModel)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(error => error.Errors).Select(error => error.ErrorMessage);

                return View(storyWithTagsModel);
            }

            StoryAddRequest storyAddRequest = new StoryAddRequest()
            {
                Title = storyWithTagsModel.Title,
                Summary = storyWithTagsModel.Summary,
                Text = storyWithTagsModel.Text,
                Tags = await TagParser(storyWithTagsModel.Tags),
                //TODO retrieve author form authentication
                AuthorId = Guid.NewGuid(),
            };

            StoryResponse storyAdded = await _storyAdderService.AddStory(storyAddRequest);


            try
            {
                var storyIDparameter = new { storyID = storyAdded.StoryId };
                return RedirectToAction(nameof(Details), storyIDparameter);
            }
            catch
            {
                return View();
            }
        }

        #region Tag management helpers
        /// <summary>
        /// Retrieves or creates the appropiate tag entities, and returns their ids
        /// </summary>
        /// <param name="tagsString">String containing tags names separated by commas</param>
        /// <returns></returns>
        private async Task<ICollection<TagResponse>?> TagParser(string tagsString)
        {
            if (string.IsNullOrEmpty(tagsString))
                return null;

            List<string> stringTags = tagsString.Split(',').ToList();

            ICollection<TagResponse> returnTags = new List<TagResponse>();
            stringTags.ForEach(async tag =>
            {
                tag.Trim();
                returnTags.Add( await GetOrAddTag(tag) );
            });

            return returnTags;
        }

        private async Task<TagResponse> GetOrAddTag(string tag)
        {
            TagResponse? tagFromGet = await _tagGetterService.GetTagByName(tag);
            if (tagFromGet != null)
                return tagFromGet;

            TagAddRequest tagAddRequest = new TagAddRequest() { TagName = tag };
            return await _tagAdderService.AddTag(tagAddRequest);
        }
        #endregion

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
