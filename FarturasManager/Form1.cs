using FarturaManager.Dados; 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace FarturasManager
{
    public partial class Form1 : Form
    {
        // Variável que guarda a quantidade (1, 2, 6, 12...)
        int quantidadeAtual = 1;
        // Variável para saber se o utilizador já tocou no teclado
        bool utilizadorEstaAEscrever = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // OBRIGA O PROGRAMA A MAXIMIZAR ANTES DE FAZER AS CONTAS!
            this.WindowState = FormWindowState.Maximized;

            // Cria a Base de Dados e os Produtos
            ConexaoBD.CriarBancoSeNaoExistir();

            // --- 1. CONFIGURAR A LISTA (GRID) ---
            gridVendas.Columns.Clear();
            gridVendas.Columns.Add("Qtd", "Qtd");
            gridVendas.Columns["Qtd"].Width = 40; // Coluna estreita

            gridVendas.Columns.Add("Produto", "Produto");
            gridVendas.Columns["Produto"].Width = 150; // Coluna larga

            gridVendas.Columns.Add("Preco", "Unit.");
            gridVendas.Columns["Preco"].Width = 60;

            gridVendas.Columns.Add("Total", "Total");
            // O resto do espaço fica para esta coluna

            // Configurações visuais 
            gridVendas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridVendas.AllowUserToAddRows = false; // Impede aquela linha vazia no fim
            gridVendas.ReadOnly = true; // Não deixa escrever na grelha
            gridVendas.RowTemplate.Height = 40; // Linhas mais altas para ler bem
            gridVendas.DefaultCellStyle.Font = new Font("Arial", 12);

            // --- 2. CARREGAR OS BOTÕES ---
            CarregarBotoesDoBanco();
        }

        // Variáveis que vão guardar os nossos painéis organizados
        private TableLayoutPanel grelhaPrincipal = null;

        private void CarregarBotoesDoBanco()
        {
            // 1. O TRUQUE: Trocar o painel antigo pela Grelha Inteligente (Só corre 1 vez)
            if (grelhaPrincipal == null)
            {
                grelhaPrincipal = new TableLayoutPanel();
                grelhaPrincipal.Dock = DockStyle.Fill;
                grelhaPrincipal.BackColor = painelProdutos.BackColor;
                grelhaPrincipal.Padding = new Padding(15);
                grelhaPrincipal.ColumnCount = 2;
                grelhaPrincipal.RowCount = 3;
                grelhaPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                grelhaPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                grelhaPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                grelhaPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                grelhaPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));

                Control pai = painelProdutos.Parent;
                int posicaoNaCamada = pai.Controls.IndexOf(painelProdutos);
                pai.Controls.Remove(painelProdutos);
                pai.Controls.Add(grelhaPrincipal);
                pai.Controls.SetChildIndex(grelhaPrincipal, posicaoNaCamada);
            }

            // LIMPA TUDO ANTES DE RECARREGAR 
            grelhaPrincipal.Controls.Clear();

            // Um Dicionário mágico que vai guardar as categorias que encontrar na Base de Dados
            Dictionary<string, FlowLayoutPanel> dicionarioPaineis = new Dictionary<string, FlowLayoutPanel>();

            using (SQLiteConnection conexao = ConexaoBD.ObterConexao())
            {
                try
                {
                    conexao.Open();

                    // 2. DESCOBRIR AS CATEGORIAS REAIS DO CLIENTE (Lê até 6 nomes diferentes da Tabela)
                    // Agora o programa conta quantos produtos tem cada categoria e mostra as 6 maiores!
                    string sqlCategorias = "SELECT Categoria FROM Produtos WHERE Categoria IS NOT NULL AND Categoria != '' GROUP BY Categoria ORDER BY COUNT(Id) DESC LIMIT 6";
                    using (SQLiteCommand cmdCat = new SQLiteCommand(sqlCategorias, conexao))
                    using (SQLiteDataReader leitorCat = cmdCat.ExecuteReader())
                    {
                        int posicao = 0;
                        while (leitorCat.Read())
                        {
                            string nomeCategoria = leitorCat["Categoria"].ToString();

                            FlowLayoutPanel novoPainel = new FlowLayoutPanel { Dock = DockStyle.Fill };
                            dicionarioPaineis.Add(nomeCategoria, novoPainel);

                            // Cria a caixa com o nome exato que o cliente escreveu (ex: Gelados)
                            GroupBox grupo = CriarGrupo(nomeCategoria, novoPainel);

                            // Matemática para posicionar na grelha 2x3
                            int coluna = posicao % 2;
                            int linha = posicao / 2;

                            grelhaPrincipal.Controls.Add(grupo, coluna, linha);
                            posicao++;
                        }
                    }

                    // 3. LER OS PRODUTOS E ATIRAR PARA A CAIXA CERTA
                    string sqlProdutos = "SELECT * FROM Produtos ORDER BY OrdemNaTela";
                    using (SQLiteCommand cmd = new SQLiteCommand(sqlProdutos, conexao))
                    using (SQLiteDataReader leitor = cmd.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            string categoria = leitor["Categoria"].ToString();

                            // Só processa se a categoria do produto estiver numa das nossas caixas ativas
                            if (dicionarioPaineis.ContainsKey(categoria))
                            {
                                Button btnNovo = new Button();
                                btnNovo.Text = leitor["NomeExibicao"].ToString();

                                FlowLayoutPanel painelDestino = dicionarioPaineis[categoria];
                                btnNovo.Margin = new Padding(5);

                                // --- MATEMÁTICA RESPONSIVA ---
                                int botoesPorLinha = 3;
                                int espacoDasMargens = btnNovo.Margin.Horizontal * botoesPorLinha;

                                // Mede a largura ideal baseada no tamanho do ecrã
                                int larguraEstimadaDaCaixa = (grelhaPrincipal.Width / 2) - 30;

                                btnNovo.Width = (larguraEstimadaDaCaixa - espacoDasMargens) / botoesPorLinha - 4;
                                if (btnNovo.Width <= 0) btnNovo.Width = 100; // Prevenção de segurança

                                btnNovo.Height = (int)(btnNovo.Width * 0.75);

                                float tamanhoLetra = btnNovo.Width > 140 ? 13f : 11f;
                                btnNovo.Font = new Font("Segoe UI", tamanhoLetra, FontStyle.Bold);
                                btnNovo.ForeColor = Color.White;
                                btnNovo.FlatStyle = FlatStyle.Flat;
                                btnNovo.FlatAppearance.BorderSize = 0;
                                btnNovo.TextAlign = ContentAlignment.BottomCenter;

                                try { btnNovo.BackColor = ColorTranslator.FromHtml(leitor["CorHexa"].ToString()); }
                                catch { btnNovo.BackColor = Color.Gray; }

                                try
                                {
                                    string nomeImagem = leitor["NomeImagem"].ToString();
                                    string caminhoImagem = System.IO.Path.Combine(Application.StartupPath, "Imagens", nomeImagem);
                                    if (!string.IsNullOrEmpty(nomeImagem) && System.IO.File.Exists(caminhoImagem))
                                    {
                                        btnNovo.BackgroundImage = Image.FromFile(caminhoImagem);
                                        btnNovo.BackgroundImageLayout = ImageLayout.Zoom;
                                    }
                                }
                                catch { }

                                btnNovo.Tag = leitor["Preco"];
                                btnNovo.Click += Produto_Click;

                                painelDestino.Controls.Add(btnNovo);
                            }
                        }
                    }

                    // 4. ATIVAR A MATEMÁTICA FLUIDA NO FIM EM TODAS AS CAIXAS CRIADAS
                    foreach (var painel in dicionarioPaineis.Values)
                    {
                        AjustarBotoesAoPainel(painel);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar layout: " + ex.Message);
                }
            }
        }

        // Função que cria as caixas com o título
        private GroupBox CriarGrupo(string titulo, FlowLayoutPanel painel)
        {
            GroupBox grupo = new GroupBox();
            grupo.Text = titulo;
            grupo.Dock = DockStyle.Fill;
            grupo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            grupo.ForeColor = Color.White; // Título a branco
            grupo.Margin = new Padding(10);

            painel.BackColor = Color.Transparent;
            grupo.Controls.Add(painel);

            return grupo;
        }

        private void Produto_Click(object sender, EventArgs e)
        {
            Button botao = (Button)sender;

            // 1. Recuperar dados
            string nomeProduto = botao.Text;
            decimal precoUnitario = Convert.ToDecimal(botao.Tag);

            // 2. Ler a quantidade que está no VISOR
            int quantidade = Convert.ToInt32(lblVisor.Text);

            // 3. Calcular Total
            decimal totalItem = precoUnitario * quantidade;

            // 4. Adicionar à Lista (O nome vai direitinho!)
            gridVendas.Rows.Add(quantidade, nomeProduto, precoUnitario, totalItem);

            // 5. ATUALIZAR TOTAIS
            AtualizarTotalGeral();

            // 6. RESET AUTOMÁTICO DO VISOR
            lblVisor.Text = "1";
            utilizadorEstaAEscrever = false;
        }

        private void AtualizarTotalGeral()
        {
            decimal totalVenda = 0;

            // Percorre todas as linhas da grelha e soma a coluna "Total" (índice 3)
            foreach (DataGridViewRow linha in gridVendas.Rows)
            {
                totalVenda += Convert.ToDecimal(linha.Cells[3].Value);
            }

            // Atualiza a Label Gigante (lblTotal)
            // O "C2" formata automaticamente como moeda (€ 10,00)
            lblTotal.Text = totalVenda.ToString("C2");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string numeroDigitado = btn.Text;

            // SE for a primeira vez que ele toca numa tecla para esta venda
            // OU se o visor estiver apenas com o "1" automático...
            if (utilizadorEstaAEscrever == false)
            {
                // ...substitui o que lá está pelo novo número
                lblVisor.Text = numeroDigitado;
                utilizadorEstaAEscrever = true; // Marca que já começou a escrever!
            }
            else
            {
                // Se já estava a escrever (ex: já escreveu "1"), junta o novo número ("2") -> "12"
                lblVisor.Text = lblVisor.Text + numeroDigitado;
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string numeroDigitado = btn.Text;

            // SE for a primeira vez que ele toca numa tecla para esta venda
            // OU se o visor estiver apenas com o "1" automático...
            if (utilizadorEstaAEscrever == false)
            {
                // ...substitui o que lá está pelo novo número
                lblVisor.Text = numeroDigitado;
                utilizadorEstaAEscrever = true; // Marca que já começou a escrever!
            }
            else
            {
                // Se já estava a escrever (ex: já escreveu "1"), junta o novo número ("2") -> "12"
                lblVisor.Text = lblVisor.Text + numeroDigitado;
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string numeroDigitado = btn.Text;

            // SE for a primeira vez que ele toca numa tecla para esta venda
            // OU se o visor estiver apenas com o "1" automático...
            if (utilizadorEstaAEscrever == false)
            {
                // ...substitui o que lá está pelo novo número
                lblVisor.Text = numeroDigitado;
                utilizadorEstaAEscrever = true; // Marca que já começou a escrever!
            }
            else
            {
                // Se já estava a escrever (ex: já escreveu "1"), junta o novo número ("2") -> "12"
                lblVisor.Text = lblVisor.Text + numeroDigitado;
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string numeroDigitado = btn.Text;

            // SE for a primeira vez que ele toca numa tecla para esta venda
            // OU se o visor estiver apenas com o "1" automático...
            if (utilizadorEstaAEscrever == false)
            {
                // ...substitui o que lá está pelo novo número
                lblVisor.Text = numeroDigitado;
                utilizadorEstaAEscrever = true; // Marca que já começou a escrever!
            }
            else
            {
                // Se já estava a escrever (ex: já escreveu "1"), junta o novo número ("2") -> "12"
                lblVisor.Text = lblVisor.Text + numeroDigitado;
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string numeroDigitado = btn.Text;

            // SE for a primeira vez que ele toca numa tecla para esta venda
            // OU se o visor estiver apenas com o "1" automático...
            if (utilizadorEstaAEscrever == false)
            {
                // ...substitui o que lá está pelo novo número
                lblVisor.Text = numeroDigitado;
                utilizadorEstaAEscrever = true; // Marca que já começou a escrever!
            }
            else
            {
                // Se já estava a escrever (ex: já escreveu "1"), junta o novo número ("2") -> "12"
                lblVisor.Text = lblVisor.Text + numeroDigitado;
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string numeroDigitado = btn.Text;

            // SE for a primeira vez que ele toca numa tecla para esta venda
            // OU se o visor estiver apenas com o "1" automático...
            if (utilizadorEstaAEscrever == false)
            {
                // ...substitui o que lá está pelo novo número
                lblVisor.Text = numeroDigitado;
                utilizadorEstaAEscrever = true; // Marca que já começou a escrever!
            }
            else
            {
                // Se já estava a escrever (ex: já escreveu "1"), junta o novo número ("2") -> "12"
                lblVisor.Text = lblVisor.Text + numeroDigitado;
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string numeroDigitado = btn.Text;

            // SE for a primeira vez que ele toca numa tecla para esta venda
            // OU se o visor estiver apenas com o "1" automático...
            if (utilizadorEstaAEscrever == false)
            {
                // ...substitui o que lá está pelo novo número
                lblVisor.Text = numeroDigitado;
                utilizadorEstaAEscrever = true; // Marca que já começou a escrever!
            }
            else
            {
                // Se já estava a escrever (ex: já escreveu "1"), junta o novo número ("2") -> "12"
                lblVisor.Text = lblVisor.Text + numeroDigitado;
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string numeroDigitado = btn.Text;

            // SE for a primeira vez que ele toca numa tecla para esta venda
            // OU se o visor estiver apenas com o "1" automático...
            if (utilizadorEstaAEscrever == false)
            {
                // ...substitui o que lá está pelo novo número
                lblVisor.Text = numeroDigitado;
                utilizadorEstaAEscrever = true; // Marca que já começou a escrever!
            }
            else
            {
                // Se já estava a escrever (ex: já escreveu "1"), junta o novo número ("2") -> "12"
                lblVisor.Text = lblVisor.Text + numeroDigitado;
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string numeroDigitado = btn.Text;

            // SE for a primeira vez que ele toca numa tecla para esta venda
            // OU se o visor estiver apenas com o "1" automático...
            if (utilizadorEstaAEscrever == false)
            {
                // ...substitui o que lá está pelo novo número
                lblVisor.Text = numeroDigitado;
                utilizadorEstaAEscrever = true; // Marca que já começou a escrever!
            }
            else
            {
                // Se já estava a escrever (ex: já escreveu "1"), junta o novo número ("2") -> "12"
                lblVisor.Text = lblVisor.Text + numeroDigitado;
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            // Verifica se há alguma linha selecionada
            if (gridVendas.SelectedRows.Count > 0)
            {
                // Remove a linha selecionada
                gridVendas.Rows.RemoveAt(gridVendas.SelectedRows[0].Index);

                // RECALCULA O TOTAL IMEDIATAMENTE
                AtualizarTotalGeral();
            }
            else
            {
                MessageBox.Show("Selecione um item na lista para remover.");
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            // 1. Se a lista estiver vazia, não faz nada
            if (gridVendas.Rows.Count == 0) return;

            using (SQLiteConnection conexao = ConexaoBD.ObterConexao())
            {
                try
                {
                    conexao.Open();

                    // 3. Vamos percorrer a grelha linha a linha e salvar no banco
                    foreach (DataGridViewRow linha in gridVendas.Rows)
                    {
                        string nome = linha.Cells["Produto"].Value.ToString();
                        int qtd = Convert.ToInt32(linha.Cells["Qtd"].Value);
                        decimal preco = Convert.ToDecimal(linha.Cells["Preco"].Value);
                        decimal totalLinha = Convert.ToDecimal(linha.Cells["Total"].Value);

                        string sql = "INSERT INTO Vendas (NomeProduto, Categoria, PrecoUnitario, Quantidade, ValorTotal, DataHora) " +
                                     "VALUES (@nome, 'Geral', @preco, @qtd, @total, DATETIME('now', 'localtime'))";

                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conexao))
                        {
                            cmd.Parameters.AddWithValue("@nome", nome);
                            cmd.Parameters.AddWithValue("@preco", preco);
                            cmd.Parameters.AddWithValue("@qtd", qtd);
                            cmd.Parameters.AddWithValue("@total", totalLinha);

                            cmd.ExecuteNonQuery(); // GRAVA NO BANCO!
                        }
                    }

                    // 4. Sucesso! Limpar tudo para o próximo cliente
                    gridVendas.Rows.Clear();
                    lblTotal.Text = "€ 0,00";
                    quantidadeAtual = 1; // Reseta o teclado

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao gravar venda: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAdmin admin = new FormAdmin();
            admin.ShowDialog(); // Fica à espera que feches a Administração

            // Quando se volta, recarrega TUDO automaticamente com os nomes novos!
            CarregarBotoesDoBanco();
        }

        private async void btnEmitirFaturaVendus_Click(object sender, EventArgs e)
        {
            // 1. Verifica se há produtos na lista
            if (gridVendas.Rows.Count > 0)
            {
                // 2. Garante que o painel não ficou preso acidentalmente dentro da grelha das farturas!
                painelPagamentos.Parent = this;

                // 3. A Matemática Mágica: Centrar o painel
                painelPagamentos.Left = (this.ClientSize.Width - painelPagamentos.Width) / 2;
                painelPagamentos.Top = (this.ClientSize.Height - painelPagamentos.Height) / 2;

                // 4. Trazer para a frente de TUDO e mostrar
                painelPagamentos.BringToFront();
                painelPagamentos.Visible = true;
            }
            else
            {
                MessageBox.Show("Adicione produtos à venda primeiro!");
            }
        }

        private async void ProcessarVenda(int idPagamentoVendus)
        {
            if (gridVendas.Rows.Count == 0) return;

            // Lemos o NIF (como combinámos antes)
            string nifDigitado = txtNIF.Text.Trim();

            try
            {
                List<object> itensParaOVendus = new List<object>();
                foreach (DataGridViewRow linha in gridVendas.Rows)
                {
                    if (linha.Cells["Produto"].Value != null)
                    {
                        itensParaOVendus.Add(new
                        {
                            title = linha.Cells["Produto"].Value.ToString(),
                            qty = Convert.ToDecimal(linha.Cells["Qtd"].Value),
                            gross_price = Convert.ToDecimal(linha.Cells["Preco"].Value),
                            tax_id = "NOR"
                        });
                    }
                }

                // CHAMAMOS A API E ENVIAMOS O ID DO PAGAMENTO
                string resultado = await VendusAPI.EmitirFaturaSimplificada(itensParaOVendus, nifDigitado, idPagamentoVendus);

                if (resultado.StartsWith("SUCESSO"))
                {
                    string jsonPuro = resultado.Replace("SUCESSO: Fatura criada no Vendus!\n\n", "");
                    dynamic fatura = JsonConvert.DeserializeObject(jsonPuro);
                    string idFatura = fatura.id.ToString().Trim();

                    string linkPDF = "https://rulote-de-testes.vendus.pt/app/documents/print/" + idFatura;

                    try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(linkPDF) { UseShellExecute = true }); }
                    catch { MessageBox.Show("Fatura gerada! Link: " + linkPDF); }

                    // LIMPEZA GERAL DEPOIS DO PAGAMENTO
                    gridVendas.Rows.Clear();
                    lblTotal.Text = "€ 0,00";
                    lblVisor.Text = "1";
                    txtNIF.Text = "";

                    // ESCONDER O PAINEL DE PAGAMENTOS NO FIM 
                    painelPagamentos.Visible = false;

                    MessageBox.Show("Venda concluída!");
                }
                else
                {
                    MessageBox.Show(resultado);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro crítico: " + ex.Message);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            painelPagamentos.Visible = false; // Esconde o painel se ele mudar de ideias
        }

        private void btnNumerario_Click(object sender, EventArgs e)
        {
            ProcessarVenda(318655625); // Manda faturar com o ID do Dinheiro
        }

        private void btnMultibanco_Click(object sender, EventArgs e)
        {
            ProcessarVenda(318655626); // Manda faturar com o ID do MB
        }

        private void btnCredito_Click(object sender, EventArgs e)
        {
            ProcessarVenda(318655627); // Manda faturar com o ID do Cartão Crédito
        }

        private void btnTransferencia_Click(object sender, EventArgs e)
        {
            ProcessarVenda(318655629); // Manda faturar com o ID da Transferência/MBWay
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn0_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string numeroDigitado = btn.Text;

            // SE for a primeira vez que ele toca numa tecla para esta venda
            // OU se o visor estiver apenas com o "1" automático...
            if (utilizadorEstaAEscrever == false)
            {
                // ...substitui o que lá está pelo novo número
                lblVisor.Text = numeroDigitado;
                utilizadorEstaAEscrever = true; // Marca que já começou a escrever!
            }
            else
            {
                // Se já estava a escrever (ex: já escreveu "1"), junta o novo número ("2") -> "12"
                lblVisor.Text = lblVisor.Text + numeroDigitado;
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            string textoAtual = lblVisor.Text;

            // Se tiver mais que 1 dígito (ex: "12"), apaga só o último ("1")
            if (textoAtual.Length > 1)
            {
                lblVisor.Text = textoAtual.Substring(0, textoAtual.Length - 1);
            }
            else
            {
                // Se só tiver 1 dígito (ex: "5"), ao apagar volta ao padrão "1"
                // Não queremos que fique vazio nem zero.
                lblVisor.Text = "1";
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            lblVisor.Text = "1";
            utilizadorEstaAEscrever = false; // Reset à memória
        }

        private void AjustarBotoesAoPainel(Control painel)
        {
            int totalBotoes = painel.Controls.Count;

            // Se a caixa estiver vazia, não fazemos contas
            if (totalBotoes == 0) return;

            // Calcula o espaço retirando as margens (10px por botão)
            int larguraDisponivel = painel.Width - (totalBotoes * 10);

            // A MAGIA: Divide a largura total pelo número exato de botões!
            int larguraBotao = larguraDisponivel / totalBotoes;

            foreach (Control controlo in painel.Controls)
            {
                if (controlo is Button btn)
                {
                    btn.Width = larguraBotao - 2; // -2 para uma folguinha de segurança

                    // Ocupa a altura INTEIRA da caixa cinzenta (nunca cria uma segunda linha)
                    btn.Height = painel.Height - 15;

                    // Inteligência extra: Se o botão ficar muito esmagado, encolhe a letra para caber!
                    float tamanhoLetra = btn.Width > 110 ? 13f : 10f;
                    btn.Font = new Font("Segoe UI", tamanhoLetra, FontStyle.Bold);
                }
            }
        }
    }
}
