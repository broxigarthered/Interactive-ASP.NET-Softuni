using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Interactive.Data;
using Interactive.Services.Extensions;
using Interactive.Models.EntityModels;
using Interactive.Models.ViewModels;
using Interactive.Services;
using Interactive.Services.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Interactive.Controllers
{
    public class PostsController : Controller
    {
        private InteractiveContext db = new InteractiveContext();
        private PostsService service;

        public PostsController()
        {
            this.service = new PostsService(db);
        }

        // GET: Posts
        public ActionResult Index()
        {
            var posts = this.service.GetAllPosts();
            return View(posts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            var request = this.service.GetHttpRequest(id);

            if (request != null)
            {
                return request;
            }

            Post post = this.service.GetPostById(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        //// POST: Posts/Comment/5
        //public ActionResult PostComment(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    // TODO: Get comment from area
        //    // Form.get("textarea")
        //    // Comment currentComment = new Comment (postId, commentStr)
        //    // Post.
        //    return View("Post.html");

        //}

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Body")] PostViewModel postViewModel)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = this.GetUser();
                var userToSet = db.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();

                if (user != null)
                {
                    this.service.AddNewPost(postViewModel, userToSet);
                }

                this.AddNotification("Post created successfully.", NotificationType.INFO);

                return RedirectToAction("Index");
            }

            return View(postViewModel);
        }

        // GET: Posts/Edit/5

        //[Authorize(Roles = "Administrators")]
        public ActionResult Edit(int? id)
        {

            ApplicationUser user = this.GetUser();

            Post post = service.GetPostById(id);

            if (user == null)
            {
                this.AddNotification("You have to be logged in, in order to edit that post.", NotificationType.INFO);

                return RedirectToAction("Index");
            }

            bool userIsAdmin = this.service.CheckUserAdmin(user.Id, post.Author_Id);

            if (userIsAdmin)
            {
                IEnumerable<ApplicationUser> authors = this.service.GetAllAuthors();
                ViewBag.Authors = authors;

                return View(post);
            }

            this.AddNotification("You cannot edit that post.", NotificationType.INFO);

            return RedirectToAction("Index");
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Administrators")]
        public ActionResult Edit([Bind(Include = "ID,Title,Body,Date,Author_ID")] Post post)
        {
            ApplicationUser user = this.GetUser();

            bool postEditted = this.service.EditPost(user, post, ModelState.IsValid);

            if (postEditted)
            {
                this.AddNotification("Post edited successfully.", NotificationType.INFO);
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<ApplicationUser> authors = this.service.GetAllAuthors();
                ViewBag.Authors = authors;
                return View(post);
            }
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = this.service.GetPostById(id);

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.service.DeletePost(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ApplicationUser GetUser()
        {
            return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }
    }
}
