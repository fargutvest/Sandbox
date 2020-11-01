#include "stdafx.h"
#include <GL/glut.h>
GLfloat rtri=3;
GLfloat x1=-20,x2=0,x3=20,y1=0,y2=20,y3=0;
void RenderScene(void){
    glClear(GL_COLOR_BUFFER_BIT);
	glRotatef(rtri,0.0f,0.0f,1.0f);  
	glBegin (GL_TRIANGLES);
	glColor3f(1.0f,0.0f,0.0f);
	glVertex2f(x1, y1);
	glVertex2f(x2, y2);
	glVertex2f(x3, y3);
	glEnd();
	glFlush();
    //glutSwapBuffers();
}
void TimerFunction(int value)
{x1++;
glutPostRedisplay();  // перерисовываем экран
glutTimerFunc(33,TimerFunction,1);
}
void SetupRC(void){
    glClearColor(0.0f,0.0f,1.0f,1.0f);
	gluOrtho2D (-40.0, 40.0, -40.0, 30.0);
}
void main(void) {
    glutInitDisplayMode( GLUT_RGB);
    glutCreateWindow("Bounce");
    glutDisplayFunc(RenderScene);
	glutTimerFunc(33,TimerFunction,1);
	SetupRC();
    glutMainLoop();
}