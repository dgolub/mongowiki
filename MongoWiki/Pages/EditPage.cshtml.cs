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

        [BindProperty]
        public string NewComment { get; set; }

        public EditPageModel(WikiPageService wikiPageService, WikiPageRevisionService revisionService)
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
            NewContent = MostRecentRevision?.Content;
            if (MostRecentRevision == null)
            {
                NewComment = "Initial revision";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string slug)
        {
            WikiPage = await _wikiPageService.FindBySlug(slug);
            if (WikiPage == null)
            {
                return NotFound();
            }
            MostRecentRevision = await _revisionService.FindMostRecentByPageId(WikiPage.Id);
            await _revisionService.AddRevision(new WikiPageRevision()
            {
                PageId = WikiPage.Id,
                RevisionNumber = (MostRecentRevision?.RevisionNumber ?? 0) + 1,
                Created = DateTime.Now,
                Content = NewContent.Replace("\r\n", "\n"),
                Comment = NewComment
            });
            return RedirectToPage("ViewPage", new { slug });
        }
    }
}
