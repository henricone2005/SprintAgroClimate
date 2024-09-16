
using System.ComponentModel.DataAnnotations;

namespace AgroClimate.Models
{
    public class AgricultorFazenda
    {
        public int AgricultorId { get; set; }
        public required Agricultor Agricultor { get; set; } // Se não for anulável, deve ser inicializado

        public int FazendaId { get; set; }
        public required Fazenda Fazenda { get; set; } // Se não for anulável, deve ser inicializado
    }
}
