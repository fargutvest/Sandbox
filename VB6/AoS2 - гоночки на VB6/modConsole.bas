Attribute VB_Name = "modConsole"
Option Explicit

Private Type ConVFormat
  Pos As D3DVECTOR
  RHW As Single
  tu As Single
  tv As Single
End Type

Dim Vert(3) As ConVFormat
Dim vSize As Long

Private Type BITMAPINFOHEADER
  biSize As Long
  biWidth As Long
  biHeight As Long
  biPlanes As Integer
  biBitCount As Integer
  biCompression As Long
  biSizeImage As Long
  biXPelsPerMeter As Double
  biClrUsed As Double
End Type

Private Type BITMAPINFO
  bmiHeader As BITMAPINFOHEADER
  bmiColors As Long
End Type

Private Declare Function GetDIBits Lib "gdi32" (ByVal aHDC As Long, ByVal hBitmap As Long, ByVal nStartScan As Long, ByVal nNumScans As Long, lpBits As Any, lpBI As BITMAPINFO, ByVal wUsage As Long) As Long

Dim bi32BitInfo As BITMAPINFO
Dim cBuf() As Long
Dim TexFPS As Direct3DTexture9
Dim SurfFPS As Direct3DSurface9
Dim TexW As Direct3DTexture9
Dim SurfW As Direct3DSurface9
Dim TexC As Direct3DTexture9
Dim SurfC As Direct3DSurface9
Dim TexMsg As Direct3DTexture9
Dim SurfMsg As Direct3DSurface9
Dim TexFinal As Direct3DTexture9
Dim SurfFinal As Direct3DSurface9
Dim ConX As Long, ConY As Long
Dim ConW As Long, ConH As Long
Dim valG As Long
Dim valS As Long
Dim valT As Long
Dim iInd(3) As Long

Private Const ConFVF = D3DFVF_XYZRHW Or D3DFVF_TEX1

Public Sub ConsoleDraw()
  CreateTexW

  Dev.SetVertexShader Nothing
  Dev.SetPixelShader Nothing
  Dev.SetRenderState D3DRS_CULLMODE, D3DCULL_CCW
  Dev.SetRenderState D3DRS_ZENABLE, D3DZB_FALSE
  Dev.SetRenderState D3DRS_ALPHABLENDENABLE, D3D_TRUE
  Dev.SetRenderState D3DRS_SRCBLEND, D3DBLEND_SRCCOLOR
  Dev.SetRenderState D3DRS_DESTBLEND, D3DBLEND_INVSRCCOLOR
  Dev.SetTextureStageState 0, D3DTSS_ALPHAOP, D3DTOP_DISABLE
  Dev.SetTextureStageState 0, D3DTSS_COLOROP, D3DTOP_SELECTARG1
  Dev.SetTextureStageState 0, D3DTSS_COLORARG1, D3DTA_TEXTURE
  Dev.SetTextureStageState 1, D3DTSS_COLOROP, D3DTOP_DISABLE
  Dev.SetFVF ConFVF

  Vert(0).Pos = Vec3(4, 4, 0.5)
  Vert(1).Pos = Vec3(132, 4, 0.5)
  Vert(2).Pos = Vec3(4, 36, 0.5)
  Vert(3).Pos = Vec3(132, 36, 0.5)
  Dev.SetTexture 0, TexFPS
  Dev.DrawPrimitiveUp D3DPT_TRIANGLESTRIP, 2, VarPtr(Vert(0)), Len(Vert(0))

  Vert(0).Pos = Vec3(ConX, ConY, 0.5)
  Vert(1).Pos = Vec3(ConX + 512, ConY, 0.5)
  Vert(2).Pos = Vec3(ConX, ConY + 64, 0.5)
  Vert(3).Pos = Vec3(ConX + 512, ConY + 64, 0.5)
  Dev.SetTexture 0, TexW
  Dev.DrawPrimitiveUp D3DPT_TRIANGLESTRIP, 2, VarPtr(Vert(0)), Len(Vert(0))

  If GameMode > 0 Then
    Vert(0).Pos = Vec3(ConW - 132, 4, 0.5)
    Vert(1).Pos = Vec3(ConW - 4, 4, 0.5)
    Vert(2).Pos = Vec3(ConW - 132, 132, 0.5)
    Vert(3).Pos = Vec3(ConW - 4, 132, 0.5)
    Dev.SetTexture 0, TexMsg
    Dev.DrawPrimitiveUp D3DPT_TRIANGLESTRIP, 2, VarPtr(Vert(0)), Len(Vert(0))
  End If
  
  If RaceTimer < 1 Then
    CreateTexCnt
    Vert(0).Pos = Vec3(ConX + 224, ConY \ 2 - 32, 0.5)
    Vert(1).Pos = Vec3(ConX + 288, ConY \ 2 - 32, 0.5)
    Vert(2).Pos = Vec3(ConX + 224, ConY \ 2 + 32, 0.5)
    Vert(3).Pos = Vec3(ConX + 288, ConY \ 2 + 32, 0.5)
    Dev.SetTexture 0, TexC
    Dev.DrawPrimitiveUp D3DPT_TRIANGLESTRIP, 2, VarPtr(Vert(0)), Len(Vert(0))
  End If

  If FinisFlag Then
    Vert(0).Pos = Vec3(ConX + 128, ConH \ 2 - 64, 0.5)
    Vert(1).Pos = Vec3(ConX + 384, ConH \ 2 - 64, 0.5)
    Vert(2).Pos = Vec3(ConX + 128, ConH \ 2 + 64, 0.5)
    Vert(3).Pos = Vec3(ConX + 384, ConH \ 2 + 64, 0.5)
    Dev.SetTexture 0, TexFinal
    Dev.DrawPrimitiveUp D3DPT_TRIANGLESTRIP, 2, VarPtr(Vert(0)), Len(Vert(0))
  End If
End Sub

Private Sub CreateTexW()
  Dim R As D3DRECT
  Dim v As Long
  Dim s As String
  Dim f As Boolean

  v = CarPh(0).KPPstep
  If valG <> v Then
    valG = v
    f = True
    frmMain.picW.Line (4, 4)-(58, 58), &H8000&, BF
    frmMain.picW.CurrentX = 8
    frmMain.picW.CurrentY = -5
    frmMain.picW.FontSize = 3.2 * Screen.TwipsPerPixelX
    Select Case valG
      Case -1: s = "R"
      Case 0: s = "N"
      Case Else
        If CarPh(0).KPPType > 0 Then
          s = "D"
        Else
          s = Chr(48 + valG)
        End If
    End Select
    frmMain.picW.Print s;
  End If

  v = Abs(CarPh(0).LockSpZ) * 4.5
  If valS <> v Then
    valS = v
    f = True
    frmMain.picW.Line (144, 4)-(506, 28), &H8000&, BF
    frmMain.picW.Line (146, 6)-(148 + valS * 2.5, 26), &HC0&, BF
    frmMain.picW.CurrentX = 145
    frmMain.picW.CurrentY = 1
    frmMain.picW.FontSize = 1.5 * Screen.TwipsPerPixelX
    s = Format(valS, "000")
    frmMain.picW.Print s;
  End If

  v = (CarPh(0).Taxo \ 10) * 10
  If valT <> v Then
    valT = v
    f = True
    frmMain.picW.Line (144, 34)-(506, 58), &H8000&, BF
    frmMain.picW.Line (146, 36)-(148 + valT * 0.035, 56), &HC0&, BF
    frmMain.picW.CurrentX = 145
    frmMain.picW.CurrentY = 31
    frmMain.picW.FontSize = 1.5 * Screen.TwipsPerPixelX
    s = Format(valT, "0000")
    frmMain.picW.Print s;
  End If

  If f Then
    R.Left = 0
    R.Right = 512
    R.Top = 0
    R.Bottom = 64
    With bi32BitInfo.bmiHeader
      .biBitCount = 32
      .biPlanes = 1
      .biSize = Len(bi32BitInfo.bmiHeader)
      .biWidth = 512
      .biHeight = -64
      .biSizeImage = 512& * 64 * 4
    End With
    GetDIBits frmMain.picW.hDC, frmMain.picW.Image.Handle, 0, frmMain.picW.Height, cBuf(0, 0), bi32BitInfo, 0
    SurfW.SetData VarPtr(R), 512& * 64 * 4, VarPtr(cBuf(0, 0))
  End If
End Sub

Public Sub CreateTexCnt()
  Dim R As D3DRECT
  Dim i As Long
  Static c As Long

  i = -Int(RaceTimer)
  If c = i Then Exit Sub
  c = i
  frmMain.picCnt.Line (0, 0)-(64, 64), &H0, BF
  If i < 6 Then
    If c = 0 Then
      SoundBeep.SetFreq 33000
      frmMain.picCnt.ForeColor = &H8080FF
    Else
      SoundBeep.SetFreq 22050
      frmMain.picCnt.ForeColor = &HC0C080
    End If
    SoundBeep.Play
    frmMain.picCnt.CurrentX = 12
    frmMain.picCnt.CurrentY = -18
    frmMain.picCnt.Print Format(c, "0");
  End If
  
  R.Left = 0
  R.Right = 64
  R.Top = 0
  R.Bottom = 64
  With bi32BitInfo.bmiHeader
    .biBitCount = 32
    .biPlanes = 1
    .biSize = Len(bi32BitInfo.bmiHeader)
    .biWidth = 64
    .biHeight = -64
    .biSizeImage = 64 * 64 * 4
  End With
  GetDIBits frmMain.picCnt.hDC, frmMain.picCnt.Image.Handle, 0, frmMain.picCnt.Height, cBuf(0, 0), bi32BitInfo, 0
  SurfC.SetData VarPtr(R), 64 * 64 * 4, VarPtr(cBuf(0, 0))
End Sub

Public Sub CreateTexFPS()
  Dim R As D3DRECT

  R.Left = 0
  R.Right = 128
  R.Top = 0
  R.Bottom = 32
  frmMain.picFPS.Line (4, 4)-(122, 26), &H8080&, BF
  frmMain.picFPS.CurrentX = 30
  frmMain.picFPS.CurrentY = 1
  frmMain.picFPS.Print Format(FPS, "0000");
  With bi32BitInfo.bmiHeader
    .biBitCount = 32
    .biPlanes = 1
    .biSize = Len(bi32BitInfo.bmiHeader)
    .biWidth = 128
    .biHeight = -32
    .biSizeImage = 128 * 32 * 4
  End With
  GetDIBits frmMain.picFPS.hDC, frmMain.picFPS.Image.Handle, 0, frmMain.picFPS.Height, cBuf(0, 0), bi32BitInfo, 0
  SurfFPS.SetData VarPtr(R), 128 * 32 * 4, VarPtr(cBuf(0, 0))
End Sub

Public Sub CreateTexFinal()
  Dim R As D3DRECT
  Dim i As Long
  Dim ii As Long
  Dim mm As Long, ss As Single

  R.Left = 0
  R.Right = 256
  R.Top = 0
  R.Bottom = 128
  frmMain.picFinal.FontSize = 3 * Screen.TwipsPerPixelX
  frmMain.picFinal.CurrentX = 28
  frmMain.picFinal.CurrentY = 24
  
  If GameMode = 1 Then
    For i = 0 To 3
      If iInd(i) = 0 Then ii = i + 1
    Next i
    frmMain.picFinal.Print Trim(Str$(ii)) & " Place";
  ElseIf GameMode = 2 Then
    mm = RaceTimer \ 60
    ss = RaceTimer - mm * 60
    frmMain.picFinal.Print (Str$(mm)) & ":" & Format(ss, "00.0");
  Else
  End If
  With bi32BitInfo.bmiHeader
    .biBitCount = 32
    .biPlanes = 1
    .biSize = Len(bi32BitInfo.bmiHeader)
    .biWidth = 256
    .biHeight = -128
    .biSizeImage = 256& * 128 * 4
  End With
  GetDIBits frmMain.picFinal.hDC, frmMain.picFinal.Image.Handle, 0, frmMain.picFinal.Height, cBuf(0, 0), bi32BitInfo, 0
  SurfFinal.SetData VarPtr(R), 256& * 128 * 4, VarPtr(cBuf(0, 0))
End Sub

Public Sub CreateTexMsg()
  Dim R As D3DRECT
  Dim n As Long
  Dim i As Long
  Dim ii As Long
  Dim t As Long
  Dim f As Boolean

  If GameMode = 0 Then Exit Sub
  If FinisFlag Then Exit Sub
  R.Left = 0
  R.Right = 128
  R.Top = 0
  R.Bottom = 128
  frmMain.picMsg.Line (4, 4)-(122, 122), &H804000, BF
  For n = 0 To 3
    iInd(n) = n
  Next n
  For n = 0 To 2
    For i = n + 1 To 3
      If TrackPos(iInd(n)) < TrackPos(iInd(i)) Then
        ii = iInd(n)
        iInd(n) = iInd(i)
        iInd(i) = ii
      End If
    Next i
  Next n
  frmMain.picMsg.ForeColor = &H8080FF
  For i = 0 To 3
    If f Then frmMain.picMsg.ForeColor = &H60FF60
    If iInd(i) = 0 Then frmMain.picMsg.ForeColor = &HFFFFFF: f = True
    frmMain.picMsg.CurrentY = i * 28 + 6
    t = (TrackPos(iInd(i)) - TrackPos(0)) * 19.1
    If t < 0 Then
      frmMain.picMsg.CurrentX = 3
    Else
      frmMain.picMsg.CurrentX = 20
    End If
    
    frmMain.picMsg.Print Format(t * 0.1, "0000.0");
  Next i
  With bi32BitInfo.bmiHeader
    .biBitCount = 32
    .biPlanes = 1
    .biSize = Len(bi32BitInfo.bmiHeader)
    .biWidth = 128
    .biHeight = -128
    .biSizeImage = 128& * 128 * 4
  End With
  GetDIBits frmMain.picMsg.hDC, frmMain.picMsg.Image.Handle, 0, frmMain.picMsg.Height, cBuf(0, 0), bi32BitInfo, 0
  SurfMsg.SetData VarPtr(R), 128& * 128 * 4, VarPtr(cBuf(0, 0))
End Sub

Public Sub ConsoleInit()
  Dim i As Long

  frmMain.picFPS.Move 0, 0, 128, 32
  frmMain.picFPS.FontSize = 1.4 * Screen.TwipsPerPixelX
  frmMain.picCnt.Move 0, 0, 64, 64
  frmMain.picCnt.FontSize = 4.2 * Screen.TwipsPerPixelX
  frmMain.picW.Move 0, 0, 512, 64
  frmMain.picW.FontSize = 1.133333 * Screen.TwipsPerPixelX
  frmMain.picW.ForeColor = &HC0FFC0
  frmMain.picW.CurrentX = 64
  frmMain.picW.CurrentY = 2
  frmMain.picW.Print "Speed";
  frmMain.picW.CurrentX = 70
  frmMain.picW.CurrentY = 32
  frmMain.picW.Print "Taxo";
  frmMain.picW.ForeColor = &H0
  frmMain.picW.CurrentX = 65
  frmMain.picW.CurrentY = 3
  frmMain.picW.Print "Speed";
  frmMain.picW.CurrentX = 71
  frmMain.picW.CurrentY = 33
  frmMain.picW.Print "Taxo";
  frmMain.picW.ForeColor = &HFFFF00
  If GameMode > 0 Then
    frmMain.picMsg.Move 0, 0, 128, 128
    frmMain.picMsg.Cls
    frmMain.picMsg.FontSize = 1.4 * Screen.TwipsPerPixelX
  End If
  frmMain.picFinal.Move 0, 0, 256, 128
  frmMain.picFinal.Line (4, 4)-(250, 122), &H804000, BF
  frmMain.picFinal.FontSize = 1 * Screen.TwipsPerPixelX
  frmMain.picFinal.CurrentX = 72
  frmMain.picFinal.CurrentY = 6
  frmMain.picFinal.Print "Your Result:"
  frmMain.picFinal.CurrentX = 54
  frmMain.picFinal.CurrentY = 96
  frmMain.picFinal.Print "Press Esc to Exit"

  ReDim cBuf(511, 63)

  Set TexFPS = Dev.CreateTexture(128, 32, 1, D3DUSAGE_DYNAMIC, D3DFMT_A8R8G8B8, D3DPOOL_DEFAULT)
  Set SurfFPS = TexFPS.GetSurfaceLevel(0)

  Set TexC = Dev.CreateTexture(64, 64, 1, D3DUSAGE_DYNAMIC, D3DFMT_A8R8G8B8, D3DPOOL_DEFAULT)
  Set SurfC = TexC.GetSurfaceLevel(0)

  Set TexW = Dev.CreateTexture(512, 64, 1, D3DUSAGE_DYNAMIC, D3DFMT_A8R8G8B8, D3DPOOL_DEFAULT)
  Set SurfW = TexW.GetSurfaceLevel(0)

  If GameMode > 0 Then
    Set TexMsg = Dev.CreateTexture(128, 128, 1, D3DUSAGE_DYNAMIC, D3DFMT_A8R8G8B8, D3DPOOL_DEFAULT)
    Set SurfMsg = TexMsg.GetSurfaceLevel(0)
  End If

  Set TexFinal = Dev.CreateTexture(256, 128, 1, D3DUSAGE_DYNAMIC, D3DFMT_A8R8G8B8, D3DPOOL_DEFAULT)
  Set SurfFinal = TexFinal.GetSurfaceLevel(0)

  Vert(0).RHW = 1
  Vert(0).tu = 0
  Vert(0).tv = 0

  Vert(1).RHW = 1
  Vert(1).tu = 1
  Vert(1).tv = 0

  Vert(2).RHW = 1
  Vert(2).tu = 0
  Vert(2).tv = 1

  Vert(3).RHW = 1
  Vert(3).tu = 1
  Vert(3).tv = 1

  ConW = frmD3D.ScaleWidth
  ConH = frmD3D.ScaleHeight
  ConX = ConW \ 2 - 256
  ConY = ConH - 68
  valG = -300
  valS = -300
  valT = -300
End Sub

Public Sub ConsoleTerminate()
  Set SurfW = Nothing
  Set TexW = Nothing
  Set SurfFPS = Nothing
  Set TexFPS = Nothing
  Set SurfC = Nothing
  Set TexC = Nothing
  Set SurfMsg = Nothing
  Set TexMsg = Nothing
  Set SurfFinal = Nothing
  Set TexFinal = Nothing
End Sub
