/* - ---
- TCP_ECHO.C - ���������������� ������ TCP (������ echo): -
- ��������� ���������� �� ��������, -
- ��������� ��������� � ���������� �� ��������. -
- ������������ ������������ ������ ���� ����������. -
- �����: tcp_echo [<����>]; ����������: <Ctrl-C> -
- ���� �� ��������� - 7 -
- � ������ ������ ���� ������� ����  wsock32.lib
--- - */
#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <winsock.h>
#include <locale.h>
#include <string.h>


#define DEFAULT_ECHO_PORT 7
char DataBuffer[1024];
char answer[15] = "server answer:";

int main(int argc, char** argv)
{
	setlocale(LC_ALL, "Russian");
	struct sockaddr_in SockAddrBase, SockAddrPeer;
	SOCKET SockBase = INVALID_SOCKET, SockData = INVALID_SOCKET;
	unsigned short nPort = DEFAULT_ECHO_PORT;
	int nAddrSize, nCnt;
	WSADATA WSAData;
	WORD wWSAVer;
	//������ ��������� ������: ����� �����
	if (argc > 1)
	if (sscanf(argv[1], "%u", &nPort) < 1)
		fprintf(stderr, "��������� ����: %s, use default", nPort);
	//������������� ���������� �������
	wWSAVer = MAKEWORD(1, 1);
	if (WSAStartup(wWSAVer, &WSAData) != 0) {
		puts("������ ������������� ���������� WinSocket");
		return -1;
	}
	//�������� ���������� ������
	SockBase = socket(PF_INET, SOCK_STREAM, 0);
	if (SockBase == INVALID_SOCKET) {
		fputs("������ �������� ������\n", stderr);
		return -1;
	}
	//�������� �������� ������ � ���������� ������
	memset(&SockAddrBase, 0, sizeof(SockAddrBase));
	SockAddrBase.sin_family = AF_INET;
	SockAddrBase.sin_addr.S_un.S_addr = INADDR_ANY;
	SockAddrBase.sin_port = htons(nPort); //(<�����_�����_�������>);
	if (bind(SockBase,
		(struct sockaddr*) &SockAddrBase, sizeof(SockAddrBase)
		) != 0)
	{
		fprintf(stderr, "������ �������� � ���������� �����: %u\n",
			ntohs(SockAddrBase.sin_port)
			);
		return -1;
	}
	//��������� ������ "�������������"
	if (listen(SockBase, 2) != 0) { //������� �� 2 �����
		closesocket(SockBase);
		fputs("������ ��������� ������ �������������\n", stderr);;
		return -1;
	}
	fprintf(stderr,
		"������ �������, ���� %u\n",
		ntohs(SockAddrBase.sin_port)
		);
	//�������� ������� ���� - ����� � ������������ ����������
	while (1) { //��� ������� ���� ������ ����������
		nAddrSize = sizeof (SockAddrPeer);
		SockData = accept(SockBase,
			(struct sockaddr*)&SockAddrPeer, &nAddrSize
			);
		if (SockData == INVALID_SOCKET) {
			fputs("������ ������ ����������\n", stderr);
			continue;
		}
		//���� ������������ ������ ����������
		while (1) {
			nCnt = recv(SockData, DataBuffer, sizeof(DataBuffer)-1, 0);
			if (nCnt <= 0)
				break;

			char resp[sizeof(answer)+sizeof(DataBuffer)];
			strcat(strcpy(resp, answer), DataBuffer);

			send(SockData, resp, nCnt, 0); //������� "���"
		}

		





		shutdown(SockData, 2);
		closesocket(SockData); SockData = INVALID_SOCKET;
	}
	//���������� - ����� ������� �� �����������!
	shutdown(SockBase, 2);
	Sleep(100);
	closesocket(SockBase); SockBase = INVALID_SOCKET;
	WSACleanup();
	return 0;
}
/* - ---
- TCP_SEND.C - ���������������� ������ TCP: -
- �������� �� ������ ���� ��� ��������� ���������, -
- ����� ������� ���� ��������� ������ � ���������� ��� -
- �����: tcp_send <�����/���> <����> <���������1> ... -
- ����������: ���������� ������ ��������� ��� <Ctrl-C> -
- � ������ ������ ���� ������� ���� wsock32.lib -
--- -
#include <windows.h>
#include <stdio.h>
#include <winsock.h>
char DataBuffer [1024];
int main (int argc, char** argv)
{
struct sockaddr_in SockAddrServer;
SOCKET SockData = INVALID_SOCKET;
struct hostent* pHostEnt;
int nPortServer, nMsgLen, i;
WSADATA WSAData;
WORD wWSAVer;
//��������� ������
if (argc < 3) {
puts ("������������ ����������\n");
puts ("�����: TCP_SEND <addr/name> <port> <msg1> ...\n");
return -1;
}
//������������� ���������� �������
wWSAVer = MAKEWORD (1, 1);
if (WSAStartup (wWSAVer, &WSAData) != 0) {
puts ("������ ������������� WinSocket");
return -1;
}
//���������� ������ �������
memset (&SockAddrServer, 0, sizeof (SockAddrServer));
SockAddrServer.sin_family = AF_INET;
if (strcmp (argv[1], "255.255.255.255") == 0) //����� broadcast
SockAddrServer.sin_addr.S_un.S_addr = INADDR_BROADCAST;
else {
SockAddrServer.sin_addr.S_un.S_addr = inet_addr (argv[1]);
if (SockAddrServer.sin_addr.S_un.S_addr == INADDR_NONE) {
if ((pHostEnt = gethostbyname (argv[1])) == NULL) {
fprintf (stderr, "���� �� �������: %s\n", argv[1]);
return -1;
}
SockAddrServer.sin_addr = *(struct in_addr*)(pHostEnt->h_addr_list[0]);
}
}
if (sscanf (argv[2], "%u", &nPortServer) < 1) {
fprintf (stderr, "��������� ����� �����: %s\n", argv[2]);
return -1;
}
SockAddrServer.sin_port = htons ((unsigned short)nPortServer);
//�������� ���������� ������
SockData = socket (PF_INET, SOCK_STREAM, 0);
if (SockData == INVALID_SOCKET) {
fputs ("������ �������� ������\n", stderr);
return -1;
}
//������ �� ������������ ����������
if (connect (SockData,
(const struct sockaddr*)&SockAddrServer, sizeof(SockAddrServer)) != 0)
{
fprintf (stderr, "������ ���������� � %s:%u\n",
inet_ntoa (SockAddrServer.sin_addr),
ntohs (SockAddrServer.sin_port)
);
closesocket (SockData);
return -1;
}
fprintf (stdout, "����������� ���������� � ��������: %s:%u\n",
inet_ntoa (SockAddrServer.sin_addr),
ntohs (SockAddrServer.sin_port)
);
//������� ����
for (i=3; i<argc; ++i) { //��������� ��������� ��������� ������
//�������� ���������
fprintf (stdout, "�������: \"%s\" \n", argv[i]);
nMsgLen = strlen (argv[i]) + 1;
if (send (SockData, argv[i], nMsgLen, 0) < nMsgLen) {
fprintf (stderr, "������ �������: \"%s\"\n", argv[i]);
continue;
}
//������� � ���������� �����
fprintf (stdout, "�����...");
nMsgLen = recv (SockData, DataBuffer, sizeof(DataBuffer)-1, 0);
if (nMsgLen <= 0) { //������ ������ ������
fputs ("������ ������\n", stderr);
continue;
}
DataBuffer [nMsgLen] = '\0';
fprintf (stdout, "\b\b\b: \"%s\" \n", DataBuffer); }
//����������
shutdown (SockData, 2);
Sleep (100);
closesocket (SockData); SockData = INVALID_SOCKET;
WSACleanup ();
return 0;
}
*/