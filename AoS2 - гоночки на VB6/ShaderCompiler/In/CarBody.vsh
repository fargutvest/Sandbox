vs_2_0

// c0..c3 - matrix WorldVievProj
// c4..c6 - matrix 3*3
// c7 - vLight
// c8 - vCam

//def c9 , 0.4, 0.3, 0.4, 0 //diffuse
//def c10, 0.5, 0.53, 0.5, 0 //ambiente

//def c11, 5, 5, 5, 0 //mul
//def c12, -4.3, -4.3, -4.1, 0 //add

dcl_position v0
dcl_normal v1

m4x4 oPos, v0, c0

m3x3 r2.xyz, v1, c4
dp3 r0, r2, c7
mul r0, r0, c9
add oD0, r0, c10

sub r1, c8, v0
nrm r0, r1
add r1, r0, c7
nrm r0, r1
dp3 r0, r0, r2
mul r0, r0, c11
add oD1, r0, c12

