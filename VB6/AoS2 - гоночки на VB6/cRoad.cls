VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cRoad"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Type vFormat
  Pos As D3DVECTOR
  tu0 As Single
  tv0 As Single
End Type

Dim vBufR As Direct3DVertexBuffer9
Dim iBufR As Direct3DIndexBuffer9
Dim vBufW As Direct3DVertexBuffer9
Dim iBufW As Direct3DIndexBuffer9
Dim PSRoad As Direct3DPixelShader9
Dim VSRoad As Direct3DVertexShader9
Dim PSWall As Direct3DPixelShader9
Dim VSWall As Direct3DVertexShader9
Dim vDecl As Direct3DVertexDeclaration

Dim vRoad() As D3DVECTOR
Dim vSize As Long
Dim vRCnt As Long
Dim iRCnt As Long
Dim tRCnt As Long
Dim vWCnt As Long
Dim iWCnt As Long
Dim tWCnt As Long

Private Const FVF = D3DFVF_XYZ Or D3DFVF_TEX1

Friend Function GetRoadPos(ByVal Ind As Long) As D3DVECTOR2
  GetRoadPos = Vec2(vRoad(Ind).x, vRoad(Ind).z)
End Function

Friend Sub GetPlace(ByVal Ind As Long, InPos As D3DVECTOR2, OutPos As D3DVECTOR2, OutDir As D3DVECTOR2)
  Dim v As D3DVECTOR2
  Dim l As Single
  Dim t As Single
  Dim iOut As Long
  Dim i As Long
  Dim i1 As Long
  Dim i2 As Long

  iOut = (NearInd(Ind) + 2) Mod RoadLen
  Vec2Subtract v, Vec2(vRoad(iOut).x, vRoad(iOut).z), InPos
  l = Vec2LengthSq(v)
  For i = NearInd(Ind) - 2 + RoadLen To NearInd(Ind) + 1 + RoadLen
    i1 = i Mod RoadLen
    Vec2Subtract v, Vec2(vRoad(i1).x, vRoad(i1).z), InPos
    t = Vec2LengthSq(v)
    If l > t Then l = t: iOut = i1
  Next i
  OutPos = Vec2(vRoad(iOut).x, vRoad(iOut).z)
  i1 = (iOut + 1) Mod RoadLen
  i2 = (iOut + RoadLen - 1) Mod RoadLen
  Vec2Subtract OutDir, Vec2(vRoad(i1).x, vRoad(i1).z), Vec2(vRoad(i2).x, vRoad(i2).z)
  If NearInd(Ind) <> iOut Then NearInd(Ind) = iOut

  i = RoadLen - ((TrackPos(Ind) + NearInd(Ind)) Mod RoadLen)
  If i < 25 Then TrackPos(Ind) = TrackPos(Ind) + i
  If FinisFlag = False Then
    If Ind = 0 And GameMode = 1 Then
      If TrackPos(Ind) > RoadLen Then FinisFlag = True: CreateTexFinal: SoundFinal.Play
    ElseIf Ind <> 0 And GameMode = 2 Then
      If TrackPos(Ind) > RoadLen Then FinisFlag = True: CreateTexFinal: SoundFinal.Play
    End If
  End If
End Sub

Friend Sub GetStart(ByVal Ind As Long, Pos As D3DVECTOR, Angle As Single)
  Dim v As D3DVECTOR

  NearInd(Ind) = 0
  Pos = vRoad(0)
  Vec3Subtract v, vRoad(1), vRoad(0)
  Angle = -Atn(v.x / v.z)
End Sub

Public Sub Draw()
  Dim Mtrx As D3DMATRIX
  Dim v4 As D3DVECTOR4

  MatrixMultiply Mtrx, mView, mProj
  MatrixTranspose Mtrx, Mtrx
  Dev.SetVertexShaderConstantF 0, VarPtr(Mtrx), 4

  Dev.SetRenderState D3DRS_ZENABLE, D3DZB_TRUE
  Dev.SetRenderState D3DRS_ALPHABLENDENABLE, D3D_TRUE
  Dev.SetRenderState D3DRS_SRCBLEND, D3DBLEND_SRCALPHA
  Dev.SetRenderState D3DRS_DESTBLEND, D3DBLEND_INVSRCALPHA

  Dev.SetVertexDeclaration vDecl

  Dev.SetRenderState D3DRS_CULLMODE, D3DCULL_CCW
  Dev.SetStreamSource 0, vBufR, 0, vSize
  Dev.SetIndices iBufR
  Dev.SetTexture 0, TexRoad
  Dev.SetTexture 1, TexTarm
  Dev.SetVertexShader VSRoad
  Dev.SetPixelShader PSRoad
  Dev.DrawIndexedPrimitive D3DPT_TRIANGLELIST, 0, 0, vRCnt, 0, tRCnt
  
  Dev.SetRenderState D3DRS_CULLMODE, D3DCULL_NONE
  Dev.SetStreamSource 0, vBufW, 0, vSize
  Dev.SetIndices iBufW
  Dev.SetTexture 0, TexWall
  Dev.SetVertexShader VSWall
  Dev.SetPixelShader PSWall
  Dev.DrawIndexedPrimitive D3DPT_TRIANGLELIST, 0, 0, vWCnt, 0, tWCnt
  
  Dev.SetRenderState D3DRS_ALPHABLENDENABLE, D3D_FALSE
End Sub

Public Sub Init(ByVal hScale As Single, ByVal vScale As Single)
  Dim IndR() As Integer
  Dim VertR() As vFormat
  Dim IndW() As Integer
  Dim VertW() As vFormat
  Dim v1 As D3DVECTOR, v2 As D3DVECTOR
  Dim vUp As D3DVECTOR, vD As D3DVECTOR, vDir As D3DVECTOR
  Dim i As Long
  Dim tu As Single
  Dim tuW1 As Single
  Dim tuW2 As Single
  Dim nf As Integer

  nf = FreeFile
  Open App.Path & "\data\1.rmap" For Binary As #nf
    Get #nf, , RoadLen
    ReDim vRoad(RoadLen - 1)
    ReDim Track(RoadLen - 1)
    Get #nf, , vRoad()
  Close #nf
  vRCnt = (RoadLen + 1) * 4
  tRCnt = RoadLen * 6
  iRCnt = tRCnt * 3
  ReDim VertR(vRCnt - 1)
  ReDim IndR(iRCnt - 1)
  vWCnt = (RoadLen + 1) * 4
  tWCnt = RoadLen * 4
  iWCnt = tWCnt * 3
  ReDim VertW(vWCnt - 1)
  ReDim IndW(iWCnt - 1)

  vUp = Vec3(0, 1, 0)
  For i = 0 To RoadLen - 1
    vRoad(i).x = vRoad(i).x * hScale
    vRoad(i).y = vRoad(i).y * vScale
    vRoad(i).z = vRoad(i).z * hScale
  Next i
  For i = 0 To RoadLen - 1
    v1 = vRoad((i + RoadLen - 1) Mod RoadLen)
    v2 = vRoad((i + 1) Mod RoadLen)
    Vec3Subtract vDir, v2, v1
    Vec3Cross vD, vDir, vUp
    Vec3Normalize vD, vD
    Vec3Scale vD, vD, 6
    Vec3Add VertR(i * 4 + 1).Pos, vRoad(i), vD
    Vec3Add VertR(i * 4 + 0).Pos, VertR(i * 4 + 1).Pos, vD
    Vec3Subtract VertR(i * 4 + 2).Pos, vRoad(i), vD
    Vec3Subtract VertR(i * 4 + 3).Pos, VertR(i * 4 + 2).Pos, vD
    VertR(i * 4 + 0).Pos.y = VertR(i * 4 + 0).Pos.y + 0.2
    VertR(i * 4 + 1).Pos.y = VertR(i * 4 + 1).Pos.y + 0.3
    VertR(i * 4 + 2).Pos.y = VertR(i * 4 + 2).Pos.y + 0.3
    VertR(i * 4 + 3).Pos.y = VertR(i * 4 + 3).Pos.y + 0.2
    VertR(i * 4 + 0).tu0 = tu
    VertR(i * 4 + 0).tv0 = 0
    VertR(i * 4 + 1).tu0 = tu
    VertR(i * 4 + 1).tv0 = 0.25
    VertR(i * 4 + 2).tu0 = tu
    VertR(i * 4 + 2).tv0 = 0.75
    VertR(i * 4 + 3).tu0 = tu
    VertR(i * 4 + 3).tv0 = 1
    tu = tu + Vec3Length(vDir) * 0.015

    Vec3Scale vD, vD, 0.5
    Vec3Add VertW(i * 4 + 0).Pos, VertR(i * 4 + 0).Pos, vD
    VertW(i * 4 + 1).Pos = VertW(i * 4 + 0).Pos
    Vec3Subtract VertW(i * 4 + 2).Pos, VertR(i * 4 + 3).Pos, vD
    VertW(i * 4 + 3).Pos = VertW(i * 4 + 2).Pos
    VertW(i * 4 + 0).Pos.y = LS.GetHeight(VertW(i * 4 + 0).Pos.x, VertW(i * 4 + 0).Pos.z) - 0.2
    VertW(i * 4 + 1).Pos.y = VertW(i * 4 + 1).Pos.y + 1.2
    VertW(i * 4 + 2).Pos.y = LS.GetHeight(VertW(i * 4 + 2).Pos.x, VertW(i * 4 + 2).Pos.z) - 0.2
    VertW(i * 4 + 3).Pos.y = VertW(i * 4 + 3).Pos.y + 1.2
  Next i
  VertR(RoadLen * 4 + 0) = VertR(0)
  VertR(RoadLen * 4 + 1) = VertR(1)
  VertR(RoadLen * 4 + 2) = VertR(2)
  VertR(RoadLen * 4 + 3) = VertR(3)
  VertR(RoadLen * 4 + 0).tu0 = tu
  VertR(RoadLen * 4 + 0).tv0 = 0
  VertR(RoadLen * 4 + 1).tu0 = tu
  VertR(RoadLen * 4 + 1).tv0 = 0.25
  VertR(RoadLen * 4 + 2).tu0 = tu
  VertR(RoadLen * 4 + 2).tv0 = 0.75
  VertR(RoadLen * 4 + 3).tu0 = tu
  VertR(RoadLen * 4 + 3).tv0 = 1
  VertW(RoadLen * 4 + 0) = VertW(0)
  VertW(RoadLen * 4 + 1) = VertW(1)
  VertW(RoadLen * 4 + 2) = VertW(2)
  VertW(RoadLen * 4 + 3) = VertW(3)
  VertW(0).tv0 = 1
  VertW(2).tv0 = 1
  tuW1 = 0.1
  tuW2 = 0.1
  For i = 1 To RoadLen
    v1 = vRoad((i + RoadLen - 1) Mod RoadLen)
    v2 = vRoad((i + 1) Mod RoadLen)
    Vec3Subtract v1, VertW(i * 4 + 0).Pos, VertW(i * 4 - 4).Pos
    Vec3Subtract v2, VertW(i * 4 + 2).Pos, VertW(i * 4 - 2).Pos
    tuW1 = tuW1 + Vec3Length(v1) * 0.5
    tuW2 = tuW2 + Vec3Length(v2) * 0.5
    VertW(i * 4 + 0).tu0 = tuW1
    VertW(i * 4 + 0).tv0 = 1
    VertW(i * 4 + 1).tu0 = tuW1
    VertW(i * 4 + 1).tv0 = 0
    VertW(i * 4 + 2).tu0 = tuW2
    VertW(i * 4 + 2).tv0 = 1
    VertW(i * 4 + 3).tu0 = tuW2
    VertW(i * 4 + 3).tv0 = 0
  Next i

  For i = 0 To RoadLen - 1
    IndR(i * 18 + 0) = i * 4 + 0
    IndR(i * 18 + 1) = i * 4 + 4
    IndR(i * 18 + 2) = i * 4 + 5
    IndR(i * 18 + 3) = i * 4 + 0
    IndR(i * 18 + 4) = i * 4 + 5
    IndR(i * 18 + 5) = i * 4 + 1

    IndR(i * 18 + 6) = i * 4 + 1
    IndR(i * 18 + 7) = i * 4 + 5
    IndR(i * 18 + 8) = i * 4 + 6
    IndR(i * 18 + 9) = i * 4 + 1
    IndR(i * 18 + 10) = i * 4 + 6
    IndR(i * 18 + 11) = i * 4 + 2

    IndR(i * 18 + 12) = i * 4 + 2
    IndR(i * 18 + 13) = i * 4 + 6
    IndR(i * 18 + 14) = i * 4 + 7
    IndR(i * 18 + 15) = i * 4 + 2
    IndR(i * 18 + 16) = i * 4 + 7
    IndR(i * 18 + 17) = i * 4 + 3

    IndW(i * 12 + 0) = i * 4 + 0
    IndW(i * 12 + 1) = i * 4 + 1
    IndW(i * 12 + 2) = i * 4 + 4
    IndW(i * 12 + 3) = i * 4 + 1
    IndW(i * 12 + 4) = i * 4 + 5
    IndW(i * 12 + 5) = i * 4 + 4

    IndW(i * 12 + 6) = i * 4 + 6
    IndW(i * 12 + 7) = i * 4 + 7
    IndW(i * 12 + 8) = i * 4 + 2
    IndW(i * 12 + 9) = i * 4 + 7
    IndW(i * 12 + 10) = i * 4 + 3
    IndW(i * 12 + 11) = i * 4 + 2
  Next i

  vSize = Len(VertR(0))

  Set vBufR = Dev.CreateVertexBuffer(vRCnt * vSize, 0, FVF, D3DPOOL_DEFAULT)
  vBufR.SetData 0, vRCnt * vSize, VarPtr(VertR(0)), 0

  Set iBufR = Dev.CreateIndexBuffer(iRCnt * Len(IndR(0)), 0, D3DFMT_INDEX16, D3DPOOL_DEFAULT)
  iBufR.SetData 0, iRCnt * Len(IndR(0)), VarPtr(IndR(0)), 0

  Set vBufW = Dev.CreateVertexBuffer(vWCnt * vSize, 0, FVF, D3DPOOL_DEFAULT)
  vBufW.SetData 0, vWCnt * vSize, VarPtr(VertW(0)), 0

  Set iBufW = Dev.CreateIndexBuffer(iWCnt * Len(IndW(0)), 0, D3DFMT_INDEX16, D3DPOOL_DEFAULT)
  iBufW.SetData 0, iWCnt * Len(IndW(0)), VarPtr(IndW(0)), 0

  VS_Create
  PS_Create
End Sub

Private Sub VS_Create()
  Dim vsDecl(2) As D3DVERTEXELEMENT9

  vsDecl(0) = VertexElement(0, 0, D3DDECLTYPE_FLOAT3, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_POSITION, 0)
  vsDecl(1) = VertexElement(0, 12, D3DDECLTYPE_FLOAT2, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_TEXCOORD, 0)
  vsDecl(2) = VertexElementEnd

  Set vDecl = Dev.CreateVertexDeclaration(VarPtr(vsDecl(0)))
  Set VSRoad = Dev.CreateVertexShaderFromFile(App.Path & "\ShaderCompiler\Out\Road.vsh.shader")
  Set VSWall = Dev.CreateVertexShaderFromFile(App.Path & "\ShaderCompiler\Out\Wall.vsh.shader")
End Sub

Private Sub PS_Create()
  Set PSRoad = Dev.CreatePixelShaderFromFile(App.Path & "\ShaderCompiler\Out\Road.psh.shader")
  Set PSWall = Dev.CreatePixelShaderFromFile(App.Path & "\ShaderCompiler\Out\Wall.psh.shader")
End Sub

Private Sub Class_Terminate()
  Set PSRoad = Nothing
  Set VSRoad = Nothing
  Set PSWall = Nothing
  Set VSWall = Nothing
  Set vDecl = Nothing
  Set vBufR = Nothing
  Set iBufR = Nothing
  Set vBufW = Nothing
  Set iBufW = Nothing
End Sub
