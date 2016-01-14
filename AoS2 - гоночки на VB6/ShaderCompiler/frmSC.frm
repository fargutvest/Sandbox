VERSION 5.00
Begin VB.Form frmSC 
   Caption         =   "- Shader Compiler -"
   ClientHeight    =   8640
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   11880
   LinkTopic       =   "Form1"
   ScaleHeight     =   576
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   792
   StartUpPosition =   2  'CenterScreen
   Begin VB.PictureBox picASM 
      BackColor       =   &H00C0C0C0&
      Height          =   975
      Left            =   3240
      ScaleHeight     =   61
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   153
      TabIndex        =   8
      Top             =   60
      Width           =   2355
      Begin VB.CommandButton bAsm 
         Caption         =   "Compile Asm"
         Enabled         =   0   'False
         Height          =   480
         Left            =   180
         Style           =   1  'Graphical
         TabIndex        =   9
         ToolTipText     =   "Run"
         Top             =   120
         Width           =   1455
      End
   End
   Begin VB.PictureBox picHLSL 
      BackColor       =   &H00C0C0C0&
      Height          =   975
      Left            =   5760
      ScaleHeight     =   61
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   217
      TabIndex        =   4
      Top             =   60
      Width           =   3315
      Begin VB.CommandButton bHLSL 
         Caption         =   "Compile HLSL"
         Enabled         =   0   'False
         Height          =   480
         Left            =   120
         Style           =   1  'Graphical
         TabIndex        =   7
         ToolTipText     =   "Run"
         Top             =   120
         Width           =   1455
      End
      Begin VB.TextBox txtHLSL 
         Height          =   285
         Left            =   1860
         TabIndex        =   6
         Text            =   "main"
         ToolTipText     =   "Стартовая ф-ция"
         Top             =   480
         Width           =   1275
      End
      Begin VB.ComboBox cmbHLSL 
         Height          =   315
         ItemData        =   "frmSC.frx":0000
         Left            =   1860
         List            =   "frmSC.frx":0028
         Style           =   2  'Dropdown List
         TabIndex        =   5
         Top             =   120
         Width           =   1275
      End
   End
   Begin VB.FileListBox File1 
      Height          =   3210
      Left            =   180
      TabIndex        =   3
      Top             =   1260
      Width           =   1875
   End
   Begin VB.CommandButton bNew 
      Caption         =   "New"
      Height          =   480
      Left            =   120
      Style           =   1  'Graphical
      TabIndex        =   2
      ToolTipText     =   "New"
      Top             =   120
      Width           =   1455
   End
   Begin VB.CommandButton bDel 
      Caption         =   "Delete"
      Enabled         =   0   'False
      Height          =   480
      Left            =   1680
      Style           =   1  'Graphical
      TabIndex        =   1
      ToolTipText     =   "Open"
      Top             =   120
      Width           =   1455
   End
   Begin VB.TextBox txt 
      Enabled         =   0   'False
      BeginProperty Font 
         Name            =   "Courier New"
         Size            =   9.75
         Charset         =   204
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   3330
      Left            =   2100
      MultiLine       =   -1  'True
      ScrollBars      =   3  'Both
      TabIndex        =   0
      Top             =   1260
      Width           =   5100
   End
End
Attribute VB_Name = "frmSC"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (Destination As Any, Source As Any, ByVal Length As Long)
Private Declare Sub AssembleShaderFromFile Lib "d3dx_sc" (ByVal pSrcFile As Long, ByVal Flags As Long, ByRef pShader As Long)
Private Declare Sub CompileShaderFromFile Lib "d3dx_sc" (ByVal pSrcFile As Long, ByVal pFunctionName As Long, ByVal pProfile As Long, ByVal Flags As Long, ByRef pShader As Long)
Private Declare Sub ReleaseBuffer Lib "d3dx_sc" (ByVal pShader As Long)
Private Declare Function GetBufferSize Lib "d3dx_sc" (ByVal pShader As Long) As Long
Private Declare Function GetBufferPointer Lib "d3dx_sc" (ByVal pShader As Long) As Long

Dim Changed As Boolean
Dim MsgRet As VbMsgBoxResult
Dim fName As String

Private Sub ShAssemble()
  Dim sFile As String
  Dim sFN As String
  Dim Ptr As Long
  Dim arPtr As Long
  Dim arSize As Long
  Dim Ar() As Byte
  Dim nf As Integer
  Dim ch As Boolean
  ch = Changed
  sFile = App.Path & "\-temp-.tmp"
  ShSave sFile
  sFN = StrConv(sFile, vbFromUnicode)
  AssembleShaderFromFile StrPtr(sFN), 0, Ptr
  If Ptr <> 0 Then
    arPtr = GetBufferPointer(Ptr)
    arSize = GetBufferSize(Ptr)
    ReDim Ar(arSize - 1)
    CopyMemory Ar(0), ByVal arPtr, arSize
    ReleaseBuffer Ptr
    nf = FreeFile
    Open App.Path & "\out\" & fName & ".shader" For Binary As #nf
    Close #nf
    Kill App.Path & "\out\" & fName & ".shader"
    Open App.Path & "\out\" & fName & ".shader" For Binary As #nf
      Put #nf, , Ar()
    Close #nf
  Else
    MsgBox "Error Compile Shader"
  End If
  Kill sFile
  Changed = ch
End Sub

Private Sub ShCompile()
  Dim sFile As String
  Dim sF As String, sP As String, sFN As String
  Dim Ptr As Long
  Dim arPtr As Long
  Dim arSize As Long
  Dim Ar() As Byte
  Dim nf As Integer
  Dim ch As Boolean
  ch = Changed
  sFile = App.Path & "\-temp-.tmp"
  ShSave sFile
  sFN = StrConv(sFile, vbFromUnicode)
  sP = StrConv(cmbHLSL.List(cmbHLSL.ListIndex), vbFromUnicode)
  sF = StrConv(Trim(txtHLSL.Text), vbFromUnicode)
  CompileShaderFromFile StrPtr(sFN), StrPtr(sF), StrPtr(sP), 0, Ptr
  If Ptr <> 0 Then
    arPtr = GetBufferPointer(Ptr)
    arSize = GetBufferSize(Ptr)
    ReDim Ar(arSize - 1)
    CopyMemory Ar(0), ByVal arPtr, arSize
    ReleaseBuffer Ptr
    nf = FreeFile
    Open App.Path & "\out\" & fName & ".shader" For Binary As #nf
    Close #nf
    Kill App.Path & "\out\" & fName & ".shader"
    Open App.Path & "\out\" & fName & ".shader" For Binary As #nf
      Put #nf, , Ar()
    Close #nf
  Else
    MsgBox "Ошибка компиляции"
  End If
  Kill sFile
  Changed = ch
End Sub

Private Sub ShLoad()
Dim nf As Integer, s As String
  On Local Error GoTo ErrLine
  txt.Text = ""
  Caption = fName
  nf = FreeFile
  Open App.Path & "\in\" & fName For Input As #nf
    Do While Not EOF(nf)
      Line Input #nf, s
      txt.Text = txt.Text & s
      If Not EOF(nf) Then txt.Text = txt.Text & Chr$(13) & Chr$(10)
    Loop
  Close #nf
  Changed = False
  txt.Enabled = True
  bAsm.Enabled = True
  bHLSL.Enabled = True
  Exit Sub
ErrLine:
  MsgBox fName, vbOKOnly, "Error Open File"
  Changed = False
End Sub

Private Sub ShSave(fN As String)
Dim nf As Integer, s As String
  On Local Error GoTo ErrLine
  nf = FreeFile
  Open fN For Output As #nf
    Print #nf, txt.Text
  Close #nf
  Changed = False
  Exit Sub
ErrLine:
  MsgBox fName, vbOKOnly, "Error Save File"
  Changed = False
End Sub

Private Sub bAsm_Click()
  ShAssemble
End Sub

Private Sub bHLSL_Click()
  ShCompile
End Sub

Private Sub bDel_Click()
  Kill App.Path & "\in\" & File1.List(File1.ListIndex)
  File1.Refresh
  bDel.Enabled = False
End Sub

Private Sub bNew_Click()
  Dim s As String
  Dim nf As Integer
  s = Trim(InputBox("Enter Name", "New Shader"))
  If s <> "" Then
    nf = FreeFile
    Open App.Path & "\in\" & s For Binary As #nf
    Close #nf
    File1.Refresh
  End If
End Sub

Private Sub File1_Click()
  If File1.ListIndex > -1 Then
    bDel.Enabled = True
  Else
    bDel.Enabled = False
  End If
End Sub

Private Sub File1_DblClick()
  If Changed Then
    MsgRet = MsgBox("Save?", vbYesNoCancel, "File Not Saved")
    If MsgRet = vbCancel Then
      Exit Sub
    ElseIf MsgRet = vbYes Then
      ShSave App.Path & "\in\" & fName
    End If
  End If
  fName = File1.List(File1.ListIndex)
  ShLoad
End Sub

Private Sub Form_Load()
  Me.Show
  Changed = False
  ChDrive App.Path
  ChDir App.Path
  File1.Path = App.Path & "\in"
  cmbHLSL.ListIndex = 0

  bNew.Move 10, 12, 65, 28
  bDel.Move 86, 12, 65, 28
  picASM.Move 164, 0, 80, 52
  bAsm.Move 6, 6, 65, 36
  picHLSL.Move 248, 0, 250, 52
  bHLSL.Move 6, 6, 65, 36
  cmbHLSL.Move 76, 2, 165
  txtHLSL.Move 76, 24, 165, 22
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
  If Changed Then
    MsgRet = MsgBox("Save Changes?", vbYesNoCancel, "TFScript")
    If MsgRet = vbYes Then
      ShSave App.Path & "\in\" & fName
    ElseIf MsgRet = vbCancel Then
      Cancel = 1
    End If
  End If
End Sub

Private Sub Form_Resize()
  If WindowState = vbMinimized Then Exit Sub
  If Width < 566 * Screen.TwipsPerPixelX Then Width = 566 * Screen.TwipsPerPixelX
  If Height < 224 * Screen.TwipsPerPixelY Then Height = 224 * Screen.TwipsPerPixelY
  File1.Move 0, 51, 160, ScaleHeight - 30
  txt.Move 160, 52, ScaleWidth - 160, ScaleHeight - 32
End Sub

Private Sub txt_Change()
  Changed = True
End Sub
