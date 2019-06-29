namespace App
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.output = new System.Windows.Forms.RichTextBox();
            this.input = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.rooms = new System.Windows.Forms.RichTextBox();
            this.users = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // output
            // 
            this.output.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.output.Location = new System.Drawing.Point(9, 10);
            this.output.Name = "output";
            this.output.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.output.Size = new System.Drawing.Size(586, 440);
            this.output.TabIndex = 0;
            this.output.Text = "";
            // 
            // input
            // 
            this.input.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.input.Location = new System.Drawing.Point(8, 493);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(445, 20);
            this.input.TabIndex = 1;
            // 
            // sendButton
            // 
            this.sendButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sendButton.Location = new System.Drawing.Point(471, 493);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(123, 19);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // rooms
            // 
            this.rooms.Location = new System.Drawing.Point(607, 42);
            this.rooms.Name = "rooms";
            this.rooms.Size = new System.Drawing.Size(154, 170);
            this.rooms.TabIndex = 3;
            this.rooms.Text = "";
            // 
            // users
            // 
            this.users.Location = new System.Drawing.Point(607, 247);
            this.users.Name = "users";
            this.users.Size = new System.Drawing.Size(153, 202);
            this.users.TabIndex = 4;
            this.users.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(609, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Rooms";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(610, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Users";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 529);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.users);
            this.Controls.Add(this.rooms);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.input);
            this.Controls.Add(this.output);
            this.Name = "Form1";
            this.Text = "UDP Multicast Chat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.RichTextBox rooms;
        private System.Windows.Forms.RichTextBox users;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

