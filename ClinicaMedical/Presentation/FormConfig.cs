﻿using System;
using System.Windows.Forms;
using Domain;

namespace Presentation
{
    public partial class FormConfig : Form
    {
        UserModel userModel = new UserModel();
        private string idUser = null;
        private bool Editar = false;

        public FormConfig()
        {
            InitializeComponent();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            MostrarUsers();
            txtPass.PasswordChar = '*';
        }

        private void MostrarUsers()
        {
            var userModel = new UserModel();
            dgvDados.DataSource = userModel.Mostrar();
            
        }

       

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Inserir
            if (Editar == false)
            {
                try
                {
                    userModel.Inserir(txtUserName.Text, txtPass.Text, txtNome.Text, txtSobrenome.Text, txtCargo.Text, txtEmail.Text,txtCpf.Text, txtTelefone.Text) ;
                    MessageBox.Show("Salvo corretamente");
                    MostrarUsers();
                    LimprarCampos();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Os dados não puderam ser inseridos devido a:" + ex);
                }
            }
            // Salvar os dados da edição
            if (Editar == true)
            {
                try
                {
                    userModel.Editar(txtUserName.Text, txtPass.Text, txtNome.Text, txtSobrenome.Text, txtCargo.Text, txtEmail.Text, txtCpf.Text, txtTelefone.Text, idUser);
                    MessageBox.Show("Dados atualizados corretamente");
                    MostrarUsers();
                    LimprarCampos();
                    Editar = false;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Os dados não puderam ser Atualizados devido a: " + ex);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Editar
            if (dgvDados.SelectedRows.Count > 0)
            {
                Editar = true;

                txtUserName.Text = dgvDados.CurrentRow.Cells["UserName"].Value.ToString();
                txtPass.Text = dgvDados.CurrentRow.Cells["Password"].Value.ToString();
                txtNome.Text = dgvDados.CurrentRow.Cells["Nome"].Value.ToString();
                txtSobrenome.Text = dgvDados.CurrentRow.Cells["Sobrenome"].Value.ToString();
                txtCargo.Text = dgvDados.CurrentRow.Cells["Cargo"].Value.ToString();
                txtEmail.Text = dgvDados.CurrentRow.Cells["Email"].Value.ToString();
                txtCpf.Text = dgvDados.CurrentRow.Cells["Cpf"].Value.ToString();
                txtTelefone.Text = dgvDados.CurrentRow.Cells["Telefone"].Value.ToString();
                idUser = dgvDados.CurrentRow.Cells["UserId"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Selecione uma linha por favor");
            }
        }


        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvDados.SelectedRows.Count > 0)
            {
                idUser = dgvDados.CurrentRow.Cells["UserId"].Value.ToString();
                userModel.Excluir(Convert.ToInt32(idUser));
                MessageBox.Show("Usúario exluido corretamente");
                MostrarUsers();
            }
            else
            {
                MessageBox.Show("Selecione uma linha por favor");
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LimprarCampos()
        {
            txtNome.Clear(); 
            txtSobrenome.Text = "";
            txtCargo.Text = "";
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtPass.Text = "";
            txtTelefone.Text = "";
            txtCpf.Text = "";
        }


        // Esconde da senha no grid
        private void dgvDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDados.Columns[e.ColumnIndex].Name == "Password" && e.Value != null)
    {
                dgvDados.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('*', e.Value.ToString().Length);
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvDados.CurrentRow.Tag != null)
                e.Control.Text = dgvDados.CurrentRow.Tag.ToString();
        }

    }
}
