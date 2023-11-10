using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroDeContatos
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=PC03LAB1783\\SQLEXPRESS01; Database= CadastroDeContatos; Trusted_Connection=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;
        public Form1()
        {
            InitializeComponent();
            ExibirDados();
        }

        private void ExibirDados()
        {
            try

            {
                con.Open();
                DataTable dt = new DataTable();
                adapt = new SqlDataAdapter("SELECT * FROM Contato", con);
                adapt.Fill(dt);
                dgvCadastroDeContatos.DataSource = dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        private void LimparDados()
        {
            txtNome.Text = "";
            txtTelefone.Text = "";
            txtEndereco.Text = "";
            txtEmail.Text = "";
            txtCelular.Text = "";

            txtNome.Focus();
        }


        private void Form1_Load(object sender, EventArgs e){}
        private void label2_Click(object sender, EventArgs e){}
        private void label4_Click(object sender, EventArgs e){}
        private void label3_Click(object sender, EventArgs e){}
        private void label5_Click(object sender, EventArgs e){}
        private void label1_Click(object sender, EventArgs e){}

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparDados();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtTelefone.Text != "" &&  txtEndereco.Text != "" && txtCelular.Text != "" && txtEmail.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("INSERT INTO Contato(Nome,Endereco,Celular,Telefone,Email) VALUES (@nome,@endereco,@celular,@telefone,@email)", con);
                    con.Open();

                    cmd.Parameters.AddWithValue("@nome", txtNome.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@celular", txtCelular.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.ToUpper());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Registro concluído com sucesso");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally 
                {
                    con.Close();
                    ExibirDados();
                    LimparDados();
                }
            }
            else
            {
                MessageBox.Show("Informe os dados requeridos!");
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtTelefone.Text != "" && txtEndereco.Text != "" && txtCelular.Text != "" && txtEmail.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("UPDATE Contato SET Nome=@nome, Endereco=@endereco, Celular=@celular, Telefone=@telefone, Email=@email WHERE Id=@id", con);
                    con.Open();

                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@celular", txtCelular.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.ToUpper());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Registro atualizado com sucesso");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                    ExibirDados();
                    LimparDados();
                }
            }
            else
            {
                MessageBox.Show("Informe os dados requeridos!");
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if(ID != 0)
            {
                if(MessageBox.Show("Deseja deletar este registro?", "Agenda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        cmd = new SqlCommand("DELETE Contato WHERE id = @id", con);
                        con.Open();
                        cmd.Parameters.AddWithValue("@id", ID);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro deletado com sucesso");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                        throw;
                    }
                    finally 
                    {
                        con.Close(); 
                        ExibirDados() ;
                        LimparDados() ;
                    }
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
                if (MessageBox.Show("Deseja sair do progama?","Agenda",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    txtNome.Focus();    
                }
        }

        private void dgvCadastroDeContatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(dgvCadastroDeContatos.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtNome.Text = (dgvCadastroDeContatos.Rows[e.RowIndex].Cells[1].Value.ToString());
                txtEndereco.Text = (dgvCadastroDeContatos.Rows[e.RowIndex].Cells[2].Value.ToString());
                txtCelular.Text = (dgvCadastroDeContatos.Rows[e.RowIndex].Cells[3].Value.ToString());
                txtTelefone.Text = (dgvCadastroDeContatos.Rows[e.RowIndex].Cells[4].Value.ToString());
                txtEmail.Text = (dgvCadastroDeContatos.Rows[e.RowIndex].Cells[5].Value.ToString());

            }
            catch{}
        }
    }
}
