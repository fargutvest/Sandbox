#include "stdafx.h"
#include <stdlib.h>
#include <GL/glut.h>
#include <iostream>
#include <cmath>
 
GLfloat lmodel_ambient[] = {0.2,0.2,0.2,1.0};
GLfloat lposition[] = { 0.0, 0.0, 0, 1.0 };
GLfloat lambient[] = { 0.5, 0.5, 0.5, 1.0 };
GLfloat m_ambitent [] = {0.2,0.2,0.2,1.0};
GLfloat m_emission [] = {1.0,0.0,0.0,0.0};
double spin = 0;
 
void init (){
    glClearColor (0,0.0,0,0);
    glEnable(GL_LIGHTING);
    glLightModelfv(GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);
    glEnable(GL_LIGHT0);
    glEnable(GL_DEPTH_TEST);
}
GLvoid DrawGLScene(){
    glClear (GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glMatrixMode(GL_MODELVIEW);
    glPushMatrix();
        gluLookAt (0, 0, 5, 0.0, 0.0, 0, 0.0, 1.0, 0.0);
    glPushMatrix();
        spin > 360?spin=0:spin+=1;
        glRotated ((GLdouble) spin, 0.0, 1.0, 0.0);
        glTranslated (0.0, 0.0, 2.0);
        glDisable (GL_LIGHTING);
        glColor3f (0.0, 1.0, 1.0);
        glutWireCube (0.1);
        glEnable (GL_LIGHTING);
        glLightfv(GL_LIGHT0,GL_AMBIENT,lambient);
        glLightfv (GL_LIGHT0, GL_POSITION, lposition);
    glPopMatrix();  
        glMaterialfv(GL_FRONT,GL_AMBIENT,m_ambitent);   
        glBegin(GL_TRIANGLE_FAN);
            glVertex3f(0,0,0);
            glNormal3f(0,0,-1);
            for (double i = 410; i>=0; i--){
                glVertex3d(cos(i*0.01705),sin(i*0.01705),0);
                glNormal3f(0,0,1);
            };
        glEnd();
    glLoadIdentity();
    glPopMatrix();
    glutSwapBuffers();
}
void reshape (int w, int h){
   glViewport (0, 0, (GLsizei) w, (GLsizei) h);
   glMatrixMode (GL_PROJECTION);
   glLoadIdentity();
   gluPerspective(40.0, (GLfloat) w/(GLfloat) h, 0.1, 20.0);
   glMatrixMode(GL_MODELVIEW);
   glLoadIdentity();
}
int main (int argc, char* argv[]){
    glutInit (&argc, argv);
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB | GLUT_DEPTH);
    glutInitWindowSize (500, 500);
    glutInitWindowPosition (10, 10);
    glutCreateWindow ("light");
    init();
    glutDisplayFunc (DrawGLScene);
    glutIdleFunc (DrawGLScene);
    glutReshapeFunc (reshape);
    glutMainLoop();
    return 0;
};