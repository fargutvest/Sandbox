Imports System
Imports System.IO
Imports System.Net
Public Class form1
    Dim pos_mess, a1, i, sec As Integer
    Dim client As WebClient = New WebClient()
    Dim message_id As String
    Dim pach As String
    Dim html As String
    Dim textovka, a2 As String

    Private Sub form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gethtml()
        pos_mess = 1
        For i = 0 To 20

            pos_mess = InStr(pos_mess, html, "message_")
            If pos_mess = 0 Then Exit For
            a1 = pos_mess
            DataGridView1.Rows.Add()
            DataGridView1.Rows(i).Cells(0).Value = i + 1 'нумерация строк
            textovka = html.Substring(InStr(pos_mess, html, "message_") - 1, 500)
            DataGridView1.Rows(i).Cells(1).Value = textovka
            message_id = html.Substring(InStr(pos_mess, html, "message_") + 7, 8)
            pos_mess += 8
        Next
        sec = 30
        Timer1.Enabled = True
        Timer3.Enabled = True

    End Sub
    Sub gethtml()
        pach = TextBox2.Text + "00"
        client.Encoding = System.Text.Encoding.UTF8
        html = client.DownloadString(pach)
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        refresh0()

    End Sub
    Sub refresh0()
        Label2.ForeColor = Color.Green
        Label2.Text = "REFRESH"
        Timer3.Stop()
        gethtml()
        For t = 0 To 20
            a2 = html.Substring(InStr(a1 + 8, html, "message_") + 7, 8) '8 цифр после _
            If a2 <> "PE html " And a2 > message_id Then
                message_id = a2
                i += 1
                a1 = InStr(a1 + 8, html, "message_")
                DataGridView1.Rows.Add()
                DataGridView1.Rows(i - 1).Cells(0).Value = i + 1 'нумерация строк
                textovka = html.Substring(a1 - 1, 500)
                DataGridView1.Rows(i - 1).Cells(1).Value = textovka
                wazzup()
            End If
        Next
        sec = 30
        Timer3.Start()
    End Sub
 
    Sub PlaySound(ByVal SoundFile As String, ByVal Volume As Integer)
        My.Computer.Audio.Play(SoundFile, Volume)
    End Sub
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Form2.Close()
        Timer2.Enabled = False

    End Sub
    Sub wazzup()
        Timer2.Enabled = True
        Form2.Label2.Text = textovka.Substring(InStr(1, textovka, "<p>") + 2, InStr(1, textovka, "</p>") - InStr(1, textovka, "<p>") - 3)
        Form2.Show()
        PlaySound("ding.wav", 1)
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form3.TextBox1.Text = html
        Form3.Show()
    End Sub



    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        Process.Start(pach)
    End Sub
    Private Sub Label1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label1.MouseMove
        Label1.ForeColor = Color.Blue
    End Sub
    Private Sub Label1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label1.MouseLeave
        Label1.ForeColor = Color.Black
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Label2.ForeColor = Color.Black
        sec -= 1
        Label2.Text = "   " & sec
    End Sub
    Private Sub Form1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        DataGridView1.Rows.Clear()
        refresh0()
    End Sub
End Class
