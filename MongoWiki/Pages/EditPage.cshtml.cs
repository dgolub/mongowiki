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

        public void OnGet(string slug)
        {
            WikiPage = _wikiPageService.FindBySlug(slug);
            MostRecentRevision = _revisionService.FindMostRecentByPageId(WikiPage.Id);
            NewContent = MostRecentRevision.Content;
        }

        public IActionResult OnPost(string slug)
        {
            WikiPage = _wikiPageService.FindBySlug(slug);
            _revisionService.AddRevision(new WikiPageRevision()
            {
                PageId = WikiPage.Id,
                Created = DateTime.Now,
                Content = NewContent.Replace("\r\n", "\n")
            });
            return RedirectToPage("ViewPage", new { slug = slug });
        }
    }
}
