using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using magnadigi.Data;
using magnadigi.Models;
using magnadigi.Services.ISecurityService;
using magnadigi.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace magnadigi.Controllers
{
    
    [BindProperties(SupportsGet = true)]
    public class PortalController : Controller

    {
        private readonly magnadigiContext _context;

        public PortalController(magnadigiContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
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

        // POST: TaskModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
    }
}

