using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace magnadigi.Models
{
    [ApiController]
    [Route("[Controller]")]
    [BindProperties(SupportsGet = true)]
    public class ViewModelBundle
    {

        [NotMapped]
        public List<string>? ProjectSelectList { get; set; } 

        [NotMapped]
        public List<TaskModel>? taskList { get; set; }

        [NotMapped]
        public TaskModel? taskModel { get; set; }

        [NotMapped]
        public UserModel? userModel { get; set; }

        [NotMapped]
        public string? JsonTasks { get; set; }

        [NotMapped]
        public List<FilesUploadedModel> FilesUploaded = new List<FilesUploadedModel>();

        [NotMapped]
        public FilesUploadedModel? FilesUploadedModel { get; set; }

        [Key]
        public int FileId { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Display(Name = "Upload Name")]
        public string? FileName { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? FilePath { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? FileSize { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? FileUploadedBy { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? ProjectId { get; set; }

        [NotMapped]
        [Display(Name = "Upload File")]
        [BindProperty(SupportsGet = true, Name = "FormFile")]
        public IFormFile? FormFile { get; set; }




    }
}
