using System.Runtime.InteropServices;

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

        private async void OnGetReadFilePathClicked(object? sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a file to read"
                });

                if (result != null)
                {
                    ReadFilePathEntry.Text = result.FullPath ?? result.FileName;
                }
            }
            catch (Exception ex)
            {
                OutputEditor.Text = $"Error opening file picker: {ex.Message}";
            }

        }

        private async void OnGetWriteFilePathClicked(object? sender, EventArgs e)
        {
            try
            {
                // Try letting the user pick an existing file to overwrite
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a file to save (pick existing to overwrite)"
                });

                if (result != null)
                {
                    WriteFilePathEntry.Text = result.FullPath ?? result.FileName;
                    return;
                }
            }
            catch (Exception ex)
            {
                OutputEditor.Text = $"Error selecting save path: {ex.Message}";
            }
        }
    }
}
