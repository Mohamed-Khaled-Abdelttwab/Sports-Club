using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace SportsClub.Models
{
	public class Sport
	{
		public int Id { get; set; }
		[DisplayName("Sport")]
		public string Name { get; set; }
		public string Description { get; set; }
		[ValidateNever]
		public List<Player> Players { get; set; }
	}
}
