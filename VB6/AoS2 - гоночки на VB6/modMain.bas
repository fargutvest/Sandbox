Attribute VB_Name = "modMain"
Option Explicit

Public Keyb(0 To 255) As Boolean
Public Running As Boolean
Public FPS As Long
Public LS As New cLandScape
Public Sky As New cSky
Public Road As New cRoad
Public Car(3) As cCar
Public CarPh(3) As cCarPhisic
Public TexFType As Long
Public StartPoint As D3DVECTOR
Dim OldTime As Double

Public Const DTime As Double = 1 / 60

Public Sub Main()
  Dim t As Double
  Dim i As Long

  Load frmMain
  Load frmD3D
  Do
    frmMain.Show
    DoEvents
    frmMain.WaitStart
    frmMain.Hide
    DoEvents
    If Running = False Then Exit Do

    frmD3D.Show
    DoEvents
    InitAll
    Do While Running
      t = QTime
      While t - OldTime > DTime
        DoEvents
        For i = 0 To AICnt
          If i = 0 Then
            CarPh(i).Driving
          Else
            CarPh(i).AIDriving
          End If
          CarPh(i).Moving
          Car(i).Update i
        Next i
        CameraControl 0
        OldTime = OldTime + DTime
        RaceTimer = RaceTimer + DTime
      Wend
      Sleep 0
      Render
      FPS = FPS + 1
      If Keyb(vbKeyEscape) Then Running = False
    Loop
    ClearAll
  Loop
  Unload frmMain
  Unload frmD3D
End Sub

Private Sub Render()
  Dim i As Long

  Dev.Clear D3DCLEAR_ZBUFFER, 0, 1, 0
  If Dev.BeginScene Then
    Sky.Draw CamPos
    LS.Draw
    Road.Draw
    ShadDraw
    For i = 0 To AICnt
      If i > 0 Or CamType > 0 Then Car(i).Draw i
    Next i
    ConsoleDraw
    Dev.EndScene
  End If
  Dev.Present
End Sub

Private Sub InitAll()
  Dim i As Long
  Dim s As Single

  Randomize Timer
  D3DInit frmD3D.hWnd
  DSInit frmD3D.hWnd

  MatrixPerspectiveFovLH mProj, Pi * 0.45, Aspect, 0.25, 5000
  Dev.SetRenderState D3DRS_BLENDOP, D3DBLENDOP_ADD
  If TexFType = 0 Then
    TexFilter 0, TexF_BiLinear
    TexFilter 1, TexF_BiLinear
    TexFilter 2, TexF_BiLinear
    TexFilter 3, TexF_BiLinear
    Keyb(vbKey1) = False
  ElseIf TexFType = 1 Then
    TexFilter 0, TexF_TriLinear
    TexFilter 1, TexF_TriLinear
    TexFilter 2, TexF_TriLinear
    TexFilter 3, TexF_TriLinear
  ElseIf TexFType = 2 Then
    TexFilter 0, TexF_Anisotropic, 2
    TexFilter 1, TexF_Anisotropic, 2
    TexFilter 2, TexF_Anisotropic, 2
    TexFilter 3, TexF_Anisotropic, 2
  ElseIf TexFType = 3 Then
    TexFilter 0, TexF_Anisotropic, 4
    TexFilter 1, TexF_Anisotropic, 4
    TexFilter 2, TexF_Anisotropic, 4
    TexFilter 3, TexF_Anisotropic, 4
  ElseIf TexFType = 4 Then
    TexFilter 0, TexF_Anisotropic, 8
    TexFilter 1, TexF_Anisotropic, 8
    TexFilter 2, TexF_Anisotropic, 8
    TexFilter 3, TexF_Anisotropic, 8
  End If

  QTimeInit
  ConsoleInit
  RaceTimer = -10
  LS.Init 10, 0.33
  Road.Init 10, 0.33
  LoadTrackData
  CarInit
  For i = 0 To AICnt
    Set Car(i) = New cCar
    Set CarPh(i) = New cCarPhisic
    Road.GetStart i, StartPoint, s
    CarPh(i).Init i, StartPoint.x + 2, StartPoint.z + i * 4 - 6
    CarPh(i).Angle = s
    TrackPos(i) = 0
  Next i
  StartPoint.y = LS.GetHeight(StartPoint.x, StartPoint.z) + 0.3
  For i = 0 To 3
    If frmMain.opKPP(i) Then CarPh(0).KPPType = i
  Next i
  For i = 0 To 255
    Keyb(i) = False
  Next i
  CameraInit 0
  OldTime = QTime
  TexInit
  ShadInit
  CreateTexFPS
  CreateTexMsg
  FinisFlag = False
  frmD3D.Timer1.Enabled = True
  frmD3D.Timer2.Enabled = True
End Sub

Private Sub ClearAll()
  Dim i As Long

  frmD3D.Timer1.Enabled = False
  frmD3D.Timer2.Enabled = False
  Set LS = Nothing
  Set Road = Nothing
  Set Sky = Nothing
  For i = 0 To AICnt
    Set Car(i) = Nothing
    Set CarPh(i) = Nothing
  Next i
  CarTerminate
  TexTerminate
  ConsoleTerminate
  DSTerminate
  D3DTerminate
  frmD3D.Hide
End Sub
