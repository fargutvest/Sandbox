VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cCarPhisic"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Dim Accel As Single
Dim Break As Single
Public IsBreak As Boolean
Public Steer As Single
Const KPPMaxstep = 4
Dim KPP(-1 To 4) As Single
Public KPPstep As Long
Public Block As Single
Public KPPType As Long

Public LockSpX As Single
Public LockSpZ As Single
Public Speed As Single
Public PosX As Single
Public PosZ As Single
Public Angle As Single
Const MaxTaxo = 8000
Public Taxo As Single
Dim RotSpeed As Single
Public SpeedX As Single
Public SpeedZ As Single

Public carDiffFB As Single
Public carDiffLR As Single
Const kYung = 5
Const carWidth = 2.85
Const carHeight = 4.3
Const resLR = 3.5
Const resFB = 2

Dim MaxFRC As Single
Dim MaxRotFRC As Single

Const Power = 0.16
                        
Const MaxSpeed = 45
Const Radius = 3
Const CtrlSens = 1.2
Const carExBack = 1.25

Dim SinA As Single
Dim CosA As Single
Dim CPoint(3) As D3DVECTOR2
Dim mePoint(3) As D3DVECTOR2
Dim meInd As Long
Dim mePos As D3DVECTOR2
Dim meSpeed As D3DVECTOR2
Dim BrkVol As Single
Dim KPPChange As Boolean
Dim Crush As Boolean
Dim CrushVol As Single
Dim meTrackPos As D3DVECTOR2
Dim meTrackDir As D3DVECTOR2

Public Sub Init(ByVal Ind As Long, ByVal x As Single, ByVal z As Single)
  meInd = Ind
  PosX = x
  PosZ = z
  Angle = 0
  RotSpeed = 0
  SpeedX = 0
  SpeedZ = 0
  KPPstep = 0
  CPoint(0) = Vec2(1.3, 2.7)
  CPoint(1) = Vec2(-1.3, 2.7)
  CPoint(2) = Vec2(-1.3, -2.85)
  CPoint(3) = Vec2(1.3, -2.85)
  
  SoundStart.SetFreq 21000 + 2000 * Rnd, Ind
  SoundStart.SetPan 1000 * Rnd - 500, Ind
  SoundStart.Play Ind
  SoundMotor.SetVol -10000, Ind
  SoundMotor.PlayLoop Ind
  SoundBreak.SetVol -10000, Ind
  SoundBreak.PlayLoop Ind
  SoundGrnd.SetVol -10000, Ind
  SoundGrnd.PlayLoop Ind
End Sub

Friend Property Get GetPoint(ByVal Ind As Long) As D3DVECTOR2
  GetPoint = mePoint(Ind)
End Property

Public Property Get WBSpeed() As Single
  If Block < 0.9 Then
    If KPPstep = 0 Then
      WBSpeed = LockSpZ * 2.5 * DTime * (1 - Break)
    Else
      WBSpeed = Taxo * KPP(KPPstep) * (1 - Break) * 0.01 * DTime
    End If
  Else
    WBSpeed = LockSpZ * 2.5 * DTime
  End If
End Property

Public Property Get WFSpeed() As Single
  If Block < 0.9 Then
    WFSpeed = LockSpZ * 2.5 * DTime * (1 - Break)
  Else
    WFSpeed = LockSpZ * 2.5 * DTime
  End If
End Property

Public Sub Driving()
  Dim t As Single
  Dim f As Boolean

'  AddTrackData meTrackPos, meTrackDir
'  If Keyb(vbKeyReturn) Then
'    SaveTrackData
'    Keyb(vbKeyReturn) = False
'  End If

  IsBreak = False
  t = DTime * CtrlSens
  If KPPType < 2 Then
    If Keyb(vbKeyUp) Then
      Accel = Accel + t: If Accel > 1 Then Accel = 1
    Else
      Accel = Accel - t: If Accel < 0.1 Then Accel = 0.1
    End If
    If Keyb(vbKeyDown) Then
      IsBreak = True
      Break = Break + t: If Break > 1 Then Break = 1
    Else
      Break = Break - t * 4: If Break < 0.1 Then Break = 0.1
    End If
  Else
    If Keyb(vbKeyUp) Then
      If KPPstep < 0 Then
        If LockSpZ > -0.05 Then
          KPPstep = 1
          KPPChange = True
        Else
          Break = Break + t: If Break > 1 Then Break = 1
          Accel = Accel - t: If Accel < 0.1 Then Accel = 0.1
          IsBreak = True
        End If
      Else
        If LockSpZ < -0.1 Then
          KPPstep = -1
          KPPChange = True
        Else
          Accel = Accel + t: If Accel > 1 Then Accel = 1
          Break = Break - t * 4: If Break < 0.1 Then Break = 0.1
        End If
      End If
    ElseIf Keyb(vbKeyDown) Then
      If KPPstep > 0 Then
        If LockSpZ < 0.05 Then
          KPPstep = -1
          KPPChange = True
        Else
          Break = Break + t: If Break > 1 Then Break = 1
          Accel = Accel - t: If Accel < 0.1 Then Accel = 0.1
          IsBreak = True
        End If
      ElseIf KPPstep < 0 Then
        If LockSpZ > 0.1 Then
          KPPstep = 1
          KPPChange = True
        Else
          Accel = Accel + t: If Accel > 1 Then Accel = 1
          Break = Break - t * 4: If Break < 0.1 Then Break = 0.1
        End If
      Else
        Break = Break + t: If Break > 1 Then Break = 1
        Accel = Accel - t: If Accel < 0.1 Then Accel = 0.1
        IsBreak = True
      End If
    Else
      Accel = Accel - t: If Accel < 0.1 Then Accel = 0.1
      Break = Break - t * 4: If Break < 0.1 Then Break = 0.1
    End If
  End If

  t = t / (1.5 + Speed * 0.15)
  If (KPPType = 3) And (KPPstep < 0) Then f = True
  If (Keyb(vbKeyLeft) And (Not f)) Or (Keyb(vbKeyRight) And f) Then
    If Steer > 0 Then Steer = Steer - t * 4
    Steer = Steer - t: If Steer < -0.5 Then Steer = -0.5
  ElseIf (Keyb(vbKeyRight) And (Not f)) Or (Keyb(vbKeyLeft) And f) Then
    If Steer < 0 Then Steer = Steer + t * 4
    Steer = Steer + t: If Steer > 0.5 Then Steer = 0.5
  Else
    If Abs(Steer) < t Then
      Steer = 0
    Else
      Steer = Steer - t * Sgn(Steer)
    End If
  End If

  If RaceTimer < 0 Then Exit Sub
  If KPPType > 1 And KPPstep = 0 Then
    KPPstep = 1
    KPPChange = True
  End If

  Select Case KPPType
    Case 0
      If Keyb(vbKeyA) Then
        If KPPstep < KPPMaxstep Then
          KPPstep = KPPstep + 1
          KPPChange = True
        End If
        Keyb(vbKeyA) = False
      ElseIf Keyb(vbKeyZ) Then
        If KPPstep > -1 Then
          KPPstep = KPPstep - 1
          KPPChange = True
        End If
        Keyb(vbKeyZ) = False
      End If
    Case 1
      If Keyb(vbKeyA) Then
        If KPPstep < 0 Then
          KPPstep = 0
          KPPChange = True
        ElseIf KPPstep = 0 Then
          KPPstep = 1
          KPPChange = True
        End If
        Keyb(vbKeyA) = False
      ElseIf Keyb(vbKeyZ) Then
        If KPPstep > 0 Then
          KPPstep = 0
          KPPChange = True
        ElseIf KPPstep = 0 Then
          KPPstep = -1
          KPPChange = True
        End If
        Keyb(vbKeyZ) = False
      End If
      If KPPstep > 0 Then
        AutoSelectGear
      End If
    Case 2, 3
      If KPPstep > 0 Then
        AutoSelectGear
      End If
  End Select
End Sub

Public Sub AIDriving()
  Dim t As Single
  Dim pressAccel As Boolean
  Dim pressBreak As Boolean
  Dim pressLeft As Boolean
  Dim pressRight As Boolean
  Dim TD1 As TrackData
  Dim Diff As Single
  Dim DiffSpeed As Single
  Static OldDiff As Single
  Static RevCnt As Long
  Dim dA As Single
  Dim v As D3DVECTOR2

  If RaceTimer < 0 Then
    pressAccel = Rnd < 0.5
  Else
    If TrackPos(meInd) > RoadLen Then
      KPPstep = 0
      Accel = 0.1
      Break = 0.6
      Steer = meInd * 0.03 - 0.06
      Exit Sub
    End If
    TD1 = Track((NearInd(meInd) + RoadLen - (Speed + 5) * 0.25) Mod RoadLen)
    TD1.Speed = TD1.Speed + 2
    TD1.Diff = TD1.Diff + meInd * 2 - 7
  
    Vec2Subtract v, mePos, meTrackPos
    Diff = Vec2CCW(meTrackDir, v)
    DiffSpeed = Diff - OldDiff
    OldDiff = Diff
    dA = Angle - TD1.Angle
    While dA > Pi: dA = dA - Pi - Pi: Wend
    While dA < -Pi: dA = dA + Pi + Pi: Wend
    dA = dA + Diff * 0.11 - TD1.Diff * 0.075 + RotSpeed * 1.3 + DiffSpeed * 2
  
    If RevCnt > 0 Then
      RevCnt = RevCnt - 1
      If (dA > -0.1) And (dA < 0.1) Then RevCnt = 0
    End If
    If (Diff > 12) And (dA > 1) Then
      If KPPstep > -1 Then KPPstep = -1: KPPChange = True: RevCnt = 150
    ElseIf (Diff < -12) And (dA < -1) Then
      If KPPstep > -1 Then KPPstep = -1: KPPChange = True: RevCnt = 150
    ElseIf RevCnt = 0 Then
      If KPPstep < 1 Then KPPstep = 1: KPPChange = True
    End If
  
    dA = dA + (Rnd - 0.5) * 0.03
    If dA > 0.02 Then
      If KPPstep > 0 Then pressLeft = True Else pressRight = True
    ElseIf dA < -0.02 Then
      If KPPstep > 0 Then pressRight = True Else pressLeft = True
    End If
    If Speed > TD1.Speed * 1.01 Then
      pressBreak = True
    ElseIf Speed < TD1.Speed * 0.9 Then
      pressAccel = True
    End If
  End If

  t = DTime * 1.2
  If pressAccel Then
    Accel = Accel + t: If Accel > 1 Then Accel = 1
  Else
    Accel = Accel - t: If Accel < 0.1 Then Accel = 0.1
  End If
  
  If pressBreak Then
    IsBreak = True
    Break = Break + t: If Break > 1 Then Break = 1
  Else
    IsBreak = False
    Break = Break - t * 4: If Break < 0.1 Then Break = 0.1
  End If

  t = t / (1.5 + Speed * 0.15)
  If pressLeft Then
    If Steer > 0 Then Steer = Steer - t * 4
    Steer = Steer - t: If Steer < -0.5 Then Steer = -0.5
  ElseIf pressRight Then
    If Steer < 0 Then Steer = Steer + t * 4
    Steer = Steer + t: If Steer > 0.5 Then Steer = 0.5
  Else
    If Abs(Steer) < t Then
      Steer = 0
    Else
      Steer = Steer - t * Sgn(Steer)
    End If
  End If

  If KPPstep > 0 Then
    AutoSelectGear
  End If
End Sub

Private Sub AutoSelectGear()
  Dim s As Single

  s = LockSpZ / ((KPP(KPPstep) + 0.1) * MaxSpeed)
  If s > Accel - 0.4 Then If KPPstep < KPPMaxstep Then KPPstep = KPPstep + 1
  If s < Accel - 0.6 Then If KPPstep > 1 Then KPPstep = KPPstep - 1
End Sub

Public Sub Moving()
  Dim FrcX As Single
  Dim FrcZ As Single
  Dim ForceX As Single
  Dim ForceZ As Single
  Dim RotFRC As Single
  Dim tmp As Single
  Dim tax As Single
  Dim Brk As Single
  Dim i As Long

  SinA = Sin(Angle)
  CosA = Cos(Angle)
  If SpeedZ > 0.0000001 Then
    tmp = Atn(SpeedX / SpeedZ)
  ElseIf SpeedZ < -0.0000001 Then
    tmp = 3.141593 + Atn(SpeedX / SpeedZ)
  Else
    tmp = 1.570796 * Sgn(SpeedX)
  End If
  tmp = tmp - Angle
  Speed = Sqr(SpeedX * SpeedX + SpeedZ * SpeedZ)
  LockSpX = -Speed * Sin(tmp)
  LockSpZ = Speed * Cos(tmp)

  If KPPstep = 0 Then
    FrcZ = 0
    tax = Accel
  Else
    tmp = KPP(KPPstep) * MaxSpeed * Accel * (1.01 - Break)
    FrcZ = (tmp - LockSpZ) * Power * (tax + 0.75) / (KPP(KPPstep) * KPP(KPPstep))
    tax = LockSpZ / (KPP(KPPstep) * MaxSpeed): If tax < 0 Then tax = 0
    tax = Accel * (1 - Break) * (1 - Block) * 3 / (2 + Abs(KPPstep)) + tax * Block
  End If

  If Abs(LockSpZ) > 1 Then Brk = Sgn(LockSpZ) * 1.1 Else Brk = LockSpZ + Sgn(LockSpZ) * 0.1
  FrcZ = FrcZ + (LS.AngleFB(PosX, PosZ) * CosA - LS.AngleLR(PosX, PosZ) * SinA) * 8 - Break * Brk * MaxFRC * 2
  FrcX = -50 * LockSpX / (1 + Abs(LockSpZ) * 0.03)
  If Abs(FrcZ) > MaxFRC Then BrkVol = (Abs(FrcZ) / MaxFRC - 1) * 1 Else BrkVol = 0
  If Abs(FrcX) > MaxFRC Then BrkVol = BrkVol + 0.1 * Abs(FrcX) / MaxFRC - 0.1
  If BrkVol > 0.2 Then BrkVol = BrkVol * 0.25 + 0.75 Else BrkVol = BrkVol * 4
  If BrkVol > 1 Then BrkVol = 1

  Block = Sqr(FrcX * FrcX * 3 / (30 + Abs(LockSpZ)) + FrcZ * FrcZ) / MaxFRC
  If Block > 1 Then
    Block = 1 / Block
    FrcX = FrcX * Block
    FrcZ = FrcZ * Block
  Else
    Block = 1
  End If

  If tax < 0.07 Then tax = 0.07
  If tax > 1.2 Then tax = 1.2
  Taxo = tax * MaxTaxo * DTime * 8 + Taxo * (1 - DTime * 8)

  If KPPstep = 0 Then tmp = 0 Else tmp = 2 / (Abs(KPP(KPPstep)) * MaxSpeed)
  RotFRC = (Steer * LockSpZ / Radius - RotSpeed + LockSpX * Accel * tmp) / DTime
  tmp = MaxRotFRC * (MaxSpeed - LockSpZ) / MaxSpeed
  If Abs(RotFRC) > tmp Then RotFRC = Sgn(RotFRC) * tmp

  tmp = DTime * resFB
  carDiffFB = carDiffFB * (1 - tmp) + FrcZ / (MaxFRC * kYung * 6) * tmp
  tmp = DTime * resLR
  carDiffLR = carDiffLR * (1 - tmp) + FrcX / (MaxFRC * kYung * 12) * tmp

  ForceZ = FrcZ * CosA + FrcX * SinA
  ForceX = FrcZ * SinA - FrcX * CosA
  
  RotSpeed = RotSpeed + RotFRC * DTime
  SpeedX = SpeedX + ForceX * DTime
  SpeedZ = SpeedZ + ForceZ * DTime
  Angle = Angle + RotSpeed * DTime
  PosX = PosX + SpeedX * DTime * 2
  PosZ = PosZ + SpeedZ * DTime * 2
  mePos = Vec2(PosX, PosZ)
  meSpeed = Vec2(SpeedX, SpeedZ)

  For i = 0 To 3
    mePoint(i).x = CPoint(i).y * SinA - CPoint(i).x * CosA
    mePoint(i).y = CPoint(i).x * SinA + CPoint(i).y * CosA
    Vec2Add mePoint(i), mePoint(i), Vec2(PosX, PosZ)
  Next i
  CollisionCars
  CollisionWall
  PutSound
End Sub

Private Sub CollisionCars()
  Dim v As D3DVECTOR2
  Dim vN As D3DVECTOR2
  Dim vSp As D3DVECTOR2
  Dim vP As D3DVECTOR2
  Dim vC As D3DVECTOR2
  Dim i As Long
  Dim ii As Long
  Dim n As Long
  Dim s As Single
  Dim f As Single

  For i = 0 To AICnt
    If i <> meInd Then
      vC = Vec2(CarPh(i).PosX, CarPh(i).PosZ)
      Vec2Subtract vP, Vec2(PosX, PosZ), vC
      If Vec2LengthSq(vP) < 100 Then
        Vec2Subtract vSp, Vec2(SpeedX, SpeedZ), Vec2(CarPh(i).SpeedX, CarPh(i).SpeedZ)
        For n = 0 To 3
          v = CarPh(i).GetPoint(n)
          If PointInCar(v, vC, vP, ii, f) Then
            Select Case ii
              Case 0: vN = Vec2(SinA, CosA)
              Case 1: vN = Vec2(CosA, -SinA)
              Case 2: vN = Vec2(-SinA, -CosA)
              Case 3: vN = Vec2(-CosA, SinA)
            End Select
            s = Vec2Dot(vN, vSp)
            If s > 0 Then
              Crush = True
              CrushVol = s * 0.5
              If CrushVol > 1 Then CrushVol = 1
              PosX = PosX + vP.x * f * 0.05
              PosZ = PosZ + vP.y * f * 0.05
              CarPh(i).PosX = CarPh(i).PosX - vP.x * f * 0.05
              CarPh(i).PosZ = CarPh(i).PosZ - vP.y * f * 0.05
              Vec2Scale vN, vN, -0.25 * (s + f)
              SetImpulse v, vN
              Vec2Scale vN, vN, -1
              CarPh(i).SetImpulse v, vN
            End If
          End If
        Next n
      End If
    End If
  Next i
End Sub

Private Function PointInCar(v As D3DVECTOR2, vC As D3DVECTOR2, vP As D3DVECTOR2, OutI As Long, OutF As Single) As Boolean
  Dim i As Long
  Dim s As Single
  Dim ss As Single
  Dim v1 As D3DVECTOR2
  Dim v2 As D3DVECTOR2

  OutF = 200
  For i = 0 To 3
    Vec2Subtract v2, mePoint((i + 1) And 3), mePoint(i)
    Vec2Subtract v1, vC, mePoint(i)
    ss = Vec2CCW(v1, v2)
    Vec2Subtract v1, v, mePoint(i)
    Vec2Normalize v1, v1
    s = Vec2CCW(v1, v2)
    If s <= 0 Then Exit Function
    If ss < 0 Then
      If OutF > s Then OutF = s: OutI = i
    End If
  Next i
  PointInCar = True
End Function

Friend Sub SetImpulse(vP As D3DVECTOR2, vI As D3DVECTOR2)
  Dim v As D3DVECTOR2
  Dim s As Single

  SpeedX = SpeedX + vI.x
  SpeedZ = SpeedZ + vI.y
  Vec2Subtract v, vP, Vec2(PosX, PosZ)
  s = -0.5 * Vec2CCW(v, vI)
  If s > 3 Then s = 3
  If s < -3 Then s = -3
  RotSpeed = RotSpeed + s
End Sub

Private Sub CollisionWall()
  Dim vPos As D3DVECTOR2
  Dim vDir As D3DVECTOR2
  Dim vRight As D3DVECTOR2
  Dim v As D3DVECTOR2
  Dim p As Single
  Dim s As Single
  Dim i As Long

  Road.GetPlace meInd, mePoint(i), meTrackPos, meTrackDir
  Vec2Normalize meTrackDir, meTrackDir
  For i = 0 To 3
    Road.GetPlace meInd, mePoint(i), vPos, vDir
    Vec2Normalize vDir, vDir
    vRight.x = vDir.y
    vRight.y = -vDir.x
    Vec2Subtract v, mePoint(i), vPos
    p = Vec2CCW(vDir, v)
    
    If p > 15 Or p < -15 Then
      s = Vec2Dot(meSpeed, vRight)
      If p > 0 Then
        Vec2Scale v, vRight, p - 15
      Else
        Vec2Scale v, vRight, p + 15
      End If
      PosX = PosX + v.x
      PosZ = PosZ + v.y
      If s * p < 0 Then
        Crush = True
        CrushVol = s * p * -0.03
        If CrushVol > 1 Then CrushVol = 1
        Vec2Scale v, vRight, -s * 1.1
        Vec2Add meSpeed, meSpeed, v
        SpeedX = meSpeed.x
        SpeedZ = meSpeed.y
      End If
    End If
  Next i
End Sub

Private Sub PutSound()
  Dim v As D3DVECTOR2
  Dim p As Single
  Dim sGrnd As Long
  Dim sBrk As Long
  Dim meVol As Long
  Dim mePan As Long

  Vec2Subtract v, mePos, CamV2Pos
  p = Vec2Length(v)
  Vec2Normalize v, v
  mePan = Vec2CCW(v, CamV2NormDir) * 2000
  meVol = 500000 / (p + 100) - 5000

  Vec2Subtract v, mePos, meTrackPos
  p = Vec2CCW(meTrackDir, v)
  If p > 9 Or p < -9 Then
    MaxFRC = 2
    MaxRotFRC = 2.4
    sGrnd = Speed * 5000 - 5000: If sGrnd > -500 Then sGrnd = -500
    sGrnd = BrkVol * 500 + sGrnd
    sBrk = -5000
  Else
    MaxFRC = 5
    MaxRotFRC = 5
    sGrnd = -5000
    sBrk = BrkVol * 5000 - 5000
  End If

  If KPPChange Then
    SoundGear.SetPan mePan, meInd
    SoundGear.SetVol meVol, meInd
    SoundGear.Play meInd
    KPPChange = False
  End If

  If Crush Then
    SoundCrush.SetPan mePan, meInd
    SoundCrush.SetVol meVol + CrushVol * 5000 - 5000, meInd
    SoundCrush.Play meInd
    Crush = False
  End If

  SoundGear.SetPan mePan, meInd
  SoundGrnd.SetFreq 22000 + Speed, meInd
  SoundGrnd.SetVol sGrnd + meVol, meInd

  SoundMotor.SetPan mePan, meInd
  SoundMotor.SetFreq Taxo * 6, meInd
  SoundMotor.SetVol Accel * 500 - 1000 + meVol, meInd

  SoundBreak.SetPan mePan, meInd
  SoundBreak.SetFreq 22000 + Speed, meInd
  SoundBreak.SetVol sBrk + meVol, meInd
End Sub

Private Sub Class_Initialize()
  MaxFRC = 5
  MaxRotFRC = 6
  KPP(-1) = -0.3
  KPP(0) = 0
  KPP(1) = 0.3
  KPP(2) = 0.566
  KPP(3) = 0.8
  KPP(4) = 1
  KPPType = 1
End Sub

Private Sub Class_Terminate()
  SoundStart.StopPlay meInd
  SoundMotor.StopPlay meInd
  SoundBreak.StopPlay meInd
  SoundGrnd.StopPlay meInd
End Sub
