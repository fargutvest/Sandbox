Public Class Form1
    Dim i As Integer
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        DataGridView1.Rows.Add(17)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        For i = 0 To 10
            DataGridView1.Rows(0).Cells(i).Value = 5
        Next

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Text = DataGridView1.Rows(2).Cells(3).Value + DataGridView1.Rows(2).Cells(4).Value
    End Sub
End Class
