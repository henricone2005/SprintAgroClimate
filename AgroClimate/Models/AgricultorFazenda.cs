namespace AgroClimate.Models;

public class AgricultorFazenda
{
    public int AgricultorId { get; set; }
    public Agricultor Agricultor { get; set; }
    public int FazendaId { get; set; }
    public Fazenda Fazenda { get; set; }
}

