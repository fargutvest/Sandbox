Attribute VB_Name = "modDX"
Option Explicit

Public Enum TexF
  TexF_None
  TexF_BiLinear
  TexF_TriLinear
  TexF_Anisotropic
End Enum

Dim d3D As Direct3D9
Public Aspect As Single
Public mView As D3DMATRIX
Public mProj As D3DMATRIX
Public mWorld As D3DMATRIX
Public Dev As Direct3DDevice9

Public Sub TexFilter(Stage As Long, TF As TexF, Optional MaxAnisotropy As Long = 2)
  Select Case TF
    Case TexF_None
      Dev.SetSamplerState Stage, D3DSAMP_MIPFILTER, D3DTEXF_NONE
      Dev.SetSamplerState Stage, D3DSAMP_MAGFILTER, D3DTEXF_NONE
      Dev.SetSamplerState Stage, D3DSAMP_MINFILTER, D3DTEXF_NONE
    Case TexF_BiLinear
      Dev.SetSamplerState Stage, D3DSAMP_MIPFILTER, D3DTEXF_POINT
      Dev.SetSamplerState Stage, D3DSAMP_MAGFILTER, D3DTEXF_LINEAR
      Dev.SetSamplerState Stage, D3DSAMP_MINFILTER, D3DTEXF_LINEAR
    Case TexF_TriLinear
      Dev.SetSamplerState Stage, D3DSAMP_MIPFILTER, D3DTEXF_LINEAR
      Dev.SetSamplerState Stage, D3DSAMP_MAGFILTER, D3DTEXF_LINEAR
      Dev.SetSamplerState Stage, D3DSAMP_MINFILTER, D3DTEXF_LINEAR
    Case TexF_Anisotropic
      Dev.SetSamplerState Stage, D3DSAMP_MIPFILTER, D3DTEXF_LINEAR
      Dev.SetSamplerState Stage, D3DSAMP_MAGFILTER, D3DTEXF_LINEAR
      Dev.SetSamplerState Stage, D3DSAMP_MINFILTER, D3DTEXF_ANISOTROPIC
      Dev.SetSamplerState Stage, D3DSAMP_MAXANISOTROPY, MaxAnisotropy
  End Select
End Sub

Public Sub D3DInit(hWnd As Long)
Dim d3dpp As D3DPRESENT_PARAMETERS

  Set d3D = DirectX9.CreateDirect3D
  d3dpp.Windowed = D3D_TRUE
  d3dpp.SwapEffect = D3DSWAPEFFECT_DISCARD
  d3dpp.BackBufferFormat = D3DFMT_UNKNOWN
  d3dpp.BackBufferCount = 0
  d3dpp.EnableAutoDepthStencil = D3D_TRUE
  d3dpp.AutoDepthStencilFormat = D3DFMT_D24S8
  If frmMain.chVSync = 1 Then
    d3dpp.PresentationInterval = D3DPRESENT_ONE
  Else
    d3dpp.PresentationInterval = D3DPRESENT_IMMEDIATE
  End If

  Set Dev = d3D.CreateDevice(hWnd, D3DCREATE_HARDWARE_VERTEXPROCESSING, d3dpp)
  If Dev Is Nothing Then
    Set Dev = d3D.CreateDevice(hWnd, D3DCREATE_SOFTWARE_VERTEXPROCESSING, d3dpp)
  End If
End Sub

Public Sub D3DTerminate()
  Set Dev = Nothing
  Set d3D = Nothing
End Sub
