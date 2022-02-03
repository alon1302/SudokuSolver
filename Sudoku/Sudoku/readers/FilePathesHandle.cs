using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;

class FilePathesHandle
{
    [STAThread]
    public static string GetSelectedFilePath()
    {
        OpenFileDialog dialog = new OpenFileDialog
        {
            Multiselect = false,
            Title = "Open Text Document",
            Filter = "Text files (*.txt)|*.txt"
        };
        using (dialog)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {
                throw new FileDialogException();
            }
        }
    }

    public static string CreateResultFilePath(string filePath)
    {
        DirectoryInfo parentPath = Directory.GetParent(filePath);
        string newPath = parentPath.FullName;
        string newFileName = Path.GetFileNameWithoutExtension(filePath);
        newFileName += "-result.txt";
        newPath = Path.Combine(newPath, newFileName);
        return newPath;
    }
}

