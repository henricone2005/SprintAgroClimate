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
       public required string Nome { get; set; }

        [Required]
        [StringLength(11)]
        public required string Cpf { get; set; }

         
        
        
    }
}
