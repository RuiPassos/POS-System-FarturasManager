using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;
using FarturaManager.Dados; // Isto liga ao Passo 2

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

            // A LINHA MÁGICA QUE FALTAVA! (Cria a Base de Dados e os Produtos)
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

            // Configurações visuais para ficar bonito
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
        private FlowLayoutPanel painelFarturas, painelChurrosCob, painelCones, painelChurrosSimp, painelRecheadas, painelBebidas;

        private void CarregarBotoesDoBanco()
        {
            // 1. O TRUQUE: Trocar o painel antigo pela Grelha Inteligente
            if (grelhaPrincipal == null)
            {
                grelhaPrincipal = new TableLayoutPanel();
                grelhaPrincipal.Dock = DockStyle.Fill;

                // Herdar a cor cinzenta escura do teu painel original
                grelhaPrincipal.BackColor = painelProdutos.BackColor;
                grelhaPrincipal.Padding = new Padding(15);

                grelhaPrincipal.ColumnCount = 2;
                grelhaPrincipal.RowCount = 3;

                grelhaPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                grelhaPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

                grelhaPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                grelhaPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                grelhaPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));

                // Substituir o painel MANTENDO a mesma camada para não ir para debaixo do teclado
                Control pai = painelProdutos.Parent;
                int posicaoNaCamada = pai.Controls.IndexOf(painelProdutos);
                pai.Controls.Remove(painelProdutos);
                pai.Controls.Add(grelhaPrincipal);
                pai.Controls.SetChildIndex(grelhaPrincipal, posicaoNaCamada);

                // Inicializar as gavetas (onde vão ficar os botões)
                painelFarturas = new FlowLayoutPanel { Dock = DockStyle.Fill };
                painelChurrosCob = new FlowLayoutPanel { Dock = DockStyle.Fill };
                painelCones = new FlowLayoutPanel { Dock = DockStyle.Fill };
                painelChurrosSimp = new FlowLayoutPanel { Dock = DockStyle.Fill };
                painelRecheadas = new FlowLayoutPanel { Dock = DockStyle.Fill };
                painelBebidas = new FlowLayoutPanel { Dock = DockStyle.Fill };

                // 2. MAPEAR O CARTAZ: Colocar as caixas nas posições exatas
                grelhaPrincipal.Controls.Add(CriarGrupo("Farturas Simples", painelFarturas), 0, 0);
                grelhaPrincipal.Controls.Add(CriarGrupo("Churros C/ Cobertura", painelChurrosCob), 1, 0);
                grelhaPrincipal.Controls.Add(CriarGrupo("Cones", painelCones), 0, 1);
                grelhaPrincipal.Controls.Add(CriarGrupo("Churros S/ Cobertura", painelChurrosSimp), 1, 1);
                grelhaPrincipal.Controls.Add(CriarGrupo("Recheadas", painelRecheadas), 0, 2);
                grelhaPrincipal.Controls.Add(CriarGrupo("Bebidas", painelBebidas), 1, 2);
            }

            // 3. Ler a Base de Dados e atirar os botões para as caixas
            using (SQLiteConnection conexao = ConexaoBD.ObterConexao())
            {
                try
                {
                    conexao.Open();
                    string sql = "SELECT * FROM Produtos ORDER BY OrdemNaTela";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conexao))
                    using (SQLiteDataReader leitor = cmd.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            Button btnNovo = new Button();
                            btnNovo.Text = leitor["NomeExibicao"].ToString();
                            string categoria = leitor["Categoria"].ToString();

                            // 1. DESCOBRIR A CAIXA DESTINO PRIMEIRO PARA A PODER MEDIR
                            // (Atenção: substitui "FlowLayoutPanel" por "Panel" se as tuas caixas forem apenas "Panels" normais)
                            Control painelDestino = null;
                            if (categoria == "Farturas") painelDestino = painelFarturas;
                            else if (categoria == "Cones") painelDestino = painelCones;
                            else if (categoria == "Recheadas") painelDestino = painelRecheadas;
                            else if (categoria == "ChurrosCob") painelDestino = painelChurrosCob;
                            else if (categoria == "ChurrosSimp") painelDestino = painelChurrosSimp;
                            else if (categoria == "Bebidas") painelDestino = painelBebidas;

                            if (painelDestino != null)
                            {
                                btnNovo.Margin = new Padding(5);

                                // ==========================================
                                // 2. A MATEMÁTICA DA RESPONSIVIDADE (Ajusta a qualquer ecrã!)
                                // ==========================================
                                int botoesPorLinha = 3; // Queremos que fiquem sempre 3 botões por linha
                                int espacoDasMargens = btnNovo.Margin.Horizontal * botoesPorLinha;

                                // Pega na largura do painel, tira as margens e divide por 3!
                                btnNovo.Width = (painelDestino.Width - espacoDasMargens) / botoesPorLinha - 4; // -4 para dar folga de segurança

                                // A altura vai ser 75% da largura (para não ficarem quadrados gigantes)
                                btnNovo.Height = (int)(btnNovo.Width * 0.75);

                                // Letra inteligente: Se o ecrã for grande, letra 13. Se for pequeno, letra 11.
                                float tamanhoLetra = btnNovo.Width > 140 ? 13f : 11f;
                                btnNovo.Font = new Font("Segoe UI", tamanhoLetra, FontStyle.Bold);

                                // ==========================================

                                btnNovo.ForeColor = Color.White;
                                btnNovo.FlatStyle = FlatStyle.Flat;
                                btnNovo.FlatAppearance.BorderSize = 0;
                                btnNovo.TextAlign = ContentAlignment.BottomCenter;

                                try { btnNovo.BackColor = ColorTranslator.FromHtml(leitor["CorHexa"].ToString()); }
                                catch { btnNovo.BackColor = Color.Gray; }

                                // ==========================================
                                // MAGIA DAS IMAGENS AQUI
                                // ==========================================
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
                                catch { /* Ignora */ }
                                // ==========================================

                                btnNovo.Tag = leitor["Preco"];
                                btnNovo.Click += Produto_Click;

                                // Atira o botão responsivo para dentro da caixa
                                painelDestino.Controls.Add(btnNovo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
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
            // Proteção simples por password (para os clientes não clicarem)
            // Se quiser algo mais profissional, fazemos depois. Por agora, um InputBox improvisado.

            // NOTA: Como C# não tem InputBox nativo fácil, vamos fazer uma validação simples:
            // Só abre se ele carregar na tecla Control (Ctrl) enquanto clica no botão? 
            // Ou simplesmente abre e confiamos que o cliente não mexe ali?

            // Vamos fazer o simples: Abre direto por agora.
            FormAdmin telaAdmin = new FormAdmin();
            telaAdmin.ShowDialog(); // ShowDialog impede de mexer na venda enquanto vê o admin
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
    }
}