Attribute VB_Name = "DirectX8"
Option Explicit

' CONST ===========================================================

Public Const DS_TRUE As Long = 1
Public Const DS_FALSE As Long = 0

Public Enum CONST_DSBCAPS
  DSBCAPS_PRIMARYBUFFER = &H1
  DSBCAPS_STATIC = &H2
  DSBCAPS_LOCHARDWARE = &H4
  DSBCAPS_LOCSOFTWARE = &H8
  DSBCAPS_CTRL3D = &H10
  DSBCAPS_CTRLFREQUENCY = &H20
  DSBCAPS_CTRLPAN = &H40
  DSBCAPS_CTRLVOLUME = &H80
  DSBCAPS_CTRLPOSITIONNOTIFY = &H100
  DSBCAPS_CTRLFX = &H200
  DSBCAPS_STICKYFOCUS = &H4000
  DSBCAPS_GLOBALFOCUS = &H8000&
  DSBCAPS_GETCURRENTPOSITION2 = &H10000
  DSBCAPS_MUTE3DATMAXDISTANCE = &H20000
  DSBCAPS_LOCDEFER = &H40000
End Enum

Public Enum CONST_DSSCL
  DSSCL_NORMAL = &H1
  DSSCL_PRIORITY = &H2
  DSSCL_EXCLUSIVE = &H3
  DSSCL_WRITEPRIMARY = &H4
End Enum

' DECLARES ========================================================

Private Declare Sub ds_Create Lib "dx_vb" (ByRef pDS As Long)

' FUNCTIONS =======================================================

Public Function CreateDirectSound() As DirectSound8
  Dim pDS As Long

  ds_Create pDS
  If pDS <> 0 Then
    Set CreateDirectSound = New DirectSound8
    CreateDirectSound.Ptr = pDS
  End If
End Function
