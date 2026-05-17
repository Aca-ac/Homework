using Microsoft.Data.Sqlite;

namespace VocabApp;

public partial class Form1 : Form
{
    private string _dbPath = "vocabulary.db";
    private List<WordEntry> _words = [];
    private int _currentIndex = 0;
    private bool _answered = false;

    public Form1()
    {
        InitializeComponent();
        InitializeDatabase();
        LoadWords();
        ShowCurrentWord();
    }

    private void InitializeDatabase()
    {
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dbPath);
        bool exists = File.Exists(fullPath);

        using var connection = new SqliteConnection($"Data Source={fullPath}");
        connection.Open();

        if (!exists)
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"CREATE TABLE Words (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                English TEXT NOT NULL,
                Chinese TEXT NOT NULL
            );";
            cmd.ExecuteNonQuery();

            string[][] seedWords =
            [
                ["apple", "苹果"],
                ["book", "书"],
                ["computer", "计算机"],
                ["dog", "狗"],
                ["elephant", "大象"],
                ["flower", "花"],
                ["garden", "花园"],
                ["house", "房子"],
                ["island", "岛屿"],
                ["jungle", "丛林"],
                ["king", "国王"],
                ["lion", "狮子"],
                ["mountain", "山"],
                ["night", "夜晚"],
                ["ocean", "海洋"],
                ["pencil", "铅笔"],
                ["queen", "女王"],
                ["river", "河流"],
                ["sun", "太阳"],
                ["tree", "树"],
            ];

            foreach (var w in seedWords)
            {
                cmd.CommandText = $"INSERT INTO Words (English, Chinese) VALUES ('{w[0]}', '{w[1]}');";
                cmd.ExecuteNonQuery();
            }
        }
    }

    private void LoadWords()
    {
        _words.Clear();
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dbPath);

        using var connection = new SqliteConnection($"Data Source={fullPath}");
        connection.Open();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT English, Chinese FROM Words ORDER BY Id;";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            _words.Add(new WordEntry(reader.GetString(0), reader.GetString(1)));
        }

        // Shuffle for variety
        var rng = new Random();
        _words = _words.OrderBy(_ => rng.Next()).ToList();
    }

    private void ShowCurrentWord()
    {
        if (_words.Count == 0)
        {
            lblChinese.Text = "无单词数据";
            return;
        }

        _answered = false;
        lblChinese.Text = _words[_currentIndex].Chinese;
        txtEnglish.Text = "";
        txtEnglish.Enabled = true;
        lblResult.Text = "";
        txtEnglish.Focus();
        lblProgress.Text = $"{_currentIndex + 1} / {_words.Count}";
    }

    private void CheckAnswer()
    {
        if (_answered) return;
        _answered = true;

        string userAnswer = txtEnglish.Text.Trim();
        string correctAnswer = _words[_currentIndex].English;

        if (string.Equals(userAnswer, correctAnswer, StringComparison.OrdinalIgnoreCase))
        {
            lblResult.Text = "✓ 正确!";
            lblResult.ForeColor = Color.Green;
        }
        else
        {
            lblResult.Text = $"✗ 错误! 正确答案: {correctAnswer}";
            lblResult.ForeColor = Color.Red;
        }

        txtEnglish.Enabled = false;
    }

    private void txtEnglish_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            CheckAnswer();
    }

    private void btnNext_Click(object? sender, EventArgs e)
    {
        _currentIndex = (_currentIndex + 1) % _words.Count;
        ShowCurrentWord();
    }
}

public record WordEntry(string English, string Chinese);
