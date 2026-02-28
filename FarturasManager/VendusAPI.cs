using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FarturasManager
{
    public static class VendusAPI
    {
        // COLAR CHAVE DA API AQUI
        private static readonly string ApiKey = "COLOQUE_A_SUA_CHAVE_AQUI";

        // variável nifCliente por omissão vai vazia
        public static async Task<string> EmitirFaturaSimplificada(List<object> itensDaVenda, string nifCliente = "", int idPagamento = 1)
        {
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    string chaveCodificada = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(ApiKey + ":"));
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", chaveCodificada);

                    double valorTotalVenda = 0;
                    foreach (dynamic item in itensDaVenda)
                    {
                        valorTotalVenda += (double)item.qty * (double)item.gross_price;
                    }

                    dynamic dadosFatura;

                    // Se NÃO houver NIF válido, fatura normal 
                    if (string.IsNullOrEmpty(nifCliente) || nifCliente.Length != 9)
                    {
                        dadosFatura = new
                        {
                            type = "FS",
                            items = itensDaVenda,
                            payments = new[] { new { id = idPagamento, amount = valorTotalVenda } }
                        };
                    }
                    else
                    {
                        // Se HOUVER NIF, envia o NIF e usa o idPagamento que escolhemos no ecrã!
                        dadosFatura = new
                        {
                            type = "FS",
                            items = itensDaVenda,
                            payments = new[] { new { id = idPagamento, amount = valorTotalVenda } },
                            client = new { fiscal_id = nifCliente, name = "Consumidor Final" }
                        };
                    }

                    string json = JsonConvert.SerializeObject(dadosFatura);
                    StringContent conteudo = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage resposta = await cliente.PostAsync("https://www.vendus.pt/ws/v1.1/documents", conteudo);
                    string respostaTexto = await resposta.Content.ReadAsStringAsync();

                    if (resposta.IsSuccessStatusCode)
                    {
                        return "SUCESSO: Fatura criada no Vendus!\n\n" + respostaTexto;
                    }
                    else
                    {
                        return "ERRO DO VENDUS:\n" + respostaTexto;
                    }
                }
            }
            catch (Exception ex)
            {
                return "ERRO DE LIGAÇÃO:\n" + ex.Message;
            }
        }

        public static async Task<string> DescarregarPDF(string idDocumento)
        {
            try
            {
                // 1. Dar 1.5 segundos de avanço para o Vendus desenhar o PDF nos servidores deles
                await Task.Delay(1500);

                using (HttpClient cliente = new HttpClient())
                {
                    string chaveCodificada = Convert.ToBase64String(Encoding.ASCII.GetBytes(ApiKey + ":"));
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", chaveCodificada);

                    // 2. Avisar o servidor que o nosso programa aceita ficheiros PDF
                    cliente.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/pdf"));

                    // 3. O Link oficial para descarregar o PDF do documento
                    string urlPdf = "https://www.vendus.pt/ws/v1.1/documents/" + idDocumento + ".pdf";

                    // GetAsync para conseguir capturar o código do erro (ex: 401, 403, 404)
                    HttpResponseMessage resposta = await cliente.GetAsync(urlPdf);

                    if (!resposta.IsSuccessStatusCode)
                    {
                        // Se falhar, lemos a resposta do Vendus para sabermos exatamente o porquê!
                        string erroServidor = await resposta.Content.ReadAsStringAsync();
                        return $"ERRO DA API ({(int)resposta.StatusCode}): {erroServidor}";
                    }

                    // Se for sucesso, guardamos o PDF
                    byte[] bytesPdf = await resposta.Content.ReadAsByteArrayAsync();

                    string caminhoPasta = System.IO.Path.GetTempPath();
                    string caminhoArquivo = System.IO.Path.Combine(caminhoPasta, "Fatura_" + idDocumento + ".pdf");

                    System.IO.File.WriteAllBytes(caminhoArquivo, bytesPdf);

                    return caminhoArquivo;
                }
            }
            catch (Exception ex)
            {
                return "ERRO NO PROGRAMA: " + ex.Message;
            }
        }
    }
}
