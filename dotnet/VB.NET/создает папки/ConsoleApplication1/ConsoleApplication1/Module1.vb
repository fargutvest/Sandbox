Module Module1

    Sub Main()

    End Sub
    Private Sub Main()
        papka = 0
        Timer1.Interval = 1
    End Sub
    Private Sub Timer1_Timer()
        On Error Resume Next ' если происходит ошибка при создании папки, то игнорируем ее
        MkDir("C:Папка №" & papka) ' создаем папку с номером равным переменной papka
        papka = papka + 1 ' Это что - то типо счетчика, каждый раз на один больше
    End Sub

End Module
