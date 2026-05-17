using System.Net;
using System.Text.RegularExpressions;

namespace SearchApp;

public partial class Form1 : Form
{
    private static readonly HttpClient _httpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(15)
    };

    static Form1()
    {
        _httpClient.DefaultRequestHeaders.Add("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
    }

    public Form1()
    {
        InitializeComponent();
    }

    private void txtKeyword_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            btnSearch.PerformClick();
    }

    private async void btnSearch_Click(object? sender, EventArgs e)
    {
        string keyword = txtKeyword.Text.Trim();
        if (string.IsNullOrEmpty(keyword))
        {
            MessageBox.Show("请输入搜索关键字。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        btnSearch.Enabled = false;
        txtBaiduResult.Text = "";
        txtBingResult.Text = "";
        lblStatus.Text = "正在并行搜索中...";

        var baiduTask = SearchBaiduAsync(keyword);
        var bingTask = SearchBingAsync(keyword);

        await Task.WhenAll(baiduTask, bingTask);

        txtBaiduResult.Text = baiduTask.Result;
        txtBingResult.Text = bingTask.Result;
        lblStatus.Text = "搜索完成";
        btnSearch.Enabled = true;
    }

    private async Task<string> SearchBaiduAsync(string keyword)
    {
        try
        {
            string url = $"https://www.baidu.com/s?wd={Uri.EscapeDataString(keyword)}";
            string html = await _httpClient.GetStringAsync(url);
            return ExtractText(html, 200);
        }
        catch (Exception ex)
        {
            return $"搜索失败: {ex.Message}";
        }
    }

    private async Task<string> SearchBingAsync(string keyword)
    {
        try
        {
            string url = $"https://www.bing.com/search?q={Uri.EscapeDataString(keyword)}";
            string html = await _httpClient.GetStringAsync(url);
            return ExtractText(html, 200);
        }
        catch (Exception ex)
        {
            return $"搜索失败: {ex.Message}";
        }
    }

    private string ExtractText(string html, int maxLength)
    {
        html = Regex.Replace(html, @"<script[^>]*>[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
        html = Regex.Replace(html, @"<style[^>]*>[\s\S]*?</style>", "", RegexOptions.IgnoreCase);
        html = Regex.Replace(html, @"<[^>]+>", " ");
        html = WebUtility.HtmlDecode(html);
        html = Regex.Replace(html, @"\s+", " ").Trim();

        return html.Length > maxLength ? html[..maxLength] : html;
    }
}
