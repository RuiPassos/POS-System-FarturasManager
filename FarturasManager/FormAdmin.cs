using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite; // <-- AQUI MUDOU PARA SQLITE
using FarturaManager.Dados;

namespace FarturasManager
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            CarregarDadosDoTurno();
        }

        private void CarregarDadosDoTurno()
        {
            // ====================================================
            // A LÓGICA DO HORÁRIO DE CORTE (O SEGREDO!)
            // ====================================================
            DateTime agora = DateTime.Now;
            DateTime inicioTurno;
            DateTime fimTurno;

            if (agora.Hour < 6)
            {
                // Se são 03:00 da manhã, o turno começou ONTEM às 06:00
                inicioTurno = agora.Date.AddDays(-1).AddHours(6);
            }
            else
            {
                // Se são 19:00, o turno começou HOJE às 06:00
                inicioTurno = agora.Date.AddHours(6);
            }

            // O fim do turno é 24h depois do início
            fimTurno = inicioTurno.AddDays(1);

            // Atualiza os calendários visuais para as datas calculadas
            dtpInicio.Value = inicioTurno;
            dtpFim.Value = fimTurno;

            // Chama a nova função que faz o trabalho pesado
            CarregarDashboard(inicioTurno, fimTurno);
        }

        // ====================================================
        // FUNÇÃO: Faz as contas e os gráficos baseada nas datas
        // ====================================================
        private void CarregarDashboard(DateTime dataInicio, DateTime dataFim)
        {
            this.Text = $"Resumo de Vendas: {dataInicio:dd/MM/yyyy HH:mm} até {dataFim:dd/MM/yyyy HH:mm}";

            // <-- AQUI MUDOU PARA SQLITECONNECTION
            using (SQLiteConnection conexao = ConexaoBD.ObterConexao())
            {
                try
                {
                    conexao.Open();

                    // 1. CALCULAR A FATURAÇÃO TOTAL
                    string sqlTotal = "SELECT IFNULL(SUM(ValorTotal), 0) FROM Vendas " +
                                      "WHERE DataHora >= @inicio AND DataHora <= @fim";

                    using (SQLiteCommand cmd = new SQLiteCommand(sqlTotal, conexao))
                    {
                        cmd.Parameters.AddWithValue("@inicio", dataInicio);
                        cmd.Parameters.AddWithValue("@fim", dataFim);

                        decimal total = Convert.ToDecimal(cmd.ExecuteScalar());
                        lblFaturacao.Text = total.ToString("C2");
                    }

                    // 2. CARREGAR O TOP DE PRODUTOS (Resumo Agrupado para o Gráfico)
                    string sqlTop = "SELECT NomeProduto, SUM(Quantidade) as Qtd, SUM(ValorTotal) as Total " +
                                    "FROM Vendas " +
                                    "WHERE DataHora >= @inicio AND DataHora <= @fim " +
                                    "GROUP BY NomeProduto " +
                                    "ORDER BY Qtd DESC";

                    SQLiteDataAdapter adaptadorTop = new SQLiteDataAdapter(sqlTop, conexao);
                    adaptadorTop.SelectCommand.Parameters.AddWithValue("@inicio", dataInicio);
                    adaptadorTop.SelectCommand.Parameters.AddWithValue("@fim", dataFim);

                    DataTable tabelaTop = new DataTable();
                    adaptadorTop.Fill(tabelaTop);

                    // Preencher a Grelha Resumo
                    gridTopVendas.DataSource = tabelaTop;
                    if (gridTopVendas.Columns.Contains("Total"))
                        gridTopVendas.Columns["Total"].DefaultCellStyle.Format = "C2";

                    if (gridTopVendas.Columns.Contains("NomeProduto"))
                        gridTopVendas.Columns["NomeProduto"].Width = 150;

                    // ====================================================
                    // 3. ATUALIZAR O GRÁFICO (Faturação por Hora)
                    // ====================================================
                    if (graficoTop != null)
                    {
                        string sqlGrafico = @"
                                        SELECT strftime('%H', DataHora) || ':00' AS Hora, 
                                               SUM(ValorTotal) AS TotalVendido
                                        FROM Vendas
                                        WHERE DataHora >= @inicio AND DataHora <= @fim
                                        GROUP BY strftime('%H', DataHora)
                                        ORDER BY strftime('%H', DataHora)";

                        SQLiteDataAdapter adaptadorHoras = new SQLiteDataAdapter(sqlGrafico, conexao);
                        adaptadorHoras.SelectCommand.Parameters.AddWithValue("@inicio", dataInicio);
                        adaptadorHoras.SelectCommand.Parameters.AddWithValue("@fim", dataFim);

                        DataTable tabelaHoras = new DataTable();
                        adaptadorHoras.Fill(tabelaHoras);

                        // Ligar os novos dados das horas ao gráfico
                        graficoTop.DataSource = tabelaHoras;
                        graficoTop.Series.Clear();
                        graficoTop.Series.Add("Faturação Horária");

                        // Voltar para gráfico de Colunas (faz mais sentido para linhas de tempo)
                        graficoTop.Series["Faturação Horária"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        // Usar os nomes exatos que vêm da Base de Dados!
                        graficoTop.Series["Faturação Horária"].XValueMember = "Hora";
                        graficoTop.Series["Faturação Horária"].YValueMembers = "TotalVendido";

                        // Formatações de Beleza e Limpeza
                        graficoTop.Series["Faturação Horária"].IsValueShownAsLabel = true;
                        graficoTop.Series["Faturação Horária"].LabelFormat = "C2";
                        graficoTop.Series["Faturação Horária"].Color = Color.FromArgb(0, 120, 215); // Azul elegante

                        graficoTop.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // Tira as linhas pretas verticais
                        graficoTop.ChartAreas[0].AxisY.MajorGrid.Enabled = false; // Tira as linhas pretas horizontais
                        graficoTop.ChartAreas[0].AxisX.Interval = 1; // Garante que não salta horas (mostra todas)

                        // Esconder elementos desnecessários para ganhar espaço
                        graficoTop.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
                        graficoTop.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
                        graficoTop.Legends.Clear();

                        graficoTop.DataBind();
                    }

                    // ====================================================
                    // 4. CARREGAR O HISTÓRICO DETALHADO
                    // ====================================================
                    string sqlHistorico = "SELECT Id, NomeProduto, Quantidade as Qtd, ValorTotal as Total, DataHora " +
                                          "FROM Vendas " +
                                          "WHERE DataHora >= @inicio AND DataHora <= @fim " +
                                          "ORDER BY DataHora DESC";

                    // SQLITEDATAADAPTER
                    SQLiteDataAdapter adaptadorHistorico = new SQLiteDataAdapter(sqlHistorico, conexao);
                    adaptadorHistorico.SelectCommand.Parameters.AddWithValue("@inicio", dataInicio);
                    adaptadorHistorico.SelectCommand.Parameters.AddWithValue("@fim", dataFim);

                    DataTable tabelaHistorico = new DataTable();
                    adaptadorHistorico.Fill(tabelaHistorico);

                    // Preencher a nova grelha
                    if (this.Controls.Find("gridHistorico", true).Length > 0)
                    {
                        DataGridView gridHist = (DataGridView)this.Controls.Find("gridHistorico", true)[0];
                        gridHist.DataSource = tabelaHistorico;

                        // ESCONDER A COLUNA DO ID 
                        if (gridHist.Columns.Contains("Id"))
                            gridHist.Columns["Id"].Visible = false;

                        // Formatações de beleza
                        if (gridHist.Columns.Contains("Total"))
                            gridHist.Columns["Total"].DefaultCellStyle.Format = "C2";

                        if (gridHist.Columns.Contains("DataHora"))
                        {
                            gridHist.Columns["DataHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                            gridHist.Columns["DataHora"].Width = 130;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar estatísticas: " + ex.Message);
                }
            }
        }

        // ====================================================
        // BOTÃO DE FILTRAR
        // ====================================================
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CarregarDashboard(dtpInicio.Value, dtpFim.Value);
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha esta janela e volta ao menu principal
        }

        private void btnVoltar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAdmin_Load_1(object sender, EventArgs e)
        {
            CarregarDadosDoTurno();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnApagarVenda_Click(object sender, EventArgs e)
        {
            DataGridView gridHist = (DataGridView)this.Controls.Find("gridHistorico", true)[0];

            int quantidadeSelecionada = gridHist.SelectedRows.Count;

            // 1. Verificar se o utilizador clicou em alguma linha
            if (quantidadeSelecionada > 0)
            {
                // Magia: A mensagem adapta-se se for 1 linha ou várias!
                string textoAviso = quantidadeSelecionada == 1
                    ? "Tem a certeza que quer eliminar permanentemente esta venda?"
                    : $"Tem a certeza que quer eliminar permanentemente as {quantidadeSelecionada} vendas selecionadas?";

                DialogResult resposta = MessageBox.Show(textoAviso, "Aviso Importante", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resposta == DialogResult.Yes)
                {
                    using (SQLiteConnection conexao = ConexaoBD.ObterConexao())
                    {
                        try
                        {
                            conexao.Open();

                            // Preparamos a "arma" (o comando SQL) apenas uma vez
                            string sql = "DELETE FROM Vendas WHERE Id = @id";
                            using (SQLiteCommand cmd = new SQLiteCommand(sql, conexao))
                            {
                                // Criamos a "bala" vazia
                                cmd.Parameters.Add("@id", System.Data.DbType.Int32);

                                // Fazemos um Loop (ciclo) por TODAS as linhas que selecionaste
                                foreach (DataGridViewRow linha in gridHist.SelectedRows)
                                {
                                    // Pega no ID da linha atual do ciclo
                                    int idVenda = Convert.ToInt32(linha.Cells["Id"].Value);

                                    // Carrega a bala com o ID e dispara!
                                    cmd.Parameters["@id"].Value = idVenda;
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            string textoSucesso = quantidadeSelecionada == 1
                                ? "Venda anulada com sucesso!"
                                : $"{quantidadeSelecionada} vendas anuladas com sucesso!";

                            MessageBox.Show(textoSucesso);

                            // RECARREGA O DASHBOARD (atualiza os gráficos e o dinheiro total)
                            CarregarDashboard(dtpInicio.Value, dtpFim.Value);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao apagar vendas: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione pelo menos uma linha no Histórico para poder apagar.");
            }
        }

        private void btnGerirProdutos_Click(object sender, EventArgs e)
        {
            // Abre a janela nova de gestão de produtos e obriga o utilizador a fechá-la antes de voltar ao admin
            FormGerirProdutos formGerir = new FormGerirProdutos();
            formGerir.ShowDialog();
        }
    }
}
