using System.Collections.Generic;  // Para List<T> e IEnumerable<T>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroClimate.Models
{
    public class Agricultor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
       public  string Nome { get; set; }

        [Required]
        [StringLength(11)]
        public string Cpf { get; set; }

         public List<AgricultorFazenda> AgricultorFazendas { get; set; } = new List<AgricultorFazenda>();
        
        
    }
}
