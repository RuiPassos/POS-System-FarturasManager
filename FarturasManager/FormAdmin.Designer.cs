namespace FarturasManager
{
    partial class FormAdmin
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFaturacao = new System.Windows.Forms.Label();
            this.gridTopVendas = new System.Windows.Forms.DataGridView();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFim = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.graficoTop = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gridHistorico = new System.Windows.Forms.DataGridView();
            this.btnApagarVenda = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopVendas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistorico)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "RESUMO DO TURNO ATUAL:";
            // 
            // lblFaturacao
            // 
            this.lblFaturacao.AutoSize = true;
            this.lblFaturacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaturacao.ForeColor = System.Drawing.Color.Green;
            this.lblFaturacao.Location = new System.Drawing.Point(617, 69);
            this.lblFaturacao.Name = "lblFaturacao";
            this.lblFaturacao.Size = new System.Drawing.Size(178, 61);
            this.lblFaturacao.TabIndex = 1;
            this.lblFaturacao.Text = "0,00 €";
            // 
            // gridTopVendas
            // 
            this.gridTopVendas.AllowUserToAddRows = false;
            this.gridTopVendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTopVendas.Location = new System.Drawing.Point(85, 133);
            this.gridTopVendas.Name = "gridTopVendas";
            this.gridTopVendas.ReadOnly = true;
            this.gridTopVendas.Size = new System.Drawing.Size(1213, 484);
            this.gridTopVendas.TabIndex = 2;
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Location = new System.Drawing.Point(1657, 956);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(206, 80);
            this.btnVoltar.TabIndex = 3;
            this.btnVoltar.Text = "VOLTAR À VENDA";
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click_1);
            // 
            // dtpInicio
            // 
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicio.Location = new System.Drawing.Point(1444, 47);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(118, 20);
            this.dtpInicio.TabIndex = 4;
            // 
            // dtpFim
            // 
            this.dtpFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFim.Location = new System.Drawing.Point(1695, 46);
            this.dtpFim.Name = "dtpFim";
            this.dtpFim.Size = new System.Drawing.Size(104, 20);
            this.dtpFim.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1610, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "até";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.Location = new System.Drawing.Point(1533, 165);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(180, 42);
            this.btnFiltrar.TabIndex = 8;
            this.btnFiltrar.Text = "FILTRAR";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // graficoTop
            // 
            chartArea1.Name = "ChartArea1";
            this.graficoTop.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.graficoTop.Legends.Add(legend1);
            this.graficoTop.Location = new System.Drawing.Point(1386, 258);
            this.graficoTop.Name = "graficoTop";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.graficoTop.Series.Add(series1);
            this.graficoTop.Size = new System.Drawing.Size(459, 579);
            this.graficoTop.TabIndex = 9;
            this.graficoTop.Text = "chart1";
            // 
            // gridHistorico
            // 
            this.gridHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHistorico.Location = new System.Drawing.Point(85, 646);
            this.gridHistorico.Name = "gridHistorico";
            this.gridHistorico.Size = new System.Drawing.Size(1213, 379);
            this.gridHistorico.TabIndex = 10;
            // 
            // btnApagarVenda
            // 
            this.btnApagarVenda.BackColor = System.Drawing.Color.Red;
            this.btnApagarVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApagarVenda.Location = new System.Drawing.Point(1304, 973);
            this.btnApagarVenda.Name = "btnApagarVenda";
            this.btnApagarVenda.Size = new System.Drawing.Size(215, 46);
            this.btnApagarVenda.TabIndex = 11;
            this.btnApagarVenda.Text = "Apagar Registo";
            this.btnApagarVenda.UseVisualStyleBackColor = false;
            this.btnApagarVenda.Click += new System.EventHandler(this.btnApagarVenda_Click);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1875, 1048);
            this.Controls.Add(this.btnApagarVenda);
            this.Controls.Add(this.gridHistorico);
            this.Controls.Add(this.graficoTop);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFim);
            this.Controls.Add(this.dtpInicio);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.gridTopVendas);
            this.Controls.Add(this.lblFaturacao);
            this.Controls.Add(this.label1);
            this.Name = "FormAdmin";
            this.Text = "FormAdmin";
            this.Load += new System.EventHandler(this.FormAdmin_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.gridTopVendas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistorico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFaturacao;
        private System.Windows.Forms.DataGridView gridTopVendas;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.DateTimePicker dtpFim;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.DataVisualization.Charting.Chart graficoTop;
        private System.Windows.Forms.DataGridView gridHistorico;
        private System.Windows.Forms.Button btnApagarVenda;
    }
}