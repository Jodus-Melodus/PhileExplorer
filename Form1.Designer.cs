namespace PhileExplorer;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private string currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
    private Panel treeViewPanel;
    private Panel topPanel;
    private Button backButton;
    private Button forwardButton;
    private string previousPath;
    private Panel filePanel;
    private TextBox pathTextBox;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Size screenSize = Screen.PrimaryScreen.Bounds.Size;

        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(screenSize.Width / 2, screenSize.Height / 2);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = Color("#303030");
        this.Text = "Phile Explorer - " + currentPath;

        topPanel = new();
        topPanel.Height = 50;
        topPanel.Width = this.Width;
        topPanel.BackColor = Color("#202020");
        this.Controls.Add(topPanel);

        treeViewPanel = new();
        treeViewPanel.Width = 200;
        treeViewPanel.Height = this.Height - topPanel.Height;
        treeViewPanel.Top = topPanel.Height;
        treeViewPanel.BackColor = Color("#353535");
        this.Controls.Add(treeViewPanel);

        filePanel = new();
        filePanel.Location = new Point(treeViewPanel.Width, topPanel.Height);
        filePanel.Width = this.Width;
        filePanel.Height = this.Height - topPanel.Height;
        filePanel.BackColor = Color("#242424");
        filePanel.AutoScroll = true;
        this.Controls.Add(filePanel);

        backButton = new();
        backButton.Location = new Point(5, 5);
        backButton.BackColor = Color("#333333");
        backButton.Size = new System.Drawing.Size(80, 30);
        backButton.Text = "<";
        backButton.FlatStyle = FlatStyle.Flat;
        backButton.FlatAppearance.BorderSize = 0;
        backButton.ForeColor = Color("#ffffff");
        backButton.Click += BackButton;
        topPanel.Controls.Add(backButton);

        forwardButton = new();
        forwardButton.Location = new Point(90, 5);
        forwardButton.BackColor = Color("#333333");
        forwardButton.Size = new System.Drawing.Size(80, 30);
        forwardButton.Text = ">";
        forwardButton.FlatStyle = FlatStyle.Flat;
        forwardButton.FlatAppearance.BorderSize = 0;
        forwardButton.ForeColor = Color("#ffffff");
        forwardButton.Click += ForwardButton;
        topPanel.Controls.Add(forwardButton);
    }

    private void UpdatePath(string newPath)
    {
        if (Directory.Exists(newPath))
        {
            foreach (Control control in filePanel.Controls)
            {
                control.Dispose();
            }
            filePanel.Controls.Clear();

            currentPath = newPath;
            this.Text = "Phile Explorer - " + currentPath;
            DisplayFiles();
        }
    }

    private void BackButton(object sender, EventArgs e)
    {
        previousPath = currentPath;
        List<string> newPath = new(currentPath.Split("\\"));
        newPath.RemoveAt(newPath.Count - 1);
        UpdatePath(string.Join("\\", newPath));
    }

    private void DisplayFiles()
    {
        string[] files = GetCurrentFiles();
        int count = 0;
        int fileButtonHeight = 30;

        foreach (string filePath in files)
        {
            Button file = new();
            file.Text = Path.GetFileName(filePath);
            file.Height = fileButtonHeight;
            file.Location = new Point(0, count * fileButtonHeight);
            file.ForeColor = Color("#eeeeee");
            file.Width = 200;
            file.TextAlign = ContentAlignment.MiddleLeft;
            file.FlatStyle = FlatStyle.Flat;
            file.FlatAppearance.BorderSize = 0;
            file.Click += (sender, e) => ChangePathToFile(sender, e, filePath);
            filePanel.Controls.Add(file);

            count++;
        }
    }

    private void ChangePathToFile(object sender, EventArgs e, string filePath)
    {
        UpdatePath(filePath);
    }

    private void ForwardButton(object sender, EventArgs e)
    {
        UpdatePath(previousPath);
    }

    private System.Drawing.Color Color(string hex)
    {
        return System.Drawing.ColorTranslator.FromHtml(hex);
    }

    private string[] GetCurrentFiles()
    {
        string[] siblingFiles = Directory.GetDirectories(currentPath).Concat(Directory.GetFiles(currentPath)).ToArray();

        return siblingFiles;
    }
}
