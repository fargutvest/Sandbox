#include "stdafx.h"
#include <windows.h>
#include <gl\gl.h>
//#include <gl\glu.h>
#include "resource.h"
#define TCX 64
#define TCY 32

BOOL CALLBACK procIDD_DIALOG1(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);

RECT rc;
BYTE textures [3] [TCX*TCY*3]; // массив с текстурами.
GLfloat ax, ay; // углы поворота.
GLfloat dx, dy; // приращения углов.
GLfloat lightpos[4] = { 9, 4, 15, 1 };
LPARAM mxy;

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	int x, y, i;
	// заполним первую текстуру ...
	for (x = 0; x < TCX; x++)
		for (y = 0; y < TCY; y++)
		{
			i = 3*TCX*y + 3*x;
			textures [0][i+0] = 0;
			textures [0][i+1] = ((x+y)&1)?255:0;
			textures [0][i+2] = 0;
		}
	// заполним вторую текстуру ...
	for (x = 0; x < TCX; x++)
		for (y = 0; y < TCY; y++)
		{
			i = 3*TCX*y + 3*x;
			if ((x/8 + y/8) & 1)
			{
				textures [1][i+0] = 255;
				textures [1][i+1] = 255;
				textures [1][i+2] = 255;
			}
			else
			{
				textures [1][i+0] = 0;
				textures [1][i+1] = 0;
				textures [1][i+2] = 0;
			}
		}
	// заполним третью текстуру ...
	for (x = 0; x < TCX; x++)
		for (y = 0; y < TCY; y++)
		{
			i = 3*TCX*y + 3*x;
			if ((x/4) & 1)
			{
				textures [2][i+0] = 0;
				textures [2][i+1] = 0;
				textures [2][i+2] = 255;
			}
			else
			{
				textures [2][i+0] = 255;
				textures [2][i+1] = 255;
				textures [2][i+2] = 255;
			}
		}
	////////////////////////////////////////////////////////////
    DialogBoxParam(hInstance, (LPCTSTR) IDD_DIALOG1, NULL, procIDD_DIALOG1, 0);
    return 0;
}

// Dialog function for IDD_DIALOG1 [Лабораторная работа по человеко-машинному взаимодействию]
BOOL CALLBACK procIDD_DIALOG1(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg)
    {
    case WM_TIMER:
		if ((ax += dx) >= 360.0f) ax -= 360.0f;
		if ((ay += dy) >= 360.0f) ay -= 360.0f;

		if (ax <= -360.0f) ax += 360.0f;
		if (ay <= -360.0f) ay += 360.0f;

		{
			GLUquadricObj *pobj = gluNewQuadric();

			glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
			glClearDepth(1.0);
			glEnable(GL_DEPTH_TEST);
			gluPerspective(50, (double)rc.right/rc.bottom, 1, 40);
			glMatrixMode(GL_MODELVIEW);
			glLoadIdentity();
			// lighting...
			glLightfv(GL_LIGHT0, GL_POSITION, lightpos);
			glLightf(GL_LIGHT0, GL_SPOT_EXPONENT, 1);
			glLightf(GL_LIGHT0, GL_SPOT_CUTOFF, 180);
			glEnable(GL_LIGHT0);
			glEnable(GL_LIGHTING);
			glEnable(GL_COLOR_MATERIAL);
			glEnable(GL_NORMALIZE);

			BYTE * t = textures [SendDlgItemMessage(hWnd, IDC_COMBO1, CB_GETCURSEL, 0, 0)];
			glTexImage2D(GL_TEXTURE_2D, 0, 3, TCX, TCY, 0, GL_RGB, GL_UNSIGNED_BYTE, t);
			glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

			glEnable(GL_TEXTURE_2D);

			glTranslatef(0, 0, -6);
			glRotatef(ax, 1, 0, 0);
			glRotatef(ay, 0, 1, 0);

			glColor3f(1.0f, 1.0f, 1.0f);
			gluQuadricTexture(pobj, GL_TRUE);
			gluSphere(pobj, 1.7, 32, 32);
			gluQuadricTexture(pobj, GL_FALSE);
			glDisable(GL_TEXTURE_2D);

			gluDeleteQuadric(pobj);
			glFinish();
			SwapBuffers(wglGetCurrentDC());
			RedrawWindow(GetDlgItem(hWnd, IDC_COMBO1), NULL, NULL, RDW_UPDATENOW|RDW_INVALIDATE);
		}
        break;

    case WM_CTLCOLORDLG:
        return (int) GetStockObject(BLACK_BRUSH);

    case WM_SYSCOMMAND:
    case WM_COMMAND:
        switch (LOWORD(wParam))
        {
        case SC_CLOSE:					// Button [x]
			// OpenGL uninit...
			{
				HGLRC hglrc;
				HDC hdc;

				if (hglrc = wglGetCurrentContext())
				{
					hdc = wglGetCurrentDC();
					wglMakeCurrent(NULL, NULL);
					ReleaseDC(hWnd, hdc);
					wglDeleteContext(hglrc);
				}
			}
            EndDialog(hWnd, IDCANCEL);
            return TRUE;

        }
        break;

    case WM_MOUSEMOVE:
		if (wParam)
		{
			dy = (-0.1f) * (GLfloat)(LOWORD(lParam) - LOWORD(mxy));
			dx = 0.1f * (GLfloat)(HIWORD(lParam) - HIWORD(mxy));
		}
        break;

    case WM_LBUTTONDOWN:
		mxy = lParam;
		SetCapture(hWnd);
        break;

    case WM_LBUTTONUP:
		ReleaseCapture();
        break;

    case WM_INITDIALOG:
		// OpenGL init...
		{
			HDC hdc;
			HGLRC hglrc;
			PIXELFORMATDESCRIPTOR pfd;
			int ipf;

			memset(&pfd, 0, sizeof pfd);
			pfd.nSize = sizeof pfd;
			pfd.nVersion = 1;
			pfd.dwFlags = PFD_DOUBLEBUFFER | PFD_DRAW_TO_WINDOW | PFD_SUPPORT_OPENGL;
			pfd.iPixelType = PFD_TYPE_RGBA;
			pfd.cColorBits = 24;
			pfd.cDepthBits = 32;
			pfd.iLayerType = PFD_MAIN_PLANE;
			hdc = GetDC(hWnd);
			ipf = ChoosePixelFormat(hdc, &pfd);
			SetPixelFormat(hdc, ipf, &pfd);
			hglrc = wglCreateContext(hdc);
			if (hglrc) wglMakeCurrent(hdc, hglrc);

			GetClientRect(hWnd, &rc);
			glViewport(0, 0, rc.right, rc.bottom);
			glMatrixMode(GL_PROJECTION);
			glLoadIdentity();
		}
		dx = 0.5f; dy = 0.5f;
		ax = 0.0f; ay = 0.0f;
		// добавляем пункты в IDC_COMBO1 ...
		SendDlgItemMessage(hWnd, IDC_COMBO1, CB_ADDSTRING, 0, (LPARAM) "В точку");
		SendDlgItemMessage(hWnd, IDC_COMBO1, CB_ADDSTRING, 0, (LPARAM) "В клеточку");
		SendDlgItemMessage(hWnd, IDC_COMBO1, CB_ADDSTRING, 0, (LPARAM) "В полоску");
		////////////////////////////////////////////
		SendDlgItemMessage(hWnd, IDC_COMBO1, CB_SETCURSEL, 0, 0);
        SetTimer(hWnd, 0, 1000/30, NULL);
        return TRUE;
    }
    return FALSE;
}

