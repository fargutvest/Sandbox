#include "stdafx.h"
#include <GL/glut.h>



void main (void)
{ glutInitDisplayMode(GLUT_SINGLE, GLUT_RGB);
  glutCreateWindow("Пример")
  glutDicplayFunc(display);
  MyInitFunction();
  glutMainLoop();
}

void display(void) {
glClearColor(0.7, 0.7, 0.9, 1.0);
glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
glMatrixMode(GL_MODELVIEW);
glLoadIdentity();
glTranslatef(camXPos, camYPos, camZPos);
 
glPushMatrix();
glTranslatef(joint_trans_x_root, 0.0, 0.0);
glTranslatef(0.0, joint_trans_y_root, 0.0);
glTranslatef(0.0, 0.0, joint_trans_z_root);
glRotatef(joint_rot_x_root, 1.0, 0.0, 0.0);
glRotatef(joint_rot_y_root, 0.0, 1.0, 0.0);
glRotatef(joint_rot_z_root, 0.0, 0.0, 1.0);
 
glPushMatrix();
glRotatef(joint_rot_head, 0.0, 0.0, 1.0);
glRotatef(joint_pitch_head, 1.0, 0.0, 0.0);
glTranslatef(0.0, -3.0, 0.0);
glPushMatrix();
glColor3f(1.0,1.0,0.0);
glutSolidSphere(4.0, 20.0, 20.0);
glPopMatrix();
 
 
//irokez
glPushMatrix();
glBegin(GL_POLYGON);
glColor3f(1.0, 0.7, 0.0);
glVertex2f(-0.5, 1);
glVertex2f(-1.5, 3);
glVertex2f(-0.25, 2);
glVertex2f(0, 3.5);
glVertex2f(0.5, 2);
glVertex2f(1.5, 3);
glVertex2f(0.75, 1);
glEnd();
glPopMatrix();
 
//kluv
glPushMatrix();
glTranslatef(0.0, -0.5, 4.0);
glColor3f(1.0, 0.7, 0.0);
glutSolidCone(1.0, 2.0, 10, 2);
glPopMatrix();
 
//eyes
glPushMatrix();
glTranslatef(-1.0, 1.5, 4.0);
glColor3f(1.0, 1.0, 1.0);
glutSolidSphere(1, 10, 10);
glPopMatrix();
 
glPushMatrix();
glTranslatef(1.0, 1.5, 4.0);
glColor3f(1.0, 1.0, 1.0);
glutSolidSphere(1, 10, 10);
glPopMatrix();
 
glPushMatrix();
glTranslatef(-1.2, 1.5, 4.4);
glColor3f(0.0, 0.0, 1.0);
glutSolidSphere(0.6, 10, 10);
glPopMatrix();
 
glPushMatrix();
glTranslatef(0.8, 1.5, 4.4);
glColor3f(0.0, 0.0, 1.0);
glutSolidSphere(0.6, 10, 10);
glPopMatrix();
 
glPushMatrix();
glTranslatef(-1.2, 1.5, 4.5);
glColor3f(0.0, 0.0, 1.0);
glutSolidSphere(0.3, 10, 10);
glPopMatrix();
 
glPushMatrix();
glTranslatef(0.8, 1.5, 4.5);
glColor3f(0.0, 0.0, 1.0);
glutSolidSphere(0.3, 10, 10);
glPopMatrix();
 
 
 
//nogi
glPushMatrix();
glBegin(GL_TRIANGLES);
glColor3f(1.0, 0.7, 0.0);
glVertex2f(-3, -14);
glVertex2f(-5.5, -16.5);
glVertex2f(-0.5, -16.5);
glEnd();
glPopMatrix();
 
glPushMatrix();
glBegin(GL_TRIANGLES);
glVertex2f(3, -14);
glVertex2f(5.5, -16.5);
glVertex2f(0.5, -16.5);
glEnd();
glPopMatrix();
glPopMatrix();
 
//hands
glPushMatrix(); 
glTranslatef(0.5, 0, 0); 
//glTranslatef(-12.625, -9.625, 0);  
glRotatef(joint_rot_x_hand_l, 1.0, 0.0, 0.0); 
glRotatef(joint_rot_y_hand_l, 0.0, 1.0, 0.0); 
glRotatef(joint_rot_z_hand_l, 0.0, 0.0, 1.0); 
glScalef(2.5, 0.5, 0.5); 
glBegin(GL_TRIANGLES);
glColor3f(1.0, 0.7, 0.0);
glVertex3f(1.5, -14, 0);
glVertex3f(2.5, -22, 0);
glVertex3f(4, -21, 0);
glEnd(); 
glPopMatrix(); 
         
glPushMatrix(); 
glTranslatef(-0.5, 0, 0); 
//glTranslatef(-2, -1, 0);
glRotatef(joint_rot_x_hand_r, 1.0, 0.0, 0.0); 
glRotatef(joint_rot_y_hand_r, 0.0, 1.0, 0.0); 
glRotatef(joint_rot_z_hand_r, 0.0, 0.0, 1.0); 
glScalef(2.5, 0.5, 0.5); 
glBegin(GL_TRIANGLES);
glColor3f(1.0, 0.7, 0.0);
glVertex3f(-1.5, -14, 0);
glVertex3f(-2.5, -22, 0);
glVertex3f(-4, -21, 0);
glEnd();
glPopMatrix();  
 
//body
glPushMatrix();
glTranslatef(0.0, -9.8,0.0);
glColor3f(1.0,1.0,0.0);
glutSolidSphere(5.0, 20, 20);
glPopMatrix();
glPopMatrix();
glFlush();
}