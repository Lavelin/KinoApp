using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KinioApp.Models.Data
{
    public class GenreKino
    {
        // Key - поле первичный ключ
        // DatabaseGenerated(DatabaseGeneratedOption.Identity) - поле автоинкреметное
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите название жанра")]
        [Display(Name = "Жанр")]
        public string Genre { get; set; }

        [Required]
        public ICollection<Movie> Movies { get; set; }

    }
}
 