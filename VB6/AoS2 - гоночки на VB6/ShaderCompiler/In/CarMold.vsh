vs_2_0

// c0..c3 - matrix WorldVievProj
// c4..c6 - matrix 3*3
// c7 - vLight

def c9 , 0.3, 0.15, 0.15, 0 //diffuse
def c10, 0.1, 0.07, 0.07, 0 //ambiente

dcl_position v0
dcl_normal v1

m4x4 oPos, v0, c0

m3x3 r2.xyz, v1, c4
dp3 r0, r2, c7
mul r0, r0, c9
add oD0, r0, c10

