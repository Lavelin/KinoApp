using System.ComponentModel.DataAnnotations;

namespace KinioApp.ViesModels.GenreKino
{
    public class EditGenreKinoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название жанра")]
        [Display(Name = "Жанр")]
        public string Genre { get; set; }

  
    }
}
