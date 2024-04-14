﻿using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Domain.Categories
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
