using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace AgroClimate.Controllers
{
    public class DadosFazenda
    {
        [LoadColumn(0)]
        public float Latitude { get; set; }

        [LoadColumn(1)]
        public float Longitude { get; set; }

        [LoadColumn(2)]
        public float Profundidade { get; set; }

        [LoadColumn(3)]
        public float Salinidade { get; set; }

        [LoadColumn(4)]
        public string EpocaDoAno { get; set; }

        [LoadColumn(5)]
        public float Temperatura { get; set; }

        [LoadColumn(6)]
        public string TipoDeSolo { get; set; }

        [LoadColumn(7)]
        public string Clima { get; set; }

        [LoadColumn(8)]
        public string AlimentoSugerido { get; set; } // Esta coluna deve estar presente no CSV
    }

    public class AlimentoSugerido
    {
        [ColumnName("PredictedLabel")]
        public string Alimento { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SugerirCulturaController : ControllerBase
    {
        private readonly string caminhoModelo = Path.Combine(Environment.CurrentDirectory, "wwwroot", "MLModels", "ModeloCultura.zip");
        private readonly string caminhoTreinamento = Path.Combine(Environment.CurrentDirectory, "Data", "fazenda-treino.csv");
        private readonly MLContext mlContext;

        public SugerirCulturaController()
        {
            mlContext = new MLContext();

            if (!System.IO.File.Exists(caminhoModelo))
            {
                Console.WriteLine("Modelo não encontrado. Iniciando treinamento...");
                TreinarModelo();
            }
        }

        private void TreinarModelo()
        {
            IDataView dadosTreinamento = mlContext.Data.LoadFromTextFile<DadosFazenda>(
                path: caminhoTreinamento, hasHeader: true, separatorChar: ',');

            // Aqui convertemos a coluna AlimentoSugerido para Key
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("AlimentoSugerido")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("TipoDeSoloEncoded", nameof(DadosFazenda.TipoDeSolo))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("ClimaEncoded", nameof(DadosFazenda.Clima)))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("EpocaDoAnoEncoded", nameof(DadosFazenda.EpocaDoAno)))
                .Append(mlContext.Transforms.Concatenate("Features", "TipoDeSoloEncoded", "ClimaEncoded", "EpocaDoAnoEncoded", nameof(DadosFazenda.Latitude), nameof(DadosFazenda.Longitude), nameof(DadosFazenda.Profundidade), nameof(DadosFazenda.Salinidade), nameof(DadosFazenda.Temperatura)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "AlimentoSugerido")));

            var modelo = pipeline.Fit(dadosTreinamento);
            mlContext.Model.Save(modelo, dadosTreinamento.Schema, caminhoModelo);
            Console.WriteLine($"Modelo treinado e salvo em: {caminhoModelo}");
        }

        [HttpPost("sugerir-alimento")]
        public ActionResult<AlimentoSugerido> SugerirAlimento([FromBody] DadosFazenda dados)
        {
            if (!System.IO.File.Exists(caminhoModelo))
            {
                return BadRequest("O modelo ainda não foi treinado.");
            }

            ITransformer modelo;
            using (var stream = new FileStream(caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                modelo = mlContext.Model.Load(stream, out var modeloSchema);
            }

            var enginePrevisao = mlContext.Model.CreatePredictionEngine<DadosFazenda, AlimentoSugerido>(modelo);
            var previsao = enginePrevisao.Predict(dados);

            return Ok(previsao);
        }
    }
}
