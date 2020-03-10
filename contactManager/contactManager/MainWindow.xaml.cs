using contactManager.src.Contacts;
using Microsoft.Win32;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using contactManager.src.Messages;

namespace contactManager
{
    /// <summary>
    /// Main window class.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Main window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //register to receive report messages
            Messenger.Default.Register<TextMessage>(this, (action) => ReceiveReportMessage(action));
        }

        /// <summary>
        /// Button for importing contacts.
        /// </summary>
        private void BtnImportContacts_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
                ContactImporter.ImportFile(openFileDialog.FileName);
        }

        /// <summary>
        /// Button for printing content of database.
        /// </summary>
        private void BtnPrintDatabase_Click(object sender, RoutedEventArgs e)
        {
            ContactPrinter.PrintContactDb();
        }
        
        /// <summary>
        /// Button for clearing log text box.
        /// </summary>
        private void BtnClearLog_Click(object sender, RoutedEventArgs e)
        {
            log.Text = "";
        }

        /// <summary>
        /// Button for closing application.
        /// </summary>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Writing message content to log.
        /// </summary>
        /// <param name="message">text message</param>
        private void ReceiveReportMessage(TextMessage message)
        {
            log.Text = log.Text + message.Text;
        }
    }
}
