using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KinioApp.Models.Data
{
    public class Session
    {
        // Key - поле первичный ключ
        // DatabaseGenerated(DatabaseGeneratedOption.Identity) - поле автоинкреметное
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите дату и время начала сеанса")]
        [Display(Name = "Время сеанса")]
        public string DateAndTime { get; set; }

        [Required(ErrorMessage = "Введите зал")]
        [Display(Name = "Зал")]
        public string Hall { get; set; }

        [Required(ErrorMessage = "Выберите фильм")]
        public short MovieId { get; set; }

        /* [Required]
         public ICollection<Movie> Movies { get; set; }*/

        // Навигационные свойства
        [Display(Name = "Фильм")]
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
    }
}
