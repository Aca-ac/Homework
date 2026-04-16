using WebExtractorApp;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // 确保这里 new 的类名和你 Form1.cs 里定义的一样
        // 如果你的类叫 Form1，就写 new Form1()
        Application.Run(new Form1());
    }
}