using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroClimate.Models;

public class Agricultor
{ 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // ID auto incremento

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(11)]
        public string? cpf { get; set; }
public ICollection<AgricultorFazenda> AgricultorFazendas { get; set; }
}
