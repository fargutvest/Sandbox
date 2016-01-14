Attribute VB_Name = "modAI"
Option Explicit

Public Type TrackData
  Diff As Single
  Angle As Single
  Speed As Single
End Type

Public Track() As TrackData
Public RoadLen As Long
Public RaceTimer As Single
Public FinisFlag As Boolean
Public NearInd(3) As Long
Public TrackPos(3) As Long
Public GameMode As Long
Public AICnt As Long

Public Sub AddTrackData(vPos As D3DVECTOR2, vDir As D3DVECTOR2)
  Dim Ind As Long
  Dim v As D3DVECTOR2

  Ind = NearInd(0)
  Vec2Subtract v, Vec2(CarPh(0).PosX, CarPh(0).PosZ), vPos
  Track(Ind).Diff = Vec2CCW(vDir, v)
  Track(Ind).Angle = CarPh(0).Angle
  Track(Ind).Speed = CarPh(0).Speed
End Sub

Public Sub SaveTrackData()
  Dim nf As Integer
  nf = FreeFile
  Open App.Path & "\data\TrackData.bin" For Binary As #nf
    Put #nf, , Track()
  Close #nf
End Sub

Public Sub LoadTrackData()
  Dim nf As Integer
  nf = FreeFile
  Open App.Path & "\data\TrackData.bin" For Binary As #nf
    Get #nf, , Track()
  Close #nf
End Sub
