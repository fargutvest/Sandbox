Public Class Form1
    Dim i, a, b As Integer
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        For i = 0 To 1000000
            For a = 0 To 2500
            Next
        Next
        TextBox1.Text = "готово"
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If ProgressBar1.Value < 1000 Then
            ProgressBar1.Value = ProgressBar1.Value + 10
        Else : ProgressBar1.Value = 0
        End If
    End Sub
End Class
