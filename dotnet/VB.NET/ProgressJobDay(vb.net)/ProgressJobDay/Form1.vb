Public Class Form1
    Dim a, hour, minute, second, currentsecondtime, b As Double
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(a)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        a = 1
        hour = TimeOfDay.Hour
        minute = TimeOfDay.Minute
        second = TimeOfDay.Second
        b = (hour * 3600 + minute * 60 + second - 32400) * 10
        ProgressBar1.Step = b
        ProgressBar1.Maximum = 306000
        ProgressBar1.PerformStep()
        Me.Text = "ProgressJobDay"
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Me.WindowState = FormWindowState.Minimized
    End Sub


    Private Sub NotifyIcon1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.Click
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Form1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
        End If
    End Sub

   
End Class