namespace HsUnpack
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            button2 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            button3 = new Button();
            button4 = new Button();
            comboBoxLanguage = new ComboBox();
            labelLanguage = new Label();
            label3 = new Label();
            textBox3 = new TextBox();
            button5 = new Button();
            button6 = new Button();
            label4 = new Label();
            textBox4 = new TextBox();
            button7 = new Button();
            button8 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(818, 90);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(136, 41);
            button1.TabIndex = 0;
            button1.Text = Properties.Resources.ButtonSelectFile;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(818, 151);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(136, 41);
            button2.TabIndex = 1;
            button2.Text = Properties.Resources.ButtonUnpack;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(33, 97);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(744, 34);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(33, 242);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(744, 34);
            textBox2.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 52);
            label1.Name = "label1";
            label1.Size = new Size(138, 28);
            label1.TabIndex = 4;
            label1.Text = "解包文件选择";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 189);
            label2.Name = "label2";
            label2.Size = new Size(180, 28);
            label2.TabIndex = 5;
            label2.Text = "封包配置文件选择";
            // 
            // button3
            // 
            button3.Location = new Point(818, 239);
            button3.Name = "button3";
            button3.Size = new Size(136, 40);
            button3.TabIndex = 6;
            button3.Text = Properties.Resources.ButtonSelectFile;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(818, 296);
            button4.Name = "button4";
            button4.Size = new Size(136, 40);
            button4.TabIndex = 7;
            button4.Text = Properties.Resources.ButtonPack;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // comboBoxLanguage
            // 
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLanguage.FormattingEnabled = true;
            comboBoxLanguage.Location = new Point(818, 742);
            comboBoxLanguage.Name = "comboBoxLanguage";
            comboBoxLanguage.Size = new Size(136, 36);
            comboBoxLanguage.TabIndex = 8;
            comboBoxLanguage.SelectedIndexChanged += comboBoxLanguage_SelectedIndexChanged;
            // 
            // labelLanguage
            // 
            labelLanguage.AutoSize = true;
            labelLanguage.Location = new Point(666, 742);
            labelLanguage.Name = "labelLanguage";
            labelLanguage.Size = new Size(111, 28);
            labelLanguage.TabIndex = 9;
            labelLanguage.Text = "Language";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 348);
            label3.Name = "label3";
            label3.Size = new Size(136, 28);
            label3.TabIndex = 10;
            label3.Text = "CTX文件解密";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(33, 396);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(744, 34);
            textBox3.TabIndex = 11;
            // 
            // button5
            // 
            button5.Location = new Point(818, 396);
            button5.Name = "button5";
            button5.Size = new Size(136, 40);
            button5.TabIndex = 12;
            button5.Text = Properties.Resources.ButtonSelectFile;
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(818, 469);
            button6.Name = "button6";
            button6.Size = new Size(136, 40);
            button6.TabIndex = 13;
            button6.Text = "解密CTX";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(33, 510);
            label4.Name = "label4";
            label4.Size = new Size(136, 28);
            label4.TabIndex = 14;
            label4.Text = "CTX文件加密";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(33, 561);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(744, 34);
            textBox4.TabIndex = 15;
            // 
            // button7
            // 
            button7.Location = new Point(818, 561);
            button7.Name = "button7";
            button7.Size = new Size(136, 40);
            button7.TabIndex = 16;
            button7.Text = Properties.Resources.ButtonSelectFile;
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Location = new Point(818, 639);
            button8.Name = "button8";
            button8.Size = new Size(136, 40);
            button8.TabIndex = 17;
            button8.Text = "加密CTX";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1036, 810);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(textBox4);
            Controls.Add(label4);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(labelLanguage);
            Controls.Add(comboBoxLanguage);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            DoubleBuffered = true;
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Hs解包Beta";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
        private Button button3;
        private Button button4;
        private ComboBox comboBoxLanguage;
        private Label labelLanguage;
        private Label label3;
        private TextBox textBox3;
        private Button button5;
        private Button button6;
        private Label label4;
        private TextBox textBox4;
        private Button button7;
        private Button button8;
    }
}
