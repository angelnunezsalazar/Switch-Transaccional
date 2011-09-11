namespace PocketPC.Transferencias
{
    partial class CuentaTerceros
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.cmbCuentaCargo = new System.Windows.Forms.ComboBox();
            this.cmbTipoCuenta = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCompra = new System.Windows.Forms.Label();
            this.lblVenta = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMoneda = new System.Windows.Forms.ComboBox();
            this.txtCuentaAbono = new System.Windows.Forms.TextBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem5);
            this.menuItem1.Text = "Pagos";
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "Recarga Telefónicas";
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.menuItem3);
            this.menuItem2.MenuItems.Add(this.menuItem4);
            this.menuItem2.Text = "Transferencias";
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Cuentas de Terceros";
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "Cuentas Otros Bancos";
            // 
            // cmbCuentaCargo
            // 
            this.cmbCuentaCargo.Location = new System.Drawing.Point(11, 66);
            this.cmbCuentaCargo.Name = "cmbCuentaCargo";
            this.cmbCuentaCargo.Size = new System.Drawing.Size(214, 22);
            this.cmbCuentaCargo.TabIndex = 0;
            // 
            // cmbTipoCuenta
            // 
            this.cmbTipoCuenta.Location = new System.Drawing.Point(89, 161);
            this.cmbTipoCuenta.Name = "cmbTipoCuenta";
            this.cmbTipoCuenta.Size = new System.Drawing.Size(136, 22);
            this.cmbTipoCuenta.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.Text = "Tipo Cuenta";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.Text = "Importe";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Cuenta de Cargo";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 20);
            this.button1.TabIndex = 7;
            this.button1.Text = "Pagar";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 20);
            this.label5.Text = "Compra S/.";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(124, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 20);
            this.label6.Text = "Venta S/.";
            // 
            // lblCompra
            // 
            this.lblCompra.Location = new System.Drawing.Point(79, 12);
            this.lblCompra.Name = "lblCompra";
            this.lblCompra.Size = new System.Drawing.Size(39, 20);
            this.lblCompra.Text = "2.850";
            // 
            // lblVenta
            // 
            this.lblVenta.Location = new System.Drawing.Point(180, 12);
            this.lblVenta.Name = "lblVenta";
            this.lblVenta.Size = new System.Drawing.Size(39, 20);
            this.lblVenta.Text = "2.930";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(11, 189);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 19);
            this.label9.Text = "Cuenta Abono";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.Text = "Moneda";
            // 
            // cmbMoneda
            // 
            this.cmbMoneda.Location = new System.Drawing.Point(89, 123);
            this.cmbMoneda.Name = "cmbMoneda";
            this.cmbMoneda.Size = new System.Drawing.Size(136, 22);
            this.cmbMoneda.TabIndex = 16;
            // 
            // txtCuentaAbono
            // 
            this.txtCuentaAbono.Location = new System.Drawing.Point(11, 208);
            this.txtCuentaAbono.Name = "txtCuentaAbono";
            this.txtCuentaAbono.Size = new System.Drawing.Size(212, 21);
            this.txtCuentaAbono.TabIndex = 25;
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(89, 96);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(136, 21);
            this.txtImporte.TabIndex = 26;
            // 
            // CuentaTerceros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.txtCuentaAbono);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbMoneda);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblVenta);
            this.Controls.Add(this.lblCompra);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbTipoCuenta);
            this.Controls.Add(this.cmbCuentaCargo);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "CuentaTerceros";
            this.Text = "Recarga Telefónica";
            this.Load += new System.EventHandler(this.CuentaTerceros_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.ComboBox cmbCuentaCargo;
        private System.Windows.Forms.ComboBox cmbTipoCuenta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCompra;
        private System.Windows.Forms.Label lblVenta;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbMoneda;
        private System.Windows.Forms.TextBox txtCuentaAbono;
        private System.Windows.Forms.TextBox txtImporte;
    }
}

