vs_2_0

// c0..c3 - matrix WorldVievProj
def c6, 0, 0, 0, 10
def c7, 0.35, 0.48, 0.54, 200

dcl_position v0
dcl_texcoord v1

m4x4 r0, v0, c0
mov oPos, r0
mov oT0, v1

mov r1, c7
add r1.a, r0.z, c6.a
rcp r1.a, r1.a
mul r1.a, r1.a, c7.a
mov oD0, r1
