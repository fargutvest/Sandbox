Attribute VB_Name = "modCamera"
Option Explicit

Public CamPos As D3DVECTOR
Public CamType As Long
Public CamV2Pos As D3DVECTOR2
Public CamV2NormDir As D3DVECTOR2
Dim Sp As Long
Dim KPP As Long
Dim cDist As Single

Public Sub CameraInit(ByVal Ind As Long)
  CamType = 1
  cDist = 6
  CameraChange Ind
End Sub

Private Sub CameraChange(ByVal Ind As Long)
  Dim cPos As D3DVECTOR
  Dim cDir As D3DVECTOR

  cPos = Vec3(CarPh(Ind).PosX, LS.GetHeight(CarPh(Ind).PosX, CarPh(Ind).PosZ) + 0.8, CarPh(Ind).PosZ)
  cDir = Vec3(Sin(CarPh(Ind).Angle), 0, Cos(CarPh(Ind).Angle))
  Vec3Scale cDir, cDir, cDist
  cDir.y = cDist * -0.1 - 1
  Vec3Subtract CamPos, cPos, cDir
End Sub

Public Sub CameraControl(ByVal Ind As Long)
  Dim SinA As Single, CosA As Single
  Dim cPos As D3DVECTOR
  Dim cDir As D3DVECTOR
  Dim v As D3DVECTOR

  If Keyb(vbKeyC) Then
    CamType = (CamType + 1) Mod 3
    Keyb(vbKeyC) = False
    If CamType = 1 Then cDist = 6 Else cDist = 9
    If CamType > 0 Then CameraChange Ind
  End If

  If CamType = 0 Then
    SinA = Sin(CarPh(Ind).Angle)
    CosA = Cos(CarPh(Ind).Angle)
    cPos = Vec3(CarPh(Ind).PosX, LS.GetHeight(CarPh(Ind).PosX, CarPh(Ind).PosZ) + 1.5, CarPh(Ind).PosZ)
    cDir = Vec3(2.3 * SinA, 2.3 * Sin(CarPh(Ind).carDiffFB), 2.3 * CosA)
    Vec3Add CamPos, cPos, cDir
    Vec3Add cDir, CamPos, cDir
    MatrixLookAtLH mView, CamPos, cDir, Vec3(CosA * Sin(CarPh(Ind).carDiffLR), 1, -SinA * Sin(CarPh(Ind).carDiffLR))
  Else
    cPos = Vec3(CarPh(Ind).PosX, LS.GetHeight(CarPh(Ind).PosX, CarPh(Ind).PosZ) + 0.8, CarPh(Ind).PosZ)
    Vec3Add v, cPos, Vec3(0, cDist * 0.1 + 1, 0)
    Vec3Lerp CamPos, CamPos, v, 2 * DTime * CarPh(Ind).Speed / cDist
    Vec3Add cPos, cPos, Vec3(0, 1.2, 0)
    MatrixLookAtLH mView, CamPos, cPos, Vec3(0, 1, 0)
  End If

  CamV2Pos = Vec2(CamPos.x, CamPos.z)
  Vec2Subtract CamV2NormDir, Vec2(CarPh(Ind).PosX, CarPh(Ind).PosZ), CamV2Pos
  Vec2Normalize CamV2NormDir, CamV2NormDir
End Sub
