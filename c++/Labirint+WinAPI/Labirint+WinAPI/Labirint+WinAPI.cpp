#include <windows.h>
#include "header.h"

int m1, n1; //количество столбцов и строчек
node* vihod;
int j;
char** matr;

LRESULT CALLBACK WndProc (HWND, UINT, WPARAM, LPARAM) ;

int WINAPI WinMain (HINSTANCE hInstance, HINSTANCE hPrevInstance,
                    PSTR szCmdLine, int iCmdShow)
{
	
	int xstart, ystart; //координаты
	ifstream fin("in.txt");
	fin >> n1 >> m1;
	fin.get();
	vihod = new node[m1*n1]; //для обратного хода
	matr = new char*[n1];
	for(int i = 0; i < n1; i++)
		matr[i] = new char[m1];
	for (int i=0; i<n1; i++)
	{
		for (int j=0; j<m1; j++)
			fin.get(matr[i][j]);
		fin.get();
	}
	fin >> xstart >> ystart;
	j=labirint(vihod, matr, xstart, ystart, m1, n1);

     static char szAppName[] = "Labirint" ;
     HWND        hwnd ;
     MSG         msg ;
     WNDCLASSEX  wndclass ;

     wndclass.cbSize        = sizeof (wndclass) ;
     wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
     wndclass.lpfnWndProc   = WndProc ;
     wndclass.cbClsExtra    = 0 ;
     wndclass.cbWndExtra    = 0 ;
     wndclass.hInstance     = hInstance ;
     wndclass.hIcon         = LoadIcon (NULL, IDI_APPLICATION) ;
     wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
     wndclass.hbrBackground = (HBRUSH) GetStockObject (WHITE_BRUSH) ;
     wndclass.lpszMenuName  = NULL ;
     wndclass.lpszClassName = szAppName ;
     wndclass.hIconSm       = LoadIcon (NULL, IDI_APPLICATION) ;

     RegisterClassEx (&wndclass) ;

     hwnd = CreateWindow (szAppName,         // window class name
		            "The Labirint",     // window caption
                    WS_OVERLAPPEDWINDOW,     // window style
                    CW_USEDEFAULT,           // initial x position
                    CW_USEDEFAULT,           // initial y position
                    CW_USEDEFAULT,           // initial x size
                    CW_USEDEFAULT,           // initial y size
                    NULL,                    // parent window handle
                    NULL,                    // window menu handle
                    hInstance,               // program instance handle
		            NULL) ;		             // creation parameters

     ShowWindow (hwnd, iCmdShow) ;
     UpdateWindow (hwnd) ;

     while (GetMessage (&msg, NULL, 0, 0))
          {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
		  Sleep(10);
          }

	delete[] vihod;
	fin.close();

     return msg.wParam ;
     }

LRESULT CALLBACK WndProc (HWND hwnd, UINT iMsg, WPARAM wParam, LPARAM lParam)
     {
     HDC         hdc ;
     PAINTSTRUCT ps ;
	 static int cxClient, cyClient;
	 HBRUSH hBrush1=CreateSolidBrush(RGB(30,30,30)), hBrush2=CreateSolidBrush(RGB(0,180,180)), hBrush3=CreateSolidBrush(RGB(250,20,20)), hBrush4=CreateSolidBrush(RGB(20,120,140));
	 switch(iMsg)
	 {
		 case WM_SIZE:
			 cxClient=LOWORD(lParam);
			 cyClient=HIWORD(lParam);
			 return 0;
		 case WM_PAINT:
			 hdc=BeginPaint(hwnd, &ps);
			 for (int i=0; i<m1; i++)
				for (int j=0; j<n1; j++)
				 {
					 int m=cxClient/m1, n=cyClient/n1;
					 switch(matr[i][j])
					 {
					 case '*':
						 SelectObject(hdc, hBrush1);
						 break;
					 default:
						 SelectObject(hdc, hBrush2);
						 break;
					 }
					Rectangle(hdc, j*m, i*n, (j+1)*m, (i+1)*n);
				 }
				/* Нарисовали исходный лабиринт */
			for(int i=0; i<j+1; i++)
			{
				int m=cxClient/m1, n=cyClient/n1;
				int x=matr[vihod[i].y][vihod[i].x];
				switch (x)
				{
				case '4':
					SelectObject(hdc, hBrush3);
					break;
				default:
					SelectObject(hdc, hBrush4);
					break;
				}
				Rectangle(hdc, vihod[i].x*m, vihod[i].y*n, (vihod[i].x+1)*m, (vihod[i].y+1)*n);
			}

			EndPaint (hwnd, &ps);
			return 0;
		 case WM_DESTROY:
			 PostQuitMessage(0);
			 return 0;
	 }
	  return DefWindowProc (hwnd, iMsg, wParam, lParam) ;
  }