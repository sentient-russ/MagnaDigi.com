using Microsoft.AspNetCore.Mvc;
using magnadigi.Models;
using System.Diagnostics;
using magnadigi.Data;
using magnadigi.Services;
using magnadigi.Services.ISecurityService;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using magnadigi.Data;
using magnadigi.Models;
using magnadigi.Services.ISecurityService;
using magnadigi.Services;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace magnadigi.Controllers
{
    [BindProperties(SupportsGet = true)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly magnadigiContext _context;
        public IWebHostEnvironment _env;
        public FileSaver _fileSaver;

        public HomeController(ILogger<HomeController> logger, magnadigiContext context, IWebHostEnvironment env)
        {
            this._logger = logger;
            this._context = context;
            this._env = env;
            this._fileSaver = new FileSaver(this._env);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult CaseStudyMomPop()
        {
            return View();
        }
        public IActionResult CaseStudyMomPopERDiagram()
        {
            return View();
        }
        public IActionResult CaseStudyMagnadigi()
        {
            return View();
        }
        public IActionResult CaseStudyOnAccount()
        {
            return View();
        }
        public IActionResult CaseStudyDisasterAversion()
        {
            return View();
        }
        public IActionResult CaseStudyGroupConscience()
        {
            return View();
        }
        public IActionResult WeddingUpload()
        {
            return View();
        }
        public IActionResult WeddingUploadConfirmed()
        {
            return View();
        }
        //File Upload
        [HttpPost]
        public async Task<IActionResult> WeddingUploadFile([Bind("FromFile")] IFormFile FormFile)
        {
            List<FilesUploadedModel> filesUploaded = new List<FilesUploadedModel>();
            FilesDAO filesDAO = new FilesDAO();
            string Project = "PROJ-00001";
            string User = "russell@magnadigi.com";
            try
            {
                await _fileSaver.WeddingFileSaveAsync(FormFile, "ClientFiles", User, Project);
                //filesUploaded = filesDAO.GetUploadedFileList(User.Identity.Name);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            ViewModelBundle viewModelBundle = new ViewModelBundle();
            return RedirectToAction(nameof(WeddingUploadConfirmed), viewModelBundle);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
