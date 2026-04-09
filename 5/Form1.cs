using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace _5
{
    // partial 关键字允许类分布在多个文件中
    public partial class Form1 : Form
    {
        private string expression = "";
        private TextBox txtDisplay;

        // 【唯一】的构造函数
        public Form1()
        {
            // 所有的初始化逻辑都写在这里调用的方法里
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "简易计算器";
            this.Size = new Size(320, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 初始化文本框
            txtDisplay = new TextBox();
            txtDisplay.Font = new Font("Arial", 18);
            txtDisplay.Dock = DockStyle.Top;
            txtDisplay.TextAlign = HorizontalAlignment.Right;
            txtDisplay.ReadOnly = true;
            this.Controls.Add(txtDisplay);

            // 定义按钮及其布局
            string[] buttons = { "7", "8", "9", "/", "4", "5", "6", "*", "1", "2", "3", "-", "C", "0", "=", "+" };
            int btnWidth = 65, btnHeight = 60;

            for (int i = 0; i < buttons.Length; i++)
            {
                Button btn = new Button();
                btn.Text = buttons[i];
                btn.Size = new Size(btnWidth, btnHeight);
                btn.Font = new Font("Arial", 12, FontStyle.Bold);

                int row = i / 4;
                int col = i % 4;
                btn.Location = new Point(15 + col * (btnWidth + 5), 70 + row * (btnHeight + 5));

                // 绑定对应的事件
                if (btn.Text == "=") btn.Click += btnEqual_Click;
                else if (btn.Text == "C") btn.Click += btnClear_Click;
                else btn.Click += Button_Click;

                this.Controls.Add(btn);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (txtDisplay.Text.Contains("=")) txtDisplay.Text = "";
            expression += btn.Text;
            txtDisplay.Text = expression;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            expression = "";
            txtDisplay.Text = "";
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                var result = dt.Compute(expression, "");
                txtDisplay.Text = $"{expression}={result}";
                expression = result.ToString();
            }
            catch { txtDisplay.Text = "错误"; expression = ""; }
        }
    }
}