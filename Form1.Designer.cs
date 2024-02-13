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
    private ContextMenuStrip rightClickFileMenu;
    private string Clipboard;

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
        this.ClientSize = new System.Drawing.Size(screenSize.Width, screenSize.Height);
        this.WindowState = FormWindowState.Maximized;
        this.MaximizeBox = false;
        this.BackColor = Color("#303030");
        this.Text = "Phile Explorer - " + currentPath;
        this.Font = new Font("Consolas", 12, FontStyle.Regular);

        rightClickFileMenu = new();
        rightClickFileMenu.BackColor = Color("#303030");
        rightClickFileMenu.ForeColor = Color("#eeeeee");
        ToolStripMenuItem fileRightClickCut = new("Cut");
        ToolStripMenuItem fileRightClickCopy = new("Copy");
        ToolStripMenuItem fileRightClickPaste = new("Paste");
        fileRightClickCut.Click += CutFile;
        fileRightClickCopy.Click += CopyFile;
        fileRightClickPaste.Click += PasteFile;
        rightClickFileMenu.Items.Add(fileRightClickCut);
        rightClickFileMenu.Items.Add(fileRightClickCopy);
        rightClickFileMenu.Items.Add(fileRightClickPaste);

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
        backButton.Size = new Size(80, 30);
        backButton.Text = "<";
        backButton.FlatStyle = FlatStyle.Flat;
        backButton.FlatAppearance.BorderSize = 0;
        backButton.ForeColor = Color("#ffffff");
        backButton.Click += BackButton;
        topPanel.Controls.Add(backButton);

        forwardButton = new();
        forwardButton.Location = new Point(90, 5);
        forwardButton.BackColor = Color("#333333");
        forwardButton.Size = new Size(80, 30);
        forwardButton.Text = ">";
        forwardButton.FlatStyle = FlatStyle.Flat;
        forwardButton.FlatAppearance.BorderSize = 0;
        forwardButton.ForeColor = Color("#ffffff");
        forwardButton.Click += ForwardButton;
        topPanel.Controls.Add(forwardButton);

        pathTextBox = new();
        pathTextBox.Location = new Point(180, 10);
        pathTextBox.Size = new Size(screenSize.Width - treeViewPanel.Width, 30);
        pathTextBox.Text = currentPath;
        pathTextBox.KeyDown += UpdatePathToUserEnteredPath;
        pathTextBox.BackColor = Color("#333333");
        pathTextBox.BorderStyle = BorderStyle.None;
        pathTextBox.ForeColor = Color("#eeeeee");
        topPanel.Controls.Add(pathTextBox);

        DisplayFiles();
    }

    private void CutFile(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void CopyFile(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void PasteFile(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void UpdatePathToUserEnteredPath(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            TextBox textBox = (TextBox)sender;
            UpdatePath(textBox.Text);
        }
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
            pathTextBox.Text = currentPath;
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
            file.Width = 400;
            file.TextAlign = ContentAlignment.MiddleLeft;
            file.FlatStyle = FlatStyle.Flat;
            file.FlatAppearance.BorderSize = 0;
            file.MouseDown += (sender, e) => FileClick(sender, e, filePath);
            filePanel.Controls.Add(file);

            count++;
        }
    }

    private void FileClick(object sender, MouseEventArgs e, string filePath)
    {
        if (e.Button == MouseButtons.Left)
        {
            UpdatePath(filePath);
        }
        else if (e.Button == MouseButtons.Right)
        {
            rightClickFileMenu.Show((Control)sender, e.Location);
        }
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
