Attribute VB_Name = "modUtil"
Option Explicit

Private Type int64
  dw1 As Long
  dw2 As Long
End Type

Public Declare Function FtoDW Lib "msvbvm60" Alias "VarPtr" (ByVal s As Single) As Long
Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

Private Declare Function QueryPerformanceCounter Lib "kernel32" (lpPerformanceCount As int64) As Long
Private Declare Function QueryPerformanceFrequency Lib "kernel32" (lpFrequency As int64) As Long
Dim QSpeed As Double

Public Function QTime() As Double
  Dim QD As int64, t As Double

  QueryPerformanceCounter QD
  If QD.dw1 < 0 Then t = QD.dw1 + 4294967296# Else t = QD.dw1
  If QD.dw2 < 0 Then t = t + (QD.dw2 + 4294967296#) * 4294967296# Else t = t + QD.dw2 * 4294967296#
  QTime = t * QSpeed
End Function

Public Sub QTimeInit()
  Dim QD As int64

  QueryPerformanceFrequency QD
  If QD.dw1 < 0 Then QSpeed = QD.dw1 + 4294967296# Else QSpeed = QD.dw1
  If QD.dw2 < 0 Then QSpeed = QSpeed + (QD.dw2 + 4294967296#) * 4294967296# Else QSpeed = QSpeed + QD.dw2 * 4294967296#
  QSpeed = 1# / QSpeed
End Sub
