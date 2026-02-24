namespace FarturasManager
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEmitirFaturaVendus = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.gridVendas = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblVisor = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnApagar = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.painelProdutos = new System.Windows.Forms.FlowLayoutPanel();
            this.txtNIF = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.painelPagamentos = new System.Windows.Forms.Panel();
            this.btnNumerario = new System.Windows.Forms.Button();
            this.btnMultibanco = new System.Windows.Forms.Button();
            this.btnCredito = new System.Windows.Forms.Button();
            this.btnTransferencia = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVendas)).BeginInit();
            this.panel2.SuspendLayout();
            this.painelProdutos.SuspendLayout();
            this.painelPagamentos.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNIF);
            this.panel1.Controls.Add(this.btnEmitirFaturaVendus);
            this.panel1.Controls.Add(this.btnRemover);
            this.panel1.Controls.Add(this.btnFechar);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Controls.Add(this.gridVendas);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnEmitirFaturaVendus
            // 
            this.btnEmitirFaturaVendus.BackColor = System.Drawing.Color.RoyalBlue;
            resources.ApplyResources(this.btnEmitirFaturaVendus, "btnEmitirFaturaVendus");
            this.btnEmitirFaturaVendus.ForeColor = System.Drawing.Color.White;
            this.btnEmitirFaturaVendus.Name = "btnEmitirFaturaVendus";
            this.btnEmitirFaturaVendus.UseVisualStyleBackColor = false;
            this.btnEmitirFaturaVendus.Click += new System.EventHandler(this.btnEmitirFaturaVendus_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.btnRemover, "btnRemover");
            this.btnRemover.ForeColor = System.Drawing.Color.White;
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.Green;
            resources.ApplyResources(this.btnFechar, "btnFechar");
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // lblTotal
            // 
            resources.ApplyResources(this.lblTotal, "lblTotal");
            this.lblTotal.ForeColor = System.Drawing.Color.Green;
            this.lblTotal.Name = "lblTotal";
            // 
            // gridVendas
            // 
            this.gridVendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.gridVendas, "gridVendas");
            this.gridVendas.Name = "gridVendas";
            this.gridVendas.ReadOnly = true;
            this.gridVendas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.lblVisor);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btn2);
            this.panel2.Controls.Add(this.btn3);
            this.panel2.Controls.Add(this.btn9);
            this.panel2.Controls.Add(this.btn0);
            this.panel2.Controls.Add(this.btn8);
            this.panel2.Controls.Add(this.btn7);
            this.panel2.Controls.Add(this.btn6);
            this.panel2.Controls.Add(this.btn5);
            this.panel2.Controls.Add(this.btn4);
            this.panel2.Controls.Add(this.btnLimpar);
            this.panel2.Controls.Add(this.btnApagar);
            this.panel2.Controls.Add(this.btn1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // lblVisor
            // 
            resources.ApplyResources(this.lblVisor, "lblVisor");
            this.lblVisor.BackColor = System.Drawing.Color.White;
            this.lblVisor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVisor.Name = "lblVisor";
            this.lblVisor.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.HotTrack;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn2
            // 
            resources.ApplyResources(this.btn2, "btn2");
            this.btn2.Name = "btn2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn3
            // 
            resources.ApplyResources(this.btn3, "btn3");
            this.btn3.Name = "btn3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // btn9
            // 
            resources.ApplyResources(this.btn9, "btn9");
            this.btn9.Name = "btn9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.btn9_Click);
            // 
            // btn0
            // 
            resources.ApplyResources(this.btn0, "btn0");
            this.btn0.Name = "btn0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.btn0_Click);
            // 
            // btn8
            // 
            resources.ApplyResources(this.btn8, "btn8");
            this.btn8.Name = "btn8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.btn8_Click);
            // 
            // btn7
            // 
            resources.ApplyResources(this.btn7, "btn7");
            this.btn7.Name = "btn7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn6
            // 
            resources.ApplyResources(this.btn6, "btn6");
            this.btn6.Name = "btn6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.btn6_Click);
            // 
            // btn5
            // 
            resources.ApplyResources(this.btn5, "btn5");
            this.btn5.Name = "btn5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.btn5_Click);
            // 
            // btn4
            // 
            resources.ApplyResources(this.btn4, "btn4");
            this.btn4.Name = "btn4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.btn4_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.Color.OrangeRed;
            resources.ApplyResources(this.btnLimpar, "btnLimpar");
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnApagar
            // 
            this.btnApagar.BackColor = System.Drawing.Color.Orange;
            resources.ApplyResources(this.btnApagar, "btnApagar");
            this.btnApagar.Name = "btnApagar";
            this.btnApagar.UseVisualStyleBackColor = false;
            this.btnApagar.Click += new System.EventHandler(this.btnApagar_Click);
            // 
            // btn1
            // 
            resources.ApplyResources(this.btn1, "btn1");
            this.btn1.Name = "btn1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // painelProdutos
            // 
            resources.ApplyResources(this.painelProdutos, "painelProdutos");
            this.painelProdutos.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.painelProdutos.Controls.Add(this.painelPagamentos);
            this.painelProdutos.Name = "painelProdutos";
            // 
            // txtNIF
            // 
            resources.ApplyResources(this.txtNIF, "txtNIF");
            this.txtNIF.Name = "txtNIF";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // painelPagamentos
            // 
            this.painelPagamentos.BackColor = System.Drawing.Color.Blue;
            this.painelPagamentos.Controls.Add(this.btnMultibanco);
            this.painelPagamentos.Controls.Add(this.btnVoltar);
            this.painelPagamentos.Controls.Add(this.btnTransferencia);
            this.painelPagamentos.Controls.Add(this.btnCredito);
            this.painelPagamentos.Controls.Add(this.btnNumerario);
            resources.ApplyResources(this.painelPagamentos, "painelPagamentos");
            this.painelPagamentos.Name = "painelPagamentos";
            // 
            // btnNumerario
            // 
            this.btnNumerario.BackColor = System.Drawing.Color.LimeGreen;
            resources.ApplyResources(this.btnNumerario, "btnNumerario");
            this.btnNumerario.Name = "btnNumerario";
            this.btnNumerario.UseVisualStyleBackColor = false;
            this.btnNumerario.Click += new System.EventHandler(this.btnNumerario_Click);
            // 
            // btnMultibanco
            // 
            this.btnMultibanco.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.btnMultibanco, "btnMultibanco");
            this.btnMultibanco.Name = "btnMultibanco";
            this.btnMultibanco.UseVisualStyleBackColor = false;
            this.btnMultibanco.Click += new System.EventHandler(this.btnMultibanco_Click);
            // 
            // btnCredito
            // 
            this.btnCredito.BackColor = System.Drawing.Color.Orange;
            resources.ApplyResources(this.btnCredito, "btnCredito");
            this.btnCredito.Name = "btnCredito";
            this.btnCredito.UseVisualStyleBackColor = false;
            this.btnCredito.Click += new System.EventHandler(this.btnCredito_Click);
            // 
            // btnTransferencia
            // 
            this.btnTransferencia.BackColor = System.Drawing.Color.Purple;
            resources.ApplyResources(this.btnTransferencia, "btnTransferencia");
            this.btnTransferencia.Name = "btnTransferencia";
            this.btnTransferencia.UseVisualStyleBackColor = false;
            this.btnTransferencia.Click += new System.EventHandler(this.btnTransferencia_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.btnVoltar, "btnVoltar");
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.painelProdutos);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVendas)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.painelProdutos.ResumeLayout(false);
            this.painelPagamentos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridVendas;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnApagar;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.FlowLayoutPanel painelProdutos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblVisor;
        private System.Windows.Forms.Button btnEmitirFaturaVendus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNIF;
        private System.Windows.Forms.Panel painelPagamentos;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button btnTransferencia;
        private System.Windows.Forms.Button btnCredito;
        private System.Windows.Forms.Button btnMultibanco;
        private System.Windows.Forms.Button btnNumerario;
    }
}

