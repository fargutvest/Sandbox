#include "stdafx.h"
#include <GL/glut.h>
#include "math.h"
#define PI 3.1415926535898



GLint circle_points = 100;
double  angle, i, a;

void Reshape(int width, int height)
{
  glViewport(0, 0, width, height);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();
  gluOrtho2D(-5, 5, -5, 5);
  glMatrixMode(GL_MODELVIEW);
}

void Draw(void)
{
  glClear(GL_COLOR_BUFFER_BIT);

  glColor3f(1.0f, 0.0f, 0.0f);
  glLineWidth(1);

  glBegin(GL_LINES);
    glVertex2f(-3.0f, 3.0f);  
    glVertex2f(3.0f, 3.0f);  
  glEnd();

  glBegin(GL_LINES);
    glVertex2f(1, 0.0f);  
    glVertex2f(0, -0.5f);  
  glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)-3, sin(angle)-3);
}
glEnd();

glBegin(GL_LINE_LOOP);
for (i = 0; i < circle_points; i++) {
   angle = 2*PI*i/circle_points;
   glVertex2f(cos(angle)*0.03f+2, sin(angle)*0.03f+1);
}
glEnd();
//  код строящий закрашенную окружность
   // glColor3f(1.0f,0.0f,0.0f);
    //glBegin( GL_TRIANGLE_FAN );
      //         glVertex2f( 0.0f, 0.0f ); // вершина в центре круга
        //       for( i = 0; i <= 100; i++ ) {
          //         a = (float)i / 100.0f * 3.1415f * 2.0f;
            //       glVertex2f( cos( a )*0.8f , sin( a )*0.8f  );
              // }
    //glEnd();

  glFlush();  
}

int main(int argc, char *argv[])
{
  glutInit(&argc, argv);
  glutInitWindowSize(1000, 1000);
  glutInitWindowPosition(100, 100);

  glutInitDisplayMode(GLUT_RGB);
  glutCreateWindow("Romka Demo");

  glutReshapeFunc(Reshape);
  glutDisplayFunc(Draw);
  glClearColor(0, 5, 0, 0);

  glutMainLoop();
  return 0;
}