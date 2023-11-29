using System.ComponentModel.DataAnnotations;

namespace KinioApp.ViesModels.GenreKino
{
    public class CreateGenreKinoViewModel
    {
        [Required(ErrorMessage = "Введите название жанра")]
        [Display(Name = "Жанр")]

        public string Genre { get; set; }
    }
}
