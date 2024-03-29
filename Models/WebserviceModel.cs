using System.ComponentModel.DataAnnotations;

namespace WebserviceApp.Models {

    public class WebserviceModel {
        // prop
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        [Url]
        public string? Url { get; set; }
        [Required]
        public bool ApiKeyRequired { get; set; } = false;
        public string? CreatedBy { get; set; }
    }

}