namespace VocabApp;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    private Label lblPrompt;
    private Label lblChinese;
    private Label lblInput;
    private TextBox txtEnglish;
    private Label lblResult;
    private Button btnNext;
    private Label lblProgress;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.lblPrompt = new Label();
        this.lblChinese = new Label();
        this.lblInput = new Label();
        this.txtEnglish = new TextBox();
        this.lblResult = new Label();
        this.btnNext = new Button();
        this.lblProgress = new Label();
        this.SuspendLayout();

        // lblPrompt
        this.lblPrompt.AutoSize = true;
        this.lblPrompt.Location = new Point(30, 30);
        this.lblPrompt.Text = "中文词义:";
        this.lblPrompt.Font = new Font("Microsoft YaHei UI", 11F);

        // lblChinese
        this.lblChinese.AutoSize = true;
        this.lblChinese.Location = new Point(30, 70);
        this.lblChinese.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
        this.lblChinese.Text = "—";
        this.lblChinese.Size = new Size(400, 40);

        // lblInput
        this.lblInput.AutoSize = true;
        this.lblInput.Location = new Point(30, 140);
        this.lblInput.Text = "请输入对应的英文单词:";
        this.lblInput.Font = new Font("Microsoft YaHei UI", 10F);

        // txtEnglish
        this.txtEnglish.Location = new Point(30, 170);
        this.txtEnglish.Size = new Size(250, 27);
        this.txtEnglish.Font = new Font("Microsoft YaHei UI", 11F);
        this.txtEnglish.KeyDown += txtEnglish_KeyDown;

        // lblResult
        this.lblResult.AutoSize = true;
        this.lblResult.Location = new Point(30, 215);
        this.lblResult.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
        this.lblResult.Text = "";
        this.lblResult.Size = new Size(200, 35);

        // btnNext
        this.btnNext.Location = new Point(300, 169);
        this.btnNext.Size = new Size(100, 30);
        this.btnNext.Text = "下一个 >>";
        this.btnNext.Click += btnNext_Click;

        // lblProgress
        this.lblProgress.AutoSize = true;
        this.lblProgress.Location = new Point(30, 265);
        this.lblProgress.Text = "";
        this.lblProgress.Font = new Font("Microsoft YaHei UI", 9F);

        // Form1
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(460, 320);
        this.Controls.Add(this.lblProgress);
        this.Controls.Add(this.btnNext);
        this.Controls.Add(this.lblResult);
        this.Controls.Add(this.txtEnglish);
        this.Controls.Add(this.lblInput);
        this.Controls.Add(this.lblChinese);
        this.Controls.Add(this.lblPrompt);
        this.Text = "背单词";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
