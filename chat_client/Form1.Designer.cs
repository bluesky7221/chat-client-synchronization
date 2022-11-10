namespace chat_client
{
    partial class ChatClientForm
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
            this.Name_label = new System.Windows.Forms.Label();
            this.Name_textBox = new System.Windows.Forms.TextBox();
            this.Enter_Btn = new System.Windows.Forms.Button();
            this.ChatBox = new System.Windows.Forms.TextBox();
            this.Chat_EnterBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Name_label
            // 
            this.Name_label.AutoSize = true;
            this.Name_label.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name_label.Location = new System.Drawing.Point(12, 25);
            this.Name_label.Name = "Name_label";
            this.Name_label.Size = new System.Drawing.Size(51, 19);
            this.Name_label.TabIndex = 0;
            this.Name_label.Text = "대화명";
            // 
            // Name_textBox
            // 
            this.Name_textBox.Location = new System.Drawing.Point(69, 23);
            this.Name_textBox.Name = "Name_textBox";
            this.Name_textBox.Size = new System.Drawing.Size(189, 23);
            this.Name_textBox.TabIndex = 1;
            // 
            // Enter_Btn
            // 
            this.Enter_Btn.Location = new System.Drawing.Point(310, 23);
            this.Enter_Btn.Name = "Enter_Btn";
            this.Enter_Btn.Size = new System.Drawing.Size(162, 23);
            this.Enter_Btn.TabIndex = 2;
            this.Enter_Btn.Text = "입장";
            this.Enter_Btn.UseVisualStyleBackColor = true;
            this.Enter_Btn.Click += new System.EventHandler(this.Enter_Btn_Click);
            // 
            // ChatBox
            // 
            this.ChatBox.Location = new System.Drawing.Point(12, 68);
            this.ChatBox.Multiline = true;
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.Size = new System.Drawing.Size(460, 352);
            this.ChatBox.TabIndex = 3;
            // 
            // Chat_EnterBox
            // 
            this.Chat_EnterBox.Location = new System.Drawing.Point(12, 426);
            this.Chat_EnterBox.Name = "Chat_EnterBox";
            this.Chat_EnterBox.Size = new System.Drawing.Size(460, 23);
            this.Chat_EnterBox.TabIndex = 4;
            this.Chat_EnterBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Chat_EnterBox_KeyPress);
            // 
            // ChatClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.Chat_EnterBox);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.Enter_Btn);
            this.Controls.Add(this.Name_textBox);
            this.Controls.Add(this.Name_label);
            this.Name = "ChatClientForm";
            this.Text = "Chat Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label Name_label;
        private TextBox Name_textBox;
        private Button Enter_Btn;
        private TextBox ChatBox;
        private TextBox Chat_EnterBox;
    }
}