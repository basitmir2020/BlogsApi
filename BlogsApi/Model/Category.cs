using BlogsApi.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogsApi.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public Status CategoryStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
