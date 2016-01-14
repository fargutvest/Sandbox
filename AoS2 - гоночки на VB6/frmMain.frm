VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   5985
   ClientLeft      =   45
   ClientTop       =   45
   ClientWidth     =   7050
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   BeginProperty Font 
      Name            =   "Times New Roman"
      Size            =   12
      Charset         =   204
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   ForeColor       =   &H00FFFFFF&
   Icon            =   "frmMain.frx":0000
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   399
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   470
   StartUpPosition =   2  'CenterScreen
   Begin VB.PictureBox picFinal 
      AutoRedraw      =   -1  'True
      BackColor       =   &H0000C000&
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   24
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H0000FFFF&
      Height          =   495
      Left            =   4680
      ScaleHeight     =   33
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   149
      TabIndex        =   23
      Top             =   4200
      Visible         =   0   'False
      Width           =   2235
   End
   Begin VB.CommandButton bStart 
      Caption         =   "Hold your opponents"
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   12
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   435
      Index           =   2
      Left            =   300
      TabIndex        =   16
      ToolTipText     =   "Гонка продолжается, пока один из соперников не достигнет финиша. Не дайте ему этого сделать!"
      Top             =   5280
      Width           =   4275
   End
   Begin VB.CommandButton bStart 
      Caption         =   "Come first"
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   12
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   435
      Index           =   1
      Left            =   300
      TabIndex        =   15
      ToolTipText     =   "Задача - первому попасть на финиш."
      Top             =   4680
      Width           =   4275
   End
   Begin VB.Frame Frame2 
      Caption         =   "Transmission"
      Height          =   2055
      Left            =   3240
      TabIndex        =   8
      Top             =   1380
      Width           =   3495
      Begin VB.OptionButton opKPP 
         Caption         =   "Automatic for blondes :)"
         Height          =   225
         Index           =   3
         Left            =   240
         TabIndex        =   12
         Top             =   1320
         Width           =   3135
      End
      Begin VB.OptionButton opKPP 
         Caption         =   "Automatic for beginners"
         Height          =   225
         Index           =   2
         Left            =   240
         TabIndex        =   11
         Top             =   1020
         Width           =   3135
      End
      Begin VB.OptionButton opKPP 
         Caption         =   "Automatic"
         Height          =   225
         Index           =   1
         Left            =   240
         TabIndex        =   10
         Top             =   720
         Width           =   2895
      End
      Begin VB.OptionButton opKPP 
         Caption         =   "Manual"
         Height          =   225
         Index           =   0
         Left            =   240
         TabIndex        =   9
         Top             =   420
         Width           =   2895
      End
   End
   Begin VB.PictureBox picCnt 
      AutoRedraw      =   -1  'True
      BackColor       =   &H00000000&
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   24
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H004040FF&
      Height          =   495
      Left            =   5220
      ScaleHeight     =   33
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   149
      TabIndex        =   21
      Top             =   2940
      Visible         =   0   'False
      Width           =   2235
   End
   Begin VB.CheckBox chVSync 
      Caption         =   "VSync"
      Height          =   315
      Left            =   600
      TabIndex        =   13
      Top             =   3540
      Value           =   1  'Checked
      Width           =   2415
   End
   Begin VB.CommandButton bCancel 
      Caption         =   "Exit"
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   12
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   435
      Left            =   5340
      TabIndex        =   17
      Top             =   5280
      Width           =   1395
   End
   Begin VB.CommandButton bStart 
      Caption         =   "Test Drive"
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   12
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   435
      Index           =   0
      Left            =   300
      TabIndex        =   14
      ToolTipText     =   "Изучить трассу. Потренироваться."
      Top             =   4080
      Width           =   4275
   End
   Begin VB.Frame Frame1 
      Caption         =   "Texture Filter"
      Height          =   2055
      Left            =   300
      TabIndex        =   2
      Top             =   1380
      Width           =   2775
      Begin VB.OptionButton opF 
         Caption         =   "Anisotropy 8x"
         Height          =   225
         Index           =   4
         Left            =   300
         TabIndex        =   7
         Top             =   1620
         Width           =   2355
      End
      Begin VB.OptionButton opF 
         Caption         =   "Anisotropy 4x"
         Height          =   225
         Index           =   3
         Left            =   300
         TabIndex        =   6
         Top             =   1320
         Width           =   2355
      End
      Begin VB.OptionButton opF 
         Caption         =   "Anisotropy 2x"
         Height          =   225
         Index           =   2
         Left            =   300
         TabIndex        =   5
         Top             =   1020
         Width           =   2355
      End
      Begin VB.OptionButton opF 
         Caption         =   "Trilinear"
         Height          =   225
         Index           =   1
         Left            =   300
         TabIndex        =   4
         Top             =   720
         Width           =   2355
      End
      Begin VB.OptionButton opF 
         Caption         =   "Bilinear"
         Height          =   225
         Index           =   0
         Left            =   300
         TabIndex        =   3
         Top             =   420
         Width           =   2355
      End
   End
   Begin VB.PictureBox picW 
      AutoRedraw      =   -1  'True
      BackColor       =   &H0000C000&
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "Courier New"
         Size            =   24
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H0000FFFF&
      Height          =   495
      Left            =   5280
      ScaleHeight     =   33
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   149
      TabIndex        =   1
      Top             =   3240
      Visible         =   0   'False
      Width           =   2235
   End
   Begin VB.PictureBox picFPS 
      AutoRedraw      =   -1  'True
      BackColor       =   &H0000C000&
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "Courier New"
         Size            =   24
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H0000FFFF&
      Height          =   495
      Left            =   5100
      ScaleHeight     =   33
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   149
      TabIndex        =   0
      Top             =   3600
      Visible         =   0   'False
      Width           =   2235
   End
   Begin VB.PictureBox picMsg 
      AutoRedraw      =   -1  'True
      BackColor       =   &H0000C000&
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "Courier New"
         Size            =   24
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H0000FFFF&
      Height          =   495
      Left            =   4860
      ScaleHeight     =   33
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   149
      TabIndex        =   22
      Top             =   3900
      Visible         =   0   'False
      Width           =   2235
   End
   Begin VB.Label Label3 
      Alignment       =   2  'Center
      BackStyle       =   0  'Transparent
      Caption         =   "Access of Speed 2"
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   24
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF8080&
      Height          =   615
      Left            =   105
      TabIndex        =   20
      Top             =   105
      Width           =   6795
   End
   Begin VB.Label Label2 
      Alignment       =   2  'Center
      Caption         =   "Demo version for GameDev.ru contest"
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   14.25
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   315
      Left            =   60
      TabIndex        =   19
      Top             =   780
      Width           =   6915
   End
   Begin VB.Label Label1 
      Alignment       =   2  'Center
      Caption         =   "Access of Speed 2"
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   24
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      Height          =   615
      Left            =   120
      TabIndex        =   18
      Top             =   120
      Width           =   6795
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim inMnu As Boolean

Private Sub Form_Load()
  opF(3).Value = True
  opKPP(1).Value = True
End Sub

Public Sub WaitStart()
  inMnu = True
  Do While inMnu
    DoEvents
    Sleep 1
  Loop
End Sub

Private Sub bStart_Click(Index As Integer)
  GameMode = Index
  If Index = 0 Then AICnt = 0 Else AICnt = 3
  Running = True
  inMnu = False
End Sub

Private Sub bCancel_Click()
  Running = False
  inMnu = False
End Sub

Private Sub opF_Click(Index As Integer)
  TexFType = Index
End Sub
