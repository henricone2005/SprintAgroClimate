
using System.ComponentModel.DataAnnotations;

namespace AgroClimate.Models
{
    public class AgricultorFazenda
    {
        public int AgricultorId { get; set; }
        public Agricultor Agricultor { get; set; } = new Agricultor(); // Inicialize com um novo objeto

        public int FazendaId { get; set; }
        public Fazenda Fazenda { get; set; } = new Fazenda(); // Inicialize com um novo objeto
    }
}
