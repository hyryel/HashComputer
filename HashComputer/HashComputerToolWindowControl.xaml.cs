namespace HashComputer
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.Win32;
    using System.Linq;

    /// <summary>
    /// Interaction logic for HashComputerToolWindowControl.
    /// </summary>
    public partial class HashComputerToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HashComputerToolWindowControl"/> class.
        /// </summary>
        public HashComputerToolWindowControl()
        {
            this.InitializeComponent();
            lblHashAlgorithm.Text = StringResources.LblHashAlgorithm;
            lblSelectFile.Text = StringResources.LblSelectFile;
            btnComputeHash.Content = StringResources.LblComputeButton;
            btnPickFile.Content = StringResources.LblBrowseButton;

        }

        private void UpdateBtnStatus()
        {
            btnComputeHash.IsEnabled = !string.IsNullOrEmpty(tbxFileName.Text);
        }

        private void BtnComputeHash_Click(object sender, RoutedEventArgs e)
        {
            System.IO.FileStream fs = null;
            try
            {
                fs = new System.IO.FileStream(tbxFileName.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] hashByte = null;
                ComboBoxItem s = lstHashAlgorithm.SelectedItem as ComboBoxItem;
                switch ( s.Content.ToString())
                {
                    case "SHA-1":
                        System.Security.Cryptography.SHA1 sha1Computer = System.Security.Cryptography.SHA1CryptoServiceProvider.Create();
                        hashByte = sha1Computer.ComputeHash(fs);
                        sha1Computer.Dispose();
                        break;
                    case "SHA-256":
                        System.Security.Cryptography.SHA256 sha256Computer = System.Security.Cryptography.SHA256CryptoServiceProvider.Create();
                        hashByte = sha256Computer.ComputeHash(fs);
                        sha256Computer.Dispose();
                        break;
                    case "SHA-512":
                        System.Security.Cryptography.SHA512 sha512Computer = System.Security.Cryptography.SHA512CryptoServiceProvider.Create();
                        hashByte = sha512Computer.ComputeHash(fs);
                        sha512Computer.Dispose();
                        break;
                    case "MD5":
                        System.Security.Cryptography.MD5 md5Computer = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
                        hashByte = md5Computer.ComputeHash(fs);
                        md5Computer.Dispose();
                        break;
                    default:
                        throw new ArgumentException("How could you possibly select another option !!!");
                }

                tbxHash.Text = string.Join("", hashByte.Select(b => b.ToString("X2")));

            }
            catch (Exception)
            {
            }
            finally
            {
                fs?.Close();
            }
        }

        private void BtnPickFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Multiselect = false;
            ofd.ReadOnlyChecked = true;
            ofd.ShowReadOnly = false;
            ofd.Title = StringResources.OfdTitle;
            if (ofd.ShowDialog() ==  true)
            {
                tbxFileName.Text = ofd.FileName;
            }
            UpdateBtnStatus();
        }

    }
}