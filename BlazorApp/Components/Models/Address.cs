using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Components.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required, MinLength(3, ErrorMessage = "Por favor, use un nombre de más de 3 letras."), MaxLength(100, ErrorMessage = "Utilice un nombre de menos de 100 letras.")]
        public string Name { get; set; }

        [Required, MinLength(5, ErrorMessage = "Utilice una dirección de más de 5 letras."), MaxLength(100, ErrorMessage = "Utilice una dirección de menos de 100 letras.")]
        public string Line1 { get; set; }

        [MaxLength(100)]
        public string Line2 { get; set; }

        [Required, MinLength(3, ErrorMessage = "Please use a City bigger than 3 letters."), MaxLength(50, ErrorMessage = "Please use a City less than 50 letters.")]
        public string City { get; set; }

        [Required, MinLength(3, ErrorMessage = "Utilice una ciudad de más de 3 letras."), MaxLength(20, ErrorMessage = "Por favor, utilice una Región de menos de 20 letras.")]
        public string Region { get; set; }

        [Required, RegularExpression(@"^([0-9]{5})$", ErrorMessage = "Utilice un código postal válido de cinco números.")]
        public string PostalCode { get; set; }
    }
}
