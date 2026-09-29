using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.GlobalClass
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {
            Guid NewGUID = Guid.NewGuid();
            return NewGUID.ToString();
        }

        public static bool CreatFolderIfDoesNotExist(string FolderPath)
        {
            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;
        }

        public static string ReplaceFileNameWithGUID(string SourceImageFile)
        {
            string FileName = SourceImageFile;
            FileInfo fileInfo = new FileInfo(FileName);
            string Extn = fileInfo.Extension;
            return GenerateGUID() + Extn;
        }

        public static bool CopyImageToProjectImageFolder(ref string SourceImageFile)
        {
            string DestinationFolder = @"C:\DVLD_People_Images\";
            if (!CreatFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }
            string DestinationFile = DestinationFolder + ReplaceFileNameWithGUID(SourceImageFile);
            try
            {
                File.Copy(SourceImageFile, DestinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            SourceImageFile = DestinationFile;
            return true;
        }
    }
}
