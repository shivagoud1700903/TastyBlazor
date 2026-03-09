using System.ComponentModel.DataAnnotations;
namespace TastyBlazor.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Requied")]
        public string Name { get; set; }
    }
}
