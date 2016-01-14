VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cMesh"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Type vFormat
  Pos As D3DVECTOR
  Normal As D3DVECTOR
End Type

Dim vBuf As Direct3DVertexBuffer9
Dim iBuf As Direct3DIndexBuffer9

Dim vSize As Long
Dim vCnt As Long
Dim iCnt As Long
Dim tCnt As Long

Private Const FVF = D3DFVF_XYZ Or D3DFVF_NORMAL

Public Sub Draw()
  Dev.SetStreamSource 0, vBuf, 0, vSize
  Dev.SetIndices iBuf
  Dev.DrawIndexedPrimitive D3DPT_TRIANGLELIST, 0, 0, vCnt, 0, tCnt
End Sub

Public Sub Init(fName As String, Optional ByVal Center As Boolean = False)
  Dim Vert() As vFormat
  Dim Ind() As Integer
  Dim nf As Integer
  Dim i As Long
  Dim MinX As Single, MaxX As Single
  Dim MinY As Single, MaxY As Single
  Dim MinZ As Single, MaxZ As Single
  Dim AddX As Single, AddY As Single, AddZ As Single

  nf = FreeFile
  Open fName For Binary As #nf
    Get #nf, , vCnt
    Get #nf, , iCnt
    ReDim Vert(vCnt - 1)
    ReDim Ind(iCnt - 1)
    Get #nf, , Vert()
    Get #nf, , Ind()
  Close #nf

  If Center Then
    MinX = 1000: MaxX = -1000
    MinY = 1000: MaxY = -1000
    MinZ = 1000: MaxZ = -1000
    For i = 0 To vCnt - 1
      If MinX > Vert(i).Pos.x Then MinX = Vert(i).Pos.x
      If MaxX < Vert(i).Pos.x Then MaxX = Vert(i).Pos.x
      If MinY > Vert(i).Pos.y Then MinY = Vert(i).Pos.y
      If MaxY < Vert(i).Pos.y Then MaxY = Vert(i).Pos.y
      If MinZ > Vert(i).Pos.z Then MinZ = Vert(i).Pos.z
      If MaxZ < Vert(i).Pos.z Then MaxZ = Vert(i).Pos.z
    Next i
    AddX = (MinX + MaxX) * -0.5
    AddY = (MinY + MaxY) * -0.5
    AddZ = (MinZ + MaxZ) * -0.5
    For i = 0 To vCnt - 1
      Vert(i).Pos.x = Vert(i).Pos.x + AddX
      Vert(i).Pos.y = Vert(i).Pos.y + AddY
      Vert(i).Pos.z = Vert(i).Pos.z + AddZ
    Next i
  End If

  vSize = Len(Vert(0))
  Set vBuf = Nothing
  Set vBuf = Dev.CreateVertexBuffer(vCnt * vSize, 0, FVF, D3DPOOL_DEFAULT)
  vBuf.SetData 0, vCnt * vSize, VarPtr(Vert(0)), 0

  tCnt = iCnt \ 3
  Set iBuf = Nothing
  Set iBuf = Dev.CreateIndexBuffer(iCnt * Len(Ind(0)), 0, D3DFMT_INDEX16, D3DPOOL_DEFAULT)
  iBuf.SetData 0, iCnt * Len(Ind(0)), VarPtr(Ind(0)), 0
End Sub

Private Sub Class_Terminate()
  Set vBuf = Nothing
  Set iBuf = Nothing
End Sub
