using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;

class FilePathesHandle
{
    /// <summary>
    /// function that using the OpenFileDialog class and let user to choose a text file from the file explorrer
    /// </summary>
    /// <exception cref="FileDialogException">if the DialogResult is not OK</exception>
    /// <returns>the string that represent the full file path the user selected</returns>
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

    /// <summary>
    /// the function receives string that represent full file path
    /// and creates path for the result file 
    /// the function build the path in a way that the new file would be in the same folder that the input file was
    /// and with the same name except the word result that will be add to the original name
    /// </summary>
    /// <param name="filePath">the input file path</param>
    /// <returns>the new file path that has been created</returns>
    public static string CreateResultFilePath(string filePath)
    {
        string newPath = Directory.GetParent(filePath).FullName; // get the path to the file parent folder
        string newFileName = Path.GetFileNameWithoutExtension(filePath); // get the name of the input file
        newFileName += "-result.txt"; // add the word result to the name of the input file 
        newPath = Path.Combine(newPath, newFileName); // combine the parent folder with the new file name
        return newPath;
    }
}

