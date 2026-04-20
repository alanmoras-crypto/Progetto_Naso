namespace Progetto_Naso
{
    partial class AggiungiMacchina
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
            Back = new Button();
            checkedListBox1 = new CheckedListBox();
            textBox1 = new TextBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            AGGIUNGI = new Button();
            MacchineVisual = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)MacchineVisual).BeginInit();
            SuspendLayout();
            // 
            // Back
            // 
            Back.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Back.Location = new Point(751, 6);
            Back.Name = "Back";
            Back.Size = new Size(85, 32);
            Back.TabIndex = 9;
            Back.Text = "BACK";
            Back.UseVisualStyleBackColor = true;
            Back.Click += Back_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(12, 48);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(159, 202);
            checkedListBox1.TabIndex = 10;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(199, 48);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(140, 58);
            textBox1.TabIndex = 11;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(199, 157);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(140, 23);
            comboBox1.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(68, 20);
            label1.TabIndex = 13;
            label1.Text = "SENSORI";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(199, 9);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 14;
            label2.Text = "NOTE";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(199, 122);
            label3.Name = "label3";
            label3.Size = new Size(96, 20);
            label3.TabIndex = 15;
            label3.Text = "PROPRIETARI";
            // 
            // AGGIUNGI
            // 
            AGGIUNGI.Location = new Point(199, 219);
            AGGIUNGI.Name = "AGGIUNGI";
            AGGIUNGI.Size = new Size(80, 31);
            AGGIUNGI.TabIndex = 16;
            AGGIUNGI.Text = "AGGIUNGI";
            AGGIUNGI.UseVisualStyleBackColor = true;
            AGGIUNGI.Click += AGGIUNGI_Click;
            // 
            // MacchineVisual
            // 
            MacchineVisual.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MacchineVisual.Location = new Point(3, 296);
            MacchineVisual.Name = "MacchineVisual";
            MacchineVisual.Size = new Size(833, 186);
            MacchineVisual.TabIndex = 17;
            // 
            // AggiungiMacchina
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(848, 494);
            Controls.Add(MacchineVisual);
            Controls.Add(AGGIUNGI);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(textBox1);
            Controls.Add(checkedListBox1);
            Controls.Add(Back);
            Name = "AggiungiMacchina";
            Text = "AggiungiMacchina";
            Load += AggiungiMacchina_Load;
            ((System.ComponentModel.ISupportInitialize)MacchineVisual).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Back;
        private CheckedListBox checkedListBox1;
        private TextBox textBox1;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button AGGIUNGI;
        private DataGridView MacchineVisual;
    }
}