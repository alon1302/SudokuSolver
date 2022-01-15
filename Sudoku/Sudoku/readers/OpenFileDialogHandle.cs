using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;

class OpenFileDialogHandle
{
    public static string GetSelectedFilePath()
    {
        OpenFileDialog dialog = new OpenFileDialog
        {
            Multiselect = false,
            Title = "Open Text Document",
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        };
        using (dialog)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return FileName;
            }
        }
    }
}

