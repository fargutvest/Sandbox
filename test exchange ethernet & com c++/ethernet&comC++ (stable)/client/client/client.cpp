#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <winsock.h>
#include <locale.h>
#include <stdlib.h>
//#include <&lt;netinet/in.h&gt>
//#include <sys/socket.h>

#include <string.h>

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




struct hostent* pHostEnt;
int nPortServer, nMsgLen, i;
WSADATA WSAData; //структура, в которой после выполнения функции будет находиться информация о библиотеке. 
WORD wWSAVer;


int typeCommunication;



//сокет конфигурирования
SOCKET SocketConfigureUDP = INVALID_SOCKET;
struct sockaddr_in SocAddrConfigure;
char ConfigureBuffer[402];


//сокет команд UDP
SOCKET SocketCommandUDP = INVALID_SOCKET;
struct sockaddr_in SocAddrCommandUDP;


//сокет обмена данными tcp
SOCKET SocketExchange = INVALID_SOCKET;
struct sockaddr_in SockAddrServer;
char IP[1024];
char port[1024];
char DataBuffer[1024];


//параметры соединеиеия через COM порт
DCB dcb;
HANDLE hCom, hEvent;
DWORD bytesRead, bWritten;
FILE* fout;
BOOL fSuccess;
char PortName[1024];
//char Baudrate[1024];
HANDLE hThread;
DWORD dwParam, dwThreadId;
char to_send[1024];



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

int SearchUDP()
{
	//открытие сокета 1
	SocketConfigureUDP = socket(AF_INET, SOCK_DGRAM, 0);
	if (SocketConfigureUDP == INVALID_SOCKET) {
		fputs("Error create socket \n", stderr);
		system("Pause");
		return -1;
	}


	//обмен сообщений с сервером
	// установка возможности широковещательного адреса
	int optval = 1;
	if (setsockopt(SocketConfigureUDP, SOL_SOCKET, SO_BROADCAST, (char *)&optval, sizeof(optval)) == -1)
	{
		printf("Error setting broadcast socket\n");
		WSACleanup();
		return -1;
	}

	// задание адреса удаленного сокета broadcast:34001
	SocAddrConfigure.sin_family = AF_INET;
	SocAddrConfigure.sin_port = htons(34001);
	SocAddrConfigure.sin_addr.S_un.S_addr = INADDR_BROADCAST;


	//подготовка буффера 
	ConfigureBuffer[0] = 0x01;
	ConfigureBuffer[1] = 0xEF;


	// Передача буффера в сеть на порт 34001
	int length2 = sendto(
		SocketConfigureUDP,
		ConfigureBuffer,
		sizeof(ConfigureBuffer),
		0,
		(SOCKADDR*)&SocAddrConfigure,
		sizeof(sockaddr_in)
		);






	//открытие сокета 2
	SocketCommandUDP = socket(AF_INET, SOCK_DGRAM, 0);
	if (SocketCommandUDP == INVALID_SOCKET) {
		fputs("Error create socket \n", stderr);
		system("Pause");
		return -1;
	}

	//обмен сообщений с сервером
	// установка возможности широковещательного адреса
	int optval2 = 1;
	if (setsockopt(SocketCommandUDP, SOL_SOCKET, SO_BROADCAST, (char *)&optval, sizeof(optval)) == -1)
	{
		printf("Error setting broadcast socket\n");
		WSACleanup();
		return -1;
	}

	// задание адреса удаленного сокета broadcast:50002
	SocAddrCommandUDP.sin_family = AF_INET;
	SocAddrCommandUDP.sin_port = htons(50002);
	SocAddrCommandUDP.sin_addr.S_un.S_addr = INADDR_BROADCAST;

	//подготовка буффера 
	char CommandBuffer[5] = "FIND";

	// Передача буфера в сеть на порт 50002
	int length = sendto(
		SocketCommandUDP,
		CommandBuffer,
		sizeof(CommandBuffer)-1,
		0,
		(SOCKADDR*)&SocAddrCommandUDP,
		sizeof(sockaddr_in)
		);



////Прием буффера из сети 
//int length3 = recvfrom(
//	SocketConfigureUDP,
//	ConfigureBuffer,
//	sizeof(ConfigureBuffer),
//	0,
//struct sockaddr FAR* from,
//	int FAR* fromlen
//	);
//

	//serv_addr.sin_port = htons(0);      //просим систему выделить порт по ее усмотрению  
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
	SOCKET my_sock = socket(AF_INET, SOCK_STREAM, 0);
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

int LoadLib()
{
	wWSAVer = MAKEWORD(1, 1); //запрашиваемая версия библиотеки (WinSock)

	if (WSAStartup(wWSAVer, &WSAData) != 0) // загрузка библиотеки
	{
		puts("Error initialize WinSocket");
		system("Pause");
		return -1;
	}
}

int ExchangeEthernet()
{

	//ИНИЦИАЛИЗАЦИЯ ПОДСИСТЕММЫ СОКЕТОВ

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



	SocketExchange = socket(PF_INET, SOCK_STREAM, 0);
	if (SocketExchange == INVALID_SOCKET) {
		fputs("Error create socket \n", stderr);
		system("Pause");
		return -1;
	}


	//запрос на установление соединения
	if (connect(SocketExchange,
		(const struct sockaddr*)&SockAddrServer, sizeof(SockAddrServer)) != 0)
	{
		fprintf(stderr, "Error connected с %s:%u\n",
			inet_ntoa(SockAddrServer.sin_addr),
			ntohs(SockAddrServer.sin_port)
			);
		closesocket(SocketExchange);
		system("Pause");
		return -1;
	}
	fprintf(stdout, "Connect to server sucessfull: %s:%u\n",
		inet_ntoa(SockAddrServer.sin_addr),
		ntohs(SockAddrServer.sin_port)
		);


	//рабочий цикл
	while (true) {
		//отослать сообщение

		cout << "input message \n";
		cin >> to_send;

		fprintf(stdout, "Отсылка: \"%s\" \n", to_send);
		nMsgLen = strlen(to_send) + 1;
		if (send(SocketExchange, to_send, nMsgLen, 0) < nMsgLen) {
			fprintf(stderr, "Ошибка отсылки: \"%s\"\n", to_send);
			system("Pause");
			continue;
		}
		//принять и отобразить ответ
		fprintf(stdout, "Receive...");
		nMsgLen = recv(SocketExchange, DataBuffer, sizeof(DataBuffer)-1, 0);
		if (nMsgLen <= 0) { //ошибка приема ответа
			fputs("Error receive\n", stderr);
			system("Pause");
			continue;
		}
		DataBuffer[nMsgLen] = '\0';
		fprintf(stdout, "\b\b\b: \"%s\" \n", DataBuffer);
	}


	return 0;
}

int ExchangeCOMport()
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

int Close()
{
	shutdown(SocketExchange, 2);
	Sleep(100);
	closesocket(SocketExchange);
	SocketExchange = INVALID_SOCKET;
	WSACleanup();
	system("Pause");
	return 0;
}

int Menu()
{
	cout << "Select type communication: \n0 - exchange WIZNet \n1 - exchange COM port \n2 - configure WIZNet to UDP \n";
	cin >> typeCommunication;

	switch (typeCommunication)
	{
	case 0: //WIZNet
		cout << "input IP Adress \n";
		cin >> IP;
		cout << "input Port \n";
		cin >> port;
		LoadLib();
		return ExchangeEthernet();
		break;

	case 1://COM port
		cout << "input COM port name \n";
		cin >> PortName;
		//cout << "input COM port baudrate \n";
		//cin >> Baudrate;
		return ExchangeCOMport();
		break;

	case 2:
		cout << "Configure to UDP mode \n";
		LoadLib();
		return SearchUDP();
		break;
	}

}

int main(void)
{
	setlocale(LC_ALL, "Russian");

	if (Menu() != 0)
	{
		return -1;
	}
}
