namespace HsUnpack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
        /// <summary>
        /// 打开文件选择对话框，返回用户选择的文件路径（如果取消则返回空字符串）。
        /// </summary>
        public string OpenFile()
        {
            // 创建 OpenFileDialog 实例
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // 设置对话框标题
                openFileDialog.Title = "打开";

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
                openFileDialog.Filter = "所有文件 (*.*)|*.*";

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
