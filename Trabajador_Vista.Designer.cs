namespace Romana_AppVendimia
{
    partial class Trabajador_Vista
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Trabajador_Vista));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.TittleLabel = new System.Windows.Forms.Label();
            this.EstadoLayout = new System.Windows.Forms.TableLayoutPanel();
            this.EstadoLabel = new System.Windows.Forms.Label();
            this.EstadoText = new System.Windows.Forms.Label();
            this.SecondLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.LecturaText = new System.Windows.Forms.Label();
            this.TicketLabel = new System.Windows.Forms.Label();
            this.TicketText = new System.Windows.Forms.Label();
            this.BotonesLayout = new System.Windows.Forms.TableLayoutPanel();
            this.IniciarButton = new Bunifu.Framework.UI.BunifuThinButton2();
            this.MedicionButton = new Bunifu.Framework.UI.BunifuThinButton2();
            this.CapturarButton = new Bunifu.Framework.UI.BunifuThinButton2();
            this.RepetirButton = new Bunifu.Framework.UI.BunifuThinButton2();
            this.BottomLayout = new System.Windows.Forms.TableLayoutPanel();
            this.DataGridInfo = new System.Windows.Forms.DataGridView();
            this.GuardarLayout = new System.Windows.Forms.TableLayoutPanel();
            this.GuardarButton = new Bunifu.Framework.UI.BunifuThinButton2();
            this.Lectura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Temperatura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Volumen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainLayout.SuspendLayout();
            this.EstadoLayout.SuspendLayout();
            this.SecondLayout.SuspendLayout();
            this.BotonesLayout.SuspendLayout();
            this.BottomLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridInfo)).BeginInit();
            this.GuardarLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainLayout
            // 
            this.MainLayout.ColumnCount = 3;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 96F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.MainLayout.Controls.Add(this.TittleLabel, 1, 0);
            this.MainLayout.Controls.Add(this.EstadoLayout, 1, 1);
            this.MainLayout.Controls.Add(this.SecondLayout, 1, 2);
            this.MainLayout.Controls.Add(this.BotonesLayout, 1, 3);
            this.MainLayout.Controls.Add(this.BottomLayout, 1, 4);
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(0, 0);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.RowCount = 5;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.16489F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.80423F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.62616F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.30383F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.94613F));
            this.MainLayout.Size = new System.Drawing.Size(1286, 757);
            this.MainLayout.TabIndex = 0;
            // 
            // TittleLabel
            // 
            this.TittleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TittleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TittleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(125)))), ((int)(((byte)(121)))));
            this.TittleLabel.Location = new System.Drawing.Point(28, 0);
            this.TittleLabel.Name = "TittleLabel";
            this.TittleLabel.Size = new System.Drawing.Size(1228, 84);
            this.TittleLabel.TabIndex = 0;
            this.TittleLabel.Text = "Captura Automática de Grado";
            this.TittleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EstadoLayout
            // 
            this.EstadoLayout.ColumnCount = 1;
            this.EstadoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.EstadoLayout.Controls.Add(this.EstadoText, 0, 1);
            this.EstadoLayout.Controls.Add(this.EstadoLabel, 0, 0);
            this.EstadoLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EstadoLayout.Location = new System.Drawing.Point(28, 87);
            this.EstadoLayout.Name = "EstadoLayout";
            this.EstadoLayout.RowCount = 2;
            this.EstadoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.EstadoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.EstadoLayout.Size = new System.Drawing.Size(1228, 159);
            this.EstadoLayout.TabIndex = 1;
            // 
            // EstadoLabel
            // 
            this.EstadoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EstadoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EstadoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(33)))), ((int)(((byte)(7)))));
            this.EstadoLabel.Location = new System.Drawing.Point(15, 0);
            this.EstadoLabel.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.EstadoLabel.Name = "EstadoLabel";
            this.EstadoLabel.Size = new System.Drawing.Size(1210, 55);
            this.EstadoLabel.TabIndex = 0;
            this.EstadoLabel.Text = "Estado del Refractómetro";
            this.EstadoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EstadoText
            // 
            this.EstadoText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.EstadoText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EstadoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EstadoText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(116)))), ((int)(((byte)(145)))));
            this.EstadoText.Location = new System.Drawing.Point(5, 65);
            this.EstadoText.Margin = new System.Windows.Forms.Padding(5, 10, 3, 10);
            this.EstadoText.Name = "EstadoText";
            this.EstadoText.Size = new System.Drawing.Size(1220, 84);
            this.EstadoText.TabIndex = 1;
            this.EstadoText.Text = " ... Esperando Información del Refractómetro";
            this.EstadoText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SecondLayout
            // 
            this.SecondLayout.ColumnCount = 2;
            this.SecondLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SecondLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SecondLayout.Controls.Add(this.TicketText, 1, 1);
            this.SecondLayout.Controls.Add(this.LecturaText, 0, 1);
            this.SecondLayout.Controls.Add(this.label2, 0, 0);
            this.SecondLayout.Controls.Add(this.TicketLabel, 1, 0);
            this.SecondLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SecondLayout.Location = new System.Drawing.Point(28, 252);
            this.SecondLayout.Name = "SecondLayout";
            this.SecondLayout.RowCount = 2;
            this.SecondLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.88189F));
            this.SecondLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.11811F));
            this.SecondLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.SecondLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.SecondLayout.Size = new System.Drawing.Size(1228, 135);
            this.SecondLayout.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(33)))), ((int)(((byte)(7)))));
            this.label2.Location = new System.Drawing.Point(15, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(15, 5, 3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(596, 50);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lectura";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LecturaText
            // 
            this.LecturaText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.LecturaText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LecturaText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LecturaText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(116)))), ((int)(((byte)(145)))));
            this.LecturaText.Location = new System.Drawing.Point(5, 70);
            this.LecturaText.Margin = new System.Windows.Forms.Padding(5, 10, 3, 10);
            this.LecturaText.Name = "LecturaText";
            this.LecturaText.Size = new System.Drawing.Size(606, 55);
            this.LecturaText.TabIndex = 2;
            this.LecturaText.Text = "1";
            this.LecturaText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TicketLabel
            // 
            this.TicketLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TicketLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TicketLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(33)))), ((int)(((byte)(7)))));
            this.TicketLabel.Location = new System.Drawing.Point(617, 0);
            this.TicketLabel.Name = "TicketLabel";
            this.TicketLabel.Size = new System.Drawing.Size(608, 60);
            this.TicketLabel.TabIndex = 3;
            this.TicketLabel.Text = "Ticket";
            this.TicketLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TicketText
            // 
            this.TicketText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.TicketText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TicketText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TicketText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(116)))), ((int)(((byte)(145)))));
            this.TicketText.Location = new System.Drawing.Point(619, 70);
            this.TicketText.Margin = new System.Windows.Forms.Padding(5, 10, 3, 10);
            this.TicketText.Name = "TicketText";
            this.TicketText.Size = new System.Drawing.Size(606, 55);
            this.TicketText.TabIndex = 4;
            this.TicketText.Text = "1";
            this.TicketText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BotonesLayout
            // 
            this.BotonesLayout.ColumnCount = 4;
            this.BotonesLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BotonesLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BotonesLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BotonesLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BotonesLayout.Controls.Add(this.RepetirButton, 3, 0);
            this.BotonesLayout.Controls.Add(this.CapturarButton, 2, 0);
            this.BotonesLayout.Controls.Add(this.MedicionButton, 1, 0);
            this.BotonesLayout.Controls.Add(this.IniciarButton, 0, 0);
            this.BotonesLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BotonesLayout.Location = new System.Drawing.Point(28, 393);
            this.BotonesLayout.Name = "BotonesLayout";
            this.BotonesLayout.RowCount = 1;
            this.BotonesLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.BotonesLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.BotonesLayout.Size = new System.Drawing.Size(1228, 72);
            this.BotonesLayout.TabIndex = 3;
            // 
            // IniciarButton
            // 
            this.IniciarButton.ActiveBorderThickness = 1;
            this.IniciarButton.ActiveCornerRadius = 20;
            this.IniciarButton.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(166)))), ((int)(((byte)(207)))));
            this.IniciarButton.ActiveForecolor = System.Drawing.Color.White;
            this.IniciarButton.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(166)))), ((int)(((byte)(207)))));
            this.IniciarButton.BackColor = System.Drawing.Color.White;
            this.IniciarButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("IniciarButton.BackgroundImage")));
            this.IniciarButton.ButtonText = "Iniciar";
            this.IniciarButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IniciarButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IniciarButton.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IniciarButton.ForeColor = System.Drawing.Color.SeaShell;
            this.IniciarButton.IdleBorderThickness = 1;
            this.IniciarButton.IdleCornerRadius = 20;
            this.IniciarButton.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.IniciarButton.IdleForecolor = System.Drawing.Color.White;
            this.IniciarButton.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.IniciarButton.Location = new System.Drawing.Point(7, 7);
            this.IniciarButton.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.IniciarButton.Name = "IniciarButton";
            this.IniciarButton.Size = new System.Drawing.Size(293, 58);
            this.IniciarButton.TabIndex = 0;
            this.IniciarButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MedicionButton
            // 
            this.MedicionButton.ActiveBorderThickness = 1;
            this.MedicionButton.ActiveCornerRadius = 20;
            this.MedicionButton.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(166)))), ((int)(((byte)(207)))));
            this.MedicionButton.ActiveForecolor = System.Drawing.Color.White;
            this.MedicionButton.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(166)))), ((int)(((byte)(207)))));
            this.MedicionButton.BackColor = System.Drawing.Color.White;
            this.MedicionButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MedicionButton.BackgroundImage")));
            this.MedicionButton.ButtonText = "Medicion";
            this.MedicionButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MedicionButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MedicionButton.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MedicionButton.ForeColor = System.Drawing.Color.SeaShell;
            this.MedicionButton.IdleBorderThickness = 1;
            this.MedicionButton.IdleCornerRadius = 20;
            this.MedicionButton.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.MedicionButton.IdleForecolor = System.Drawing.Color.White;
            this.MedicionButton.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.MedicionButton.Location = new System.Drawing.Point(314, 7);
            this.MedicionButton.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.MedicionButton.Name = "MedicionButton";
            this.MedicionButton.Size = new System.Drawing.Size(293, 58);
            this.MedicionButton.TabIndex = 1;
            this.MedicionButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CapturarButton
            // 
            this.CapturarButton.ActiveBorderThickness = 1;
            this.CapturarButton.ActiveCornerRadius = 20;
            this.CapturarButton.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(166)))), ((int)(((byte)(207)))));
            this.CapturarButton.ActiveForecolor = System.Drawing.Color.White;
            this.CapturarButton.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(166)))), ((int)(((byte)(207)))));
            this.CapturarButton.BackColor = System.Drawing.Color.White;
            this.CapturarButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CapturarButton.BackgroundImage")));
            this.CapturarButton.ButtonText = "Capturar Datos";
            this.CapturarButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CapturarButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CapturarButton.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CapturarButton.ForeColor = System.Drawing.Color.SeaShell;
            this.CapturarButton.IdleBorderThickness = 1;
            this.CapturarButton.IdleCornerRadius = 20;
            this.CapturarButton.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.CapturarButton.IdleForecolor = System.Drawing.Color.White;
            this.CapturarButton.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.CapturarButton.Location = new System.Drawing.Point(621, 7);
            this.CapturarButton.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.CapturarButton.Name = "CapturarButton";
            this.CapturarButton.Size = new System.Drawing.Size(293, 58);
            this.CapturarButton.TabIndex = 2;
            this.CapturarButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RepetirButton
            // 
            this.RepetirButton.ActiveBorderThickness = 1;
            this.RepetirButton.ActiveCornerRadius = 20;
            this.RepetirButton.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(166)))), ((int)(((byte)(207)))));
            this.RepetirButton.ActiveForecolor = System.Drawing.Color.White;
            this.RepetirButton.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(166)))), ((int)(((byte)(207)))));
            this.RepetirButton.BackColor = System.Drawing.Color.White;
            this.RepetirButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RepetirButton.BackgroundImage")));
            this.RepetirButton.ButtonText = "Repetir";
            this.RepetirButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RepetirButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RepetirButton.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RepetirButton.ForeColor = System.Drawing.Color.SeaShell;
            this.RepetirButton.IdleBorderThickness = 1;
            this.RepetirButton.IdleCornerRadius = 20;
            this.RepetirButton.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.RepetirButton.IdleForecolor = System.Drawing.Color.White;
            this.RepetirButton.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.RepetirButton.Location = new System.Drawing.Point(928, 7);
            this.RepetirButton.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.RepetirButton.Name = "RepetirButton";
            this.RepetirButton.Size = new System.Drawing.Size(293, 58);
            this.RepetirButton.TabIndex = 3;
            this.RepetirButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BottomLayout
            // 
            this.BottomLayout.ColumnCount = 2;
            this.BottomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.29642F));
            this.BottomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.70358F));
            this.BottomLayout.Controls.Add(this.DataGridInfo, 0, 0);
            this.BottomLayout.Controls.Add(this.GuardarLayout, 1, 0);
            this.BottomLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomLayout.Location = new System.Drawing.Point(28, 471);
            this.BottomLayout.Name = "BottomLayout";
            this.BottomLayout.RowCount = 1;
            this.BottomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BottomLayout.Size = new System.Drawing.Size(1228, 283);
            this.BottomLayout.TabIndex = 4;
            // 
            // DataGridInfo
            // 
            this.DataGridInfo.AllowUserToDeleteRows = false;
            this.DataGridInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DataGridInfo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(33)))), ((int)(((byte)(7)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Lectura,
            this.Grado,
            this.Temperatura,
            this.Volumen,
            this.Estado});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(33)))), ((int)(((byte)(7)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridInfo.GridColor = System.Drawing.Color.White;
            this.DataGridInfo.Location = new System.Drawing.Point(3, 3);
            this.DataGridInfo.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.DataGridInfo.Name = "DataGridInfo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(33)))), ((int)(((byte)(7)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridInfo.RowHeadersWidth = 51;
            this.DataGridInfo.RowTemplate.Height = 24;
            this.DataGridInfo.Size = new System.Drawing.Size(1066, 265);
            this.DataGridInfo.TabIndex = 0;
            // 
            // GuardarLayout
            // 
            this.GuardarLayout.ColumnCount = 1;
            this.GuardarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.GuardarLayout.Controls.Add(this.GuardarButton, 0, 1);
            this.GuardarLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GuardarLayout.Location = new System.Drawing.Point(1075, 3);
            this.GuardarLayout.Name = "GuardarLayout";
            this.GuardarLayout.RowCount = 3;
            this.GuardarLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.GuardarLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.GuardarLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.GuardarLayout.Size = new System.Drawing.Size(150, 277);
            this.GuardarLayout.TabIndex = 1;
            // 
            // GuardarButton
            // 
            this.GuardarButton.ActiveBorderThickness = 1;
            this.GuardarButton.ActiveCornerRadius = 20;
            this.GuardarButton.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(166)))), ((int)(((byte)(207)))));
            this.GuardarButton.ActiveForecolor = System.Drawing.SystemColors.Window;
            this.GuardarButton.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(166)))), ((int)(((byte)(207)))));
            this.GuardarButton.BackColor = System.Drawing.Color.White;
            this.GuardarButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GuardarButton.BackgroundImage")));
            this.GuardarButton.ButtonText = "Guardar";
            this.GuardarButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GuardarButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GuardarButton.Font = new System.Drawing.Font("Century Gothic", 16.2F);
            this.GuardarButton.ForeColor = System.Drawing.Color.SeaShell;
            this.GuardarButton.IdleBorderThickness = 1;
            this.GuardarButton.IdleCornerRadius = 20;
            this.GuardarButton.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.GuardarButton.IdleForecolor = System.Drawing.Color.White;
            this.GuardarButton.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.GuardarButton.Location = new System.Drawing.Point(7, 99);
            this.GuardarButton.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.GuardarButton.Name = "GuardarButton";
            this.GuardarButton.Size = new System.Drawing.Size(136, 78);
            this.GuardarButton.TabIndex = 0;
            this.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lectura
            // 
            this.Lectura.HeaderText = "Lectura";
            this.Lectura.MinimumWidth = 6;
            this.Lectura.Name = "Lectura";
            // 
            // Grado
            // 
            this.Grado.HeaderText = "Grado";
            this.Grado.MinimumWidth = 6;
            this.Grado.Name = "Grado";
            // 
            // Temperatura
            // 
            this.Temperatura.HeaderText = "Temperatura";
            this.Temperatura.MinimumWidth = 6;
            this.Temperatura.Name = "Temperatura";
            // 
            // Volumen
            // 
            this.Volumen.HeaderText = "Volumen";
            this.Volumen.MinimumWidth = 6;
            this.Volumen.Name = "Volumen";
            // 
            // Estado
            // 
            this.Estado.HeaderText = "Estado";
            this.Estado.MinimumWidth = 6;
            this.Estado.Name = "Estado";
            // 
            // Trabajador_Vista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1286, 757);
            this.Controls.Add(this.MainLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Trabajador_Vista";
            this.Text = "Trabajador";
            this.Load += new System.EventHandler(this.Load_Refracto);
            this.MainLayout.ResumeLayout(false);
            this.EstadoLayout.ResumeLayout(false);
            this.SecondLayout.ResumeLayout(false);
            this.BotonesLayout.ResumeLayout(false);
            this.BottomLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridInfo)).EndInit();
            this.GuardarLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.Label TittleLabel;
        private System.Windows.Forms.TableLayoutPanel EstadoLayout;
        private System.Windows.Forms.Label EstadoLabel;
        private System.Windows.Forms.Label EstadoText;
        private System.Windows.Forms.TableLayoutPanel SecondLayout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LecturaText;
        private System.Windows.Forms.Label TicketText;
        private System.Windows.Forms.Label TicketLabel;
        private System.Windows.Forms.TableLayoutPanel BotonesLayout;
        private Bunifu.Framework.UI.BunifuThinButton2 IniciarButton;
        private Bunifu.Framework.UI.BunifuThinButton2 MedicionButton;
        private Bunifu.Framework.UI.BunifuThinButton2 RepetirButton;
        private Bunifu.Framework.UI.BunifuThinButton2 CapturarButton;
        private System.Windows.Forms.TableLayoutPanel BottomLayout;
        private System.Windows.Forms.DataGridView DataGridInfo;
        private System.Windows.Forms.TableLayoutPanel GuardarLayout;
        private Bunifu.Framework.UI.BunifuThinButton2 GuardarButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lectura;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Temperatura;
        private System.Windows.Forms.DataGridViewTextBoxColumn Volumen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
    }
}