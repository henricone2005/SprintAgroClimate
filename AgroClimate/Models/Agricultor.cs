
using System.Collections.Generic;
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
        public string Nome { get; set; } = string.Empty; // Inicializando como string vazia

        [Required]
        [StringLength(11)]
        public string Cpf { get; set; } = string.Empty; // Inicializando como string vazia

        // Inicializando a coleção de Fazendas
        public ICollection<Fazenda> Fazendas { get; set; } = new List<Fazenda>();
    }
}

