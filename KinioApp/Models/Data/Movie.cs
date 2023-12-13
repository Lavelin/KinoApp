using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace KinioApp.Models.Data
{
    public class Movie
    {
        // Key - поле первичный ключ
        // DatabaseGenerated(DatabaseGeneratedOption.Identity) - поле автоинкреметное
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите название фильма")]
        [Display(Name = "Фильм")]
        public string MovieTitle { get; set; }

        [Required(ErrorMessage = "Введите страну")]
        [Display(Name = "Страна")]
        public string CountryMovie { get; set; }

        [Required(ErrorMessage = "Введите год выпуска")]
        [Display(Name = "Год выпуска")]
        public string YearMovie { get; set; }

        [Required(ErrorMessage = "Выберите жанр")]
        public short GenreId { get; set; }

        [Required(ErrorMessage = "Введите длительность")]
        [Display(Name = "Длительность")]
        public string DurationMovie { get; set; }

        [Required(ErrorMessage = "Введите дату начала проката")]
        [Display(Name = "Дата начала проката")]
        public string StartOfRentalMovie { get; set; }

        [Required(ErrorMessage = "Введите дату окончания проката")]
        [Display(Name = "Дата окончания проката")]
        public string EndOfRentalMovie { get; set; }

        [Required(ErrorMessage = "Введите название компании прокатчика")]
        [Display(Name = "Компания прокатчика")]
        public string RentalCompanyMovie { get; set; }


        // Навигационные свойства
        [Display(Name = "Жанр")]
        [ForeignKey("GenreId")]
        public GenreKino GenreKino { get; set; }


        //public ICollection<Group> Groups { get; set; }

    }
}
