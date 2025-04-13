using MySql.Data.MySqlClient;

using System.Data;

namespace CadastroAlunos
{
    public partial class cadastroAlunos : Form
    {
        public cadastroAlunos()
        {
            InitializeComponent();
        }


        private void groupBox2_Enter(object sender, EventArgs e) { }


        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {

                string nomeAluno = txtNome.Text;
                string Curso = txtCurso.Text;
                string dataNascimento = mskdDataNascimento.Text;
                string Telefone = mskdTelefone.Text;

                DateTime dataMySqlFormat = Convert.ToDateTime(dataNascimento);
                string dataNascimentoFormat = dataMySqlFormat.ToString("yyyy-MM-dd");

                string conexaoBanco = "Server=localHost; Database=cadastroalunos; Uid=root; pwd=''";
                MySqlConnection conexao = new MySqlConnection(conexaoBanco);

                conexao.Open();

                string insert = "INSERT INTO cadastroalunos (Nome, data_nascimento, Curso, Telefone) values (@Nome, @dataNascimento, @Curso, @Telefone)";
                MySqlCommand insertAluno = new MySqlCommand(insert, conexao);


                insertAluno.Parameters.AddWithValue("@Nome", nomeAluno);
                insertAluno.Parameters.AddWithValue("@dataNascimento", dataNascimentoFormat);
                insertAluno.Parameters.AddWithValue("@Curso", Curso);
                insertAluno.Parameters.AddWithValue("@Telefone", Telefone);

                int contLinhas = Convert.ToInt32(insertAluno.ExecuteNonQuery());



                if (contLinhas > 0)
                {
                    MessageBox.Show("Usuário cadastrado!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Não foi possível cadastrar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                conexao.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }


        private void btnListar_Click(object sender, EventArgs e)
        {
            string conexaoBanco = "Server=localHost; Database=cadastroalunos; Uid=root; pwd=''";
            MySqlConnection conexao = new MySqlConnection(conexaoBanco);

            conexao.Open();

            string consultaSQL = "SELECT * FROM cadastroalunos";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(consultaSQL, conexao);

            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            GrindView.AllowUserToAddRows = false;
            GrindView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            GrindView.DataSource = dataTable;
            GrindView.AutoResizeColumns();
            GrindView.ClearSelection();

            conexao.Close();
        }
        public int BuscarId()
        {
            int idAlunoSelected = Convert.ToInt32(GrindView.CurrentRow.Cells[0].Value.ToString());
            return idAlunoSelected;
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {

                string nomeAluno = txtNome.Text;
                string Curso = txtCurso.Text;
                string dataNascimento = mskdDataNascimento.Text;
                string Telefone = mskdTelefone.Text;

                DateTime dataMySqlFormat = Convert.ToDateTime(dataNascimento);
                string dataNascimentoFormat = dataMySqlFormat.ToString("yyyy-MM-dd");

                string conexaoBanco = "Server=localHost; Database=cadastroalunos; Uid=root; pwd=''";
                MySqlConnection conexao = new MySqlConnection(conexaoBanco);

                conexao.Open();

                int idAluno = BuscarId();


                string update = "UPDATE cadastroalunos SET Nome = @nome, data_nascimento = @dataNascimento, Curso = @Curso, Telefone = @Telefone WHERE id = @id";
                MySqlCommand updateAluno = new MySqlCommand(update, conexao);


                updateAluno.Parameters.AddWithValue("@Nome", nomeAluno);
                updateAluno.Parameters.AddWithValue("@dataNascimento", dataNascimentoFormat);
                updateAluno.Parameters.AddWithValue("@Curso", Curso);
                updateAluno.Parameters.AddWithValue("@Telefone", Telefone);
                updateAluno.Parameters.AddWithValue("@id", idAluno);

                int contLinhas = updateAluno.ExecuteNonQuery();



                if (contLinhas > 0)
                {
                    MessageBox.Show("Dados alterados", "Atualização de dados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Não foi possível editar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                conexao.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult result = MessageBox.Show("Deseja apagar este registro? ", "Atualização de dados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {


                    string conexaoBanco = "Server=localHost; Database=cadastroalunos; Uid=root; pwd=''";
                    MySqlConnection conexao = new MySqlConnection(conexaoBanco);

                    conexao.Open();

                    string delete = "DELETE from cadastroalunos WHERE nome = @nome";
                    MySqlCommand deleteAluno = new MySqlCommand(delete, conexao);


                    deleteAluno.Parameters.AddWithValue("@Nome", GrindView.CurrentRow.Cells[1].Value.ToString());



                    int contLinhas = Convert.ToInt32(deleteAluno.ExecuteNonQuery());



                    if (contLinhas > 0)
                    {
                        MessageBox.Show("Dados deletados!", "Atualização de dados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível deletar", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    conexao.Close();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCurso.Clear();
            txtNome.Clear();
            mskdTelefone.Clear();
            mskdDataNascimento.Clear();
        }


        private void mskdTelefone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mskdDataNascimento_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void GrindView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GrindView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    BuscarId();
                    txtNome.Text = GrindView.Rows[e.RowIndex].Cells["nome"].Value.ToString();
                    string dataNascimento = GrindView.Rows[e.RowIndex].Cells["dataNascimento"].Value.ToString();
                    mskdDataNascimento.Text = DateTime.Parse(dataNascimento).ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pegar dados" + ex.Message);
            }
        }

        private void GrindView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    BuscarId();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar id do aluno" + ex.Message);
            }
        }

        private void cadastroAlunos_Load(object sender, EventArgs e)
        {

        }
    }
}
