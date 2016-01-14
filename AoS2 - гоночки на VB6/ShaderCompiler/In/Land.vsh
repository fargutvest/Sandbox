vs_2_0

// c0..c3 - matrix WorldVievProj
def c5, 0.00078125, -0.00078125, 0.177, 0.5
def c6, 0.01, 0, 0, 10
def c7, 0.35, 0.48, 0.54, 200

dcl_position v0

m4x4 r0, v0, c0
mov oPos, r0
mov r1, c7
add r1.a, r0.z, c6.a
rcp r1.a, r1.a
mul r1.a, r1.a, c7.a
mov oD0, r1

mul r0.xy, v0.xz, c5.xy
add oT0.xy, r0.xy, c5.w
mul oT1.xy, v0.xz, c5.z
mul oT2.xy, v0.xz, c6.x

