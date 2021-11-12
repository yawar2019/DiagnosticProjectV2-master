using System.ComponentModel.DataAnnotations;

namespace NamrataKalyani.Models
{
    public class ServiceModel
    {
        [Key]
        public int ServiceNo { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public int OP { get; set; }
        [Required]
        public int LDSL { get; set; }
        [Required]
        public string Method { get; set; }
        [Required]
        public string Sample { get; set; }
        [Required]
        public string Schedule { get; set; }
        [Required]
        public string CutOfTime { get; set; }
        [Required]
        public string TAT { get; set; }
        [Required]
        public string Vacutainer { get; set; }
    }
}


 