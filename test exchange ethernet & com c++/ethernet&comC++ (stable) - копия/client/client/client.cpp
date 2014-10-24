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
- TCP_SEND.C - ���������������� ������ TCP: -
- �������� �� ������ ���� ��� ��������� ���������, -
- ����� ������� ���� ��������� ������ � ���������� ��� -
- �����: tcp_send <�����/���> <����> <���������1> ... -
- ����������: ���������� ������ ��������� ��� <Ctrl-C> -
- � ������ ������ ���� ������� ���� wsock32.lib -
*/




struct hostent* pHostEnt;
int nPortServer, nMsgLen, i;
WSADATA WSAData; //���������, � ������� ����� ���������� ������� ����� ���������� ���������� � ����������. 
WORD wWSAVer;


int typeCommunication;



//����� ����������������
SOCKET SocketConfigureUDP = INVALID_SOCKET;
struct sockaddr_in SocAddrConfigure;
char ConfigureBuffer[402];


//����� ������ UDP
SOCKET SocketCommandUDP = INVALID_SOCKET;
struct sockaddr_in SocAddrCommandUDP;


//����� ������ ������� tcp
SOCKET SocketExchange = INVALID_SOCKET;
struct sockaddr_in SockAddrServer;
char IP[1024];
char port[1024];
char DataBuffer[1024];


//��������� ����������� ����� COM ����
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

int SearchUDP()
{
	//�������� ������ 1
	SocketConfigureUDP = socket(AF_INET, SOCK_DGRAM, 0);
	if (SocketConfigureUDP == INVALID_SOCKET) {
		fputs("Error create socket \n", stderr);
		system("Pause");
		return -1;
	}


	//����� ��������� � ��������
	// ��������� ����������� ������������������ ������
	int optval = 1;
	if (setsockopt(SocketConfigureUDP, SOL_SOCKET, SO_BROADCAST, (char *)&optval, sizeof(optval)) == -1)
	{
		printf("Error setting broadcast socket\n");
		WSACleanup();
		return -1;
	}

	// ������� ������ ���������� ������ broadcast:34001
	SocAddrConfigure.sin_family = AF_INET;
	SocAddrConfigure.sin_port = htons(34001);
	SocAddrConfigure.sin_addr.S_un.S_addr = INADDR_BROADCAST;


	//���������� ������� 
	ConfigureBuffer[0] = 0x01;
	ConfigureBuffer[1] = 0xEF;


	// �������� ������� � ���� �� ���� 34001
	int length2 = sendto(
		SocketConfigureUDP,
		ConfigureBuffer,
		sizeof(ConfigureBuffer),
		0,
		(SOCKADDR*)&SocAddrConfigure,
		sizeof(sockaddr_in)
		);






	//�������� ������ 2
	SocketCommandUDP = socket(AF_INET, SOCK_DGRAM, 0);
	if (SocketCommandUDP == INVALID_SOCKET) {
		fputs("Error create socket \n", stderr);
		system("Pause");
		return -1;
	}

	//����� ��������� � ��������
	// ��������� ����������� ������������������ ������
	int optval2 = 1;
	if (setsockopt(SocketCommandUDP, SOL_SOCKET, SO_BROADCAST, (char *)&optval, sizeof(optval)) == -1)
	{
		printf("Error setting broadcast socket\n");
		WSACleanup();
		return -1;
	}

	// ������� ������ ���������� ������ broadcast:50002
	SocAddrCommandUDP.sin_family = AF_INET;
	SocAddrCommandUDP.sin_port = htons(50002);
	SocAddrCommandUDP.sin_addr.S_un.S_addr = INADDR_BROADCAST;

	//���������� ������� 
	char CommandBuffer[5] = "FIND";

	// �������� ������ � ���� �� ���� 50002
	int length = sendto(
		SocketCommandUDP,
		CommandBuffer,
		sizeof(CommandBuffer)-1,
		0,
		(SOCKADDR*)&SocAddrCommandUDP,
		sizeof(sockaddr_in)
		);



////����� ������� �� ���� 
//int length3 = recvfrom(
//	SocketConfigureUDP,
//	ConfigureBuffer,
//	sizeof(ConfigureBuffer),
//	0,
//struct sockaddr FAR* from,
//	int FAR* fromlen
//	);
//

	//serv_addr.sin_port = htons(0);      //������ ������� �������� ���� �� �� ����������  
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

int LoadLib()
{
	wWSAVer = MAKEWORD(1, 1); //������������� ������ ���������� (WinSock)

	if (WSAStartup(wWSAVer, &WSAData) != 0) // �������� ����������
	{
		puts("Error initialize WinSocket");
		system("Pause");
		return -1;
	}
}

int ExchangeEthernet()
{

	//������������� ����������� �������

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


	return 0;
}

int ExchangeCOMport()
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
