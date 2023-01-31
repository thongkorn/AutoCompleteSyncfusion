#Region "ABOUT"
' / --------------------------------------------------------------------------------
' / Developer : Mr.Surapon Yodsanga (Thongkorn Tubtimkrob)
' / eMail : thongkorn@hotmail.com
' / URL: http://www.g2gnet.com (Khon Kaen - Thailand)
' / Facebook: https://www.facebook.com/g2gnet (For Thailand)
' / Facebook: https://www.facebook.com/commonindy (Worldwide)
' / More Info: http://www.g2gsoft.com/
' /
' / Purpose: AutoComplete in TextBox Control with Syncfusion.
' / Microsoft Visual Basic .NET (2010)
' /
' / This is open source code under @Copyleft by Thongkorn Tubtimkrob.
' / You can modify and/or distribute without to inform the developer.
' / --------------------------------------------------------------------------------
#End Region

Imports System.Data.OleDb
Imports Syncfusion.Windows.Forms.Tools

Public Class frmAutoComplete

    Private Conn As New OleDbConnection
    Dim autoComplete As AutoComplete = New AutoComplete

    ' / --------------------------------------------------------------------------------
    Private Sub frmAutoComplete_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' / --------------------------------------------------------------------------------
        '// AutoComplete with sample DataTable.
        AutoComplete1.SetAutoComplete(TextBox1, AutoCompleteModes.Both) '// Mode Append + Suggest.
        Me.AutoComplete1.DataSource = MyDataTable()

        '// AutoComplete with DataTable from DataBase and create it with code.
        Call ConnectDB()
        With autoComplete
            .ParentForm = Me    '// Must have for AutoComplete @Run time.
            .ShowColumnHeader = True
            .ShowCloseButton = True
            .ShowGripper = True
            '.AdjustHeightToItemCount = False
            '.AutoPersistentDropDownSize = True
            '.Style = AutoCompleteStyle.Metro
        End With
        autoComplete.SetAutoComplete(TextBox2, AutoCompleteModes.MultiSuggestExtended)
        autoComplete.DataSource = GetDataTable()
    End Sub

    ' / --------------------------------------------------------------------------------
    '// Create Sample DataTable.
    ' / --------------------------------------------------------------------------------
    Function MyDataTable() As DataTable
        '// Create DataTable
        Dim dt As DataTable = New DataTable()
        '// Add Columns
        With dt.Columns
            .Add("Country")
            .Add("Capital")
        End With
        '// Add Rows
        With dt.Rows
            .Add(New String() {"Thailand", "Bangkok"})
            .Add(New String() {"United Kingdom", "London"})
            .Add(New String() {"USA", "Washington, D.C."})
            .Add(New String() {"Brazil", "Brasilia"})
            .Add(New String() {"France", "Paris"})
            .Add(New String() {"Russia", "Moscow"})
            .Add(New String() {"India", "Delhi"})
            .Add(New String() {"Japan", "Tokyo"})
            .Add(New String() {"Taiwan", "Taipei"})
            .Add(New String() {"Croatia", "Zagreb"})
            .Add(New String() {"Brunei", "Bandar Seri Begawan"})
        End With
        '// Return DataTable
        Return dt
    End Function

    ' / --------------------------------------------------------------------------------
    '// DataTable from DataBase.
    ' / --------------------------------------------------------------------------------
    Function GetDataTable() As DataTable
        Dim dt As New DataTable
        Dim strSQL As String = String.Empty
        '//
        strSQL = _
            " SELECT Country, Capital " & _
            " FROM(Table1) " & _
            " ORDER BY Country, Capital "
        If Conn.State = ConnectionState.Closed Then Conn.Open()
        Using DA As New OleDbDataAdapter(strSQL, Conn)
            DA.Fill(dt)
        End Using
        '// Return DataTable
        Return dt
    End Function

    Public Function ConnectDB() As Boolean
        Dim strConn As String = _
            " Provider = Microsoft.ACE.OLEDB.12.0; " & _
            " Data Source = " & MyPath(Application.StartupPath) & "data\SampleDB.accdb"
        Try
            Conn = New OleDb.OleDbConnection(strConn)
            Conn.Open()
            '// Return
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Conn = Nothing
            Return False
        End Try
    End Function

    ' / --------------------------------------------------------------------------------
    ' / Get my project path
    ' / AppPath = C:\My Project\bin\debug
    ' / Replace "\bin\debug" with "\"
    ' / Return : C:\My Project\
    Function MyPath(ByVal AppPath As String) As String
        '/ MessageBox.Show(AppPath);
        AppPath = AppPath.ToLower()
        '/ Return Value
        MyPath = AppPath.Replace("\bin\debug", "\").Replace("\bin\release", "\").Replace("\bin\x86\debug", "\").Replace("\bin\x86\release", "\")
        '// If not found folder then put the \ (BackSlash - ASCII Code = 92) at the end.
        If Microsoft.VisualBasic.Right(MyPath, 1) <> Chr(92) Then MyPath = MyPath & Chr(92)
    End Function

    Private Sub frmAutoComplete_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        GC.SuppressFinalize(Me)
        End
    End Sub
End Class
