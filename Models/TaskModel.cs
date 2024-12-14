using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace magnadigi.Models
{
    [ApiController]
    [Route("[Controller]")]
    [BindProperties(SupportsGet = true)]
    public class TaskModel
    {
        [Key]
        [Required]
        [BindProperty(SupportsGet = true, Name = "TaskRef")]
        [DisplayName("Task Reference")]
        public string? TaskRef { get; set; } = "N/A";

        [Required]
        [BindProperty(SupportsGet = true, Name = "Project")]
        [DisplayName("Project")]
        public string? Project { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "Email")]
        [DisplayName("Email Address")]
        public string? Email { get; set; } = String.Empty;

        [Required]
        [BindProperty(SupportsGet = true, Name = "Details")]
        [DisplayName("Update Details")]
        public string? Details { get; set; } = "";

        [Required]
        [BindProperty(SupportsGet = true, Name = "Description")]
        [DisplayName("Task Description")]
        public string? Subject { get; set; } = "";

        [Required]
        [BindProperty(SupportsGet = true, Name = "StartDateTime")]
        [DisplayName("Date/Time Started")]
        public DateTime? StartDateTime { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "ExpEndDdate")]
        [DisplayName("Due Date")]
        public DateTime? ExpEndDate { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "PriorityLevel")]
        [DisplayName("Priority Level")]
        public string? PriorityLevel { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "Status")]
        [DisplayName("Status")]
        public string? Status { get; set; }
        public string? PriorStatus { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string? Progress { get; set; } 
        public string? DetailsString { get; set; }


    }

}



