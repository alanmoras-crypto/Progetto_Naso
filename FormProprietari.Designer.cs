namespace Progetto_Naso
{
    partial class FormProprietari
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
            dataGridView1 = new DataGridView();
            Back = new Button();
            Salva = new Button();
            Cancella = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 32);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(585, 406);
            dataGridView1.TabIndex = 0;
            // 
            // Back
            // 
            Back.Location = new Point(695, 12);
            Back.Name = "Back";
            Back.Size = new Size(93, 32);
            Back.TabIndex = 4;
            Back.Text = "Back";
            Back.UseVisualStyleBackColor = true;
            Back.Click += Back_Click;
            // 
            // Salva
            // 
            Salva.Location = new Point(702, 412);
            Salva.Name = "Salva";
            Salva.Size = new Size(86, 26);
            Salva.TabIndex = 5;
            Salva.Text = "SALVA";
            Salva.UseVisualStyleBackColor = true;
            Salva.Click += Salva_Click;
            // 
            // Cancella
            // 
            Cancella.Location = new Point(603, 412);
            Cancella.Name = "Cancella";
            Cancella.Size = new Size(93, 26);
            Cancella.TabIndex = 6;
            Cancella.Text = "CANCELLA";
            Cancella.UseVisualStyleBackColor = true;
            Cancella.Click += Cancella_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(79, 20);
            label1.TabIndex = 7;
            label1.Text = "Proprietari";
            // 
            // FormProprietari
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(Cancella);
            Controls.Add(Salva);
            Controls.Add(Back);
            Controls.Add(dataGridView1);
            Name = "FormProprietari";
            Text = "FormProprietari";
            Load += FormProprietari_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button Back;
        private Button Salva;
        private Button Cancella;
        private Label label1;
    }
}