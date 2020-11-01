Public Class Form1

  
    Dim papka As Long ' вводим переменную papka, для подсчета номера папки
    Private Sub Form_Load()
        papka = 0
        Timer1.Interval = 1000
    End Sub
    Private Sub Timer1_Timer()
        On Error Resume Next ' если происходит ошибка при создании папки, то игнорируем ее
        MkDir("C:Папка №" & papka) ' создаем папку с номером равным переменной papka
        papka = papka + 1 ' Это что - то типо счетчика, каждый раз на один больше
    End Sub
End Class
