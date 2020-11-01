Public Class Form1
    Dim a As String = "http://forum.onliner.by/viewtopic.php?t=1051943&start="
    Dim b As Integer = 100000

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ' b = b + 20
        WebBrowser1.Navigate(a & b)
        TextBox1.Text = a & b
        TextBox2.Text = ""
        TextBox2.Text = WebBrowser1.DocumentType
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WebBrowser1.Navigate(a & b)
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Label1.Text = TimeOfDay
        TextBox2.Text = WebBrowser1.DocumentText



    End Sub


End Class






'	TextBox1.Text = (New System.Net.WebClient).DownloadString("http://ya.ru")