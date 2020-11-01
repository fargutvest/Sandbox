Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Mail

Module Module1

    Sub Main()
        Dim texte As String
        Shell("cmd.exe /c c:\getmyip.exe>c:\1.txt")
        System.Threading.Thread.Sleep(10000)
        Dim path As String = "c:\1.txt"
        Using sr As StreamReader = File.OpenText(path)
            texte = sr.ReadLine()
            sr.Close()
        End Using
        ' --- формируем само письмо
        Dim msg As MailMessage
        msg = New MailMessage()
        msg.From = New MailAddress("genadys@mail.ru", "Николай")
        msg.To.Add("fargutvest@gmail.com")
        msg.Subject = "Тема письма"
        msg.Body = texte
        msg.IsBodyHtml = True


        ' --- Подключаемся к Smtp и выполняем отправку
        Dim smtpclient As New SmtpClient("smtp.mail.ru", 25)
        'If ssl = 1 Then smtpclient.EnableSsl = True Else smtpclient.EnableSsl = False
        smtpclient.Credentials = New NetworkCredential("genadys", "basmakarpull")
        smtpclient.Send(msg)

        ' --- Очищаем память :)
        msg.Dispose()

    End Sub


End Module
