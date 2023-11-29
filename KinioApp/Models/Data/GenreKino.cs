using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KinioApp.Models.Data
{
    public class GenreKino
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display (Name ="Ид")]

        public int Id { get; set; }

        [Required (ErrorMessage ="Введите название жанра")]
        [Display(Name ="Жанр")]

        public string Genre { get; set; }

    }
}
 