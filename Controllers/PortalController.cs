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
    public class PortalController : Controller

    {
        private readonly magnadigiContext _context;
        IWebHostEnvironment _env;
        private FileSaver _fileSaver;
        public PortalController(magnadigiContext context, IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
            this._fileSaver = new FileSaver(this._env);
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //retrievs loged in user account details from .net database
            ISecurityService securityService = new ISecurityService();
            UserModel userModel = new UserModel();
            userModel.UserName = User.Identity.Name;
            UsersDAO user = new UsersDAO();
            userModel = user.getUser(userModel);
            ViewData["fname"] = userModel.FirstName;
            ViewData["lname"] = userModel.LastName;
            ViewData["bname"] = userModel.Business;
            ViewData["phone"] = userModel.Phone;
            ViewData["email"] = userModel.Email;
            //retreives the users tasks from the erp.memoryspace.io database
            List<TaskModel> tasks = new List<TaskModel>();
            TaskDAO todoDAO = new TaskDAO();
            tasks = todoDAO.GetTaskItems(userModel.Email);
            //Currently being used to retrieve tasks for the gantt feature
            List<JsonTaskModel> JsonTasks = new List<JsonTaskModel>();
            TaskDAO JsonToDoDAO = new TaskDAO();
            JsonTasks = JsonToDoDAO.GetJsonTaskItems(userModel.Email);
            string formattedJson = Newtonsoft.Json.JsonConvert.SerializeObject(JsonTasks);
            //retreives the uploaded files
            List<FilesUploadedModel> filesUploaded = new List<FilesUploadedModel>();
            FilesDAO filesDAO = new FilesDAO();
            filesUploaded = filesDAO.GetUploadedFileList(User.Identity.Name);
            //creates dropdown list containing projects for assigned user for file upload association
            var Projects = new List<string>();
            for (int i = 0; i < tasks.Count; i++)
            {
                if (!Projects.Contains(tasks[i].Project))
                {
                    Projects.Add(tasks[i].Project);
                }
            }
            ViewBag.Projects = new SelectList(Projects);
            //creates and loads data bundle model to be passed to view
            ViewModelBundle viewModelBundle = new ViewModelBundle();
            viewModelBundle.taskList = tasks;
            viewModelBundle.FilesUploaded = filesUploaded;
            viewModelBundle.ProjectSelectList = Projects;
            viewModelBundle.JsonTasks = formattedJson;
            ViewData["JsonTasks"] = formattedJson;
            return View(viewModelBundle);
        }
        // GET: TaskModels/Create
        [Authorize]
        public IActionResult CreateNew()
        {
            ISecurityService securityService = new ISecurityService();
            UserModel userModel = new UserModel();
            userModel.UserName = User.Identity.Name;
            UsersDAO user = new UsersDAO();
            userModel = user.getUser(userModel);
            ViewData["fname"] = userModel.FirstName;
            ViewData["lname"] = userModel.LastName;
            ViewData["bname"] = userModel.Business;
            ViewData["phone"] = userModel.Phone;
            ViewData["email"] = userModel.Email;

            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditTask([Bind("TaskRef, Project, Email, Details, DateTime, StartDateTime, DateTime, ExpEndDate, PriorityLevel, Status, PriorStatus, CompletedOn, CompletedBy")] TaskModel taskModelIn)
        {
            ISecurityService securityService = new ISecurityService();
            UserModel userModel = new UserModel();
            userModel.UserName = User.Identity.Name;
            UsersDAO user = new UsersDAO();
            userModel = user.getUser(userModel);
            ViewData["fname"] = userModel.FirstName;
            ViewData["lname"] = userModel.LastName;
            ViewData["bname"] = userModel.Business;
            ViewData["phone"] = userModel.Phone;
            ViewData["email"] = userModel.Email;
            TaskDAO desAccess = new TaskDAO();
            string oldDescription = desAccess.GetTaskDetails(taskModelIn.TaskRef);
            desAccess.PutTask(taskModelIn, userModel, oldDescription);
            return RedirectToAction(nameof(Index));
        }
        // GET: TaskModels/Edit
        [HttpGet]
        [Authorize]
        [Route("PortalController/Edit/{TaskRef}")]
        public async Task<IActionResult> Edit([FromRoute] string? TaskRef)
        {
            ISecurityService securityService = new ISecurityService();
            UserModel userModel = new UserModel();
            userModel.UserName = User.Identity.Name;
            UsersDAO user = new UsersDAO();
            userModel = user.getUser(userModel);
            ViewData["fname"] = userModel.FirstName;
            ViewData["lname"] = userModel.LastName;
            ViewData["bname"] = userModel.Business;
            ViewData["phone"] = userModel.Phone;
            ViewData["email"] = userModel.Email;
            ViewData["taskid"] = TaskRef;
            TaskDAO taskDAO = new TaskDAO();
            TaskModel editTask = new TaskModel();
            editTask = taskDAO.GetTask(TaskRef, userModel.Email);
            editTask.DetailsString = editTask.Details;
            editTask.Details = "";
            return View(editTask);
        }
        //File Upload
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile([Bind("FromFile, Projects")] IFormFile FormFile, string Projects)
        {
            List<FilesUploadedModel> filesUploaded = new List<FilesUploadedModel>();
            FilesDAO filesDAO = new FilesDAO();
            try
            {
                await _fileSaver.FileSaveAsync(FormFile, "ClientFiles", User.Identity.Name, Projects);
                filesUploaded = filesDAO.GetUploadedFileList(User.Identity.Name);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            //retries loged in user account details from .net database
            ISecurityService securityService = new ISecurityService();
            UserModel userModel = new UserModel();
            userModel.UserName = User.Identity.Name;
            UsersDAO user = new UsersDAO();
            userModel = user.getUser(userModel);
            ViewData["fname"] = userModel.FirstName;
            ViewData["lname"] = userModel.LastName;
            ViewData["bname"] = userModel.Business;
            ViewData["phone"] = userModel.Phone;
            ViewData["email"] = userModel.Email;
            //retreives the users tasks from the erp.memoryspace.io database
            List<TaskModel> tasks = new List<TaskModel>();
            TaskDAO todoDAO = new TaskDAO();
            tasks = todoDAO.GetTaskItems(userModel.Email);
            List<JsonTaskModel> JsonTasks = new List<JsonTaskModel>();
            TaskDAO JsonToDoDAO = new TaskDAO();
            JsonTasks = JsonToDoDAO.GetJsonTaskItems(userModel.Email);
            string formattedJson = Newtonsoft.Json.JsonConvert.SerializeObject(JsonTasks);
            ViewModelBundle viewModelBundle = new ViewModelBundle();
            viewModelBundle.taskList = tasks;
            viewModelBundle.FilesUploaded = filesUploaded;
            viewModelBundle.JsonTasks = formattedJson;
            ViewData["JsonTasks"] = formattedJson;
            return RedirectToAction(nameof(Index), viewModelBundle);
        }
        //File Deletion
        [HttpGet]
        [Authorize]
        [Route("PortalController/DeleteFile/{FileId}")]
        public async Task<IActionResult> DeleteFile([FromRoute] string FileId)
        {
            List<FilesUploadedModel> filesUploaded = new List<FilesUploadedModel>();
            FilesDAO filesDAO = new FilesDAO();
            try
            {
                filesDAO.FileDeleteAsync(FileId, User.Identity.Name);
                filesUploaded = filesDAO.GetUploadedFileList(User.Identity.Name);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            //retries loged in user account details from .net database
            ISecurityService securityService = new ISecurityService();
            UserModel userModel = new UserModel();
            userModel.UserName = User.Identity.Name;
            UsersDAO user = new UsersDAO();
            userModel = user.getUser(userModel);
            ViewData["fname"] = userModel.FirstName;
            ViewData["lname"] = userModel.LastName;
            ViewData["bname"] = userModel.Business;
            ViewData["phone"] = userModel.Phone;
            ViewData["email"] = userModel.Email;
            //retreives the users tasks from the erp.memoryspace.io database
            List<TaskModel> tasks = new List<TaskModel>();
            TaskDAO todoDAO = new TaskDAO();
            tasks = todoDAO.GetTaskItems(userModel.Email);
            List<JsonTaskModel> JsonTasks = new List<JsonTaskModel>();
            TaskDAO JsonToDoDAO = new TaskDAO();
            JsonTasks = JsonToDoDAO.GetJsonTaskItems(userModel.Email);
            string formattedJson = Newtonsoft.Json.JsonConvert.SerializeObject(JsonTasks);
            ViewModelBundle viewModelBundle = new ViewModelBundle();
            viewModelBundle.taskList = tasks;
            viewModelBundle.FilesUploaded = filesUploaded;
            viewModelBundle.JsonTasks = formattedJson;
            ViewData["JsonTasks"] = formattedJson;
            return RedirectToAction(nameof(Index), viewModelBundle);
        }
    }
}

