// ConsoleApplication1.cpp: ���������� ����� ����� ��� ����������� ����������.
//

#include "stdafx.h"
#include "header.h"



int _tmain(int argc, _TCHAR* argv[])
{

	//������������� ����������� �������
	wWSAVer = MAKEWORD(1, 1); //������������� ������ ���������� (WinSock)
	if (WSAStartup(wWSAVer, &WSAData) != 0) // �������� ����������
		throw (new exception());

	//�������� ������
	Socket = socket(AF_INET, SOCK_DGRAM, 0);
	if (Socket == INVALID_SOCKET)
		throw (new exception());


	//��������� ������
	//��������� �������� 
	if (setsockopt(Socket, SOL_SOCKET, SO_RCVTIMEO, (char *)&timeout, sizeof(timeout)))
		throw (new exception());

	// ��������� ����������� ������������������ ������
	//int optval2 = 1;
	//if (setsockopt(Socket, SOL_SOCKET, SO_BROADCAST, (char *)&optval2, sizeof(optval2)))
	//	throw (new exception());

	// ������� ���������� ���������� ������ 
	RemoteSocAddr.sin_addr.S_un.S_addr = WStringIPaddressToULong(IP);
	RemoteSocAddr.sin_family = AF_INET;
	RemoteSocAddr.sin_port = htons(RemotePort);

	// ������� ���������� ���������� ������ 
	LocalSocAddr.sin_addr.S_un.S_addr = WStringIPaddressToULong(IP);
	LocalSocAddr.sin_family = AF_INET;
	LocalSocAddr.sin_port = htons(LocalPort);

	//������� 19.01.15 16:55
	int res = bind(Socket, (struct sockaddr*)&LocalSocAddr, sizeof(LocalSocAddr));

	//������ ������������� �����

	hThread = (HANDLE)CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)ListenPort, NULL, 0, NULL);

	bIsOpen = true;

	Sleep(10000000);
	return 0;
}

