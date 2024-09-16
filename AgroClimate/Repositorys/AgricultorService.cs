using AgroClimate.Models;

namespace AgroClimate.Repositorys;

public class AgricultorService
{
    private readonly IFazendaRepository _fazendaRepository;

    public AgricultorService(IFazendaRepository fazendaRepository)
    {
        _fazendaRepository = fazendaRepository;
    }

    public IEnumerable<AgricultorFazenda> GetAgricultorFazendas()
    {
        // Obtém a lista de Fazendas do repositório
        var listaDeFazendas = _fazendaRepository.GetFazendas();

        // Converte a lista de Fazendas para AgricultorFazenda
        var resultado = listaDeFazendas.Select(fazenda => new AgricultorFazenda
        {
            Id = fazenda.Id,
            Nome = fazenda.Nome,
            Area = fazenda.Area
        });

        return resultado;
    }
}
