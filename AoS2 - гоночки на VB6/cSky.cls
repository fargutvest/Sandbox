VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cSky"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Type vFormat
  Pos As D3DVECTOR
  tu As Single
  tv As Single
End Type

Const TriCount As Long = 32
Dim PS As Direct3DPixelShader9
Dim VS As Direct3DVertexShader9
Dim vDecl As Direct3DVertexDeclaration

Dim vBuf As Direct3DVertexBuffer9
Dim vSize As Long

Friend Sub Draw(cPos As D3DVECTOR)
  Dim Mtrx As D3DMATRIX
  Dim v4 As D3DVECTOR4

  MatrixTranslation Mtrx, cPos.x, cPos.y, cPos.z
  MatrixMultiply Mtrx, Mtrx, mView
  MatrixMultiply Mtrx, Mtrx, mProj
  MatrixTranspose Mtrx, Mtrx
  Dev.SetVertexShaderConstantF 0, VarPtr(Mtrx), 4
  
  Dev.SetStreamSource 0, vBuf, 0, vSize
  Dev.SetTexture 0, TexSky
  Dev.SetRenderState D3DRS_ZENABLE, D3DZB_FALSE
  Dev.SetRenderState D3DRS_CULLMODE, D3DCULL_NONE
  Dev.SetRenderState D3DRS_ALPHABLENDENABLE, D3D_FALSE

  Dev.SetVertexDeclaration vDecl
  Dev.SetVertexShader VS
  Dev.SetPixelShader PS
  Dev.DrawPrimitive D3DPT_TRIANGLEFAN, 0, TriCount
End Sub

Private Sub Class_Initialize()
  Dim n As Integer, tmp As Single
  Dim Vert() As vFormat

  tmp = 2 * Pi / TriCount
  ReDim Vert(TriCount + 1)
  vSize = Len(Vert(0))
  Set vBuf = Dev.CreateVertexBuffer((TriCount + 2) * vSize, D3DUSAGE_NONE, D3DFVF_XYZ Or D3DFVF_TEX1, D3DPOOL_DEFAULT)

  Vert(0).Pos = Vec3(0, 10, 0)
  Vert(0).tu = 0.5
  Vert(0).tv = 0.5
  For n = 1 To TriCount + 1
    Vert(n).Pos = Vec3(10 * Sin(n * tmp), 0, -10 * Cos(n * tmp))
    Vert(n).tu = (1 + Sin(n * tmp)) * 0.5
    Vert(n).tv = (1 + Cos(n * tmp)) * 0.5
  Next n
  vBuf.SetData 0, (TriCount + 2) * vSize, VarPtr(Vert(0)), 0
  VS_Create
  PS_Create
End Sub

Private Sub VS_Create()
  Dim vsDecl(2) As D3DVERTEXELEMENT9

  vsDecl(0) = VertexElement(0, 0, D3DDECLTYPE_FLOAT3, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_POSITION, 0)
  vsDecl(1) = VertexElement(0, 12, D3DDECLTYPE_FLOAT2, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_TEXCOORD, 0)
  vsDecl(2) = VertexElementEnd

  Set vDecl = Dev.CreateVertexDeclaration(VarPtr(vsDecl(0)))
  Set VS = Dev.CreateVertexShaderFromFile(App.Path & "\ShaderCompiler\Out\Sky.vsh.shader")
End Sub

Private Sub PS_Create()
  Set PS = Dev.CreatePixelShaderFromFile(App.Path & "\ShaderCompiler\Out\Sky.psh.shader")
End Sub

Private Sub Class_Terminate()
  Set VS = Nothing
  Set PS = Nothing
  Set vDecl = Nothing
  Set vBuf = Nothing
End Sub
