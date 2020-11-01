Imports System
Imports System.IO
Imports System.Data
Public Class Form1
    Sub save()
        Dim path As String = "C:\ini\settings.ini"
        Dim pathDr As String = "ini"
        If File.Exists(path) Then
            Kill(path)
        End If
        If Directory.Exists(pathDr) = False Then
            MkDir(pathDr)
        End If
        If File.Exists(path) = False Then
            ' Create a file to write to.
            Using sw As StreamWriter = New StreamWriter(path)
                sw.WriteLine(ComboBox1.Text)
                sw.WriteLine(ComboBox2.Text)
                sw.WriteLine(ComboBox3.Text)
                sw.WriteLine(ComboBox4.Text)
                sw.WriteLine(ComboBox5.Text)

                sw.Flush()
                sw.Close()
            End Using
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        save()
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim setpath As String = "C:\ini\settings.ini"

        Using sr As StreamReader = File.OpenText(setpath)
            ComboBox1.Text = sr.ReadLine()
            ComboBox2.Text = sr.ReadLine()
            ComboBox3.Text = sr.ReadLine()
            ComboBox4.Text = sr.ReadLine()
            ComboBox5.Text = sr.ReadLine()
            sr.Close()
        End Using
    End Sub

  
End Class
