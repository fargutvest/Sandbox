Attribute VB_Name = "modTex"
Option Explicit

Public TexMask As Direct3DTexture9
Public TexGrass As Direct3DTexture9
Public TexGround As Direct3DTexture9
Public TexTarm As Direct3DTexture9
Public TexRoad As Direct3DTexture9
Public TexWall As Direct3DTexture9
Public TexSky As Direct3DTexture9
Public TexChrom As Direct3DTexture9
Public TexStop As Direct3DTexture9
Public TexShad As Direct3DTexture9
Public TexStart As Direct3DTexture9

Public Sub TexInit()
  Set TexMask = CreateTextureFromFile(Dev, App.Path & "\data\mask.png")
  Set TexGrass = CreateTextureFromFile(Dev, App.Path & "\data\grass.jpg")
  Set TexGround = CreateTextureFromFile(Dev, App.Path & "\data\ground.jpg")
  Set TexTarm = CreateTextureFromFile(Dev, App.Path & "\data\tarm.jpg")
  Set TexRoad = CreateTextureFromFile(Dev, App.Path & "\data\road.png")
  Set TexWall = CreateTextureFromFile(Dev, App.Path & "\data\wall.png")
  Set TexSky = CreateTextureFromFile(Dev, App.Path & "\data\sky.jpg")
  Set TexChrom = CreateTextureFromFile(Dev, App.Path & "\data\chrom.jpg")
  Set TexStop = CreateTextureFromFile(Dev, App.Path & "\data\stop.jpg")
  Set TexShad = CreateTextureFromFile(Dev, App.Path & "\data\shad.png")
  Set TexStart = CreateTextureFromFile(Dev, App.Path & "\data\start.png")
End Sub

Public Sub TexTerminate()
  Set TexMask = Nothing
  Set TexGrass = Nothing
  Set TexGround = Nothing
  Set TexTarm = Nothing
  Set TexRoad = Nothing
  Set TexWall = Nothing
  Set TexSky = Nothing
  Set TexChrom = Nothing
  Set TexStop = Nothing
  Set TexShad = Nothing
  Set TexStart = Nothing
End Sub
