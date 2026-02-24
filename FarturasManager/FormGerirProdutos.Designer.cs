namespace FarturasManager
{
    partial class FormGerirProdutos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridProdutos = new System.Windows.Forms.DataGridView();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnApagar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEscolherFoto = new System.Windows.Forms.Button();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.numPreco = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPreco)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridProdutos);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnLimpar);
            this.splitContainer1.Panel2.Controls.Add(this.btnGuardar);
            this.splitContainer1.Panel2.Controls.Add(this.btnApagar);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.btnEscolherFoto);
            this.splitContainer1.Panel2.Controls.Add(this.picFoto);
            this.splitContainer1.Panel2.Controls.Add(this.cmbCategoria);
            this.splitContainer1.Panel2.Controls.Add(this.numPreco);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.txtNome);
            this.splitContainer1.Size = new System.Drawing.Size(1381, 765);
            this.splitContainer1.SplitterDistance = 459;
            this.splitContainer1.TabIndex = 1;
            // 
            // gridProdutos
            // 
            this.gridProdutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProdutos.Location = new System.Drawing.Point(0, 0);
            this.gridProdutos.Name = "gridProdutos";
            this.gridProdutos.Size = new System.Drawing.Size(459, 765);
            this.gridProdutos.TabIndex = 0;
            // 
            // btnLimpar
            // 
            this.btnLimpar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.Location = new System.Drawing.Point(714, 665);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(168, 33);
            this.btnLimpar.TabIndex = 8;
            this.btnLimpar.Text = "Limpar Campos";
            this.btnLimpar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.LawnGreen;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(714, 626);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(168, 33);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar Produto";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnApagar
            // 
            this.btnApagar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApagar.BackColor = System.Drawing.Color.Red;
            this.btnApagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApagar.Location = new System.Drawing.Point(714, 704);
            this.btnApagar.Name = "btnApagar";
            this.btnApagar.Size = new System.Drawing.Size(168, 33);
            this.btnApagar.TabIndex = 7;
            this.btnApagar.Text = "Apagar Produto";
            this.btnApagar.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(317, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Preço:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(300, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Categoria:";
            // 
            // btnEscolherFoto
            // 
            this.btnEscolherFoto.AllowDrop = true;
            this.btnEscolherFoto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnEscolherFoto.BackColor = System.Drawing.Color.DarkSalmon;
            this.btnEscolherFoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEscolherFoto.Location = new System.Drawing.Point(291, 682);
            this.btnEscolherFoto.Name = "btnEscolherFoto";
            this.btnEscolherFoto.Size = new System.Drawing.Size(136, 37);
            this.btnEscolherFoto.TabIndex = 5;
            this.btnEscolherFoto.Text = "Escolher Foto";
            this.btnEscolherFoto.UseVisualStyleBackColor = false;
            // 
            // picFoto
            // 
            this.picFoto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picFoto.Location = new System.Drawing.Point(139, 264);
            this.picFoto.Name = "picFoto";
            this.picFoto.Size = new System.Drawing.Size(435, 395);
            this.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFoto.TabIndex = 4;
            this.picFoto.TabStop = false;
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Items.AddRange(new object[] {
            "Farturas",
            "Cones",
            "Recheadas",
            "ChurrosCob",
            "ChurrosSimp",
            "Bebidas"});
            this.cmbCategoria.Location = new System.Drawing.Point(290, 117);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(136, 21);
            this.cmbCategoria.TabIndex = 3;
            this.cmbCategoria.SelectedIndexChanged += new System.EventHandler(this.cmbCategoria_SelectedIndexChanged);
            // 
            // numPreco
            // 
            this.numPreco.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numPreco.DecimalPlaces = 2;
            this.numPreco.Location = new System.Drawing.Point(291, 193);
            this.numPreco.Name = "numPreco";
            this.numPreco.Size = new System.Drawing.Size(120, 20);
            this.numPreco.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(264, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome do Produto:";
            // 
            // txtNome
            // 
            this.txtNome.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNome.Location = new System.Drawing.Point(254, 53);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(214, 20);
            this.txtNome.TabIndex = 0;
            // 
            // FormGerirProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 765);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormGerirProdutos";
            this.Text = "FormGerirProdutos";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPreco)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView gridProdutos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.NumericUpDown numPreco;
        private System.Windows.Forms.Button btnApagar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEscolherFoto;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}