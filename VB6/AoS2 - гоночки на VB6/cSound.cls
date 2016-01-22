VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cSound"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Dim DSB() As DirectSoundBuffer8
Dim DSBCnt As Long

Public Sub Play(Optional ByVal Ind As Long = 0)
  DSB(Ind).SetCurrentPosition 0
  DSB(Ind).Play
End Sub

Public Sub PlayLoop(Optional ByVal Ind As Long = 0)
  DSB(Ind).SetCurrentPosition 0
  DSB(Ind).PlayLooping
End Sub

Public Sub StopPlay(Optional ByVal Ind As Long = 0)
  DSB(Ind).StopPlaying
End Sub

Public Sub SetFreq(ByVal Freq As Long, Optional ByVal Ind As Long = 0)
  DSB(Ind).SetFrequency Freq
End Sub

Public Sub SetPan(ByVal Pan As Long, Optional ByVal Ind As Long = 0)
  DSB(Ind).SetPan Pan
End Sub

Public Sub SetVol(ByVal Vol As Long, Optional ByVal Ind As Long = 0)
  DSB(Ind).SetVolume Vol
End Sub

Public Sub Init(fName As String, Optional ByVal Cnt As Long = 1)
  Dim ar() As Integer
  Dim nf As Integer
  Dim sz As Long
  Dim i As Long

  DSBCnt = Cnt
  ReDim DSB(DSBCnt - 1)
  nf = FreeFile
  Open fName For Binary As #nf
  Get #nf, &H29, sz
  ReDim ar(sz \ 2 - 1)
  Get #nf, &H2D, ar()
  Close #nf

  Set DSB(0) = DS8.CreateSoundBuffer(1, 8, 22050, sz, DSBCAPS_CTRLVOLUME Or DSBCAPS_CTRLPAN Or DSBCAPS_CTRLFREQUENCY)
  DSB(0).SetData 0, sz, VarPtr(ar(0))
  For i = 1 To DSBCnt - 1
    Set DSB(i) = DS8.DuplicateSoundBuffer(DSB(0))
  Next i
End Sub

Private Sub Class_Terminate()
  Dim i As Long
  
  For i = 0 To DSBCnt - 1
    DSB(i).StopPlaying
    Set DSB(i) = Nothing
  Next i
End Sub
