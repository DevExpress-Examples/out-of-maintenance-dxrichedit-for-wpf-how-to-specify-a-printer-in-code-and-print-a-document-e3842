Imports Microsoft.VisualBasic
#Region "#usings"
Imports System.Printing
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.Printing
Imports System.Windows.Documents
Imports System.Drawing.Printing
#End Region ' #usings

Namespace PrintToCustomPrinter
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()

		End Sub

		Private Sub barButtonItem1_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
			CType(New CustomXpfRichEditPrinter(richEditControl1), CustomXpfRichEditPrinter).PrintToMyPrinter()
		End Sub
	End Class


	#Region "#customprinter"
	Public Class CustomXpfRichEditPrinter
		Inherits XpfRichEditPrinter
		Public Sub New(ByVal control As IRichEditControl)
			MyBase.New(control)
		End Sub

		Public Sub PrintToMyPrinter()
			Dim pDialog As New PrintDialog()
			Dim enumerationFlags() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections}
			Dim queues As PrintQueueCollection = New PrintServer().GetPrintQueues(enumerationFlags)
			Dim localPrinterEnumerator As System.Collections.IEnumerator = queues.GetEnumerator()
			Dim printQueue As PrintQueue = Nothing

			If localPrinterEnumerator.MoveNext() Then
				printQueue = CType(localPrinterEnumerator.Current, PrintQueue)
			End If

			If printQueue IsNot Nothing Then
				pDialog.PrintQueue = printQueue
				Dim document As FixedDocument = Me.CreateFixedDocument()
				pDialog.PrintDocument(document.DocumentPaginator, String.Empty)
			End If
		End Sub
	End Class
	#End Region ' #customprinter


End Namespace
