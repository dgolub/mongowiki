using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoWiki.Services;

namespace MongoWiki.Pages
{
    public class CreatePageModel : PageModel
    {
        private WikiPageService _wikiPageService;

        [BindProperty]
        public string Name { get; set; }

        public CreatePageModel(WikiPageService wikiPageService)
        {
            _wikiPageService = wikiPageService;
        }

        public IActionResult OnPost()
        {
            string slug = _wikiPageService.AddPage(Name);
            return RedirectToPage("EditPage", new { slug });
        }
    }
}
