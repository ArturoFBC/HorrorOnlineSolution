using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using HorrorOnline.Core.DTO;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.ServiceContracts.Tags;
using HorrorOnline.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        private UserManager<ApplicationUser> _userManager;

        public StoryController(IStoryAdderService storyAdderService, IStoryGetterService storyGetterService, IStoryDeleterService storyDeleterService, ITagAdderService tagAdderService, ITagGetterService tagGetterService, UserManager<ApplicationUser> userManager)
        {
            _storyAdderService = storyAdderService;
            _storyGetterService = storyGetterService;
            _storyDeleterService = storyDeleterService;

            _tagAdderService = tagAdderService;
            _tagGetterService = tagGetterService;

            _userManager = userManager;
        }

        [Route("/")]
        [Route("[action]")]
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

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                ViewBag.Errors = new List<string>() { "No se pudo encontrar el usuario. Accede como usuario para poder añadirte como autor al relato." };

                return View(storyWithTagsModel);
            }

            StoryAddRequest storyAddRequest = new StoryAddRequest()
            {
                Title = storyWithTagsModel.Title,
                Summary = storyWithTagsModel.Summary,
                Text = storyWithTagsModel.Text,
                Tags = await TagParser(storyWithTagsModel.Tags),
                AuthorId = user.Id
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
        private async Task<ICollection<TagResponse>?> TagParser(string? tagsString)
        {
            if (string.IsNullOrEmpty(tagsString))
                return null;

            List<string> stringTags = tagsString.Split(',').ToList();

            ICollection<TagResponse> returnTags = new List<TagResponse>();
            foreach (string stringTag in stringTags)
            {
                stringTag.Trim();
                returnTags.Add( await GetOrAddTag(stringTag) );
            }

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
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            throw new NotImplementedException();
        }
    }
}
