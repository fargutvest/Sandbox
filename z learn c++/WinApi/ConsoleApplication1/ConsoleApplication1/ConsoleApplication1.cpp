// ConsoleApplication1.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <winsock.h>
#include <locale.h>
#include <stdlib.h>
#include <iostream>
#include <vector>
#include "process.h"
using std::vector;

bool bThreadStop;
HANDLE hThread;
DWORD dwParam, dwThreadId;
vector<DWORD> errHistoty;

int i = 0;
VOID WINAPI ThreadProc(PVOID* dummy)
{
	while (!bThreadStop)
	{
		Sleep(1000);
		i++;
		system("cls");
		std::cout << i;




		mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -120, 0); //работает везде
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	bThreadStop = false;
	hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)ThreadProc,
		&dwParam, 0, &dwThreadId);

	_getch();

	return 0;
}

//SendMessageW(GetForegroundWindow(), WM_VSCROLL, SB_LINEDOWN, 0); работает на браузере Mozilla

