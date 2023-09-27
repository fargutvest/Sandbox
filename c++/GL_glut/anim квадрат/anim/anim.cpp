#include "stdafx.h"
#include <GL/glut.h>
GLfloat x1 = 0.0f;   /// GLfloat - аналог float
GLfloat y1 = 0.0f;
GLfloat rsize = 5;

GLfloat xstep = 3.0f;
GLfloat ystep = 2.0f;

GLfloat windowWidth;
GLfloat windowHeight;

void RenderScene(void){
    glClear(GL_COLOR_BUFFER_BIT);
    glColor3f(1.0f,0.0f,0.0f);  //красный
    glRectf(x1,y1,x1+rsize,y1-rsize);   /// рисуем квадрат
    glutSwapBuffers();
}

void SetupRC(void){
    glClearColor(0.0f,0.0f,1.0f,1.0f);
}

void ChangeSize(GLsizei w, GLsizei h) {
    GLfloat aspectRatio;
    if (h == 0)
        h = 1;
    glViewport(0, 0, w, h);
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    aspectRatio = (GLfloat)w / (GLfloat)h;
    if (w <= h){
        windowWidth = 100;
        windowHeight = 100 / aspectRatio;
        glOrtho(-100.0,100.0,-windowHeight,windowHeight,1.0,-1.0);
    } else {
        windowWidth = 100 * aspectRatio;
        windowHeight = 100;
        glOrtho(-windowWidth,windowWidth,-100.0,100.0,1.0,-1.0);
    }
    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
}


int _tmain(int argc, _TCHAR* argv[])
{
	return 0;
}

void TimerFunction(int value){
    if (x1 > windowWidth - rsize || x1 < -windowWidth)   /// проверка на достижение края экрана по OX
        xstep = -xstep;

    if (y1 > windowHeight || y1 < -windowHeight + rsize)  /// ... по OY
        ystep = -ystep;

    x1 += xstep;   // сдвигаем координаты на заданный шаг
    y1 += ystep;

    if (x1 > (windowWidth-rsize+xstep))   /// проверяем не приведёт ли это к выходу за границу экрана
        x1 = windowWidth-rsize-1;
    else if (x1 < -(windowWidth + xstep))  // с другой стороны
        x1 = -windowWidth - 1;

    if (y1 > (windowHeight+ystep))    ///по другим осям
        y1 = windowHeight-1;
    else if(y1 < -(windowHeight-rsize+ystep))
        y1 = -windowHeight+rsize-1;

    glutPostRedisplay();  // перерисовываем экран
    glutTimerFunc(33,TimerFunction,1);  //запускаем таймер заново.
}

int main(int argc,char**argv) {
    glutInit(&argc,argv);
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB);
    glutCreateWindow("Bounce");
    glutDisplayFunc(RenderScene);
    glutReshapeFunc(ChangeSize);
    glutTimerFunc(33,TimerFunction,1);  //первый запуск таймера
    SetupRC();
    glutMainLoop();
    return 0;
}