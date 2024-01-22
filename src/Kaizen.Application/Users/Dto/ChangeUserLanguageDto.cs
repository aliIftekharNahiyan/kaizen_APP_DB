using System.ComponentModel.DataAnnotations;

namespace Kaizen.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}