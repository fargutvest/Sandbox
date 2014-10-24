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
- TCP_SEND.C - ���������������� ������ TCP: -
- �������� �� ������ ���� ��� ��������� ���������, -
- ����� ������� ���� ��������� ������ � ���������� ��� -
- �����: tcp_send <�����/���> <����> <���������1> ... -
- ����������: ���������� ������ ��������� ��� <Ctrl-C> -
- � ������ ������ ���� ������� ���� wsock32.lib -
*/

char DataBuffer[1024];

struct hostent* pHostEnt;
int nPortServer, nMsgLen, i;
WSADATA WSAData; //���������, � ������� ����� ���������� ������� ����� ���������� ���������� � ����������. 
WORD wWSAVer;


int typeCommunication;
HANDLE hThread;
DWORD dwParam, dwThreadId;
char to_send[1024];


//��������� ���������� ����� Ethernet
SOCKET SocketExchange = INVALID_SOCKET;
SOCKET SocketSendUDP = INVALID_SOCKET;
SOCKET SocketConfigureUDP = INVALID_SOCKET;

struct sockaddr_in SockAddrServer;
char IP[1024];
char port[1024];

//��������� ����������� ����� COM ����
DCB dcb;
HANDLE hCom, hEvent;
DWORD bytesRead, bWritten;
FILE* fout;
BOOL fSuccess;
char PortName[1024];
//char Baudrate[1024];




VOID WINAPI ThreadProc(PVOID* dummy){
	printf("Thread started \n");
	printf("����� ������ �� ����� %s...\n", PortName);
	while (true){
		ReadFile(hCom, DataBuffer, 1, &bytesRead, NULL);
		if (bytesRead > 0)
		{
			WriteFile(hCom, DataBuffer, 1, &bWritten, NULL); //�������� ������
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

int Udp()
{

}

int a()
{
	char SERVERADDR[4];
	char buff[10 * 1014];

	// ������������� WinSock
	WORD wVersionRequested = MAKEWORD(2, 2);
	WSADATA wsaData;
	int err = WSAStartup(wVersionRequested, &wsaData);
	if (err != 0) printf("WSAStartup error: %d\n", WSAGetLastError());

	// �������� � �������� ������
	SOCKET my_sock = socket(AF_INET, SOCK_STREAM, 0);
	if (my_sock == INVALID_SOCKET)
	{
		printf("socket() error: %d\n", WSAGetLastError());
		WSACleanup();
	}
	printf("Enter Server IP: ");
	gets(SERVERADDR);

	// ��� 3 - ����� ��������� � ��������
	HOSTENT *hostent;
	sockaddr_in dest_addr;

	dest_addr.sin_family = AF_INET;
	dest_addr.sin_port = htons(50002);
		
	// ����������� IP-������ ����
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
	
	//������������� ����������� �������

	wWSAVer = MAKEWORD(1, 1); //������������� ������ ���������� (WinSock)

	if (WSAStartup(wWSAVer, &WSAData) != 0) // �������� ����������
	{
		puts("Error initialize WinSocket");
		system("Pause");
		return -1;
	}

	//���������� ������ �������
	memset(&SockAddrServer, 0, sizeof (SockAddrServer));
	SockAddrServer.sin_family = AF_INET;
	if (strcmp(IP, "255.255.255.255") == 0) //����� broadcast
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


	//������ �� ������������ ����������
	if (connect(SocketExchange,
		(const struct sockaddr*)&SockAddrServer, sizeof(SockAddrServer)) != 0)
	{
		fprintf(stderr, "Error connected � %s:%u\n",
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
	return 0;
}

int InitializeCOMport()
{
	hCom = CreateFile(PortName, GENERIC_READ | GENERIC_WRITE,
		0, NULL, OPEN_EXISTING, 0, NULL);
	if (hCom == INVALID_HANDLE_VALUE){
		printf("������ �������� %s !\n", PortName);
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
		printf("������� ������� SetCommState �����������!\n");
		while (!kbhit());
		return 1;
	}
	printf("COM ���� %s ������� ���������������\n", PortName);
	GetCommState(hCom, &dcb);
	printf("�������� ����� %s ����� %d\n", PortName, dcb.BaudRate);

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

	Configure();

	//������������� ���������� �������
	int i = Initialize();
	if (i != 0) return i;

	//������� ����
	while (true) {
		//�������� ���������

		cout << "input message \n";
		cin >> to_send;

		fprintf(stdout, "�������: \"%s\" \n", to_send);
		nMsgLen = strlen(to_send) + 1;
		if (send(SocketExchange, to_send, nMsgLen, 0) < nMsgLen) {
			fprintf(stderr, "������ �������: \"%s\"\n", to_send);
			system("Pause");
			continue;
		}
		//������� � ���������� �����
		fprintf(stdout, "Receive...");
		nMsgLen = recv(SocketExchange, DataBuffer, sizeof(DataBuffer)-1, 0);
		if (nMsgLen <= 0) { //������ ������ ������
			fputs("Error receive\n", stderr);
			system("Pause");
			continue;
		}
		DataBuffer[nMsgLen] = '\0';
		fprintf(stdout, "\b\b\b: \"%s\" \n", DataBuffer);
	}
	/*hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)ThreadProc,
		&dwParam, 0, &dwThreadId);*/

	//����������
	//return Close();
}
