#include "stdafx.h"
#include <windows.h>
#include <conio.h>
#include <stdio.h>
#include <locale.h>


char buf[128];
char* pbuf = buf;
int counter = 0;
char* pcComPort = "COM1";

HANDLE hCom, hEvent;
DWORD bytesRead, bWritten;

BOOL fSuccess;
HANDLE hThread;
DWORD dwParam, dwThreadId;


VOID WINAPI ThreadProc(PVOID* dummy){
	printf("Ready data from Com port %s...\n",pcComPort);
	while (true){
		ReadFile(hCom, pbuf, 1, &bytesRead, NULL);
		if (bytesRead > 0)
		{
			WriteFile(hCom, pbuf++, 1, &bWritten, NULL);
			counter++;
			if (counter == 10){
				SetEvent(hEvent);
				buf[counter] = '\0';
				counter = 0;
				pbuf = buf;
			}
		}
	}
}

int main(void)
{
	setlocale(LC_ALL, "Russian");
	
	DCB dcb;
	FILE* fout;
	hCom = CreateFile(pcComPort, GENERIC_READ | GENERIC_WRITE,
		0, NULL, OPEN_EXISTING, 0, NULL);
	if (hCom == INVALID_HANDLE_VALUE)
	{
		printf("Error open %s!\n",pcComPort);
		while (!_kbhit());
		return 1;
	}
	GetCommState(hCom, &dcb);
	dcb.BaudRate = CBR_9600;
	dcb.ByteSize = 8;
	dcb.Parity = NOPARITY;
	dcb.StopBits = ONESTOPBIT;
	fSuccess = SetCommState(hCom, &dcb);
	if (!fSuccess){
		printf("Try call SetCommState failed !\n");
		while (!_kbhit());
		return 1;
	}
	printf("COM port %s configure siccesfull \n", pcComPort);
	GetCommState(hCom, &dcb);
	printf("baudrate port %s equal %d\n", pcComPort, dcb.BaudRate);
	hEvent = CreateEvent(NULL, FALSE, FALSE, NULL);
	hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)ThreadProc,
		&dwParam, 0, &dwThreadId);

	if ((fout = fopen("C:\\test", "a+")) == NULL)
	{
		printf("Error create file C:\\test");
		while (!_kbhit());
		return 1;
	}
	printf("Press any key for exit...\n");
	do{
		WaitForSingleObject(hEvent, INFINITE);
		printf("%s\n", buf);
		fwrite(buf, sizeof(char), 10, fout);
	} while (!_kbhit());
	fclose(fout);
	CloseHandle(hCom);

	return 0;
}

