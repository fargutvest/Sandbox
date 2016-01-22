Attribute VB_Name = "modMap"
Option Explicit

Public Type MapVFormat
  Pos As D3DVECTOR
  tu As Single
  tv As Single
  Tang As D3DVECTOR
  Bnrm As D3DVECTOR
  Norm As D3DVECTOR
End Type

Dim vBuf As Direct3DVertexBuffer9
Dim iBuf As Direct3DIndexBuffer9
Dim Tex As Direct3DTexture9
Dim TexN As Direct3DTexture9
Dim PS As Direct3DPixelShader9
Dim VS As Direct3DVertexShader9
Dim vDecl As Direct3DVertexDeclaration

Dim Vert() As MapVFormat
Dim Ind() As Integer
Dim vSize As Long
Dim Sect() As cSector, sCnt As Long
Dim MapICnt As Long, MapVCnt As Long, MapTCnt As Long

Public Function SphereInMap(p As D3DVECTOR, ByVal r As Single, OutN As D3DVECTOR) As Boolean
Dim i As Long
  For i = 0 To sCnt - 1
    If Sect(i).SphereInSector(p, r, OutN) Then SphereInMap = True: Exit Function
  Next i
End Function

Public Function PointInMap(p As D3DVECTOR) As Boolean
Dim i As Long
  For i = 0 To sCnt - 1
    If Sect(i).PointInSector(p) Then PointInMap = True: Exit Function
  Next i
End Function

Public Function RayInMap(p As D3DVECTOR, d As D3DVECTOR, OutP As D3DVECTOR, OutN As D3DVECTOR) As Boolean
Dim i As Long, v As D3DVECTOR, vN As D3DVECTOR, vv As D3DVECTOR, L As Single, LL As Single, f As Boolean
  L = 100000000
  For i = 0 To sCnt - 1
    If Sect(i).RayInSector(p, d, v, vN) Then
      Vec3Subtract vv, v, p
      LL = Vec3LengthSq(vv)
      If L > LL Then
        L = LL
        OutP = v
        OutN = vN
        f = True
      End If
    End If
  Next i
  If Not f Then
    Vec3Scale v, d, 10000
    Vec3Add OutP, p, v
  End If
  RayInMap = f
End Function

Public Sub MapDraw()
Dim Mtrx As D3DMATRIX
Dim v4 As D3DVECTOR4
  MatrixIdentity Mtrx
  Dev.SetTransform D3DTS_WORLD, Mtrx
  MatrixMultiply Mtrx, mView, mProj
  MatrixTranspose Mtrx, Mtrx
  Dev.SetVertexShaderConstantF 0, VarPtr(Mtrx), 4

  v4 = Vec4(Player(0).Pos.x, Player(0).Pos.y, Player(0).Pos.z, 0)
  Dev.SetPixelShaderConstantF 4, VarPtr(v4), 1
  v4 = Vec4(2000, 4000, 9000, 0)
  Dev.SetPixelShaderConstantF 5, VarPtr(v4), 1
  v4 = Vec4(-1000, -1000, -14000, 0)
  Dev.SetPixelShaderConstantF 6, VarPtr(v4), 1

  Dev.SetStreamSource 0, vBuf, 0, vSize
  Dev.SetIndices iBuf
  Dev.SetVertexDeclaration vDecl
  Dev.SetVertexShader VS
  Dev.SetRenderState D3DRS_LIGHTING, D3D_TRUE
  Dev.SetRenderState D3DRS_ZENABLE, D3DZB_TRUE
  Dev.SetRenderState D3DRS_ALPHABLENDENABLE, D3D_FALSE
  Dev.SetRenderState D3DRS_CULLMODE, D3DCULL_CCW
  Dev.SetTexture 0, Tex
  Dev.SetTexture 1, TexN
  Dev.SetPixelShader PS

  Dev.DrawIndexedPrimitive D3DPT_TRIANGLELIST, 0, 0, MapVCnt, 0, MapTCnt

  Dev.SetVertexShader Nothing
  Dev.SetPixelShader Nothing
End Sub

Public Sub MapLoad()
Dim sI As Long
Dim nf As Integer
  For sI = 0 To sCnt - 1
    Set Sect(sI) = Nothing
  Next sI
  nf = FreeFile
  Open "data\test.map" For Binary As #nf
  Get #nf, , sCnt
  ReDim Sect(sCnt - 1)
  For sI = 0 To sCnt - 1
    Set Sect(sI) = New cSector
    Sect(sI).Load nf, Vert(), Ind(), MapICnt, MapVCnt
  Next sI
  Close #nf

  vSize = Len(Vert(0))
  Set vBuf = Dev.CreateVertexBuffer(MapVCnt * vSize, 0, D3DFVF_XYZ, D3DPOOL_DEFAULT)
  vBuf.SetData 0, MapVCnt * vSize, VarPtr(Vert(0)), 0

  Set iBuf = Dev.CreateIndexBuffer(MapICnt * Len(Ind(0)), 0, D3DFMT_INDEX16, D3DPOOL_DEFAULT)
  iBuf.SetData 0, MapICnt * Len(Ind(0)), VarPtr(Ind(0)), 0
  MapTCnt = MapICnt \ 3

  Set Tex = CreateTextureFromFile(Dev, App.Path & "\data\tex1.jpg")
  Set TexN = CreateTextureFromFile(Dev, App.Path & "\data\tex1n.jpg")
  VS_Create
  PS_Create
End Sub

Private Sub VS_Create()
Dim vsDecl(5) As D3DVERTEXELEMENT9
  vsDecl(0) = VertexElement(0, 0, D3DDECLTYPE_FLOAT3, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_POSITION, 0)
  vsDecl(1) = VertexElement(0, 12, D3DDECLTYPE_FLOAT2, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_TEXCOORD, 0)
  vsDecl(2) = VertexElement(0, 20, D3DDECLTYPE_FLOAT3, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_TANGENT, 0)
  vsDecl(3) = VertexElement(0, 32, D3DDECLTYPE_FLOAT3, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_BINORMAL, 0)
  vsDecl(4) = VertexElement(0, 44, D3DDECLTYPE_FLOAT3, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_NORMAL, 0)
  vsDecl(5) = VertexElementEnd

  Set vDecl = Dev.CreateVertexDeclaration(VarPtr(vsDecl(0)))
  Set VS = Dev.CreateVertexShaderFromFile(App.Path & "\ShaderCompiler\Out\Map.vsh.shader")
End Sub

Private Sub PS_Create()
  Set PS = Dev.CreatePixelShaderFromFile(App.Path & "\ShaderCompiler\Out\Map.psh.shader")
End Sub

Public Function MapVertex(ByVal x As Single, ByVal y As Single, ByVal z As Single, n As D3DVECTOR, ByVal tu As Single, ByVal tv As Single) As MapVFormat
Dim Tang As D3DVECTOR
Dim Bnrm As D3DVECTOR
Dim Norm As D3DVECTOR
  MapVertex.Pos = Vec3(x, y, z)
  MapVertex.tu = tu
  MapVertex.tv = tv
  Select Case n.y
    Case Is < -0.9
      Tang = Vec3(1, 0, 0)
      Bnrm = Vec3(0, 0, -1)
      Norm = Vec3(0, -1, 0)
    Case Is > 0.9
      Tang = Vec3(1, 0, 0)
      Bnrm = Vec3(0, 0, 1)
      Norm = Vec3(0, 1, 0)
    Case Else
      Norm = n
      Bnrm = Vec3(0, 1, 0)
      Vec3Cross Tang, Norm, Bnrm
  End Select
  MapVertex.Tang = Tang
  MapVertex.Bnrm = Bnrm
  MapVertex.Norm = Norm
End Function

Public Sub MapTerminate()
Dim n As Long
  For n = 0 To sCnt - 1
    Set Sect(n) = Nothing
  Next n
  Set VS = Nothing
  Set PS = Nothing
  Set vDecl = Nothing
  Set vBuf = Nothing
  Set iBuf = Nothing
  Set Tex = Nothing
  Set TexN = Nothing
End Sub
