using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ProjetoTesteLuiz
{
    public partial class frmCadastroClientes : Form
    {
        private FormDbContext dbContext;

        public frmCadastroClientes()
        {
            InitializeComponent();
            dbContext = new FormDbContext(@"Data Source=LUIZ-DESKTOP\SQLEXPRESS;Initial Catalog=testeLuiz;Integrated Security=False;User ID=usrluiz;Password=1234;");
        }

        private void desabilitarCampos()
        {
            txtNome.Enabled = false;
            lstSexo.Enabled = false;
            mskTelefone.Enabled = false;
            mskCPF.Enabled = false;
            mskDataNascimento.Enabled = false;
            lstNacionalidade.Enabled = false;
            cmdSalvar.Enabled = false;
            cmdDeletar.Enabled = false;
            cmdLimpar.Enabled = false;
            cmdNovo.Enabled = true;
        }

        private void habilitarCampos()
        {
            txtNome.Enabled = true;
            lstSexo.Enabled = true;
            mskTelefone.Enabled = true;
            mskCPF.Enabled = true;
            mskDataNascimento.Enabled = true;
            lstNacionalidade.Enabled = true;
            cmdSalvar.Enabled = true;
            cmdDeletar.Enabled = true;
            cmdLimpar.Enabled = true;
            cmdNovo.Enabled = false;
        }

        private void limparCampos()
        {
            txtNome.Text = "";
            lstSexo.Text = "";
            lstNacionalidade.Text = "";
            mskTelefone.Text = "";
            mskCPF.Text = "";
            mskDataNascimento.Text = "";
            grClientes.DataSource = null;
            mskCPFPesquisar.Text = "";
            txtNomePesquisar.Text = "";
            txtID.Text = "";
        }

        private void AtualizarGrid()
        {
            try
            {
                // Consulta o banco de dados para obter todos os clientes
                string sql = "SELECT * FROM vwClientes";
                DataTable dt = dbContext.ExecuteQuery(sql);

                // Atualiza o DataSource do grid com os novos dados
                grClientes.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar o grid: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCadastroClientes_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT Nacionalidade FROM tblNacionalidades";
                DataTable dataTable = dbContext.ExecuteQuery(sql);

                foreach (DataRow row in dataTable.Rows)
                {
                    lstNacionalidade.Items.Add(row["Nacionalidade"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar nacionalidades: " + ex.Message);
            }

            AtualizarGrid();
            desabilitarCampos();
        }

        private void grClientes_SelectionChanged(object sender, EventArgs e)
        {
            // Verifica se há uma linha selecionada no grid
            if (grClientes.SelectedRows.Count > 0)
            {
                // Obtém o ID do cliente selecionado na grade
                string clienteID = grClientes.SelectedRows[0].Cells["ID"].Value.ToString();

                // Consulta o banco de dados para obter os detalhes do cliente com base no ID
                string sql = @"SELECT c.*, n.Nacionalidade
                               FROM tblClientes c
                               INNER JOIN tblNacionalidades n ON c.Nacionalidade_id = n.Id
                               WHERE c.ID = @id";
                SqlParameter parameter = new SqlParameter("@id", clienteID);

                try
                {
                    DataTable dt = dbContext.ExecuteQuery(sql, new SqlParameter[] { parameter });

                    // Verifica se foram encontrados dados para o cliente com o ID fornecido
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        habilitarCampos();

                        // Preenche os campos de entrada com os detalhes do cliente
                        txtID.Text = row["ID"].ToString();
                        txtNome.Text = row["Nome"].ToString();
                        mskCPF.Text = row["CPF"].ToString();
                        mskTelefone.Text = row["Telefone"].ToString();

                        // Verifica se o valor do sexo no banco de dados é NULL
                        string sexo = row["Sexo"] != DBNull.Value.ToString() ? row["Sexo"].ToString() : "\"\"";
                        lstSexo.SelectedItem = sexo;

                        lstNacionalidade.Text = row["Nacionalidade"].ToString();

                        // Converte a data de nascimento para o formato esperado e a define no campo correspondente
                        DateTime dataNascimento = Convert.ToDateTime(row["DataNascimento"]);
                        mskDataNascimento.Text = dataNascimento.ToString("ddMMyyyy");
                    }
                    else
                    {
                        MessageBox.Show("Cliente não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao recuperar dados do cliente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmdNovo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            limparCampos();
            mskCPF.Focus();
        }

        private void cmdLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
            desabilitarCampos();
        }

        private void cmdSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmdSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                AtualizarGrid();
                string sql;
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(txtID.Text))
                {
                    // Atualização de um cliente existente
                    sql = "UPDATE tblClientes SET Nome = @nome, CPF = @cpf, Telefone = @telefone, Sexo = @sexo, " +
                        "Nacionalidade_id = @nacionalidade, DataNascimento = @dataNascimento WHERE ID = @id";
                    parameters.Add(new SqlParameter("@id", txtID.Text));
                }
                else
                {
                    // Inserção de um novo cliente
                    sql = "INSERT INTO tblClientes (Nome, CPF, Telefone, Sexo, Nacionalidade_id, DataNascimento, DataHoraCadastro) " +
                        "VALUES (@nome, @cpf, @telefone, @sexo, @nacionalidade, @dataNascimento, @dataHoraCadastro)";
                }

                parameters.Add(new SqlParameter("@nome", txtNome.Text));
                parameters.Add(new SqlParameter("@cpf", mskCPF.Text));
                parameters.Add(new SqlParameter("@telefone", mskTelefone.Text));

                // Verifica se o valor selecionado para Sexo é uma string vazia e, se for, define o valor como null
                string sexo = lstSexo.Text == "\"\"" ? DBNull.Value.ToString() : lstSexo.Text;
                parameters.Add(new SqlParameter("@sexo", sexo));

                if (lstNacionalidade.SelectedItem != null)
                {
                    string nacionalidadeSelecionada = lstNacionalidade.SelectedItem.ToString();
                    short nacionalidadeId = ObterIdNacionalidade(nacionalidadeSelecionada); // Função para obter o ID com base no nome da nacionalidade
                    parameters.Add(new SqlParameter("@nacionalidade", nacionalidadeId));
                }

                // Converter a string de data de nascimento para um objeto DateTime
                DateTime dataNascimento;
                if (DateTime.TryParseExact(mskDataNascimento.Text, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
                {
                    parameters.Add(new SqlParameter("@dataNascimento", dataNascimento.ToString("yyyy-MM-dd")));
                }
                else
                {
                    throw new ArgumentException("Formato de data de nascimento inválido. Use o formato dd/MM/yyyy. Valor recebido: " + mskDataNascimento.Text);
                }

                parameters.Add(new SqlParameter("@dataHoraCadastro", DateTime.Now));

                dbContext.ExecuteNonQuery(sql, parameters.ToArray());

                MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                limparCampos();
                desabilitarCampos();
            }
            catch (Exception error)
            {
                MessageBox.Show("Erro ao salvar cliente: " + error.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdPesquisar_Click(object sender, EventArgs e)
        {
            cmdLimpar.Enabled = true;
            cmdDeletar.Enabled = true;
            try
            {
                string sql = "SELECT * FROM vwClientes WHERE 1 = 1";
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(txtNomePesquisar.Text))
                {
                    sql += " AND Nome LIKE @nome";
                    parameters.Add(new SqlParameter("@nome", "%" + txtNomePesquisar.Text.Trim() + "%"));
                }

                if (!string.IsNullOrEmpty(mskCPFPesquisar.Text))
                {
                    sql += " AND CPF = @cpf";
                    parameters.Add(new SqlParameter("@cpf", mskCPFPesquisar.Text.Trim()));
                }

                if (!string.IsNullOrEmpty(txtID.Text))
                {
                    sql += " AND ID = @id";
                    parameters.Add(new SqlParameter("@id", txtID.Text.Trim()));
                }

                DataTable dt = dbContext.ExecuteQuery(sql, parameters.ToArray());
                grClientes.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar clientes: " + ex.Message);
            }
        }

        private void mskCPF_Leave(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM tblClientes WHERE CPF = @cpf";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@cpf", mskCPF.Text)
                };
                DataTable dt = dbContext.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    // Se o CPF já existir no banco, traz os dados para a tela
                    txtNome.Text = row["Nome"].ToString();
                    mskTelefone.Text = row["Telefone"].ToString();
                    lstSexo.Text = row["Sexo"].ToString();
                    lstNacionalidade.Text = row["Nacionalidade_id"].ToString();
                    // Converte a data de nascimento para o formato dd/MM/yyyy antes de atribuí-la ao campo
                    DateTime dataNascimento = Convert.ToDateTime(row["DataNascimento"]);
                    mskDataNascimento.Text = dataNascimento.ToString("ddMMyyyy");
                    // Defina outras atribuições de campos conforme necessário

                    MessageBox.Show("CPF já cadastrado. Os dados foram carregados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar cliente por CPF: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdDeletar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja remover este cliente?", "Confirmação de Remoção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    AtualizarGrid();
                    string sql = "DELETE FROM tblClientes WHERE ID = @id";
                    SqlParameter[] parameters = { new SqlParameter("@id", txtID.Text) };
                    dbContext.ExecuteNonQuery(sql, parameters);

                    MessageBox.Show("Cliente removido com sucesso!", "Remoção Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    limparCampos();
                    desabilitarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover cliente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private short ObterIdNacionalidade(string nacionalidade)
        {
            try
            {
                string sql = "SELECT Id FROM tblNacionalidades WHERE Nacionalidade = @nacionalidade";
                SqlParameter parameter = new SqlParameter("@nacionalidade", nacionalidade);
                object result = dbContext.ExecuteScalar(sql, parameter);
                return result != null ? (short)result : (short)0; // Retornar o ID da nacionalidade ou 0 se não for encontrado
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter o ID da nacionalidade: " + ex.Message);
                return 0; // Retorna 0 em caso de erro
            }
        }
    }
}
