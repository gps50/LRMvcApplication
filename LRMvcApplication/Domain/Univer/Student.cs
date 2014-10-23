using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using LRMvcApplication.Libraries.LambdaExpressions;
using LRMvcApplication.Services.Repository;

namespace LRMvcApplication.Domain.Univer
{
	public class Student
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(10)]
		public string FirstName { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(10)]
		public string LastName { get; set; }
	}
	
}