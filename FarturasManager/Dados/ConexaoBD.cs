using System.IO;
using System.Windows.Forms;
using System.Data.SQLite; // Mudamos de SqlClient para SQLite!

namespace FarturaManager.Dados
{
    public static class ConexaoBD
    {
        // O ficheiro vai ser criado na mesma pasta do teu programa
        private static string caminhoDB = Path.Combine(Application.StartupPath, "FarturasDB.sqlite");
        private static string stringConexao = $"Data Source={caminhoDB};Version=3;";

        public static SQLiteConnection ObterConexao()
        {
            return new SQLiteConnection(stringConexao);
        }

        // Função mágica que corre ao abrir o programa
        public static void CriarBancoSeNaoExistir()
        {
            if (!File.Exists(caminhoDB))
            {
                SQLiteConnection.CreateFile(caminhoDB); // Cria o ficheiro vazio

                using (SQLiteConnection conexao = ObterConexao())
                {
                    conexao.Open();
                    string sql = @"
                        CREATE TABLE Vendas (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            NomeProduto TEXT NOT NULL,
                            Categoria TEXT,
                            PrecoUnitario REAL,
                            Quantidade INTEGER DEFAULT 1,
                            ValorTotal REAL,
                            DataHora DATETIME DEFAULT (DATETIME('now', 'localtime'))
                        );

                        CREATE TABLE Produtos (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            NomeExibicao TEXT,
                            NomeInterno TEXT,
                            Preco REAL,
                            Categoria TEXT,
                            CorHexa TEXT,
                            OrdemNaTela INTEGER,
                            NomeImagem TEXT
                        );

                        -- Inserir os teus produtos exatos do cartaz!
                        INSERT INTO Produtos (NomeExibicao, NomeInterno, Preco, Categoria, CorHexa, OrdemNaTela, NomeImagem) VALUES 
                        ('Fartura Simples Unidade', 'Fartura Simples Unidade', 1.50, 'Farturas', '#FFD700', 1, 'fartura1.png'),
                        ('Fartura Simples Meia Dúzia', 'Fartura Simples Meia Dúzia', 7.00, 'Farturas', '#FFD700', 2, 'fartura6.png'),
                        ('Fartura Simples Dúzia', 'Fartura Simples Dúzia', 14.00, 'Farturas', '#FFD700', 3, 'fartura12.png'),
                        ('Cone Normal', 'Cone Normal', 9.00, 'Cones', '#FFA500', 4, 'cone.png'),
                        ('Cone Personalizado', 'Cone Personalizado', 10.00, 'Cones', '#FFA500', 5, 'cone_pers.png'),
                        ('Recheada S/ Cobertura', 'Recheada S/ Cobertura', 3.00, 'Recheadas', '#FF8C00', 6, 'recheada.png'),
                        ('Recheada C/ Cobertura', 'Recheada C/ Cobertura', 4.00, 'Recheadas', '#FF8C00', 7, 'recheada_cob.png'),
                        ('Waffle', 'Waffle', 5.00, 'Recheadas', '#FF8C00', 8, 'waffle.png'),
                        ('Churros Simples Meia Dúzia', 'Churros Simples Meia Dúzia', 5.00, 'ChurrosSimp', '#D2691E', 9, 'churros6.png'),
                        ('Churros Simples Dúzia', 'Churros Simples Dúzia', 10.00, 'ChurrosSimp', '#D2691E', 10, 'churros12.png'),
                        ('Churros Cob. Caixa Mini', 'Churros Cob. Caixa Mini', 9.00, 'ChurrosCob', '#8B4513', 11, 'churros_mini.png'),
                        ('Churros Cob. Box 10', 'Churros Cob. Box 10', 10.00, 'ChurrosCob', '#8B4513', 12, 'churros_box.png'),
                        ('Churros Cob. Max 14', 'Churros Cob. Max 14', 15.00, 'ChurrosCob', '#8B4513', 13, 'churros_max.png'),
                        ('Água', 'Água', 1.00, 'Bebidas', '#00BFFF', 14, 'agua.png'),
                        ('Cerveja', 'Cerveja', 2.00, 'Bebidas', '#1E90FF', 15, 'cerveja.png'),
                        ('Lata', 'Lata', 2.00, 'Bebidas', '#1E90FF', 16, 'lata.png');
                    ";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conexao))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
