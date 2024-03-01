namespace ProjetoTesteLuiz
{
    partial class frmCadastroClientes
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grClientes = new DataGridView();
            txtNome = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            fileSystemWatcher1 = new FileSystemWatcher();
            label4 = new Label();
            label5 = new Label();
            lstNacionalidade = new ComboBox();
            label6 = new Label();
            cmdSalvar = new Button();
            cmdEditar = new Button();
            cmdDeletar = new Button();
            mskDataNascimento = new MaskedTextBox();
            mskTelefone = new MaskedTextBox();
            mskCPF = new MaskedTextBox();
            cmdNovo = new Button();
            cmdLimpar = new Button();
            cmdSair = new Button();
            mskCPFPesquisar = new MaskedTextBox();
            label7 = new Label();
            label8 = new Label();
            txtNomePesquisar = new TextBox();
            cmdPesquisar = new Button();
            lstSexo = new ListBox();
            ((System.ComponentModel.ISupportInitialize)grClientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // grClientes
            // 
            grClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grClientes.Location = new Point(12, 303);
            grClientes.Name = "grClientes";
            grClientes.RowTemplate.Height = 25;
            grClientes.Size = new Size(509, 135);
            grClientes.TabIndex = 0;
            // 
            // txtNome
            // 
            txtNome.Location = new Point(31, 47);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(191, 23);
            txtNome.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 29);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 2;
            label1.Text = "Nome";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(250, 29);
            label2.Name = "label2";
            label2.Size = new Size(28, 15);
            label2.TabIndex = 4;
            label2.Text = "CPF";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 86);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 6;
            label3.Text = "Telefone";
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(250, 143);
            label4.Name = "label4";
            label4.Size = new Size(32, 15);
            label4.TabIndex = 8;
            label4.Text = "Sexo";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(250, 86);
            label5.Name = "label5";
            label5.Size = new Size(83, 15);
            label5.TabIndex = 10;
            label5.Text = "Nacionalidade";
            // 
            // lstNacionalidade
            // 
            lstNacionalidade.FormattingEnabled = true;
            lstNacionalidade.Location = new Point(250, 104);
            lstNacionalidade.Name = "lstNacionalidade";
            lstNacionalidade.Size = new Size(121, 23);
            lstNacionalidade.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(31, 143);
            label6.Name = "label6";
            label6.Size = new Size(114, 15);
            label6.TabIndex = 12;
            label6.Text = "Data de Nascimento";
            // 
            // cmdSalvar
            // 
            cmdSalvar.Location = new Point(109, 274);
            cmdSalvar.Name = "cmdSalvar";
            cmdSalvar.Size = new Size(75, 23);
            cmdSalvar.TabIndex = 13;
            cmdSalvar.Text = "Salvar";
            cmdSalvar.UseVisualStyleBackColor = true;
            cmdSalvar.Click += cmdSalvar_Click;
            // 
            // cmdEditar
            // 
            cmdEditar.Location = new Point(190, 274);
            cmdEditar.Name = "cmdEditar";
            cmdEditar.Size = new Size(75, 23);
            cmdEditar.TabIndex = 14;
            cmdEditar.Text = "Editar";
            cmdEditar.UseVisualStyleBackColor = true;
            // 
            // cmdDeletar
            // 
            cmdDeletar.Location = new Point(271, 274);
            cmdDeletar.Name = "cmdDeletar";
            cmdDeletar.Size = new Size(75, 23);
            cmdDeletar.TabIndex = 15;
            cmdDeletar.Text = "Deletar";
            cmdDeletar.UseVisualStyleBackColor = true;
            // 
            // mskDataNascimento
            // 
            mskDataNascimento.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskDataNascimento.Location = new Point(31, 162);
            mskDataNascimento.Mask = "00/00/0000";
            mskDataNascimento.Name = "mskDataNascimento";
            mskDataNascimento.Size = new Size(66, 23);
            mskDataNascimento.TabIndex = 18;
            mskDataNascimento.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskDataNascimento.ValidatingType = typeof(DateTime);
            // 
            // mskTelefone
            // 
            mskTelefone.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskTelefone.Location = new Point(31, 104);
            mskTelefone.Mask = "(99) 00000-0000";
            mskTelefone.Name = "mskTelefone";
            mskTelefone.Size = new Size(89, 23);
            mskTelefone.TabIndex = 19;
            mskTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            // 
            // mskCPF
            // 
            mskCPF.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskCPF.Location = new Point(250, 47);
            mskCPF.Mask = "000.000.000-00";
            mskCPF.Name = "mskCPF";
            mskCPF.Size = new Size(96, 23);
            mskCPF.TabIndex = 20;
            mskCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            // 
            // cmdNovo
            // 
            cmdNovo.Location = new Point(28, 274);
            cmdNovo.Name = "cmdNovo";
            cmdNovo.Size = new Size(75, 23);
            cmdNovo.TabIndex = 21;
            cmdNovo.Text = "Novo";
            cmdNovo.UseVisualStyleBackColor = true;
            cmdNovo.Click += cmdNovo_Click;
            // 
            // cmdLimpar
            // 
            cmdLimpar.Location = new Point(352, 274);
            cmdLimpar.Name = "cmdLimpar";
            cmdLimpar.Size = new Size(75, 23);
            cmdLimpar.TabIndex = 22;
            cmdLimpar.Text = "Limpar";
            cmdLimpar.UseVisualStyleBackColor = true;
            cmdLimpar.Click += cmdLimpar_Click;
            // 
            // cmdSair
            // 
            cmdSair.Location = new Point(433, 274);
            cmdSair.Name = "cmdSair";
            cmdSair.Size = new Size(75, 23);
            cmdSair.TabIndex = 23;
            cmdSair.Text = "Sair";
            cmdSair.UseVisualStyleBackColor = true;
            cmdSair.Click += cmdSair_Click;
            // 
            // mskCPFPesquisar
            // 
            mskCPFPesquisar.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskCPFPesquisar.Location = new Point(65, 217);
            mskCPFPesquisar.Mask = "000.000.000-00";
            mskCPFPesquisar.Name = "mskCPFPesquisar";
            mskCPFPesquisar.Size = new Size(96, 23);
            mskCPFPesquisar.TabIndex = 25;
            mskCPFPesquisar.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(31, 222);
            label7.Name = "label7";
            label7.Size = new Size(28, 15);
            label7.TabIndex = 24;
            label7.Text = "CPF";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(182, 222);
            label8.Name = "label8";
            label8.Size = new Size(40, 15);
            label8.TabIndex = 27;
            label8.Text = "Nome";
            // 
            // txtNomePesquisar
            // 
            txtNomePesquisar.Location = new Point(228, 219);
            txtNomePesquisar.Name = "txtNomePesquisar";
            txtNomePesquisar.Size = new Size(143, 23);
            txtNomePesquisar.TabIndex = 26;
            // 
            // cmdPesquisar
            // 
            cmdPesquisar.Location = new Point(377, 222);
            cmdPesquisar.Name = "cmdPesquisar";
            cmdPesquisar.Size = new Size(75, 23);
            cmdPesquisar.TabIndex = 28;
            cmdPesquisar.Text = "Pesquisar";
            cmdPesquisar.UseVisualStyleBackColor = true;
            cmdPesquisar.Click += cmdPesquisar_Click;
            // 
            // lstSexo
            // 
            lstSexo.AllowDrop = true;
            lstSexo.DisplayMember = "adasd";
            lstSexo.FormattingEnabled = true;
            lstSexo.ItemHeight = 15;
            lstSexo.Items.AddRange(new object[] { "\"M\"", "\"F\"", "\"\"" });
            lstSexo.Location = new Point(250, 161);
            lstSexo.Name = "lstSexo";
            lstSexo.Size = new Size(58, 49);
            lstSexo.TabIndex = 29;
            lstSexo.ValueMember = "sdadsa";
            // 
            // frmCadastroClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(533, 450);
            Controls.Add(lstSexo);
            Controls.Add(cmdPesquisar);
            Controls.Add(label8);
            Controls.Add(txtNomePesquisar);
            Controls.Add(mskCPFPesquisar);
            Controls.Add(label7);
            Controls.Add(cmdSair);
            Controls.Add(cmdLimpar);
            Controls.Add(cmdNovo);
            Controls.Add(mskCPF);
            Controls.Add(mskTelefone);
            Controls.Add(mskDataNascimento);
            Controls.Add(cmdDeletar);
            Controls.Add(cmdEditar);
            Controls.Add(cmdSalvar);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(lstNacionalidade);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtNome);
            Controls.Add(grClientes);
            Name = "frmCadastroClientes";
            Text = "Cadastro Clientes";
            Load += frmCadastroClientes_Load;
            ((System.ComponentModel.ISupportInitialize)grClientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView grClientes;
        private TextBox txtNome;
        private Label label1;
        private Label label2;
        private Label label3;
        private FileSystemWatcher fileSystemWatcher1;
        private Label label6;
        private Label label5;
        private ComboBox lstNacionalidade;
        private Label label4;
        private Button cmdDeletar;
        private Button cmdEditar;
        private Button cmdSalvar;
        private MaskedTextBox mskDataNascimento;
        private Button cmdNovo;
        private MaskedTextBox mskCPF;
        private MaskedTextBox mskTelefone;
        private Button cmdLimpar;
        private Button cmdSair;
        private Button cmdPesquisar;
        private Label label8;
        private TextBox txtNomePesquisar;
        private MaskedTextBox mskCPFPesquisar;
        private Label label7;
        private ListBox lstSexo;
    }
}