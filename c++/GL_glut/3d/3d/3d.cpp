#include "stdafx.h"
#include <GL/glut.h>
GLfloat rtri,rquad;
void RenderScene(void){
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();
	glTranslatef(-1.5f,0.0f,-6.0f);
	glRotatef(rtri,0.0f,1.0f,1.0f);                 // �������� �������� �� ��� Y
	glBegin (GL_TRIANGLES);
	glColor3f(1.0f,0.0f,0.0f);                      // �������
        glVertex3f( 0.0f, 1.0f, 0.0f);                  // ���� ������������ (��������)
        glColor3f(0.0f,1.0f,0.0f);                      // ��������
        glVertex3f(-1.0f,-1.0f, 1.0f);                  // ����� �����
        glColor3f(0.0f,0.0f,1.0f);                      // �����
        glVertex3f( 1.0f,-1.0f, 1.0f);                  // ������ �����
	    glColor3f(1.0f,0.0f,0.0f);                      // �������
        glVertex3f( 0.0f, 1.0f, 0.0f);                  // ���� ������������ (������)
        glColor3f(0.0f,0.0f,1.0f);                      // �����
        glVertex3f( 1.0f,-1.0f, 1.0f);                  // ���� ������������ (������)
        glColor3f(0.0f,1.0f,0.0f);                      // ��������
        glVertex3f( 1.0f,-1.0f, -1.0f);                 // ����� ������������ (������)
	    glColor3f(1.0f,0.0f,0.0f);                      // �������
        glVertex3f( 0.0f, 1.0f, 0.0f);                  // ��� ������������ (�����)
        glColor3f(0.0f,1.0f,0.0f);                      // ��������
        glVertex3f( 1.0f,-1.0f, -1.0f);                 // ���� ������������ (�����)
        glColor3f(0.0f,0.0f,1.0f);                      // �����
        glVertex3f(-1.0f,-1.0f, -1.0f);                 // ����� ������������ (�����)
		glColor3f(1.0f,0.0f,0.0f);                      // �������
        glVertex3f( 0.0f, 1.0f, 0.0f);                  // ���� ������������ (����)
        glColor3f(0.0f,0.0f,1.0f);                      // �����
        glVertex3f(-1.0f,-1.0f,-1.0f);                  // ���� ������������ (����)
        glColor3f(0.0f,1.0f,0.0f);                      // ��������
        glVertex3f(-1.0f,-1.0f, 1.0f);                  // ����� ������������ (����)
glEnd();                // ������� �������� ��������

glLoadIdentity();
glTranslatef(1.5f,0.0f,-7.0f);
glRotatef(rquad,1.0f,1.0f,1.0f);
glBegin(GL_QUADS);                      // ������ ���
 glColor3f(0.0f,1.0f,0.0f);              // �����
        glVertex3f( 1.0f, 1.0f,-1.0f);          // ����� ���� �������� (����)
        glVertex3f(-1.0f, 1.0f,-1.0f);          // ���� ����
        glVertex3f(-1.0f, 1.0f, 1.0f);          // ���� ���
        glVertex3f( 1.0f, 1.0f, 1.0f);          // ����� ���
		  glColor3f(1.0f,0.5f,0.0f);              // ���������
        glVertex3f( 1.0f,-1.0f, 1.0f);          // ���� ����� �������� (���)
        glVertex3f(-1.0f,-1.0f, 1.0f);          // ���� ����
        glVertex3f(-1.0f,-1.0f,-1.0f);          // ��� ����
        glVertex3f( 1.0f,-1.0f,-1.0f);          // ��� �����
		glColor3f(1.0f,0.0f,0.0f);              // �������
        glVertex3f( 1.0f, 1.0f, 1.0f);          // ���� ����� �������� (�����)
        glVertex3f(-1.0f, 1.0f, 1.0f);          // ���� ����
        glVertex3f(-1.0f,-1.0f, 1.0f);          // ��� ����
        glVertex3f( 1.0f,-1.0f, 1.0f);          // ��� �����
		 glColor3f(1.0f,1.0f,0.0f);              // ������
        glVertex3f( 1.0f,-1.0f,-1.0f);          // ���� ����� �������� (���)
        glVertex3f(-1.0f,-1.0f,-1.0f);          // ���� ����
        glVertex3f(-1.0f, 1.0f,-1.0f);          // ��� ����
        glVertex3f( 1.0f, 1.0f,-1.0f);          // ��� �����
	   glColor3f(0.0f,0.0f,1.0f);              // �����
        glVertex3f(-1.0f, 1.0f, 1.0f);          // ���� ����� �������� (����)
        glVertex3f(-1.0f, 1.0f,-1.0f);          // ���� ����
        glVertex3f(-1.0f,-1.0f,-1.0f);          // ��� ����
        glVertex3f(-1.0f,-1.0f, 1.0f);          // ��� �����
		 glColor3f(1.0f,0.0f,1.0f);              // ����������
        glVertex3f( 1.0f, 1.0f,-1.0f);          // ���� ����� �������� (�����)
        glVertex3f( 1.0f, 1.0f, 1.0f);          // ���� ����
        glVertex3f( 1.0f,-1.0f, 1.0f);          // ��� ����
        glVertex3f( 1.0f,-1.0f,-1.0f);          // ��� �����
        glEnd();                                // ��������� ��������
		rtri+=0.2f;
		rquad-=0.15f;
		//return TRUE;
		glFlush();
    //glutSwapBuffers();
}

void TimerFunction(int value)
{
glutPostRedisplay();  // �������������� �����
glutTimerFunc(33,TimerFunction,1);
}
void SetupRC(void){
    glClearColor(0.0f,0.0f,0.0f,1.0f);
	gluOrtho2D (-4.0, 4.0, -4.0, 4.0);
}
void main(void) {
    glutInitDisplayMode( GLUT_RGB);
    glutCreateWindow("Bounce");
    glutDisplayFunc(RenderScene);
	glutTimerFunc(33,TimerFunction,1);
	SetupRC();
    glutMainLoop();
}