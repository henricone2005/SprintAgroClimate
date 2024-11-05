using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.IO;

namespace beautyTechAPI.Controllers
{
    public class ClienteProduto
    {
        [LoadColumn(0)] public string PELE_CLIENTE { get; set; }
        [LoadColumn(1)] public string ESTADO_CIVIL_CLIENTE { get; set; }
        [LoadColumn(2)] public string CABELO_CLIENTE { get; set; }
        [LoadColumn(3)] public string NM_PRODUTO { get; set; } 
    }

    public class ProdutoPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedProduto { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PrevisaoProdutoController : ControllerBase
    {
        private readonly string caminhoModelo = Path.Combine(Environment.CurrentDirectory, "wwwroot", "MLModels", "ModeloProduto.zip");
        private readonly string caminhoTreinamento = Path.Combine(Environment.CurrentDirectory, "Data", "produto-train.csv");
        private readonly MLContext mlContext;

        public PrevisaoProdutoController()
        {
            mlContext = new MLContext();

            if (!System.IO.File.Exists(caminhoModelo))
            {
                TreinarModelo();
            }
        }

        private void TreinarModelo()
        {
            var pastaModelo = Path.GetDirectoryName(caminhoModelo);
            if (!Directory.Exists(pastaModelo))
            {
                Directory.CreateDirectory(pastaModelo);
            }

            IDataView dadosTreinamento = mlContext.Data.LoadFromTextFile<ClienteProduto>(
                path: caminhoTreinamento, hasHeader: true, separatorChar: ',');

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(ClienteProduto.NM_PRODUTO))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "PELE_CLIENTE_encoded", inputColumnName: nameof(ClienteProduto.PELE_CLIENTE)))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "ESTADO_CIVIL_CLIENTE_encoded", inputColumnName: nameof(ClienteProduto.ESTADO_CIVIL_CLIENTE)))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "CABELO_CLIENTE_encoded", inputColumnName: nameof(ClienteProduto.CABELO_CLIENTE)))
                .Append(mlContext.Transforms.Concatenate("Features", "PELE_CLIENTE_encoded", "ESTADO_CIVIL_CLIENTE_encoded", "CABELO_CLIENTE_encoded"))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var modelo = pipeline.Fit(dadosTreinamento);
            mlContext.Model.Save(modelo, dadosTreinamento.Schema, caminhoModelo);
        }

        [HttpPost("prever")]
        public ActionResult<ProdutoPrediction> PreverProduto([FromBody] ClienteProduto dadosCliente)
        {
            if (!System.IO.File.Exists(caminhoModelo))
            {
                return BadRequest("O modelo ainda não foi treinado.");
            }

            try
            {
                ITransformer modelo;
                using (var stream = new FileStream(caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    modelo = mlContext.Model.Load(stream, out var modeloSchema);
                }

                var enginePrevisao = mlContext.Model.CreatePredictionEngine<ClienteProduto, ProdutoPrediction>(modelo);
                var previsao = enginePrevisao.Predict(dadosCliente);

                return Ok(previsao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao prever produto: {ex.Message}");
            }
        }
    }
}
