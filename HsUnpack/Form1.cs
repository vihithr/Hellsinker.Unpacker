using System.Globalization;

namespace HsUnpack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeLanguageSelector();
        }

        private void InitializeLanguageSelector()
        {
            // 添加语言选项
            comboBoxLanguage.Items.Add("中文");
            comboBoxLanguage.Items.Add("English");
            comboBoxLanguage.Items.Add("日本語");

            // 根据当前系统语言设置默认选项
            var currentCulture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            comboBoxLanguage.SelectedIndex = currentCulture switch
            {
                "zh" => 0,
                "en" => 1,
                "ja" => 2,
                _ => 0 // 默认中文
            };
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cultureName = comboBoxLanguage.SelectedIndex switch
            {
                0 => "zh-CN",
                1 => "en",
                2 => "ja",
                _ => "zh-CN"
            };

            // 设置新语言
            var culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;

            // 重新加载窗体
            RefreshUI();
        }

        private void RefreshUI()
        {
            // 更新所有文本
            this.Text = Properties.Resources.FormTitle;
            button1.Text = Properties.Resources.ButtonSelectFile;
            button2.Text = Properties.Resources.ButtonUnpack;
            button3.Text = Properties.Resources.ButtonSelectFile;
            button4.Text = Properties.Resources.ButtonPack;
            button5.Text = Properties.Resources.ButtonSelectFile;
            button6.Text = Properties.Resources.ButtonDecryptCtx;
            button7.Text = Properties.Resources.ButtonSelectFile;
            button8.Text = Properties.Resources.ButtonEncryptCtx;
            label1.Text = Properties.Resources.LabelUnpackFile;
            label2.Text = Properties.Resources.LabelPackConfig;
            label3.Text = Properties.Resources.LabelCtxDecrypt;
            label4.Text = Properties.Resources.LabelCtxEncrypt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filepath = OpenFile();
            if (filepath != "")
            {
                textBox1.Text = filepath;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            HsDataProgress.UnpackDoProgress(textBox1.Text);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string filepath = OpenFile();
            if (filepath != "")
            {
                textBox2.Text = filepath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HsDataProgress.PackDoProgress(textBox2.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string filepath = OpenFile();
            if (filepath != "")
            {
                textBox3.Text = filepath;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show(Properties.Resources.MessageSelectFile, Properties.Resources.MessageError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(textBox3.Text))
                {
                    MessageBox.Show(Properties.Resources.MessageFileNotFound, Properties.Resources.MessageError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 解密CTX文件
                string outputPath = CtxCrypto.DecryptFile(textBox3.Text);
                MessageBox.Show($"{Properties.Resources.MessageDecryptSuccess}\n{outputPath}", Properties.Resources.MessageSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Properties.Resources.MessageDecryptFailed}\n{ex.Message}", Properties.Resources.MessageError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string filepath = OpenFile();
            if (filepath != "")
            {
                textBox4.Text = filepath;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox4.Text))
                {
                    MessageBox.Show(Properties.Resources.MessageSelectFile, Properties.Resources.MessageError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(textBox4.Text))
                {
                    MessageBox.Show(Properties.Resources.MessageFileNotFound, Properties.Resources.MessageError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 加密文件为CTX格式
                string outputPath = CtxCrypto.EncryptFile(textBox4.Text);
                MessageBox.Show($"{Properties.Resources.MessageEncryptSuccess}\n{outputPath}", Properties.Resources.MessageSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Properties.Resources.MessageEncryptFailed}\n{ex.Message}", Properties.Resources.MessageError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 打开文件选择对话框，返回用户选择的文件路径（如果取消则返回空字符串）。
        /// </summary>
        public string OpenFile()
        {
            // 创建 OpenFileDialog 实例
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // 设置对话框标题
                openFileDialog.Title = Properties.Resources.DialogTitleOpen;

                // 设置初始目录（可选）
                try
                {
                    if (textBox1.Text == null)
                        openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    else
                        openFileDialog.InitialDirectory = Path.GetDirectoryName(textBox1.Text);
                }
                catch
                {
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }


                // 设置文件过滤器（例如只允许选择 .txt 文件）
                openFileDialog.Filter = Properties.Resources.FileFilterAll;

                // 允许多选（可选）
                openFileDialog.Multiselect = false;

                // 显示对话框，并检查用户是否点击了“确定”
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 返回选择的文件路径
                    return openFileDialog.FileName;
                }
                else
                {
                    // 用户取消选择，返回空字符串
                    return string.Empty;
                }
            }
        }


    }
}
