using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace FileMergeApp
{
    // 定义窗体类
    public class MainForm : Form
    {
        private TextBox txtPath1;
        private TextBox txtPath2;
        private Button btnSelect1;
        private Button btnSelect2;
        private Button btnMerge;

        public MainForm()
        {
            // 设置窗体基本属性
            this.Text = "文件合并工具";
            this.Size = new Size(500, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 初始化控件
            txtPath1 = new TextBox { Location = new Point(20, 30), Width = 300 };
            btnSelect1 = new Button { Text = "选择文件1", Location = new Point(340, 28) };

            txtPath2 = new TextBox { Location = new Point(20, 80), Width = 300 };
            btnSelect2 = new Button { Text = "选择文件2", Location = new Point(340, 78) };

            btnMerge = new Button
            {
                Text = "合并文件并保存到 Data 目录",
                Location = new Point(20, 130),
                Width = 400,
                Height = 40
            };

            // 绑定事件
            btnSelect1.Click += (s, e) => SelectFile(txtPath1);
            btnSelect2.Click += (s, e) => SelectFile(txtPath2);
            btnMerge.Click += BtnMerge_Click;

            // 将控件添加到窗体
            this.Controls.Add(txtPath1);
            this.Controls.Add(btnSelect1);
            this.Controls.Add(txtPath2);
            this.Controls.Add(btnSelect2);
            this.Controls.Add(btnMerge);
        }

        private void SelectFile(TextBox target)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "文本文件|*.txt|所有文件|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    target.Text = ofd.FileName;
                }
            }
        }

        private void BtnMerge_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath1.Text) || string.IsNullOrEmpty(txtPath2.Text))
            {
                MessageBox.Show("请先选择两个文件！");
                return;
            }

            try
            {
                // 获取程序运行目录下的 Data 文件夹
                string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);

                // 生成新文件名
                string newFileName = $"Merged_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string destPath = Path.Combine(dataDir, newFileName);

                // 读取并合并
                string content1 = File.ReadAllText(txtPath1.Text);
                string content2 = File.ReadAllText(txtPath2.Text);

                File.WriteAllText(destPath, content1 + Environment.NewLine + content2);

                MessageBox.Show($"合并成功！\n保存位置：{destPath}", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败: {ex.Message}", "错误");
            }
        }

        // 程序入口点
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}