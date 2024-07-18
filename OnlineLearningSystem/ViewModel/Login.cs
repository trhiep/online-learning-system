﻿using System.ComponentModel.DataAnnotations;

namespace OnlineLearningSystem.ViewModel
{
	public class Login
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }	
	}
}
