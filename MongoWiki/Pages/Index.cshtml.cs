using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoWiki.Models;
using MongoWiki.Services;
using System.Collections.Generic;

namespace MongoWiki.Pages
{
    public class IndexModel : PageModel
    {
        private WikiPageService _wikiPageService;

        public List<WikiPage> WikiPages { get; set; }

        public IndexModel(WikiPageService wikiPageService)
        {
            _wikiPageService = wikiPageService;
        }

        public void OnGet()
        {
            WikiPages = _wikiPageService.FindAll();
        }
    }
}
