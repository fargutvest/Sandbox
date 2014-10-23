#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <winsock.h>
#include <locale.h>
#include <stdlib.h>
//#include <string>
#include <iostream>
using namespace std;


/* - ---
- TCP_SEND.C - демонстрационный клиент TCP: -
- отсылает на сервер одно или несколько сообщений, -
- после каждого ждет получения ответа и отображает его -
- Вызов: tcp_send <адрес/имя> <порт> <сообщение1> ... -
- Завершение: исчерпание списка сообщений или <Ctrl-C> -
- В проект должен быть включен файл wsock32.lib -
*/

char DataBuffer[1024];

struct hostent* pHostEnt;
int nPortServer, nMsgLen, i;
WSADATA WSAData;
WORD wWSAVer;


int typeCommunication;
HANDLE hThread;
DWORD dwParam, dwThreadId;
char to_send[1024];


//параметры соединения через Ethernet
SOCKET SockData = INVALID_SOCKET;
struct sockaddr_in SockAddrServer;
char IP[1024];
char port[1024];

//параметры соединеиеия через COM порт
DCB dcb;
HANDLE hCom, hEvent;
DWORD bytesRead, bWritten;
FILE* fout;
BOOL fSuccess;
char PortName[1024];
//char Baudrate[1024];




VOID WINAPI ThreadProc(PVOID* dummy){
	printf("Thread started \n");
	printf("Ждите данные из порта %s...\n", PortName);
	while (true){
		ReadFile(hCom, DataBuffer, 1, &bytesRead, NULL);
		if (bytesRead > 0)
		{
			WriteFile(hCom, DataBuffer, 1, &bWritten, NULL); //отправка ответа
			SetEvent(hEvent);
		}
	}
}


void Configure()
{
	cout << "Select type communication WIZNet(0), COM port (1) \n";
	cin >> typeCommunication;

	switch (typeCommunication)
	{
	case 0: //WIZNet
		cout << "input IP Adress \n";
		cin >> IP;
		cout << "input Port \n";
		cin >> port;
		break;

	case 1://COM port
		cout << "input COM port name \n";
		cin >> PortName;
		//cout << "input COM port baudrate \n";
		//cin >> Baudrate;
		break;
	}

}

int InitializeWIZNet()
{
	//инициализация подсистемы сокетов
	wWSAVer = MAKEWORD(1, 1);
	if (WSAStartup(wWSAVer, &WSAData) != 0) {
		puts("Error initialize WinSocket");
		system("Pause");
		return -1;
	}

	//подготовка адреса сервера
	memset(&SockAddrServer, 0, sizeof (SockAddrServer));
	SockAddrServer.sin_family = AF_INET;
	if (strcmp(IP, "255.255.255.255") == 0) //адрес broadcast
		SockAddrServer.sin_addr.S_un.S_addr = INADDR_BROADCAST;
	else {
		SockAddrServer.sin_addr.S_un.S_addr = inet_addr(IP);
		if (SockAddrServer.sin_addr.S_un.S_addr == INADDR_NONE) {
			if ((pHostEnt = gethostbyname(IP)) == NULL) {
				fprintf(stderr, "Host not detected: %s\n", IP);
				system("Pause");
				return -1;
			}
			SockAddrServer.sin_addr = *(struct in_addr*)(pHostEnt->h_addr_list[0]);
		}
	}
	if (sscanf(port, "%u", &nPortServer) < 1) {
		fprintf(stderr, "Wrong port number: %s\n", port);
		system("Pause");
		return -1;
	}
	SockAddrServer.sin_port = htons((unsigned short)nPortServer);


	//создание локального сокета
	SockData = socket(PF_INET, SOCK_STREAM, 0);
	if (SockData == INVALID_SOCKET) {
		fputs("Error create socket \n", stderr);
		system("Pause");
		return -1;
	}


	//запрос на установление соединения
	if (connect(SockData,
		(const struct sockaddr*)&SockAddrServer, sizeof(SockAddrServer)) != 0)
	{
		fprintf(stderr, "Error connected с %s:%u\n",
			inet_ntoa(SockAddrServer.sin_addr),
			ntohs(SockAddrServer.sin_port)
			);
		closesocket(SockData);
		system("Pause");
		return -1;
	}
	fprintf(stdout, "Connect to server sucessfull: %s:%u\n",
		inet_ntoa(SockAddrServer.sin_addr),
		ntohs(SockAddrServer.sin_port)
		);
	return 0;
}

int InitializeCOMport()
{
	hCom = CreateFile(PortName, GENERIC_READ | GENERIC_WRITE,
		0, NULL, OPEN_EXISTING, 0, NULL);
	if (hCom == INVALID_HANDLE_VALUE){
		printf("Ошибка открытия %s !\n", PortName);
		while (!kbhit());
		return 1;
	}
	GetCommState(hCom, &dcb);
	dcb.BaudRate = 9600;
	dcb.ByteSize = 8;
	dcb.Parity = NOPARITY;
	dcb.StopBits = ONESTOPBIT;
	fSuccess = SetCommState(hCom, &dcb);
	if (!fSuccess){
		printf("Попытка вызвать SetCommState провалилась!\n");
		while (!kbhit());
		return 1;
	}
	printf("COM порт %s успешно сконфигурирован\n", PortName);
	GetCommState(hCom, &dcb);
	printf("Скорость порта %s равна %d\n", PortName, dcb.BaudRate);

	hEvent = CreateEvent(NULL, FALSE, FALSE, NULL);
	hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)ThreadProc,
		&dwParam, 0, &dwThreadId);
	do{
		WaitForSingleObject(hEvent, INFINITE);
		printf("%s\n", DataBuffer);
	} while (!kbhit());

	CloseHandle(hCom);
	return 0;
}

int Initialize()
{
	switch (typeCommunication)
	{
	case 0: //WIZNet
		return InitializeWIZNet();
		break;

	case 1://COM port
		return InitializeCOMport();
		break;
	}
}

int Close()
{
	shutdown(SockData, 2);
	Sleep(100);
	closesocket(SockData); SockData = INVALID_SOCKET;
	WSACleanup();
	system("Pause");
	return 0;
}

int main(void)
{
	setlocale(LC_ALL, "Russian");

	Configure();

	//инициализация подсистемы сокетов
	int i = Initialize();
	if (i != 0) return i;

	//рабочий цикл
	while (true) {
		//отослать сообщение

		cout << "input message \n";
		cin >> to_send;

		fprintf(stdout, "Отсылка: \"%s\" \n", to_send);
		nMsgLen = strlen(to_send) + 1;
		if (send(SockData, to_send, nMsgLen, 0) < nMsgLen) {
			fprintf(stderr, "Ошибка отсылки: \"%s\"\n", to_send);
			system("Pause");
			continue;
		}
		//принять и отобразить ответ
		fprintf(stdout, "Receive...");
		nMsgLen = recv(SockData, DataBuffer, sizeof(DataBuffer)-1, 0);
		if (nMsgLen <= 0) { //ошибка приема ответа
			fputs("Error receive\n", stderr);
			system("Pause");
			continue;
		}
		DataBuffer[nMsgLen] = '\0';
		fprintf(stdout, "\b\b\b: \"%s\" \n", DataBuffer);
	}
	/*hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)ThreadProc,
		&dwParam, 0, &dwThreadId);*/

	//завершение
	//return Close();
}
