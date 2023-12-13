using System.ComponentModel.DataAnnotations;

namespace KinioApp.ViesModels.Session
{
    public class CreateSessionViewModel
    {
        [Required(ErrorMessage = "Введите дату и время начала сеанса")]
        [Display(Name = "Время сеанса")]
        public string DateAndTime { get; set; }

        [Required(ErrorMessage = "Введите зал")]
        [Display(Name = "Зал")]
        public string Hall { get; set; }

        [Required(ErrorMessage = "Выберите фильм")]
        public short MovieId { get; set; }
    }
}
