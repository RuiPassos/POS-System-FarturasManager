using FarturaManager.Dados;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FarturasManager
{
    public partial class FormGerirProdutos : Form
    {
        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Variáveis para sabermos o que estamos a editar
        private int idProdutoSelecionado = 0;
        private string caminhoNovaFoto = "";
        private string nomeFotoAtual = "";

        public FormGerirProdutos()
        {
            InitializeComponent();
            this.Load += FormGerirProdutos_Load;

            // Ligar os botões às funções
            btnGuardar.Click += btnGuardar_Click;
            btnApagar.Click += btnApagar_Click;
            btnLimpar.Click += btnLimpar_Click;
            btnEscolherFoto.Click += btnEscolherFoto_Click;
            gridProdutos.CellClick += gridProdutos_CellClick;
        }

        private void FormGerirProdutos_Load(object sender, EventArgs e)
        {
            CarregarCategoriasNaCombo(); 
            CarregarGrelha();
            gridProdutos.AllowUserToAddRows = false;
        }

        private void CarregarCategoriasNaCombo()
        {
            cmbCategoria.Items.Clear(); 

            using (SQLiteConnection conexao = ConexaoBD.ObterConexao())
            {
                conexao.Open();
                string sql = "SELECT Nome FROM Categorias ORDER BY Id";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conexao))
                using (SQLiteDataReader leitor = cmd.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        cmbCategoria.Items.Add(leitor["Nome"].ToString());
                    }
                }
            }
        }

        // ==========================================
        // 1. CARREGAR A LISTA DE PRODUTOS
        // ==========================================
        private void CarregarGrelha()
        {
            using (SQLiteConnection conexao = ConexaoBD.ObterConexao())
            {
                conexao.Open();
                string sql = "SELECT Id, NomeExibicao as Nome, Categoria, Preco, NomeImagem FROM Produtos";
                SQLiteDataAdapter adaptador = new SQLiteDataAdapter(sql, conexao);
                DataTable tabela = new DataTable();
                adaptador.Fill(tabela);

                gridProdutos.DataSource = tabela;

                // Esconder colunas feias ou técnicas
                gridProdutos.Columns["Id"].Visible = false;
                gridProdutos.Columns["NomeImagem"].Visible = false;

                // Formatar o preço para euros
                gridProdutos.Columns["Preco"].DefaultCellStyle.Format = "C2";

                // Ocupar o espaço todo da grelha
                gridProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // ==========================================
        // 2. CLICAR NUM PRODUTO DA LISTA PARA EDITAR
        // ==========================================
        private void gridProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se clicou numa linha válida 
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = gridProdutos.Rows[e.RowIndex];

                // =========================================================
                // O NOSSO ESCUDO PROTETOR 
                // =========================================================
                if (linha.IsNewRow) return; // Se for linha nova, ignora
                if (linha.Cells["Id"].Value == DBNull.Value) return; // Se estiver vazia, ignora
                // =========================================================

                idProdutoSelecionado = Convert.ToInt32(linha.Cells["Id"].Value);
                txtNome.Text = linha.Cells["Nome"].Value.ToString();
                cmbCategoria.Text = linha.Cells["Categoria"].Value.ToString();
                numPreco.Value = Convert.ToDecimal(linha.Cells["Preco"].Value);

                nomeFotoAtual = linha.Cells["NomeImagem"].Value.ToString();
                caminhoNovaFoto = ""; // Fica vazio até o utilizador escolher uma nova

                // Carregar a imagem na PictureBox
                string pastaImagens = Path.Combine(Application.StartupPath, "Imagens");
                string caminhoCompleto = Path.Combine(pastaImagens, nomeFotoAtual);

                if (File.Exists(caminhoCompleto))
                {
                    picFoto.Image = Image.FromFile(caminhoCompleto);
                }
                else
                {
                    picFoto.Image = null; // Se não tiver foto, limpa
                }
            }
        }

        // ==========================================
        // 3. ESCOLHER FOTO NOVA
        // ==========================================
        private void btnEscolherFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Escolher Imagem do Produto";
                ofd.Filter = "Imagens (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    caminhoNovaFoto = ofd.FileName;
                    picFoto.Image = Image.FromFile(caminhoNovaFoto);
                }
            }
        }

        // ==========================================
        // 4. GUARDAR OU ATUALIZAR 
        // ==========================================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(cmbCategoria.Text))
            {
                MessageBox.Show("O Nome e a Categoria são obrigatórios!");
                return;
            }

            // 1. DECIDIR A COR AUTOMATICAMENTE COM BASE NA CATEGORIA
            string corDecidida = "#808080"; // Cinzento padrão se algo falhar

            switch (cmbCategoria.Text)
            {
                case "Farturas":
                    corDecidida = "#FFD700"; // Amarelo Dourado
                    break;
                case "Cones":
                    corDecidida = "#FFA500"; // Laranja
                    break;
                case "Recheadas":
                    corDecidida = "#FF69B4"; // Rosa
                    break;
                case "ChurrosCob":
                case "ChurrosSimp":
                    corDecidida = "#8B4513"; // Castanho Churro
                    break;
                case "Bebidas":
                    corDecidida = "#3498db"; // Azul Bebida
                    break;
                default:
                    corDecidida = "#3498db"; // Azul padrão para outros
                    break;
            }


            // 2. TRATAR DA IMAGEM 
            string nomeImagemParaBD = nomeFotoAtual;
            if (!string.IsNullOrEmpty(caminhoNovaFoto))
            {
                string pastaImagens = Path.Combine(Application.StartupPath, "Imagens");
                if (!Directory.Exists(pastaImagens)) Directory.CreateDirectory(pastaImagens);

                string extensao = Path.GetExtension(caminhoNovaFoto);
                nomeImagemParaBD = txtNome.Text.Replace(" ", "_") + "_" + DateTime.Now.Ticks + extensao;

                string destino = Path.Combine(pastaImagens, nomeImagemParaBD);
                File.Copy(caminhoNovaFoto, destino, true);
            }

            // 3. GRAVAR NA BASE DE DADOS 
            using (SQLiteConnection conexao = ConexaoBD.ObterConexao())
            {
                conexao.Open();
                string sql;

                if (idProdutoSelecionado == 0) 
                {           
                    sql = "INSERT INTO Produtos (NomeExibicao, Categoria, Preco, NomeImagem, CorHexa) VALUES (@nome, @categoria, @preco, @imagem, @cor)";
                }
                else 
                {
                    sql = "UPDATE Produtos SET NomeExibicao = @nome, Categoria = @categoria, Preco = @preco, NomeImagem = @imagem, CorHexa = @cor WHERE Id = @id";
                }

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@categoria", cmbCategoria.Text);
                    cmd.Parameters.AddWithValue("@preco", numPreco.Value);
                    cmd.Parameters.AddWithValue("@imagem", nomeImagemParaBD);
                    cmd.Parameters.AddWithValue("@cor", corDecidida); // <-- Passamos a cor escolhida
                    if (idProdutoSelecionado != 0) cmd.Parameters.AddWithValue("@id", idProdutoSelecionado);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Produto guardado com sucesso!");
            CarregarGrelha();
            btnLimpar_Click(null, null);
        }

        // ==========================================
        // 5. APAGAR PRODUTO
        // ==========================================
        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (idProdutoSelecionado == 0)
            {
                MessageBox.Show("Por favor, selecione um produto da lista primeiro.");
                return;
            }

            DialogResult resposta = MessageBox.Show($"Quer mesmo apagar o produto '{txtNome.Text}'?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resposta == DialogResult.Yes)
            {
                using (SQLiteConnection conexao = ConexaoBD.ObterConexao())
                {
                    conexao.Open();
                    string sql = "DELETE FROM Produtos WHERE Id = @id";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", idProdutoSelecionado);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Produto apagado!");
                CarregarGrelha();
                btnLimpar_Click(null, null);
            }
        }

        // ==========================================
        // 6. LIMPAR CAMPOS
        // ==========================================
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            idProdutoSelecionado = 0;
            txtNome.Clear();
            cmbCategoria.SelectedIndex = -1;
            numPreco.Value = 0;
            picFoto.Image = null;
            caminhoNovaFoto = "";
            nomeFotoAtual = "";
        }
    }
}
