namespace MauiLocalFiles.Pages
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnReadFileClicked(object? sender, EventArgs e)
        {
            try
            {
                string filePath = ReadFilePathEntry.Text;

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    OutputEditor.Text = "Error: Please enter a file path";
                    return;
                }

                if (!File.Exists(filePath))
                {
                    OutputEditor.Text = $"Error: File not found at {filePath}";
                    return;
                }

                string content = await File.ReadAllTextAsync(filePath);
                OutputEditor.Text = $"✓ File read successfully:\n\n{content}";
            }
            catch (Exception ex)
            {
                OutputEditor.Text = $"Error reading file: {ex.Message}";
            }
        }

        private async void OnWriteFileClicked(object? sender, EventArgs e)
        {
            try
            {
                string filePath = WriteFilePathEntry.Text;
                string content = WriteFileContentEditor.Text;

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    OutputEditor.Text = "Error: Please enter a file path";
                    return;
                }

                if (string.IsNullOrWhiteSpace(content))
                {
                    OutputEditor.Text = "Error: Please enter content to write";
                    return;
                }

                // Create directory if it doesn't exist
                string? directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                await File.WriteAllTextAsync(filePath, content);
                OutputEditor.Text = $"✓ File written successfully to:\n{filePath}";
            }
            catch (Exception ex)
            {
                OutputEditor.Text = $"Error writing file: {ex.Message}";
            }
        }

        private void OnClearClicked(object? sender, EventArgs e)
        {
            OutputEditor.Text = "Ready...";
        }
    }
}
