#include "stdafx.h"
#include <GL/glut.h>
void display()
{
	glClear(GL_COLOR_BUFFER_BIT);
	glRotatef(1,1,1,0);
	glBegin(GL_LINE_STRIP);
	glVertex3f(-50,-50,-50);
	glVertex3f(50,-50,-50);
	glVertex3f(50,50,-50);
	glVertex3f(-50,50,-50);
	glVertex3f(-50,-50,-50);
	glEnd();
	/*	glBegin(GL_LINE_STRIP);
	glVertex3f(-50,-50,50);
	glVertex3f(50,-50,50);
	glVertex3f(50,50,50);
	glVertex3f(-50,50,50);
	glVertex3f(-50,-50,50);
	glEnd();
			glBegin(GL_LINES);
	glVertex3f(-50,-50,50);
	glVertex3f(-50,-50,-50);
	glVertex3f(50,-50,50);
	glVertex3f(50,-50,-50);
	glVertex3f(50,50,50);
	glVertex3f(50,50,-50);
	glVertex3f(-50,50,50);
	glVertex3f(-50,50,-50);
	glEnd();*/
	glutSwapBuffers();

}
void timer (int=0)
{
	display();
	glutTimerFunc(10, timer, 0);
}
int main (int argc, char **argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB);
	glutInitWindowSize(500,500);
	glutInitWindowPosition(20, 20);
	glutCreateWindow("cube");
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(-100,100,-100,100,-100,100);
	glutDisplayFunc(display);
	timer();
	glutMainLoop();
}