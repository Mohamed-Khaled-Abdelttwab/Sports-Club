using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsClub.Models
{
	public class Player
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "You have to provide a vaild full name")]
		[MinLength(8, ErrorMessage = "Full name can't be less than 8 characters")]
		[MaxLength(50, ErrorMessage = "Full name must't exceed 50 characters")]
		[Display(Name = "Full Name")]
		public string FullName { get; set; }
		[Required(ErrorMessage = "You have to provide a vaild Nationa lId")]
		[MinLength(14, ErrorMessage = "National Id can't be less than 14 characters")]
		[MaxLength(14, ErrorMessage = "National Id must't exceed 14 characters")]
		[Display(Name = "National Id")]
		public string NationalId { get; set; }

		[Required(ErrorMessage = "You have to provide a vaild Position")]

		[DisplayName("Player Position")]

		public string PlayerPosition { get; set; }

		[Range(10000, 70000, ErrorMessage = "Salary must be between 10000 EGP and 70000")]
        public decimal Salary { get; set; }

        [DataType(DataType.Date)]
		public DateTime Birthdate { get; set; }

        [DisplayName("Player registration date")]
        public DateTime PlayerRegistrationDate { get; set; }

		[DataType(DataType.Time)]
		[DisplayName("Attendence Time")]
		public DateTime Attendence { get; set; }
		[RegularExpression("^01\\d{9}$", ErrorMessage = "Invalid Phone Number")]
		[DisplayName("Phone No")]
		public string PhoneNumber { get; set; }
		[RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$")]
		[DisplayName("Email Address")]
		public string EmailAddress { get; set; }
		[NotMapped]
		[Compare("EmailAddress", ErrorMessage = "Email and Confirm Email don't match")]
		[DisplayName("Confirm Email Address")]
		public string ConfirmationEmailAddress { get; set; }
		[ValidateNever]
		public string? Notes { get; set; }
		[DisplayName("Sport")]
		[Range(1, double.MaxValue, ErrorMessage = "Choose a vaild Sport")]
		public int SportId { get; set; }
		[ValidateNever]
		public Sport sport { get; set; }
		[ValidateNever]
		public string? ImagePath { get; set; }

		[NotMapped]
		[DisplayName("Image")]
		[ValidateNever]
		public IFormFile ImageFile { get; set; }


	}
}
