using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WebExtractorApp
{
    // 确保这里继承了 Form。
    // 如果你是在 VS 默认模板里改的，请删除 Form1.Designer.cs 文件，
    // 或者将下面的 partial 去掉，做成一个独立的类。
    public class Form1 : Form
    {
        private TextBox txtUrl;
        private Button btnStart;
        private ListBox lstPhones;
        private ListBox lstEmails;

        public Form1()
        {
            // 窗口基础设置
            this.Text = "正则表达式网页提取器";
            this.Size = new Size(540, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            SetupLayout();
        }

        private void SetupLayout()
        {
            txtUrl = new TextBox { Location = new Point(20, 20), Size = new Size(380, 25), Text = "https://www.baidu.com" };
            btnStart = new Button { Text = "开始提取", Location = new Point(410, 18), Size = new Size(85, 30) };

            Label lblP = new Label { Text = "手机号:", Location = new Point(20, 60), AutoSize = true };
            lstPhones = new ListBox { Location = new Point(20, 85), Size = new Size(230, 300) };

            Label lblE = new Label { Text = "邮箱:", Location = new Point(260, 60), AutoSize = true };
            lstEmails = new ListBox { Location = new Point(260, 85), Size = new Size(230, 300) };

            btnStart.Click += async (s, e) => await StartExtracting();

            this.Controls.AddRange(new Control[] { txtUrl, btnStart, lblP, lstPhones, lblE, lstEmails });
        }

        private async Task StartExtracting()
        {
            string url = txtUrl.Text.Trim();
            if (string.IsNullOrEmpty(url)) return;

            try
            {
                btnStart.Enabled = false;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                    string html = await client.GetStringAsync(url);

                    // 填充数据
                    MatchAndFill(html, @"1[3-9]\d{9}", lstPhones);
                    MatchAndFill(html, @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}", lstEmails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("抓取失败: " + ex.Message);
            }
            finally
            {
                btnStart.Enabled = true;
            }
        }

        private void MatchAndFill(string content, string pattern, ListBox lb)
        {
            lb.Items.Clear();
            HashSet<string> results = new HashSet<string>();
            foreach (Match m in Regex.Matches(content, pattern))
            {
                if (results.Add(m.Value)) lb.Items.Add(m.Value);
            }
        }

        // 如果你不需要 Designer 自动生成的 Dispose，可以不用重写它。
        // 纯代码环境下，基础的 Form 会自动处理资源释放。
    }
}