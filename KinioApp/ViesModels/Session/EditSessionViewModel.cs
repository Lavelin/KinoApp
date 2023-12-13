using System.ComponentModel.DataAnnotations;

namespace KinioApp.ViesModels.Session
{
    public class EditSessionViewModel
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите индекс сеанса")]
        [Display(Name = "Индекс")]
        public string Code { get; set; }

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
