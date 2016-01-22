#include "header.h"
node* koord; // координаты вершин
Graph g, g1;// граф
int radm, radb; //  радиус большого кружочка и маленького
const double pi=3.1415927;
 int stoimost, *a, kol, kolv; // a- ходы
 int st, en; // старт и финиш

LRESULT CALLBACK WndProc (HWND, UINT, WPARAM, LPARAM) ;

int WINAPI WinMain (HINSTANCE hInstance, HINSTANCE hPrevInstance,
                    PSTR szCmdLine, int iCmdShow)
     {
	ifstream fin("in.txt");
	fin >> radm >> radb;
	 g.input(fin);
	 fin >> st >> en;
	 fin.close();
	 kol=g.kol_reb();
	 kolv=g.kol_ver();
	 koord=new node[kolv];
	 for(int ugol=0; ugol<kolv; ugol++)
			{
				koord[ugol].x=cos(ugol*2*pi/kolv)*radb;
				koord[ugol].y=sin(ugol*2*pi/kolv)*radb;
			}
	 for (int i=0; i<kolv; i++)
		 g.cost(koord);

     static char szAppName[] = "Round" ;
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
		            "Round",     // window caption
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
          }
	 delete[] a;
     return msg.wParam ;
     }

LRESULT CALLBACK WndProc (HWND hwnd, UINT iMsg, WPARAM wParam, LPARAM lParam)
     {
     HDC         hdc ;
     PAINTSTRUCT ps ;
	 static int cxClient, cyClient;
	 HBRUSH hBrush1=CreateSolidBrush(RGB(100,220,100)), hBrush2=CreateSolidBrush(RGB(190,70,70));
	 HPEN hPen2=CreatePen(PS_SOLID, 3, RGB(70, 170, 120)), hPen1=CreatePen(PS_SOLID, 1, RGB(0, 0, 0));
	 static int change; // номер вершины, коодинаты которой надо изменить
	 switch(iMsg)
	 {
		 case WM_SIZE:
			 change=-1;
			 cxClient=LOWORD(lParam);
			 cyClient=HIWORD(lParam);
			 return 0;
		 case WM_LBUTTONDOWN:
			 SetCapture(hwnd);
			for (int i=0; i< kolv; i++)
			{
				int x1=LOWORD(lParam), y1=HIWORD(lParam);
				 if ((LOWORD(lParam)<=cxClient/2+koord[i].x+radm) && (LOWORD(lParam)>=cxClient/2+koord[i].x-radm) && (HIWORD(lParam)<=cyClient/2+koord[i].y+radm) && (HIWORD(lParam)>=cyClient/2+koord[i].y-radm))
				 {
					 change=i;
					 return 0;
				 }
			}
			change=-1;
			return 0;
		 /*case WM_LBUTTONDBLCLK:
			 for (int i=0; i< kolv; i++)
			{
				int x1=LOWORD(lParam), y1=HIWORD(lParam);
				 if ((LOWORD(lParam)<=cxClient/2+koord[i].x+radm) && (LOWORD(lParam)>=cxClient/2+koord[i].x-radm) && (HIWORD(lParam)<=cyClient/2+koord[i].y+radm) && (HIWORD(lParam)>=cyClient/2+koord[i].y-radm))
				 {
					 st=i;
					 InvalidateRect(hwnd, NULL, TRUE);
					 return 0;
				 }
			}*/
		 case WM_RBUTTONDOWN:
			 for (int i=0; i< kolv; i++)
			{
				int x1=LOWORD(lParam), y1=HIWORD(lParam);
				 if ((LOWORD(lParam)<=cxClient/2+koord[i].x+radm) && (LOWORD(lParam)>=cxClient/2+koord[i].x-radm) && (HIWORD(lParam)<=cyClient/2+koord[i].y+radm) && (HIWORD(lParam)>=cyClient/2+koord[i].y-radm))
				 {
					 if (wParam&MK_SHIFT)
						 st=i;
					 else
						 en=i;
					 InvalidateRect(hwnd, NULL, TRUE);
					 return 0;
				 }
			}
		 case WM_LBUTTONUP:
			 ReleaseCapture();
			 if (change!=-1)
			 {
				 koord[change].x=LOWORD(lParam)-cxClient/2;
				 koord[change].y=HIWORD(lParam)-cyClient/2;
				 g.cost(koord);
			 }
			 change=-1;
			 InvalidateRect(hwnd, NULL, TRUE);
			 return 0;
		case WM_PAINT:
			hdc=BeginPaint(hwnd, &ps);
			a=new int[kol];
			g1=g;
			stoimost=g1.algoritm_Deijkstra(a, &kol, st, en);
			// рисуем вершины
			for (int ugol=0; ugol<kolv; ugol++)
			{
				Ellipse (hdc, cxClient/2+koord[ugol].x-radm, cyClient/2+koord[ugol].y+radm, cxClient/2+koord[ugol].x+radm, cyClient/2+koord[ugol].y-radm);
				char *s=new char[2];
				itoa(ugol, s, 10); 
				TextOut(hdc, cxClient/2+koord[ugol].x, cyClient/2+koord[ugol].y-radm*2, s, strlen(s));
				delete[] s;
			}
			// помечаем начальные и конечные вершины
			SelectObject(hdc, hBrush1);
			Ellipse (hdc, cxClient/2+koord[st].x-radm, cyClient/2+koord[st].y+radm, cxClient/2+koord[st].x+radm, cyClient/2+koord[st].y-radm);
			SelectObject(hdc, hBrush2);
			Ellipse (hdc, cxClient/2+koord[en].x-radm, cyClient/2+koord[en].y+radm, cxClient/2+koord[en].x+radm, cyClient/2+koord[en].y-radm);
			// рисуем ребра
			for (int i=0; i<kolv; i++)
			{
				SelectObject(hdc, hPen1);
				g1=g;
				while (!g1.get_queue(i).isEmpty())
				{

					MoveToEx(hdc, cxClient/2+koord[i].x, cyClient/2+koord[i].y, NULL);
					node xy=koord[g1.get_queue(i).PopFront().x];
					LineTo(hdc, cxClient/2+xy.x, cyClient/2+xy.y);
				}
				// рисуем путь
				for (int i=0; i<kol-1; i++)
				{
					SelectObject(hdc, hPen2);
					MoveToEx(hdc, cxClient/2+koord[a[i]].x, cyClient/2+koord[a[i]].y, NULL);
					LineTo(hdc, cxClient/2+koord[a[i+1]].x, cyClient/2+koord[a[i+1]].y);
				}
			}
			EndPaint (hwnd, &ps);
			return 0;
		 case WM_DESTROY:
			 PostQuitMessage(0);
			 return 0;
	 }
	  return DefWindowProc (hwnd, iMsg, wParam, lParam) ;
  }