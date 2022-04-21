using BlogsApi.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogsApi.Dtos
{
    public class CategoryDTOS
    {
        [Required(ErrorMessage = "Category Name Is Required")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Category Slug Is Required")]
        public string Slug { get; set; }
        [Required(ErrorMessage = "Category Slug Is Required")]
        public Status CategoryStatus { get; set; }
    }

    public class ShowCategoryDTOS
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public Status CategoryStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
