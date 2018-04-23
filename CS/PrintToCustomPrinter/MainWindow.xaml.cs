#region #usings
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Printing;
using System.Windows.Documents;
using System.Drawing.Printing;
#endregion #usings

namespace PrintToCustomPrinter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            new CustomXpfRichEditPrinter(richEditControl1).PrintToMyPrinter();
        }
    }


    #region #customprinter
    public class CustomXpfRichEditPrinter : XpfRichEditPrinter
    {
        public CustomXpfRichEditPrinter(IRichEditControl control)
            : base(control) {}

        public void PrintToMyPrinter()
        {
            PrintDialog pDialog = new PrintDialog();
            PrintQueueCollection queues = new PrintServer().GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local,
                EnumeratedPrintQueueTypes.Connections });
            System.Collections.IEnumerator localPrinterEnumerator = queues.GetEnumerator();
            PrintQueue printQueue = null;

            if (localPrinterEnumerator.MoveNext()) {
                printQueue = (PrintQueue)localPrinterEnumerator.Current;
            }

            if (printQueue != null) {
                pDialog.PrintQueue = printQueue;
                FixedDocument document = this.CreateFixedDocument();
                pDialog.PrintDocument(document.DocumentPaginator, string.Empty);
            }
        }
    }
    #endregion #customprinter


}
