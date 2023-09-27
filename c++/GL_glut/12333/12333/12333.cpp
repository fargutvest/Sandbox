#include "stdafx.h"
#include <windows.h>
#include <GL\glut.h>
#include <GL\glui.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
 
// ----- GLOBAL VARIABLES -----
const float SPINNER_SPEED = 0.2;
// ----- USER INTERFACE VARIABLES -----
int Win[2];
int windowID;
GLUI *glui;
// ----- CAMERA VARIABLES -----
GLdouble camXPos = 0.0;
GLdouble camYPos = 0.0;
GLdouble camZPos = -30;
const GLdouble CAMERA_FOVY = 100.0;
const GLdouble NEAR_CLIP = 0.1;
const GLdouble FAR_CLIP = 500.0;
const float CAMERA_MIN = -180.0;
const float CAMERA_MAX = 180.0;
// ----- ANIMATION VARIABLES -----
const float ROOT_TRANSLATE_X_MIN = -10.0; 
const float ROOT_TRANSLATE_X_MAX =  10.0; 
const float ROOT_TRANSLATE_Y_MIN = -10.0; 
const float ROOT_TRANSLATE_Y_MAX =  10.0; 
const float ROOT_TRANSLATE_Z_MIN = -10.0; 
const float ROOT_TRANSLATE_Z_MAX =  10.0; 
const float ROOT_ROTATE_X_MIN    = -180.0; 
const float ROOT_ROTATE_X_MAX    =  180.0; 
const float ROOT_ROTATE_Y_MIN    = -180.0; 
const float ROOT_ROTATE_Y_MAX    =  180.0; 
const float ROOT_ROTATE_Z_MIN    = -180.0; 
const float ROOT_ROTATE_Z_MAX    =  180.0; 
const float HEAD_MIN             = -10.0; 
const float HEAD_MAX             =  10.0; 
const float HEAD_PITCH_MIN       = -45.0; 
const float HEAD_PITCH_MAX       =  45.0; 
const float HAND_X_MIN   = -180.0; 
const float HAND_X_MAX   =  180.0; 
const float HAND_Y_MIN     = -45.0; 
const float HAND_Y_MAX     =  45.0; 
const float HAND_Z_MIN    = -45.0; 
const float HAND_Z_MAX    =  45.0; 
 
int joint_trans_x_root ;
float joint_trans_y_root = 0.0f;
float joint_trans_z_root = 0.0f;
float joint_rot_x_root = 0.0f;
float joint_rot_y_root = 0.0f;
float joint_rot_z_root = 0.0f;
float joint_rot_head = 0.0f;
float joint_pitch_head = 0.0f;
float joint_rot_x_hand_r = 0.0f;
float joint_rot_y_hand_r = 0.0f;
float joint_rot_z_hand_r = 0.0f;
float joint_rot_x_hand_l = 0.0f;
float joint_rot_y_hand_l = 0.0f;
float joint_rot_z_hand_l = 0.0f;
// ----- FUNCTION DECLARATIONS -----
void display(void);
void myReshape(int w, int h);
void drawSquare(int i);
void initGlui();
void initGlut(char *winName);
void animate();
void MyTimerProcessingFunction(void);
// main() function
// Initializes the user interface (and any user variables)
// then hands over control to the event handler, which calls
// display() whenever the GL window needs to be redrawn.
int main(int argc, char** argv) {
// Process program arguments
	
if(argc != 3) {
printf("Usage: demo [width] [height]\n");
printf("Using 400x400 window by default...\n");
Win[0] = 500;
Win[1] = 500;
} else {
Win[0] = atoi(argv[1]);
Win[1] = atoi(argv[2]);
}
glutInit(&argc, argv);
initGlut(argv[0]);
initGlui();
glClearColor(0.7f, 0.7f, 0.9f, 1.0f);
glEnable(GL_DEPTH_TEST);
 
// Standard GLUT main event loop
glutMainLoop();
return 0;
}
void initGlut(char *winName)
{
glutInitDisplayMode(GLUT_RGB | GLUT_DEPTH);
// Create window
glutInitWindowPosition(10, 10);
glutInitWindowSize(Win[0], Win[1]);
windowID = glutCreateWindow(winName);
// Callback functions for event handling
glutReshapeFunc(myReshape);
glutDisplayFunc(display);
//glutTimerFunc(50, MyTimerProcessingFunction,1);
}
// Initialize GLUI and the user interface
void initGlui() {
GLUI_Panel* glui_panel;
GLUI_Spinner* glui_spinner;
GLUI_Master.set_glutIdleFunc(NULL);
// Create GLUI window (joint controls) ***************
//
glui = GLUI_Master.create_glui("Joint Controlhopahopa", 0, Win[0]+12, 0);
// Create controls to specify root position and orientation
glui_panel = glui->add_panel("Root");
glui_spinner = glui->add_spinner_to_panel(glui_panel, "translate x:", GLUI_SPINNER_FLOAT, &joint_trans_x_root);
glui_spinner->set_float_limits(ROOT_TRANSLATE_X_MIN, ROOT_TRANSLATE_X_MAX, GLUI_LIMIT_CLAMP);
glui_spinner->set_speed(SPINNER_SPEED);
glui_spinner = glui->add_spinner_to_panel(glui_panel, "translate y:", GLUI_SPINNER_FLOAT, &joint_trans_y_root);
glui_spinner->set_float_limits(ROOT_TRANSLATE_Y_MIN, ROOT_TRANSLATE_Y_MAX, GLUI_LIMIT_CLAMP);
glui_spinner->set_speed(SPINNER_SPEED);
glui_spinner = glui->add_spinner_to_panel(glui_panel, "translate z:", GLUI_SPINNER_FLOAT, &joint_trans_z_root);
glui_spinner->set_float_limits(ROOT_TRANSLATE_Z_MIN, ROOT_TRANSLATE_Z_MAX, GLUI_LIMIT_CLAMP);
glui_spinner->set_speed(SPINNER_SPEED);
glui_spinner = glui->add_spinner_to_panel(glui_panel, "rotate x:", GLUI_SPINNER_FLOAT, &joint_rot_x_root);
glui_spinner->set_float_limits(ROOT_ROTATE_X_MIN, ROOT_ROTATE_X_MAX, GLUI_LIMIT_WRAP);
glui_spinner->set_speed(SPINNER_SPEED);
glui_spinner = glui->add_spinner_to_panel(glui_panel, "rotate y:", GLUI_SPINNER_FLOAT, &joint_rot_y_root);
glui_spinner->set_float_limits(ROOT_ROTATE_Y_MIN, ROOT_ROTATE_Y_MAX, GLUI_LIMIT_WRAP);
glui_spinner->set_speed(SPINNER_SPEED);
glui_spinner = glui->add_spinner_to_panel(glui_panel, "rotate z:", GLUI_SPINNER_FLOAT, &joint_rot_z_root);
glui_spinner->set_float_limits(ROOT_ROTATE_Z_MIN, ROOT_ROTATE_Z_MAX, GLUI_LIMIT_WRAP);
glui_spinner->set_speed(SPINNER_SPEED);
// Create controls to specify head rotation
glui_panel = glui->add_panel("Head");
glui_spinner = glui->add_spinner_to_panel(glui_panel, "roll:", GLUI_SPINNER_FLOAT, &joint_rot_head);
glui_spinner->set_float_limits(HEAD_MIN, HEAD_MAX, GLUI_LIMIT_CLAMP);
glui_spinner->set_speed(SPINNER_SPEED);
glui_spinner = glui->add_spinner_to_panel(glui_panel, "nod:", GLUI_SPINNER_FLOAT, &joint_pitch_head);
glui_spinner->set_float_limits(HEAD_PITCH_MIN, HEAD_PITCH_MAX, GLUI_LIMIT_CLAMP);
glui_spinner->set_speed(SPINNER_SPEED);
glui->add_column(false);
// Create controls to specify right arm
glui_panel = glui->add_panel("Right arm");
glui_spinner = glui->add_spinner_to_panel(glui_panel, "hand x:", GLUI_SPINNER_FLOAT, &joint_rot_x_hand_r);
glui_spinner->set_float_limits(HAND_X_MIN, HAND_X_MAX, GLUI_LIMIT_CLAMP);
glui_spinner->set_speed(SPINNER_SPEED);
glui_spinner = glui->add_spinner_to_panel(glui_panel, "hand y:", GLUI_SPINNER_FLOAT, &joint_rot_y_hand_r);
glui_spinner->set_float_limits(HAND_Y_MIN, HAND_Y_MAX, GLUI_LIMIT_CLAMP);
glui_spinner->set_speed(SPINNER_SPEED);
glui_spinner = glui->add_spinner_to_panel(glui_panel, "hand z:", GLUI_SPINNER_FLOAT, &joint_rot_z_hand_r);
glui_spinner->set_float_limits(HAND_Z_MIN, HAND_Z_MAX, GLUI_LIMIT_CLAMP);
glui_spinner->set_speed(SPINNER_SPEED);
// Create controls to specify left arm
glui_panel = glui->add_panel("Left arm");
glui_spinner = glui->add_spinner_to_panel(glui_panel, "hand x:", GLUI_SPINNER_FLOAT, &joint_rot_x_hand_l);
glui_spinner->set_float_limits(HAND_X_MIN, HAND_X_MAX, GLUI_LIMIT_CLAMP);
glui_spinner->set_speed(SPINNER_SPEED);
glui_spinner = glui->add_spinner_to_panel(glui_panel, "hand y:", GLUI_SPINNER_FLOAT, &joint_rot_y_hand_l);
glui_spinner->set_float_limits(HAND_Y_MIN, HAND_Y_MAX, GLUI_LIMIT_CLAMP);
glui_spinner->set_speed(SPINNER_SPEED);
glui_spinner = glui->add_spinner_to_panel(glui_panel, "hand z:", GLUI_SPINNER_FLOAT, &joint_rot_z_hand_l);
glui_spinner->set_float_limits(HAND_Z_MIN, HAND_Z_MAX, GLUI_LIMIT_CLAMP);
glui_spinner->set_speed(SPINNER_SPEED);
// Set the main window to be the "active" window
glui->set_main_gfx_window(windowID);
}
void myReshape(int w, int h) {
// Update internal variables and OpenGL viewport
Win[0] = w;
Win[1] = h;
glViewport(0, 0, (GLsizei)Win[0], (GLsizei)Win[1]);
// Setup projection matrix for new window
glMatrixMode(GL_PROJECTION);
glLoadIdentity();
gluPerspective(CAMERA_FOVY, (GLdouble)Win[0]/(GLdouble)Win[1], NEAR_CLIP, FAR_CLIP);
}
// display callback
//
// This gets called by the event handler to draw the scene
void MyTimerProcessingFunction(int value){
	
	glutPostRedisplay();  // перерисовываем экран
    glutTimerFunc(50, MyTimerProcessingFunction,1); 
}
void display(void) {
glClearColor(0.0, 0.0, 0.0, 0.0);
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
glColor3f(1.0,0.0,0.0);
glutSolidSphere(5.0, 20, 20);
glPopMatrix();
glPopMatrix();
glFlush();
}