using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoWiki.Models;
using MongoWiki.Services;

namespace MongoWiki.Pages
{
    public class ViewPageModel : PageModel
    {
        private WikiPageService _wikiPageService;
        private WikiPageRevisionService _revisionService;

        public WikiPage WikiPage { get; set; }
        public WikiPageRevision MostRecentRevision { get; set; }

        public ViewPageModel(WikiPageService wikiPageService, WikiPageRevisionService revisionService)
        {
            _wikiPageService = wikiPageService;
            _revisionService = revisionService;
        }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            WikiPage = await _wikiPageService.FindBySlug(slug);
            if (WikiPage == null)
            {
                return NotFound();
            }
            MostRecentRevision = await _revisionService.FindMostRecentByPageId(WikiPage.Id);
            return Page();
        }
    }
}
