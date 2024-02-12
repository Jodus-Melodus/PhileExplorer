using System.Drawing.Design;

namespace PhileExplorer;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private string currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
    private Panel treeViewPanel;
    private Panel topPanel;
    private Button backButton;
    private Button forwardButton;

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

        backButton = new();
        backButton.Location = new System.Drawing.Point(5, 5);
        backButton.BackColor = Color("#333333");
        backButton.Size = new System.Drawing.Size(80, 30);
        backButton.Text = "Back";
        backButton.ForeColor = Color("#ffffff");
        backButton.Click += BackButton;

        topPanel.Controls.Add(backButton);
    }

    private void BackButton(object sender, EventArgs e)
    {
        List<string> newPath = new(currentPath.Split("\\"));
        newPath.RemoveAt(newPath.Count - 1);
        currentPath = string.Join("\\", newPath);
        this.Text = "Phile Explorer - " + currentPath;
    }

    private System.Drawing.Color Color(string hex)
    {
        return System.Drawing.ColorTranslator.FromHtml(hex);
    }

    private string[] GetCurrentFiles()
    {
        string directoryPath = Path.GetDirectoryName(currentPath);

        string[] siblingFiles = Directory.GetFiles(directoryPath);

        return siblingFiles;
    }
}
