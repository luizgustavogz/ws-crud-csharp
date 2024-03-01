using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace ProjetoTesteLuiz
{
    public partial class frmCadastroClientes : Form
    {
        public frmCadastroClientes()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=LUIZ-DESKTOP\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=testeLuiz");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dtr;

        public void desabilitarCampos()
        {
            txtNome.Enabled = false;
            lstSexo.Enabled = false;
            mskTelefone.Enabled = false;
            mskCPF.Enabled = false;
            mskDataNascimento.Enabled = false;
            lstNacionalidade.Enabled = false;
            cmdSalvar.Enabled = false;
            cmdDeletar.Enabled = false;
            cmdEditar.Enabled = false;
            cmdLimpar.Enabled = false;
            cmdNovo.Enabled = true;
        }

        public void habilitarCampos()
        {
            txtNome.Enabled = true;
            lstSexo.Enabled = true;
            mskTelefone.Enabled = true;
            mskCPF.Enabled = true;
            mskDataNascimento.Enabled = true;
            lstNacionalidade.Enabled = true;
            cmdSalvar.Enabled = true;
            cmdDeletar.Enabled = true;
            cmdEditar.Enabled = true;
            cmdLimpar.Enabled = true;
            cmdNovo.Enabled = false;
        }

        public void limparCampos()
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
        }

        private void frmCadastroClientes_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string sql = "SELECT Id, Nacionalidade FROM tblNacionalidades";
                cmd.CommandText = sql;
                cmd.Connection = conn;

                using (dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        lstNacionalidade.Items.Add(new KeyValuePair<short, string>((short)dtr["Id"], (string)dtr["Nacionalidade"]));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar nacionalidades: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            desabilitarCampos();
        }

        private void cmdNovo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
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
                conn.Open();
                string sql = "SELECT CPF FROM tblClientes WHERE CPF = @cpf";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cpf", mskCPF.Text);
                dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    MessageBox.Show("CPF já cadastrado", "Ops", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dtr.Close();
                    sql = "INSERT INTO tblClientes (Nome, CPF, Telefone, Sexo, Nacionalidade_id, DataNascimento, DataHoraCadastro) " +
                        "VALUES (@nome, @cpf, @telefone, @sexo, @nacionalidade, @dataNascimento, @dataHoraCadastro)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@cpf", mskCPF.Text);
                    cmd.Parameters.AddWithValue("@telefone", mskTelefone.Text);
                    cmd.Parameters.AddWithValue("@sexo", lstSexo.Text);
                    cmd.Parameters.AddWithValue("@nacionalidade", ((KeyValuePair<short, string>)lstNacionalidade.SelectedItem).Key); // Alterado para short

                    // Converter a string de data de nascimento para um objeto DateTime
                    DateTime dataNascimento;
                    if (DateTime.TryParseExact(mskDataNascimento.Text, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
                    {
                        cmd.Parameters.AddWithValue("@dataNascimento", dataNascimento.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        throw new ArgumentException("Formato de data de nascimento inválido. Use o formato dd/MM/yyyy. Valor recebido: " + mskDataNascimento.Text);
                    }

                    cmd.Parameters.AddWithValue("@dataHoraCadastro", DateTime.Now);

                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Dados cadastrados com sucesso!!", "Dados cadastrados", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    limparCampos();
                    desabilitarCampos();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Erro ao salvar cliente: " + error.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void cmdPesquisar_Click(object sender, EventArgs e)
        {
            cmdLimpar.Enabled = true;
            try
            {
                conn.Open();
                string sql = "SELECT * FROM tblClientes WHERE 1 = 1";
                if (!string.IsNullOrEmpty(txtNomePesquisar.Text))
                {
                    sql += " AND Nome LIKE @nome";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@nome", "%" + txtNomePesquisar.Text.Trim() + "%");
                }
                if (!string.IsNullOrEmpty(mskCPFPesquisar.Text))
                {
                    sql += " AND CPF = @cpf";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@cpf", mskCPFPesquisar.Text.Trim());
                }

                cmd.CommandText = sql;                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grClientes.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar clientes: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}