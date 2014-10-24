#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <winsock.h>
#include <locale.h>
#include <stdlib.h>
//#include <string>
#include <iostream>
#include <sys/types.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>

using namespace std;


/* ----
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




int senderBroadcast()
{

#define PORT 34001
	// #define SRC_ADDR "172.16.1.120"
#define DEST_ADDR "255.255.255.255"

	
		int sockfd;
		int broadcast = 1;
		struct sockaddr_in sendaddr;
		struct sockaddr_in recvaddr;
		int numbytes;

		if ((sockfd = socket(PF_INET, SOCK_DGRAM, IPPROTO_IP)) == -1)
		{
			perror("sockfd");
			exit(1);
		}

		if ((setsockopt(sockfd, SOL_SOCKET, SO_BROADCAST, (char*)broadcast, sizeof broadcast)) == -1)
		{
			perror("setsockopt - SO_SOCKET ");
			exit(1);
		}

		sendaddr.sin_family = AF_INET;
		sendaddr.sin_port = htons(PORT);
		sendaddr.sin_addr.s_addr = INADDR_ANY;
		memset(sendaddr.sin_zero, '\0', sizeof sendaddr.sin_zero);

		if (bind(sockfd, (struct sockaddr*) &sendaddr, sizeof sendaddr) == -1)
		{
			perror("bind");
			exit(1);
		}

		recvaddr.sin_family = AF_INET;
		recvaddr.sin_port = htons(PORT);
		recvaddr.sin_addr.s_addr = inet_addr(DEST_ADDR);
		memset(recvaddr.sin_zero, '\0', sizeof recvaddr.sin_zero);

		if ((numbytes = sendto(sockfd, "abcd", 4, 0,
			(struct sockaddr *)&recvaddr, sizeof recvaddr)) == -1)
		{
			perror("sendto");
			exit(1);
		}
		
		closesocket(sockfd);

		return 0;

}

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

int a()
{
	char SERVERADDR[4];
	char buff[10 * 1014];

	// Инициализация WinSock
	WORD wVersionRequested = MAKEWORD(2, 2);
	WSADATA wsaData;
	int err = WSAStartup(wVersionRequested, &wsaData);
	if (err != 0) printf("WSAStartup error: %d\n", WSAGetLastError());

	// Открытие и закрытие сокета
	SOCKET my_sock = socket(AF_INET, SOCK_DGRAM, 0);
	if (my_sock == INVALID_SOCKET)
	{
		printf("socket() error: %d\n", WSAGetLastError());
		WSACleanup();
	}
	printf("Enter Server IP: ");
	gets(SERVERADDR);

	// Шаг 3 - обмен сообщений с сервером
	HOSTENT *hostent;
	sockaddr_in dest_addr;

	dest_addr.sin_family = AF_INET;
	dest_addr.sin_port = htons(50002);

	// определение IP-адреса узла
	if (inet_addr(SERVERADDR))
		dest_addr.sin_addr.s_addr = inet_addr(SERVERADDR);
	else
	if (hostent = gethostbyname(SERVERADDR))
		dest_addr.sin_addr.s_addr = ((unsigned long **)
		hostent->h_addr_list)[0][0];
	else
	{
		printf("Unknown host: %d\n", WSAGetLastError());
		closesocket(my_sock);
		WSACleanup();
		return -1;
	}


}

int InitializeEthernetPort()
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
	if (strcmp("192.168.1.190", "255.255.255.255") == 0) //адрес broadcast
		SockAddrServer.sin_addr.S_un.S_addr = INADDR_BROADCAST;
	else {
		SockAddrServer.sin_addr.S_un.S_addr = inet_addr("192.168.1.190");
		if (SockAddrServer.sin_addr.S_un.S_addr == INADDR_NONE) {
			if ((pHostEnt = gethostbyname("192.168.1.190")) == NULL) {
				fprintf(stderr, "Host not detected: %s\n", "192.168.1.190");
				system("Pause");
				return -1;
			}
			SockAddrServer.sin_addr = *(struct in_addr*)(pHostEnt->h_addr_list[0]);
		}
	}
	char pport[6] = "34001";
	if (sscanf(pport, "%u", &nPortServer) < 1) {
		fprintf(stderr, "Wrong port number: %s\n", pport);
		system("Pause");
		return -1;
	}
	SockAddrServer.sin_port = htons((unsigned short)nPortServer);


	//создание локального сокета
	SockData = socket(PF_INET, SOCK_STREAM, IPPROTO_IP);
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

	typedef unsigned char byte;
	byte bytes[402];
	bytes[0] = 0x1;
	bytes[1] = 0xef;
	for (int i = 2; i < sizeof(bytes); i++)
	{
		bytes[i] = 0;
	}
	int j = send(SockData, (const char*)bytes, sizeof(bytes), 0);
	closesocket(SockData);


	//------------------------------------------------------------------------------

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
	if (strcmp("192.168.1.190", "255.255.255.255") == 0) //адрес broadcast
		SockAddrServer.sin_addr.S_un.S_addr = INADDR_BROADCAST;
	else {
		SockAddrServer.sin_addr.S_un.S_addr = inet_addr("192.168.1.190");
		if (SockAddrServer.sin_addr.S_un.S_addr == INADDR_NONE) {
			if ((pHostEnt = gethostbyname("192.168.1.190")) == NULL) {
				fprintf(stderr, "Host not detected: %s\n", "192.168.1.190");
				system("Pause");
				return -1;
			}
			SockAddrServer.sin_addr = *(struct in_addr*)(pHostEnt->h_addr_list[0]);
		}
	}
	char ppport[6] = "50002";
	if (sscanf(ppport, "%u", &nPortServer) < 1) {
		fprintf(stderr, "Wrong port number: %s\n", ppport);
		system("Pause");
		return -1;
	}
	SockAddrServer.sin_port = htons((unsigned short)nPortServer);


	//создание локального сокета
	SockData = socket(PF_INET, SOCK_STREAM, IPPROTO_IP);
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

	typedef unsigned char byte;
	byte bytes2[5] = "FIND";
	
	
	for (int i = 2; i < sizeof(bytes2); i++)
	{
		bytes2[i] = 0;
	}
	j = send(SockData, (const char*)bytes2, sizeof(bytes2), 0);
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
		return InitializeEthernetPort();
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

	puts("Press any key to start");
	getch();
	
	senderBroadcast();

	//Configure();

	//инициализация подсистемы сокетов
	/*int i = Initialize();
	if (i != 0) return i;*/

	//рабочий цикл
	while (true) {
		//отослать сообщение

	/*	cout << "input message \n";
		cin >> to_send;

		fprintf(stdout, "Отсылка: \"%s\" \n", to_send);
		nMsgLen = strlen(to_send) + 1;
		if (send(SockData, to_send, nMsgLen, 0) < nMsgLen) {
			fprintf(stderr, "Ошибка отсылки: \"%s\"\n", to_send);
			system("Pause");
			continue;
		}*/
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
