using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Hosting;
using ProCodeGuide.Samples.BrokenAccessControl.Models;
using ProCodeGuide.Samples.BrokenAccessControl.Services;
using System.Security.Claims;

namespace ProCodeGuide.Samples.BrokenAccessControl.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostsService? _postsService = null;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        public IActionResult Index()
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<Post> posts = _postsService.GetAll(userId);
            return View(posts);
        }

        public IActionResult Create()
        {
            return View(new Post());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _postsService.Create(post, userId);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Post post = _postsService.GetById(id);
            return View(post);
        }
    }
}
