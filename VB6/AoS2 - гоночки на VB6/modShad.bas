Attribute VB_Name = "modShad"
Option Explicit

Private Type vFormat
  Pos As D3DVECTOR
  tu0 As Single
  tv0 As Single
End Type

Dim Vert(3) As vFormat
Dim vSize As Long

Public Sub ShadDraw()
  Dim i As Long
  Dim n As Long
  Dim v As D3DVECTOR2

  Dev.SetVertexShader Nothing
  Dev.SetPixelShader Nothing

  Dev.SetRenderState D3DRS_SRCBLEND, D3DBLEND_ZERO
  Dev.SetRenderState D3DRS_DESTBLEND, D3DBLEND_SRCCOLOR
  Dev.SetRenderState D3DRS_ALPHABLENDENABLE, D3D_TRUE
  Dev.SetRenderState D3DRS_CULLMODE, D3DCULL_CCW
  Dev.SetRenderState D3DRS_ZENABLE, D3DZB_FALSE
  Dev.SetRenderState D3DRS_ZWRITEENABLE, D3D_FALSE

  Dev.SetTextureStageState 0, D3DTSS_ALPHAOP, D3DTOP_SELECTARG1
  Dev.SetTextureStageState 0, D3DTSS_ALPHAARG1, D3DTA_TEXTURE
  Dev.SetTextureStageState 0, D3DTSS_COLOROP, D3DTOP_SELECTARG1
  Dev.SetTextureStageState 0, D3DTSS_COLORARG1, D3DTA_TEXTURE
  Dev.SetTextureStageState 1, D3DTSS_COLOROP, D3DTOP_DISABLE

  Dev.SetFVF D3DFVF_XYZ Or D3DFVF_TEX1
  Dev.SetTexture 0, TexShad
  MatrixIdentity mWorld
  Dev.SetTransform D3DTS_WORLD, mWorld
  Dev.SetTransform D3DTS_VIEW, mView
  Dev.SetTransform D3DTS_PROJECTION, mProj

  For i = 0 To AICnt
    If i > 0 Or CamType > 0 Then
      Vec2Subtract v, Vec2(CarPh(i).PosX, CarPh(i).PosZ), Vec2(CamPos.x, CamPos.z)
      If Vec2LengthSq(v) < 10000 Then
        For n = 0 To 3
          v = CarPh(i).GetPoint(n)
          Vert(n).Pos.x = v.x
          Vert(n).Pos.z = v.y
          Vert(n).Pos.y = LS.GetHeight(v.x, v.y) + 0.25
        Next n
        Dev.DrawPrimitiveUp D3DPT_TRIANGLEFAN, 2, VarPtr(Vert(0)), vSize
      End If
    End If
  Next i
  Vec2Subtract v, Vec2(StartPoint.x, StartPoint.z), Vec2(CamPos.x, CamPos.z)
  If Vec2LengthSq(v) < 16000 Then
    Dev.SetRenderState D3DRS_SRCBLEND, D3DBLEND_ONE
    Dev.SetRenderState D3DRS_DESTBLEND, D3DBLEND_ONE
    Vert(0).Pos.x = StartPoint.x - 2
    Vert(0).Pos.z = StartPoint.z - 8
    Vert(0).Pos.y = StartPoint.y
    Vert(1).Pos.x = StartPoint.x - 2
    Vert(1).Pos.z = StartPoint.z + 8
    Vert(1).Pos.y = StartPoint.y
    Vert(2).Pos.x = StartPoint.x + 2
    Vert(2).Pos.z = StartPoint.z + 8
    Vert(2).Pos.y = StartPoint.y
    Vert(3).Pos.x = StartPoint.x + 2
    Vert(3).Pos.z = StartPoint.z - 8
    Vert(3).Pos.y = StartPoint.y
    Dev.SetTexture 0, TexStart
    Dev.DrawPrimitiveUp D3DPT_TRIANGLEFAN, 2, VarPtr(Vert(0)), vSize
  End If

  Dev.SetRenderState D3DRS_ZWRITEENABLE, D3D_TRUE
End Sub

Public Sub ShadInit()
  vSize = Len(Vert(0))
  Vert(0).tu0 = 0
  Vert(0).tv0 = 0
  Vert(1).tu0 = 1
  Vert(1).tv0 = 0
  Vert(2).tu0 = 1
  Vert(2).tv0 = 1
  Vert(3).tu0 = 0
  Vert(3).tv0 = 1
End Sub
