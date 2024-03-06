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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void AtualizarGrid()
        {
            try
            {
                string sql = "SELECT * FROM vwClientes";
                DataTable dt = dbContext.ExecuteQuery(sql);

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
            if (grClientes.SelectedRows.Count > 0)
            {
                string clienteID = grClientes.SelectedRows[0].Cells["ID"].Value.ToString();

                string sql = @"SELECT c.*, n.Nacionalidade
                               FROM tblClientes c
                               INNER JOIN tblNacionalidades n ON c.Nacionalidade_id = n.Id
                               WHERE c.ID = @id";
                SqlParameter parameter = new SqlParameter("@id", clienteID);

                try
                {
                    DataTable dt = dbContext.ExecuteQuery(sql, new SqlParameter[] { parameter });

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        habilitarCampos();

                        txtID.Text = row["ID"].ToString();
                        txtNome.Text = row["Nome"].ToString();
                        mskCPF.Text = row["CPF"].ToString();
                        mskTelefone.Text = row["Telefone"].ToString();

                        string sexo = row["Sexo"] != DBNull.Value.ToString() ? row["Sexo"].ToString() : "\"\"";
                        lstSexo.SelectedItem = sexo;

                        lstNacionalidade.Text = row["Nacionalidade"].ToString();

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

                if (txtNome.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("O campo de nome não pode conter números.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.IsNullOrEmpty(txtID.Text))
                {
                    sql = "UPDATE tblClientes SET Nome = @nome, CPF = @cpf, Telefone = @telefone, Sexo = @sexo, " +
                        "Nacionalidade_id = @nacionalidade, DataNascimento = @dataNascimento WHERE ID = @id";
                    parameters.Add(new SqlParameter("@id", txtID.Text));
                }
                else
                {
                    sql = "INSERT INTO tblClientes (Nome, CPF, Telefone, Sexo, Nacionalidade_id, DataNascimento, DataHoraCadastro) " +
                        "VALUES (@nome, @cpf, @telefone, @sexo, @nacionalidade, @dataNascimento, @dataHoraCadastro)";
                }

                parameters.Add(new SqlParameter("@nome", txtNome.Text));
                parameters.Add(new SqlParameter("@cpf", mskCPF.Text));
                parameters.Add(new SqlParameter("@telefone", mskTelefone.Text));

                string sexo = lstSexo.Text == "\"\"" ? DBNull.Value.ToString() : lstSexo.Text;
                parameters.Add(new SqlParameter("@sexo", sexo));

                if (lstNacionalidade.SelectedItem != null)
                {
                    string nacionalidadeSelecionada = lstNacionalidade.SelectedItem.ToString();
                    short nacionalidadeId = ObterIdNacionalidade(nacionalidadeSelecionada);
                    parameters.Add(new SqlParameter("@nacionalidade", nacionalidadeId));
                }

                DateTime dataNascimento;
                if (ValidarDataNascimento() && DateTime.TryParseExact(mskDataNascimento.Text, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
                    parameters.Add(new SqlParameter("@dataNascimento", dataNascimento.ToString("yyyy-MM-dd")));
                else
                    throw new ArgumentException("Formato de data de nascimento inválido. Use o formato dd/MM/yyyy. Valor recebido: " + mskDataNascimento.Text);

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
                string cpf = mskCPF.Text;
                if (!ValidarCPF(cpf) && cpf != "")
                {
                    MessageBox.Show("CPF inválido. Digite um número e CPF válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mskCPF.Focus();
                    mskCPF.Clear();
                }

                string sql = "SELECT * FROM tblClientes WHERE CPF = @cpf";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@cpf", mskCPF.Text)
                };
                DataTable dt = dbContext.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtNome.Text = row["Nome"].ToString();
                    mskTelefone.Text = row["Telefone"].ToString();
                    lstSexo.Text = row["Sexo"].ToString();
                    lstNacionalidade.Text = row["Nacionalidade_id"].ToString();
                    DateTime dataNascimento = Convert.ToDateTime(row["DataNascimento"]);
                    mskDataNascimento.Text = dataNascimento.ToString("ddMMyyyy");

                    MessageBox.Show("CPF já cadastrado. Os dados foram carregados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar cliente por CPF: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                return result != null ? (short)result : (short)0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter o ID da nacionalidade: " + ex.Message);
                return 0;
            }
        }

        private bool ValidarCPF(string cpf)
        {
            if (cpf.Length != 11)
                return false;

            bool todosIguais = true;
            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[i - 1])
                {
                    todosIguais = false;
                    break;
                }
            }
            if (todosIguais)
                return false;

            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }
            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (digitoVerificador1 != int.Parse(cpf[9].ToString()))
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }
            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            if (digitoVerificador2 != int.Parse(cpf[10].ToString()))
                return false;

            return true;
        }

        private bool ValidarDataNascimento()
        {
            DateTime dataNascimento;
            if (!DateTime.TryParseExact(mskDataNascimento.Text, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
            {
                return false;
            }

            if (dataNascimento > DateTime.Now)
            {
                return false;
            }

            if (dataNascimento.Day > 31 || dataNascimento.Month > 12)
            {
                return false;
            }

            mskDataNascimento.Focus();
            return true;
        }
    }
}
