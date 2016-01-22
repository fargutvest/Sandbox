vs_2_0

// c0..c3 - matrix WorldVievProj
// c4..c6 - matrix 3*3
// c7 - vLight
// c8 - vCam

def c9, -0.5, 0.5, 0, 0

dcl_position v0
dcl_normal v1

m4x4 oPos, v0, c0
m3x3 r0.xyz, v1, c4

sub r1, c8, v0
nrm r2, r1
crs r1.xyz, r2, r0
crs r3.xyz, r1, r0
add r1, r3, r3
add r1, r1, r2
mad r2.y, r1.y, c9.x, c9.y
max r2.y, r2.y, c9.z
mov r1.y, c9.w
nrm r0, r1
mad r0, r0, r2.y, c9.y
mov oT0, -r0.xz
