using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;


namespace WebApplication2.ViewModels
{
    public class OrderViewModel
    {

        [Required(ErrorMessage = "Город отправителя обязателен")]
        public string Sendercity { get; set; } = null!;

        [Required(ErrorMessage = "Адрес отправителя обязателен")]
        public string Senderaddress { get; set; } = null!;

        [Required(ErrorMessage = "Город получателя обязателен")]
        public string Recipientcity { get; set; } = null!;

        [Required(ErrorMessage = "Адрес получателя обязателен")]
        public string Recipientaddress { get; set; } = null!;

        [Required(ErrorMessage = "Вес груза обязателен")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Вес груза должен быть больше 0")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Cargoweight { get; set; }

        [Required(ErrorMessage = "Дата забора обязательна")]
        public DateOnly Pickupdate { get; set; }
    }
}
