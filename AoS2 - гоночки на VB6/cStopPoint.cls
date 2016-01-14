VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cStopPoint"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Type vFormat
  Dir As D3DVECTOR
End Type

Dim vBuf As Direct3DVertexBuffer9
Dim vSize As Long

Public Sub Draw()
  Dev.SetVertexShader Nothing
  Dev.SetPixelShader Nothing

  Dev.SetRenderState D3DRS_SRCBLEND, D3DBLEND_ONE
  Dev.SetRenderState D3DRS_DESTBLEND, D3DBLEND_ONE
  Dev.SetRenderState D3DRS_ALPHABLENDENABLE, D3D_TRUE
  Dev.SetTextureStageState 0, D3DTSS_ALPHAOP, D3DTOP_SELECTARG1
  Dev.SetTextureStageState 0, D3DTSS_ALPHAARG1, D3DTA_TEXTURE
  Dev.SetTextureStageState 0, D3DTSS_COLOROP, D3DTOP_SELECTARG1
  Dev.SetTextureStageState 0, D3DTSS_COLORARG1, D3DTA_TEXTURE
  Dev.SetTextureStageState 1, D3DTSS_COLOROP, D3DTOP_DISABLE
  
  Dev.SetRenderState D3DRS_POINTSPRITEENABLE, D3D_TRUE
  Dev.SetRenderState D3DRS_POINTSCALEENABLE, D3D_TRUE
  Dev.SetRenderState D3DRS_POINTSIZE_MIN, FtoDW(1)
  Dev.SetRenderState D3DRS_POINTSIZE_MAX, FtoDW(1024)
  Dev.SetRenderState D3DRS_POINTSCALE_A, FtoDW(1)
  Dev.SetRenderState D3DRS_POINTSCALE_B, FtoDW(1)
  Dev.SetRenderState D3DRS_POINTSCALE_C, FtoDW(1)
  
  Dev.SetStreamSource 0, vBuf, 0, vSize
  Dev.SetFVF D3DFVF_XYZ
  Dev.SetTexture 0, TexStop
  
  Dev.SetRenderState D3DRS_POINTSIZE, FtoDW(0.4)
  Dev.SetTransform D3DTS_WORLD, mWorld
  Dev.SetTransform D3DTS_VIEW, mView
  Dev.SetTransform D3DTS_PROJECTION, mProj
  Dev.DrawPrimitive D3DPT_POINTLIST, 0, 2
  Dev.SetRenderState D3DRS_POINTSPRITEENABLE, D3D_FALSE
End Sub

Private Sub Class_Initialize()
  Dim Vert(1) As vFormat

  vSize = Len(Vert(0))
  Vert(0).Dir = Vec3(-0.9, 0.95, -2.7)
  Vert(1).Dir = Vec3(0.9, 0.95, -2.7)
  Set vBuf = Dev.CreateVertexBuffer(2 * vSize, D3DUSAGE_NONE, D3DFVF_XYZ, D3DPOOL_DEFAULT)
  vBuf.SetData 0, 2 * vSize, VarPtr(Vert(0)), 0
End Sub

Private Sub Class_Terminate()
  Set vBuf = Nothing
End Sub
