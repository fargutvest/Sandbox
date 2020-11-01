Public Class Form1
    Dim min0x, sec0x, hour0x, secx0, minx0, hourx0 As String
    Dim bmin0x, bsec0x, bhour0x, bsecx0, bminx0, bhourx0 As String
    Dim b, d As String
    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        sec0x += 1
        If sec0x > 9 Then
            secx0 += 1
            sec0x = 0
        End If
        If secx0 > 5 Then
            minx0 += 1
            secx0 = 0
        End If
        If min0x > 9 Then
            minx0 += 1
            min0x = 0
        End If
        If minx0 > 5 Then
            hourx0 += 1
            minx0 = 0
        End If


        bsec0x = Convert.ToString(CInt(sec0x), 2)
        bsecx0 = Convert.ToString(CInt(secx0), 2)
        bmin0x = Convert.ToString(CInt(min0x), 2)
        bminx0 = Convert.ToString(CInt(minx0), 2)
        bhour0x = Convert.ToString(CInt(hour0x), 2)
        bhourx0 = Convert.ToString(CInt(hourx0), 2)


        Label1.Text = minx0 + " " + min0x + " " + secx0 + " " + sec0x
        Label2.Text = "bminx0= " + bminx0 + " minx0= " + bmin0x + " secx0= " + bsecx0 + " sec0x= " + bsec0x

    End Sub
End Class


'if a>9 then 