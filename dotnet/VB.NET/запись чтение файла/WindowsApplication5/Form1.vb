Imports System
Imports System.IO
Imports System.Data
Public Class Form1

    'запись в фаил
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim path As String = "ini\текст.ini"
        Dim pathDr As String = "Energomera\ActionMeters v2.0\Config\ConfigForm26"
        If File.Exists(path) Then ' если файл существует, удалить файл
            Kill(path)
        End If
        If Directory.Exists(pathDr) = False Then 'есть ли нужный каталог, если нет создать 
            MkDir(pathDr)
        End If
        If File.Exists(path) = False Then 'если файла нет, сдает и записывает строки
            ' Create a file to write to.
            Using sw As StreamWriter = New StreamWriter(path)
                sw.WriteLine(TextBox1.Text)
                sw.Flush()
                sw.Close()
            End Using
        End If
    End Sub

    'чтение из файла
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim path0 As String = "ini\текст.ini"
        If File.Exists(path0) Then
            Using sr As StreamReader = File.OpenText(path0)
                TextBox2.Text = sr.ReadLine()
                TextBox3.Text = sr.ReadLine()
                sr.Close()
            End Using
        End If
    End Sub


End Class
