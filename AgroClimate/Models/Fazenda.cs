
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroClimate.Models
{
    public class Fazenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public  string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public  string Area { get; set; }

public List<AgricultorFazenda> AgricultorFazendas { get; set; } = new List<AgricultorFazenda>();
        

        
      
    }

}