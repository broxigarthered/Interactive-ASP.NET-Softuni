using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Interactive.Data;
using Interactive.Models.EntityModels;
using Interactive.Services;

namespace Interactive.Controllers
{
    public class HomeController : Controller
    {
        InteractiveContext db = new InteractiveContext();
        private HomeService service;

        public HomeController()
        {
            this.service = new HomeService(db);
        }

        public ActionResult Index(int page = 0)
        {
            const int PagePostsCount = 3;

            var count = this.db.Posts.Count();

            IEnumerable<Post> posts = this.service.GetPostsFromDataBase(page);

            this.ViewBag.MaxPage = (count / PagePostsCount) - (count % PagePostsCount == 0 ? 1 : 0);
            this.ViewBag.Page = page;

            IEnumerable<Post> postsForSideBars = this.service.GetPostsForSidebars();

            ViewBag.SidebarPosts = postsForSideBars.ToList();

            return View(posts.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}