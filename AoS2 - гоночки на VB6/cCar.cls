VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cCar"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Dim WFRoll As Single
Dim WBRoll As Single
Dim aFB As Single, aLR As Single
Dim Angle As Single, mePos As D3DVECTOR

Friend Property Get Pos() As D3DVECTOR
  Pos = mePos
End Property

Friend Sub Update(ByVal Ind As Long)
  WFRoll = WFRoll + CarPh(Ind).WFSpeed
  WBRoll = WBRoll + CarPh(Ind).WBSpeed
  mePos = Vec3(CarPh(Ind).PosX, LS.GetHeight(CarPh(Ind).PosX, CarPh(Ind).PosZ) + 0.25, CarPh(Ind).PosZ)
  Angle = CarPh(Ind).Angle
  aFB = CarPh(Ind).carDiffFB
  aLR = CarPh(Ind).carDiffLR
End Sub

Friend Sub Draw(ByVal Ind As Long)
  Dim Mtrx As D3DMATRIX
  Dim MW As D3DMATRIX
  Dim MWVP As D3DMATRIX
  Dim MSteer As D3DMATRIX
  Dim v3 As D3DVECTOR
  Dim v4 As D3DVECTOR4
  Dim v4Disk As D3DVECTOR4
  Dim v4Wheel As D3DVECTOR4
  Dim SinA As Single, CosA As Single
  Dim LSLR As Single, LSFB As Single

  LSLR = LS.AngleLR(mePos.x, mePos.z): LSFB = LS.AngleFB(mePos.x, mePos.z)
  SinA = Sin(Angle): CosA = Cos(Angle)
  MatrixRotationYawPitchRoll mWorld, Angle, LSFB * CosA - LSLR * SinA - aFB, LSFB * SinA + LSLR * CosA - aLR
  mWorld.m41 = mePos.x: mWorld.m42 = mePos.y: mWorld.m43 = mePos.z
  MatrixMultiply MWVP, mWorld, mView
  MatrixMultiply MWVP, MWVP, mProj
  MatrixTranspose Mtrx, MWVP
  Dev.SetVertexShaderConstantF 0, VarPtr(Mtrx), 4
  MatrixTranspose Mtrx, mWorld
  Dev.SetVertexShaderConstantF 4, VarPtr(Mtrx), 3
  v4 = Vec4(0, 0.707, -0.707, 0)
  Dev.SetVertexShaderConstantF 7, VarPtr(v4), 1
  Vec3Subtract v3, CamPos, mePos
  Dev.SetVertexShaderConstantF 8, VarPtr(v3), 1
  v4 = Vec4(0.35, 0.48, 0.54, 0)
  v4.w = 200 / (Vec3Length(v3) + 200)
  Dev.SetPixelShaderConstantF 2, VarPtr(v4), 1
  Dev.SetRenderState D3DRS_CULLMODE, D3DCULL_CCW
  Dev.SetRenderState D3DRS_ZENABLE, D3DZB_TRUE
  Dev.SetRenderState D3DRS_ALPHABLENDENABLE, D3D_FALSE

  Dev.SetVertexDeclaration vDeclCar

  Dev.SetVertexShaderConstantF 9, VarPtr(CarCol(Ind)), 4
  Dev.SetVertexShader VSBody
  Dev.SetPixelShader PSBody
  carBody.Draw

  Dev.SetVertexShader VSMold
  Dev.SetPixelShader PSMold
  carMold.Draw
  carSalon.Draw

  Dev.SetTexture 0, TexChrom
  Dev.SetVertexShader VSChrom
  Dev.SetPixelShader PSChrom
  carChrom.Draw

  Dev.SetRenderState D3DRS_SRCBLEND, D3DBLEND_SRCALPHA
  Dev.SetRenderState D3DRS_DESTBLEND, D3DBLEND_INVSRCALPHA
  Dev.SetRenderState D3DRS_ALPHABLENDENABLE, D3D_TRUE
  Dev.SetPixelShader PSGlass
  carGlass.Draw
  Dev.SetRenderState D3DRS_ALPHABLENDENABLE, D3D_FALSE

  Dev.SetPixelShader PSStop
  v4Disk = Vec4(0, 0, 0, 0.9)
  v4Wheel = Vec4(0.1, 0.1, 0.1, 0.05)
  MatrixRotationY MSteer, CarPh(Ind).Steer

  MatrixTranslation MW, -1.2, 0.52 - aLR - aFB * 1.5, 1.87
  MatrixRotationX Mtrx, WFRoll
  MatrixMultiply MW, MSteer, MW
  MatrixMultiply MW, Mtrx, MW
  MatrixMultiply Mtrx, MW, mWorld
  MatrixMultiply MW, MW, MWVP
  MatrixTranspose MW, MW
  Dev.SetVertexShaderConstantF 0, VarPtr(MW), 4
  MatrixTranspose MW, Mtrx
  Dev.SetVertexShaderConstantF 4, VarPtr(MW), 3
  Dev.SetPixelShaderConstantF 0, VarPtr(v4Wheel), 1
  carWheel.Draw
  Dev.SetPixelShaderConstantF 0, VarPtr(v4Disk), 1
  carWDisk.Draw

  MatrixTranslation MW, -1.13, 0.52 - aLR + aFB * 1.5, -1.99
  MatrixRotationX Mtrx, WBRoll
  MatrixMultiply MW, Mtrx, MW
  MatrixMultiply Mtrx, MW, mWorld
  MatrixMultiply MW, MW, MWVP
  MatrixTranspose MW, MW
  Dev.SetVertexShaderConstantF 0, VarPtr(MW), 4
  MatrixTranspose MW, Mtrx
  Dev.SetVertexShaderConstantF 4, VarPtr(MW), 3
  Dev.SetPixelShaderConstantF 0, VarPtr(v4Wheel), 1
  carWheel.Draw
  Dev.SetPixelShaderConstantF 0, VarPtr(v4Disk), 1
  carWDisk.Draw

  MatrixTranslation MW, -1.2, 0.52 + aLR - aFB * 1.5, -1.87
  MatrixRotationX Mtrx, -WFRoll
  MatrixMultiply MW, MSteer, MW
  MatrixMultiply MW, Mtrx, MW
  MatrixRotationY Mtrx, Pi
  MatrixMultiply MW, MW, Mtrx
  MatrixMultiply Mtrx, MW, mWorld
  MatrixMultiply MW, MW, MWVP
  MatrixTranspose MW, MW
  Dev.SetVertexShaderConstantF 0, VarPtr(MW), 4
  MatrixTranspose MW, Mtrx
  Dev.SetVertexShaderConstantF 4, VarPtr(MW), 3
  Dev.SetPixelShaderConstantF 0, VarPtr(v4Wheel), 1
  carWheel.Draw
  Dev.SetPixelShaderConstantF 0, VarPtr(v4Disk), 1
  carWDisk.Draw

  MatrixTranslation MW, -1.13, 0.52 + aLR + aFB * 1.5, 1.99
  MatrixRotationX Mtrx, -WBRoll
  MatrixMultiply MW, Mtrx, MW
  MatrixRotationY Mtrx, Pi
  MatrixMultiply MW, MW, Mtrx
  MatrixMultiply Mtrx, MW, mWorld
  MatrixMultiply MW, MW, MWVP
  MatrixTranspose MW, MW
  Dev.SetVertexShaderConstantF 0, VarPtr(MW), 4
  MatrixTranspose MW, Mtrx
  Dev.SetVertexShaderConstantF 4, VarPtr(MW), 3
  Dev.SetPixelShaderConstantF 0, VarPtr(v4Wheel), 1
  carWheel.Draw
  Dev.SetPixelShaderConstantF 0, VarPtr(v4Disk), 1
  carWDisk.Draw

  If CarPh(Ind).IsBreak Then
    v4 = Vec4(1, 0, 0, 0.25)
  Else
    v4 = Vec4(0.25, 0, 0, 0.25)
  End If

  MatrixTranspose Mtrx, MWVP
  Dev.SetVertexShaderConstantF 0, VarPtr(Mtrx), 4
  MatrixTranspose Mtrx, mWorld
  Dev.SetVertexShaderConstantF 4, VarPtr(Mtrx), 3
  Dev.SetPixelShaderConstantF 0, VarPtr(v4), 1

  If CarPh(Ind).IsBreak Then
    Dev.SetRenderState D3DRS_ZWRITEENABLE, D3D_FALSE
    carStop.Draw
    StopPoint.Draw
    Dev.SetRenderState D3DRS_ZWRITEENABLE, D3D_TRUE
  Else
    carStop.Draw
  End If
End Sub
