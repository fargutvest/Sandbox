﻿Imports System
Imports System.IO
Imports System.Net
Public Class form1
    Dim pos_mess, a1, i As Integer
    Dim go As Integer
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

        Timer1.Enabled = True
        Timer3.Enabled = True

    End Sub
    Sub gethtml()
        If go = 0 Then
            pach = "http://forum.onliner.by/viewtopic.php?t=3064071&start=267600" + "0"
            TextBox2.Text = pach
        Else : pach = TextBox2.Text
        End If

        client.Encoding = System.Text.Encoding.UTF8
        html = client.DownloadString(pach)
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        gethtml()
        a2 = html.Substring(InStr(a1 + 8, html, "message_") + 7, 8)
        If a2 <> "PE html " And a2 > message_id Then
            i += 1
            a1 = InStr(a1 + 8, html, "message_")
            DataGridView1.Rows.Add()
            DataGridView1.Rows(i - 1).Cells(0).Value = i + 1 'нумерация строк
            textovka = html.Substring(a1 - 1, 500)
            DataGridView1.Rows(i - 1).Cells(1).Value = textovka
            wazzup()
        End If

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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        go = 1
        Timer1.Stop()
        Timer1.Start()
    End Sub



    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form3.TextBox1.Text = html
        Form3.Show()
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        If ProgressBar1.Value < 91 Then
            ProgressBar1.Value = ProgressBar1.Value + 1
        Else : ProgressBar1.Value = 0
        End If


    End Sub
End Class
