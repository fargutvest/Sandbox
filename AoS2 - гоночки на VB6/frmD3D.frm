VERSION 5.00
Begin VB.Form frmD3D 
   BackColor       =   &H00000000&
   BorderStyle     =   0  'None
   Caption         =   "Form1"
   ClientHeight    =   3585
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   3555
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MouseIcon       =   "frmD3D.frx":0000
   MousePointer    =   99  'Custom
   ScaleHeight     =   239
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   237
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.Timer Timer2 
      Enabled         =   0   'False
      Interval        =   200
      Left            =   1920
      Top             =   1980
   End
   Begin VB.Timer Timer1 
      Enabled         =   0   'False
      Interval        =   1000
      Left            =   360
      Top             =   2040
   End
End
Attribute VB_Name = "frmD3D"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
  Keyb(KeyCode) = True
End Sub

Private Sub Form_KeyUp(KeyCode As Integer, Shift As Integer)
  Keyb(KeyCode) = False
End Sub

Private Sub Form_Load()
  Me.Move 0, 0, Screen.Width, Screen.Height
  Aspect = ScaleWidth / ScaleHeight
End Sub

Private Sub Timer1_Timer()
  CreateTexFPS
  FPS = 0
End Sub

Private Sub Timer2_Timer()
  CreateTexMsg
End Sub
