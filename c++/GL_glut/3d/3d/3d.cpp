#include "stdafx.h"
#include <GL/glut.h>
GLfloat rtri,rquad;
void RenderScene(void){
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();
	glTranslatef(-1.5f,0.0f,-6.0f);
	glRotatef(rtri,0.0f,1.0f,1.0f);                 // Вращение пирамиды по оси Y
	glBegin (GL_TRIANGLES);
	glColor3f(1.0f,0.0f,0.0f);                      // Красный
        glVertex3f( 0.0f, 1.0f, 0.0f);                  // Верх треугольника (Передняя)
        glColor3f(0.0f,1.0f,0.0f);                      // Зеленный
        glVertex3f(-1.0f,-1.0f, 1.0f);                  // Левая точка
        glColor3f(0.0f,0.0f,1.0f);                      // Синий
        glVertex3f( 1.0f,-1.0f, 1.0f);                  // Правая точка
	    glColor3f(1.0f,0.0f,0.0f);                      // Красная
        glVertex3f( 0.0f, 1.0f, 0.0f);                  // Верх треугольника (Правая)
        glColor3f(0.0f,0.0f,1.0f);                      // Синия
        glVertex3f( 1.0f,-1.0f, 1.0f);                  // Лево треугольника (Правая)
        glColor3f(0.0f,1.0f,0.0f);                      // Зеленная
        glVertex3f( 1.0f,-1.0f, -1.0f);                 // Право треугольника (Правая)
	    glColor3f(1.0f,0.0f,0.0f);                      // Красный
        glVertex3f( 0.0f, 1.0f, 0.0f);                  // Низ треугольника (Сзади)
        glColor3f(0.0f,1.0f,0.0f);                      // Зеленный
        glVertex3f( 1.0f,-1.0f, -1.0f);                 // Лево треугольника (Сзади)
        glColor3f(0.0f,0.0f,1.0f);                      // Синий
        glVertex3f(-1.0f,-1.0f, -1.0f);                 // Право треугольника (Сзади)
		glColor3f(1.0f,0.0f,0.0f);                      // Красный
        glVertex3f( 0.0f, 1.0f, 0.0f);                  // Верх треугольника (Лево)
        glColor3f(0.0f,0.0f,1.0f);                      // Синий
        glVertex3f(-1.0f,-1.0f,-1.0f);                  // Лево треугольника (Лево)
        glColor3f(0.0f,1.0f,0.0f);                      // Зеленный
        glVertex3f(-1.0f,-1.0f, 1.0f);                  // Право треугольника (Лево)
glEnd();                // Кончили рисовать пирамиду

glLoadIdentity();
glTranslatef(1.5f,0.0f,-7.0f);
glRotatef(rquad,1.0f,1.0f,1.0f);
glBegin(GL_QUADS);                      // Рисуем куб
 glColor3f(0.0f,1.0f,0.0f);              // Синий
        glVertex3f( 1.0f, 1.0f,-1.0f);          // Право верх квадрата (Верх)
        glVertex3f(-1.0f, 1.0f,-1.0f);          // Лево верх
        glVertex3f(-1.0f, 1.0f, 1.0f);          // Лево низ
        glVertex3f( 1.0f, 1.0f, 1.0f);          // Право низ
		  glColor3f(1.0f,0.5f,0.0f);              // Оранжевый
        glVertex3f( 1.0f,-1.0f, 1.0f);          // Верх право квадрата (Низ)
        glVertex3f(-1.0f,-1.0f, 1.0f);          // Верх лево
        glVertex3f(-1.0f,-1.0f,-1.0f);          // Низ лево
        glVertex3f( 1.0f,-1.0f,-1.0f);          // Низ право
		glColor3f(1.0f,0.0f,0.0f);              // Красный
        glVertex3f( 1.0f, 1.0f, 1.0f);          // Верх право квадрата (Перед)
        glVertex3f(-1.0f, 1.0f, 1.0f);          // Верх лево
        glVertex3f(-1.0f,-1.0f, 1.0f);          // Низ лево
        glVertex3f( 1.0f,-1.0f, 1.0f);          // Низ право
		 glColor3f(1.0f,1.0f,0.0f);              // Желтый
        glVertex3f( 1.0f,-1.0f,-1.0f);          // Верх право квадрата (Зад)
        glVertex3f(-1.0f,-1.0f,-1.0f);          // Верх лево
        glVertex3f(-1.0f, 1.0f,-1.0f);          // Низ лево
        glVertex3f( 1.0f, 1.0f,-1.0f);          // Низ право
	   glColor3f(0.0f,0.0f,1.0f);              // Синий
        glVertex3f(-1.0f, 1.0f, 1.0f);          // Верх право квадрата (Лево)
        glVertex3f(-1.0f, 1.0f,-1.0f);          // Верх лево
        glVertex3f(-1.0f,-1.0f,-1.0f);          // Низ лево
        glVertex3f(-1.0f,-1.0f, 1.0f);          // Низ право
		 glColor3f(1.0f,0.0f,1.0f);              // Фиолетовый
        glVertex3f( 1.0f, 1.0f,-1.0f);          // Верх право квадрата (Право)
        glVertex3f( 1.0f, 1.0f, 1.0f);          // Верх лево
        glVertex3f( 1.0f,-1.0f, 1.0f);          // Низ лево
        glVertex3f( 1.0f,-1.0f,-1.0f);          // Низ право
        glEnd();                                // Закончили квадраты
		rtri+=0.2f;
		rquad-=0.15f;
		//return TRUE;
		glFlush();
    //glutSwapBuffers();
}

void TimerFunction(int value)
{
glutPostRedisplay();  // перерисовываем экран
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