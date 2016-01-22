Attribute VB_Name = "modCar"
Option Explicit

Public Type CarColor
  Diffuse As D3DVECTOR4
  Ambiente As D3DVECTOR4
  SpecMul As D3DVECTOR4
  SpecAdd As D3DVECTOR4
End Type

Public carBody As New cMesh
Public carMold As New cMesh
Public carSalon As New cMesh
Public carGlass As New cMesh
Public carChrom As New cMesh
Public carStop As New cMesh
Public carWheel As New cMesh
Public carWDisk As New cMesh
Public StopPoint As New cStopPoint
Public PSBody As Direct3DPixelShader9
Public VSBody As Direct3DVertexShader9
Public PSMold As Direct3DPixelShader9
Public VSMold As Direct3DVertexShader9
Public PSChrom As Direct3DPixelShader9
Public VSChrom As Direct3DVertexShader9
Public PSGlass As Direct3DPixelShader9
Public PSStop As Direct3DPixelShader9
Public vDeclCar As Direct3DVertexDeclaration
Public CarCol(3) As CarColor

Public Sub CarInit()
  carBody.Init App.Path & "\data\body.mesh"
  carMold.Init App.Path & "\data\mold.mesh"
  carSalon.Init App.Path & "\data\salon.mesh"
  carGlass.Init App.Path & "\data\glass.mesh"
  carChrom.Init App.Path & "\data\chrom.mesh"
  carStop.Init App.Path & "\data\stop.mesh"
  carWheel.Init App.Path & "\data\wheel.mesh", True
  carWDisk.Init App.Path & "\data\wdisk.mesh", True

  VS_Create
  PS_Create
  CarCol(0).Diffuse = Vec4(0.4, 0.3, 0.4, 0)
  CarCol(0).Ambiente = Vec4(0.5, 0.53, 0.5, 0)
  CarCol(0).SpecMul = Vec4(5, 5, 5, 0)
  CarCol(0).SpecAdd = Vec4(-4.3, -4.3, -4.1, 0)
  CarCol(1).Diffuse = Vec4(0.1, 0.1, 0.4, 0)
  CarCol(1).Ambiente = Vec4(0.2, 0.21, 0.55, 0)
  CarCol(1).SpecMul = Vec4(9, 9, 9, 0)
  CarCol(1).SpecAdd = Vec4(-8.3, -8.1, -8.3, 0)
  CarCol(2).Diffuse = Vec4(0.1, 0.34, 0.1, 0)
  CarCol(2).Ambiente = Vec4(0.15, 0.4, 0.17, 0)
  CarCol(2).SpecMul = Vec4(15, 15, 15, 0)
  CarCol(2).SpecAdd = Vec4(-14, -14.3, -14.3, 0)
  CarCol(3).Diffuse = Vec4(0.4, 0.36, 0.07, 0)
  CarCol(3).Ambiente = Vec4(0.5, 0.53, 0.09, 0)
  CarCol(3).SpecMul = Vec4(8, 8, 8, 0)
  CarCol(3).SpecAdd = Vec4(-7.3, -7.3, -7.1, 0)
End Sub

Private Sub VS_Create()
Dim vsDecl(2) As D3DVERTEXELEMENT9
  vsDecl(0) = VertexElement(0, 0, D3DDECLTYPE_FLOAT3, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_POSITION, 0)
  vsDecl(1) = VertexElement(0, 12, D3DDECLTYPE_FLOAT3, D3DDECLMETHOD_DEFAULT, D3DDECLUSAGE_NORMAL, 0)
  vsDecl(2) = VertexElementEnd

  Set vDeclCar = Dev.CreateVertexDeclaration(VarPtr(vsDecl(0)))
  Set VSBody = Dev.CreateVertexShaderFromFile(App.Path & "\ShaderCompiler\Out\carBody.vsh.shader")
  Set VSMold = Dev.CreateVertexShaderFromFile(App.Path & "\ShaderCompiler\Out\carMold.vsh.shader")
  Set VSChrom = Dev.CreateVertexShaderFromFile(App.Path & "\ShaderCompiler\Out\carChrom.vsh.shader")
End Sub

Private Sub PS_Create()
  Set PSBody = Dev.CreatePixelShaderFromFile(App.Path & "\ShaderCompiler\Out\carBody.psh.shader")
  Set PSMold = Dev.CreatePixelShaderFromFile(App.Path & "\ShaderCompiler\Out\carMold.psh.shader")
  Set PSChrom = Dev.CreatePixelShaderFromFile(App.Path & "\ShaderCompiler\Out\carChrom.psh.shader")
  Set PSGlass = Dev.CreatePixelShaderFromFile(App.Path & "\ShaderCompiler\Out\carGlass.psh.shader")
  Set PSStop = Dev.CreatePixelShaderFromFile(App.Path & "\ShaderCompiler\Out\carStop.psh.shader")
End Sub

Public Sub CarTerminate()
  Set carBody = Nothing
  Set carMold = Nothing
  Set carSalon = Nothing
  Set carGlass = Nothing
  Set carChrom = Nothing
  Set carStop = Nothing
  Set carWheel = Nothing
  Set carWDisk = Nothing
  Set StopPoint = Nothing
  Set PSBody = Nothing
  Set VSBody = Nothing
  Set PSMold = Nothing
  Set VSMold = Nothing
  Set PSChrom = Nothing
  Set VSChrom = Nothing
  Set PSGlass = Nothing
  Set PSStop = Nothing
  Set vDeclCar = Nothing
End Sub
