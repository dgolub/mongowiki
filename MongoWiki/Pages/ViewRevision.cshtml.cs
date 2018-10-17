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
    public class ViewRevisionModel : PageModel
    {
        private WikiPageService _wikiPageService;
        private WikiPageRevisionService _revisionService;

        public WikiPage WikiPage { get; set; }
        public WikiPageRevision Revision { get; set; }

        public ViewRevisionModel(WikiPageService wikiPageService, WikiPageRevisionService revisionService)
        {
            _wikiPageService = wikiPageService;
            _revisionService = revisionService;
        }

        public IActionResult OnGet(string slug, int revisionNumber)
        {
            WikiPage = _wikiPageService.FindBySlug(slug);
            if (WikiPage == null)
            {
                return NotFound();
            }
            Revision = _revisionService.FindByRevisionNumber(WikiPage.Id, revisionNumber);
            if (Revision == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
