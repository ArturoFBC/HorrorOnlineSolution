using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.DTO;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.ServiceContracts.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HorrorOnline.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class TagsController : Controller
    {
        public readonly ITagGetterService _tagGetterService;
        public readonly IStoryGetterService _storyGetterService;

        public TagsController(ITagGetterService tagGetterService, IStoryGetterService storyGetterService)
        {
            _tagGetterService = tagGetterService;
            _storyGetterService = storyGetterService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            IEnumerable<TagResponse> tags = await _tagGetterService.GetAllTags();

            return View(tags);
        }

        [HttpGet, Route("{tagName:alpha}")]
        [AllowAnonymous]
        public async Task<ActionResult> TagByName(string tagName)
        {
            TagResponse? tagResponse = await _tagGetterService.GetTagByName(tagName);

            return View();
        }

        [HttpGet, Route("{tagId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult> TagByID(Guid tagId)
        {
            IEnumerable<StoryResponse>? storiesWithTag = await _storyGetterService.GetStoriesByTagId(tagId);

            return View("~/Views/Story/Index.cshtml", storiesWithTag);
        }
    }
}
