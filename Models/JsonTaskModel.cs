using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace magnadigi.Models
{
    [ApiController]
    [Route("[Controller]")]
    [BindProperties(SupportsGet = true)]
    public class JsonTaskModel
	{
        [Required]
        [BindProperty(SupportsGet = true, Name = "Started")]
        [DisplayName("Started")]
        public string? start { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "Ending")]
        [DisplayName("Ending")]
        public string? end { get; set; }
        
        [Required]
        [BindProperty(SupportsGet = true, Name = "Name")]
        [DisplayName("Name")]
        public string? name { get; set; }
        [Key]
        [Required]
        [BindProperty(SupportsGet = true, Name = "id")]
        [DisplayName("id")]
        public string? id { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "Progress")]
        [DisplayName("Progress")]
        public string? progress { get; set; }

        [Required]
        [BindProperty(SupportsGet = true, Name = "Progress")]
        [DisplayName("Progress")]
        public string? dependencies { get; set; }


    }
}
