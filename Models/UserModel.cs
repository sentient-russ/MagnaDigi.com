using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

/*
 * This model is used post to proceess user specific data.  It's attributes are populated via UsersDAO and TodoDAO at present.
 * note that authentication process uses Identity Server data models intigrated with each management cshtml.cs file to define data fields.
 * This model is used for API user management
 * @Author Russell Steele 8/01/2022
 * 
 */

namespace magnadigi.Models

{
    public class UserModel
    {
        //Users identity account specifics begin here. 
        [DisplayName("User Id")]
        public string? Id { get; set; } = "";
        

        [StringLength(40, MinimumLength = 2)]
        [DisplayName("Business Name")]
        [Required]
        public string? Business { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 4)]
        [DisplayName("User Name")]
        public string? UserName { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 4)]
        [DisplayName("First Name")]
        public string? FirstName { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 4)]
        [DisplayName("Last Name")]
        public string? LastName { get; set; } = string.Empty;

        [StringLength(40, MinimumLength = 4)]
        [DisplayName("Normalized UserName")]
        public string? NormalizedUserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(40, MinimumLength = 6)]
        [DisplayName("Email Address")]
        [Required]
        public string? Email { get; set; } = String.Empty;

        [DataType(DataType.PhoneNumber)]
        [StringLength(12, MinimumLength = 10)]
        [DisplayName("PhoneNumber")]
        [Required]
        public string? Phone { get; set; } = string.Empty;



    }

}


