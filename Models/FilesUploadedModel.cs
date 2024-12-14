using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace magnadigi.Models
{
    [ApiController]
    [Route("[Controller]")]
    [BindProperties(SupportsGet = true)]
    public class FilesUploadedModel{

        [Key]
        [Required]
        [BindProperty(SupportsGet = true, Name = "FileId")]
        [DisplayName("File Id")]
        public string? FileId { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "UploadedDate")]
        [DisplayName("Upload Date")]
        public string? UploadedDate { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "FileName")]
        [DisplayName("File Name")]
        public string? FileName { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "FilePath")]
        [DisplayName("File Path")]
        public string? FilePath { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "FileUploadedBy")]
        [DisplayName("File Uploaded By")]
        public string? FileUploadedBy { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "ProjectId")]
        [DisplayName("Project Id")]
        public string? ProjectId { get; set; }

    }

}



