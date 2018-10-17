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
    public class EditPageModel : PageModel
    {
        private WikiPageService _wikiPageService;
        private WikiPageRevisionService _revisionService;

        public WikiPage WikiPage { get; set; }
        public WikiPageRevision MostRecentRevision { get; set; }

        [BindProperty]
        public string NewContent { get; set; }

        public EditPageModel(WikiPageService wikiPageService, WikiPageRevisionService revisionService)
        {
            _wikiPageService = wikiPageService;
            _revisionService = revisionService;
        }

        public IActionResult OnGet(string slug)
        {
            WikiPage = _wikiPageService.FindBySlug(slug);
            if (WikiPage == null)
            {
                return NotFound();
            }
            MostRecentRevision = _revisionService.FindMostRecentByPageId(WikiPage.Id);
            if (MostRecentRevision == null)
            {
                return NotFound();
            }
            NewContent = MostRecentRevision.Content;
            return Page();
        }

        public IActionResult OnPost(string slug)
        {
            WikiPage = _wikiPageService.FindBySlug(slug);
            if (WikiPage == null)
            {
                return NotFound();
            }
            MostRecentRevision = _revisionService.FindMostRecentByPageId(WikiPage.Id);
            _revisionService.AddRevision(new WikiPageRevision()
            {
                PageId = WikiPage.Id,
                RevisionNumber = (MostRecentRevision?.RevisionNumber ?? 0) + 1,
                Created = DateTime.Now,
                Content = NewContent.Replace("\r\n", "\n")
            });
            return RedirectToPage("ViewPage", new { slug = slug });
        }
    }
}
