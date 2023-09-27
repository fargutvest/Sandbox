#include "stdafx.h"
#include <GL/glut.h>
#include "math.h"
#define PI 3.1415926535898
GLfloat x,y,z,q,w,schet;
GLfloat ugol= 0.3;
GLfloat step = 2;
GLfloat step1 = 4;
GLfloat step2 = 6;
GLfloat step3 = 8;
GLfloat step4 = 1;
GLint circle_points = 100;
double  angle, i, a;

void Reshape(int width, int height)
{
  glViewport(0, 0, width, height);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();
  gluOrtho2D(-300, 300, -300, 300);
  glMatrixMode(GL_MODELVIEW);
}

void Draw(void)
{
  glClear(GL_COLOR_BUFFER_BIT);

  glColor3f(1.0f, 1.0f, 0.0f);
  glLineWidth(1);
  //body
    glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.49f, 0.51f, 0.011f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*43.0f-175 , sin( a )*43.0f+125  );
               }
			   glEnd();

			      glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.49f, 0.51f, 0.011f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*43.0f+160 , sin( a )*43.0f+153  );
               }
			   glEnd();

			      glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.49f, 0.51f, 0.011f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*43.0f-148 , sin( a )*43.0f-170  );
               }
			   glEnd();

			      glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.49f, 0.51f, 0.011f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*43.0f+35 , sin( a )*43.0f+35  );
               }
			   glEnd();

			      glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.49f, 0.51f, 0.011f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*43.0f+158 , sin( a )*43.0f-152  );
               }
			   glEnd();
//heads

			        glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.98f, 0.396f, 0.113f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*21.0f-148 , sin( a )*21.0f+182  );
               }
			   glEnd();

			        glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.98f, 0.396f, 0.113f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*21.0f+10 , sin( a )*21.0f+92  );
               }
			   glEnd();

			        glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.98f, 0.396f, 0.113f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*21.0f+187 , sin( a )*21.0f+211  );
               }
			   glEnd();

			        glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.98f, 0.396f, 0.113f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*21.0f-170 , sin( a )*21.0f-111  );
               }
			   glEnd();

			        glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.98f, 0.396f, 0.113f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*21.0f+181 , sin( a )*21.0f-93  );
               }
			   glEnd();

			   //глаза
			          glBegin( GL_TRIANGLE_FAN );
	           glColor3f(1.0f, 1.0f, 1.0f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*4.0f-144 , sin( a )*4.0f+189  );
               }
			   glEnd();

			            glBegin( GL_TRIANGLE_FAN );
	           glColor3f(1.0f, 1.0f, 1.0f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*4.0f+4 , sin( a )*4.0f+97  );
               }
			   glEnd();

			            glBegin( GL_TRIANGLE_FAN );
	           glColor3f(1.0f, 1.0f, 1.0f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*4.0f+193 , sin( a )*4.0f+217  );
               }
			   glEnd();

			            glBegin( GL_TRIANGLE_FAN );
	           glColor3f(1.0f, 1.0f, 1.0f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*4.0f-176 , sin( a )*4.0f-106  );
               }
			   glEnd();

			            glBegin( GL_TRIANGLE_FAN );
	           glColor3f(1.0f, 1.0f, 1.0f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*4.0f+187 , sin( a )*4.0f-87  );
               }
			   glEnd();
			   //зрачки
			             glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.0f, 0.0f, 0.0f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*1.5f+187 , sin( a )*1.5f-87  );
               }
			   glEnd();

			   
			           glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.0f, 0.0f, 0.0f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*1.5f+4 , sin( a )*1.5f+97  );
               }
			   glEnd();
			   
			            glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.0f, 0.0f, 0.0f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*1.5f+193 , sin( a )*1.5f+217  );
               }
			   glEnd();

			            glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.0f, 0.0f, 0.0f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*1.5f-176 , sin( a )*1.5f-106  );
               }
			   glEnd();

			            glBegin( GL_TRIANGLE_FAN );
	           glColor3f(0.0f, 0.0f, 0.0f);
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*1.5f-144 , sin( a )*1.5f+189  );
               }
			   glEnd();

  glColor3f(1.0f, 1.0f, 0.0f);

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*43-175, sin(angle)*43+125);
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*43+35, sin(angle)*43+35);
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*43+160, sin(angle)*43+153);
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*43-148, sin(angle)*43-170);
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*43+158, sin(angle)*43-152);
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*21-148, sin(angle)*21+182);
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*21+10, sin(angle)*21+92);
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*21+187, sin(angle)*21+211);
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*21-170, sin(angle)*21-111);
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*21+181, sin(angle)*21-93);
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*4-144, sin(angle)*4+189);    //--------
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*4+4, sin(angle)*4+97);  
}
glEnd();




glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*4+193, sin(angle)*4+217);  //-------
}
glEnd();



glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*4-176, sin(angle)*4-106);  
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*4+187, sin(angle)*4-87);  
}
glEnd();


glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*1.5-141, sin(angle)*1.5+189);  
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*1.5+1, sin(angle)*1.5+97);  
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*1.5+195, sin(angle)*1.5+217);  
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*1.5-179, sin(angle)*1.5-106);  
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*1.5+190, sin(angle)*1.5-88);  
}
glEnd();

  glBegin(GL_LINES);
  glVertex2f(-178.0f, 98.0f);  
  glVertex2f(-194.0f, 64.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(38.0f, 6.0f);  
  glVertex2f(52.0f, -28.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(160.0f, 126.0f);  
  glVertex2f(144.0f, 92.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-143.0f, -197.0f);  
  glVertex2f(-127.0f, -231.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(154.0f, -179.0f);  
  glVertex2f(138.0f, -213.0f);  
  glEnd();




    glBegin(GL_LINES);
  glVertex2f(-164.0f, 84.0f);  
  glVertex2f(-160.0f, 58.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(23.0f, -7.0f);  
  glVertex2f(19.0f, -34.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(174.0f, 113.0f);  
  glVertex2f(178.0f, 86.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-157.0f, -211.0f);  
  glVertex2f(-161.0f, -237.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(168.0f, -193.0f);  
  glVertex2f(172.0f, -219.0f);  
  glEnd();

  //------клювы
    glBegin(GL_LINES);
  glVertex2f(-127.0f, 183.0f);  
  glVertex2f(-115.0f, 177.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-11.0f, 91.0f);  
  glVertex2f(-26.0f, 85.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(208.0f, 211.0f);  
  glVertex2f(223.0f, 205.0f);  
  glEnd();


      glBegin(GL_LINES);
  glVertex2f(-206.0f, -118.0f);  
  glVertex2f(-191.0f, -111.0f);  
  glEnd();

      glBegin(GL_LINES);
  glVertex2f(202.0f, -93.0f);  
  glVertex2f(217.0f, -100.0f);  
  glEnd();





      glBegin(GL_LINES);
  glVertex2f(-128.0f, 175.0f);  
  glVertex2f(-115.0f, 177.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-10.0f, 83.0f);  
  glVertex2f(-26.0f, 85.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(207.0f, 203.0f);  
  glVertex2f(223.0f, 205.0f);  
  glEnd();


      glBegin(GL_LINES);
  glVertex2f(-206.0f, -118.0f);  
  glVertex2f(-190.0f, -120.0f);  
  glEnd();

      glBegin(GL_LINES);
  glVertex2f(201.0f, -102.0f);  
  glVertex2f(217.0f, -100.0f);  
  glEnd();





  

      glBegin(GL_LINES);
  glVertex2f(-127.0f, 179.0f);  
  glVertex2f(-115.0f, 177.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-12.0f, 87.0f);  
  glVertex2f(-26.0f, 85.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(208.0f, 207.0f);  
  glVertex2f(223.0f, 205.0f);  
  glEnd();


      glBegin(GL_LINES);
  glVertex2f(-191.0f, -116.0f);  
  glVertex2f(-206.0f, -118.0f); 
  glEnd();

      glBegin(GL_LINES);
  glVertex2f(203.0f, -97.0f);  
  glVertex2f(217.0f, -100.0f);  
  glEnd();







  glBegin(GL_LINES);
  glVertex2f(-200.0f, 62.0f);  
  glVertex2f(-194.0f, 64.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(47.0f, -32.0f);  
  glVertex2f(52.0f, -28.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(138.0f, 90.0f);  
  glVertex2f(144.0f, 92.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-135.0f, -235.0f);  
  glVertex2f(-127.0f, -231.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(132.0f, -215.0f);  
  glVertex2f(138.0f, -213.0f);  
  glEnd();



    glBegin(GL_LINES);
  glVertex2f(-200.0f, 62.0f);  
  glVertex2f(-194.0f, 64.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(47.0f, -32.0f);  
  glVertex2f(52.0f, -28.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(138.0f, 90.0f);  
  glVertex2f(144.0f, 92.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-135.0f, -235.0f);  
  glVertex2f(-127.0f, -231.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(132.0f, -215.0f);  
  glVertex2f(138.0f, -213.0f);  
  glEnd();





    glBegin(GL_LINES);
  glVertex2f(-188.0f, 60.0f);  
  glVertex2f(-194.0f, 64.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(59.0f, -30.0f);  
  glVertex2f(52.0f, -28.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(150.0f, 89.0f);  
  glVertex2f(144.0f, 92.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-121.0f, -233.0f);  
  glVertex2f(-127.0f, -231.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(144.0f, -217.0f);  
  glVertex2f(138.0f, -213.0f);  
  glEnd();





  glBegin(GL_LINES);
  glVertex2f(-154.0f, 57.0f);  
  glVertex2f(-160.0f, 58.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(24.0f, -37.0f);  
  glVertex2f(19.0f, -34.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(186.0f, 82.0f);  
  glVertex2f(178.0f, 86.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-154.0f, -238.0f);  
  glVertex2f(-161.0f, -237.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(181.0f, -222.0f);  
  glVertex2f(172.0f, -219.0f);  
  glEnd();





    glBegin(GL_LINES);
  glVertex2f(-166.0f, 56.0f);  
  glVertex2f(-160.0f, 58.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(11.0f, -34.0f);  
  glVertex2f(19.0f, -34.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(171.0f, 84.0f);  
  glVertex2f(178.0f, 86.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-169.0f, -240.0f);  
  glVertex2f(-161.0f, -237.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(172.0f, -219.0f);  
  glVertex2f(166.0f, -225.0f);  
  glEnd();



  //   крылышки


  //    линии 1 - 2
  //glLoadIdentity();
 // glTranslatef(1.5f, 0.0f, -5.0f);
  
  glRotatef(ugol,0.0f,0.0f,1.0f);

  glBegin (GL_TRIANGLES);
  	glColor3f(1.0f,0.0f,0.0f);
	glVertex2f(45, 53);
	glVertex2f(73, 85+q);
	glVertex2f(84, 68+q);
	glEnd();
	 //glLoadIdentity();

	 glBegin (GL_TRIANGLES);
	glColor3f(1.0f,0.0f,0.0f);
	glVertex2f(152, 172);
	glVertex2f(124+x, 205+x);
	glVertex2f(113+x, 188+x);
	glEnd();

	
	 glBegin (GL_TRIANGLES);
	glColor3f(1.0f,0.0f,0.0f);
	glVertex2f(-135, -151);
	glVertex2f(-107,-118+w );
	glVertex2f(-96, -135+w);
	glEnd();

	 glBegin (GL_TRIANGLES);
	glColor3f(1.0f,0.0f,0.0f);
	glVertex2f(146, -133);
	glVertex2f(118+y, -100+y);
	glVertex2f(107+y, -117+y);
	glEnd();

	 glBegin (GL_TRIANGLES);
	glColor3f(1.0f,0.0f,0.0f);
	glVertex2f(-186, 144);
	glVertex2f(-214+z, 177+z);
	glVertex2f(-220+z, 160+z);
	glEnd();

	glColor3f(1.0f,1.0f,0.0f);
   /* glBegin(GL_LINES);
  glVertex2f(-186.0f, 144.0f);  
  glVertex2f(-214.0f, 177.0f);   
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(45.0f, 53.0f);  
 glVertex2f(73.0f, 85.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(152.0f, 172.0f);  
  glVertex2f(124.0f, 205.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-135.0f, -151.0f);  
  glVertex2f(-107.0f, -118.0f);
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(146.0f, -133.0f);  
  glVertex2f(118.0f, -100.0f);  
  glEnd();




  // линии  2 - 3

    glBegin(GL_LINES);
  glVertex2f(-214.0f, 177.0f);  
  glVertex2f(-220.0f, 160.0f);    
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(73.0f, 85.0f);  
 glVertex2f(84.0f, 68.0f); 
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(124.0f, 205.0f);  
  glVertex2f(113.0f, 188.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-107.0f, -118.0f);  
  glVertex2f(-96.0f, -135.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(118.0f, -100.0f);  
  glVertex2f(107.0f, -117.0f); 
  glEnd();


  //  линии  3 - 1


      glBegin(GL_LINES);
  glVertex2f(-220.0f, 160.0f);  
   glVertex2f(-186.0f, 144.0f);    
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(84.0f, 68.0f);  
 glVertex2f(45.0f, 53.0f);
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(113.0f, 188.0f);  
   glVertex2f(152.0f, 172.0f);
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(-96.0f, -135.0f);  
  glVertex2f(-135.0f, -151.0f);  
  glEnd();

    glBegin(GL_LINES);
  glVertex2f(107.0f, -117.0f);  
 glVertex2f(146.0f, -133.0f);  
  glEnd();*/

 /* //код строящий закрашенную окружность
    glColor3f(1.0f,0.0f,0.0f);
    glBegin( GL_TRIANGLE_FAN );
               glVertex2f( 0.0f, 0.0f ); // вершина в центре круга
               for( i = 0; i <= 100; i++ ) {
                   a = (float)i / 100.0f * 3.1415f * 2.0f;
                   glVertex2f( cos( a )*5.0f , sin( a )*5.8f  );
               }
    glEnd();*/

  glFlush();  
}

void TimerFunction(int value)
{
	if (x>20 ) step=-step;
	if (x<-20 ) step=-step;
		x=x+step;
if (y>20 ) step1=-step1;
	if (y<-20 ) step1=-step1;
		y=y+step1;
if (z>20 ) step2=-step2;
	if (z<-20 ) step2=-step2;
		z=z+step2;

		if (q>20 ) step3=-step3;
	if (q<-20 ) step3=-step3;
		q=q+step3;

	if (w>20 ) step4=-step4;
	if (w<-20 ) step4=-step4;
		w=w+step4;
		if (schet>100) ugol=-ugol,schet=-100;
			schet=schet+1;

glutPostRedisplay();  // перерисовываем экран
glutTimerFunc(33,TimerFunction,1);
}

int main(int argc, char *argv[])
{
  glutInit(&argc, argv);
  glutInitWindowSize(1000, 1000);
  glutInitWindowPosition(0, 0);

  glutInitDisplayMode(GLUT_RGB);
  glutCreateWindow("Myslicki lab2");
  glutTimerFunc(33,TimerFunction,1);
  glutReshapeFunc(Reshape);
  glutDisplayFunc(Draw);
  glClearColor(0, 0, 0, 0);

  glutMainLoop();
  return 0;
}