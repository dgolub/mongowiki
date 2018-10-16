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

        public WikiPage WikiPage { get; set; }

        public ViewPageModel(WikiPageService wikiPageService)
        {
            _wikiPageService = wikiPageService;
        }

        public void OnGet(string slug)
        {
            WikiPage = _wikiPageService.FindBySlug(slug);
        }
    }
}
