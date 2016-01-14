VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cLandScape"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Type vFormat
  Pos As D3DVECTOR
End Type

Dim vBuf As Direct3DVertexBuffer9
Dim iBuf As Direct3DIndexBuffer9
Dim PS As Direct3DPixelShader9
Dim VS As Direct3DVertexShader9
Dim vDecl As Direct3DVertexDeclaration

Dim vHeight() As Single
Dim vSize As Long
Dim vCnt As Long
Dim iCnt As Long
Dim tCnt As Long
Dim InvHScale As Single

Private Const FVF = D3DFVF_XYZ

Public Sub Draw()
  Dim Mtrx As D3DMATRIX
  Dim v4 As D3DVECTOR4

  MatrixMultiply Mtrx, mView, mProj
  MatrixTranspose Mtrx, Mtrx
  Dev.SetVertexShaderConstantF 0, VarPtr(Mtrx), 4
  Dev.SetRenderState D3DRS_CULLMODE, D3DCULL_CCW
  Dev.SetRenderState D3DRS_ZENABLE, D3DZB_TRUE
  Dev.SetRenderState D3DRS_ZWRITEENABLE, D3D_TRUE
  Dev.SetRenderState D3DRS_ALPHABLENDENABLE, D3D_FALSE

  Dev.SetStreamSource 0, vBuf, 0, vSize
  Dev.SetIndices iBuf
  Dev.SetTexture 0, TexMask
  Dev.SetTexture 1, TexGrass
  Dev.SetTexture 2, TexGround

  Dev.SetVertexDeclaration vDecl
  Dev.SetVertexShader VS
  Dev.SetPixelShader PS
  Dev.DrawIndexedPrimitive D3DPT_TRIANGLELIST, 0, 0, vCnt, 0, tCnt
End Sub

Public Function AngleLR(ByVal x As Single, ByVal z As Single) As Single
  AngleLR = Atn((GetHeight(x + 5, z) - GetHeight(x - 5, z)) * 0.1)
End Function

Public Function AngleFB(ByVal x As Single, ByVal z As Single) As Single
  AngleFB = Atn((GetHeight(x, z - 5) - GetHeight(x, z + 5)) * 0.1)
End Function

Public Function GetHeight(ByVal x As Single, ByVal z As Single) As Single
Dim xx As Single, zz As Single, ix As Long, iz As Long, i As Long
  xx = x * InvHScale + 64
  zz = z * InvHScale + 64
  ix = Int(xx)
  iz = Int(zz)
  xx = xx - ix
  zz = zz - iz
  If ix < 0 Or ix >= 128 Then GetHeight = 0: Exit Function
  If iz < 0 Or iz >= 128 Then GetHeight = 0: Exit Function
  i = ix + iz * 129
  GetHeight = vHeight(i) * (1 - xx) * (1 - zz) + vHeight(i + 1) * xx * (1 - zz) + vHeight(i + 129) * (1 - xx) * zz + vHeight(i + 130) * xx * zz
End Function

Public Sub Init(ByVal hScale As Single, ByVal vScale As Single)
  Dim Ind() As Integer
  Dim Vert() As vFormat
  Dim s() As Single
  Dim x As Long, z As Long
  Dim nf As Integer

  vCnt = 129 * 129
  tCnt = 128 * 128 * 2&
  iCnt = tCnt * 3
  InvHScale = 1 / hScale
  ReDim s(vCnt - 1)
  ReDim Vert(vCnt - 1)
  ReDim vHeight(vCnt - 1)
  ReDim Ind(iCnt - 1)

  nf = FreeFile
  Open App.Path & "\data\1.smap" For Binary As #nf
    Get #nf, , s()
  Close #nf
  For z = 0 To 128
    For x = 0 To 128
      Vert(x + z * 129).Pos = Vec3((x - 64) * hScale, s(x + z * 129) * vScale, (z - 64) * hScale)
    Next x
  Next z
  For z = 0 To 128
    For x = 0 To 128
      vHeight(x + z * 129) = Vert(x + z * 129).Pos.y
    Next x
  Next z

  For z = 0 To 127
    For x = 0 To 127
      Ind((x + z * 128) * 6 + 0) = x + z * 129 + 0
      Ind((x + z * 128) * 6 + 1) = x + z * 129 + 129
      Ind((x + z * 128) * 6 + 2) = x + z * 129 + 130
      Ind((x + z * 128) * 6 + 3) = x + z * 129 + 0
      Ind((x + z * 128) * 6 + 4) = x + z * 129 + 130
      Ind((x + z * 128) * 6 + 5) = x + z * 129 + 1
    Next x
  Next z

  Set vBuf = Nothing
  vSize = Len(Vert(0))
  Set vBuf = Dev.CreateVertexBuffer(vCnt * vSize, 0, FVF, D3DPOOL_DEFAULT)
  vBuf.SetData 0, vCnt * vSize, VarPtr(Vert(0)), 0

  Set iBuf = Nothing
  Set iBuf = Dev.CreateIndexBuffer(iCnt * Len(Ind(0)), 0, D3DFMT_INDEX16, D3DPOOL_DEFAULT)
  iBuf.SetData 0, iCnt * Len(Ind(0)), VarPtr(Ind(0)), 0

  VS_Create
  PS_Create
End Sub

Private Sub VS_Create()
  Dim vsDecl(1) As D3DVERTEXELEMENT9

  vsDecl(0) = VertexElement(0, 0, D3DDECLTYPE_FLOAT3, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_POSITION, 0)
  vsDecl(1) = VertexElementEnd

  Set vDecl = Dev.CreateVertexDeclaration(VarPtr(vsDecl(0)))
  Set VS = Dev.CreateVertexShaderFromFile(App.Path & "\ShaderCompiler\Out\Land.vsh.shader")
End Sub

Private Sub PS_Create()
  Set PS = Dev.CreatePixelShaderFromFile(App.Path & "\ShaderCompiler\Out\Land.psh.shader")
End Sub

Private Sub Class_Terminate()
  Set PS = Nothing
  Set VS = Nothing
  Set vDecl = Nothing
  Set vBuf = Nothing
  Set iBuf = Nothing
End Sub
