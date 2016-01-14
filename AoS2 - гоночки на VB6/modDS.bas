Attribute VB_Name = "modDS"
Option Explicit

Public DS8 As DirectSound8
Public SoundStart As New cSound
Public SoundMotor As New cSound
Public SoundGrnd As New cSound
Public SoundGear As New cSound
Public SoundCrush As New cSound
Public SoundBreak As New cSound
Public SoundBeep As New cSound
Public SoundFinal As New cSound

Public Sub DSInit(hWnd As Long)
  Set DS8 = DirectX8.CreateDirectSound
  DS8.SetCooperativeLevel hWnd, DSSCL_NORMAL

  SoundStart.Init App.Path & "\Sound\Start.wav", 4
  SoundMotor.Init App.Path & "\Sound\Motor.wav", 4
  SoundGrnd.Init App.Path & "\Sound\Grnd.wav", 4
  SoundGear.Init App.Path & "\Sound\Gear.wav", 4
  SoundCrush.Init App.Path & "\Sound\Crush.wav", 4
  SoundBreak.Init App.Path & "\Sound\Break.wav", 4
  SoundBeep.Init App.Path & "\Sound\Beep.wav"
  SoundFinal.Init App.Path & "\Sound\Final.wav"
End Sub

Public Sub DSTerminate()
  Set SoundStart = Nothing
  Set SoundMotor = Nothing
  Set SoundGrnd = Nothing
  Set SoundGear = Nothing
  Set SoundCrush = Nothing
  Set SoundBreak = Nothing
  Set SoundBeep = Nothing
  Set SoundFinal = Nothing

  Set DS8 = Nothing
End Sub

