namespace SearchApp;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    private TextBox txtKeyword;
    private Button btnSearch;
    private TextBox txtBaiduResult;
    private TextBox txtBingResult;
    private Label lblKeyword;
    private Label lblBaidu;
    private Label lblBing;
    private Label lblStatus;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.lblKeyword = new Label();
        this.txtKeyword = new TextBox();
        this.btnSearch = new Button();
        this.lblBaidu = new Label();
        this.txtBaiduResult = new TextBox();
        this.lblBing = new Label();
        this.txtBingResult = new TextBox();
        this.lblStatus = new Label();
        this.SuspendLayout();

        // lblKeyword
        this.lblKeyword.AutoSize = true;
        this.lblKeyword.Location = new Point(20, 20);
        this.lblKeyword.Text = "请输入关键字:";

        // txtKeyword
        this.txtKeyword.Location = new Point(20, 45);
        this.txtKeyword.Size = new Size(400, 27);
        this.txtKeyword.KeyDown += txtKeyword_KeyDown;

        // btnSearch
        this.btnSearch.Location = new Point(430, 44);
        this.btnSearch.Size = new Size(100, 30);
        this.btnSearch.Text = "搜索";
        this.btnSearch.Click += btnSearch_Click;

        // lblBaidu
        this.lblBaidu.AutoSize = true;
        this.lblBaidu.Location = new Point(20, 90);
        this.lblBaidu.Text = "百度搜索结果 (前200字):";

        // txtBaiduResult
        this.txtBaiduResult.Location = new Point(20, 115);
        this.txtBaiduResult.Multiline = true;
        this.txtBaiduResult.ReadOnly = true;
        this.txtBaiduResult.ScrollBars = ScrollBars.Vertical;
        this.txtBaiduResult.Size = new Size(845, 220);

        // lblBing
        this.lblBing.AutoSize = true;
        this.lblBing.Location = new Point(20, 350);
        this.lblBing.Text = "Bing搜索结果 (前200字):";

        // txtBingResult
        this.txtBingResult.Location = new Point(20, 375);
        this.txtBingResult.Multiline = true;
        this.txtBingResult.ReadOnly = true;
        this.txtBingResult.ScrollBars = ScrollBars.Vertical;
        this.txtBingResult.Size = new Size(845, 220);

        // lblStatus
        this.lblStatus.AutoSize = true;
        this.lblStatus.Location = new Point(20, 605);
        this.lblStatus.Text = "就绪";

        // Form1
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(892, 640);
        this.Controls.Add(this.lblStatus);
        this.Controls.Add(this.txtBingResult);
        this.Controls.Add(this.lblBing);
        this.Controls.Add(this.txtBaiduResult);
        this.Controls.Add(this.lblBaidu);
        this.Controls.Add(this.btnSearch);
        this.Controls.Add(this.txtKeyword);
        this.Controls.Add(this.lblKeyword);
        this.Text = "多引擎搜索工具";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
