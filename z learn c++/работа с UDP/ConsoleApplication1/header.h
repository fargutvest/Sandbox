#define WIN32_LEAN_AND_MEAN             // ��������� ����� ������������ ���������� �� ���������� Windows
// ����� ���������� Windows:
#define WIN32_LEAN_AND_MEAN
#pragma comment(lib, "ws2_32.lib")
#include "stdafx.h"
#include <winsock.h>
#include <string.h>
#include <string>
#include <vector>
#include <process.h>

using namespace std;


#pragma region ����� ������ UDP
SOCKET Socket = INVALID_SOCKET;
struct sockaddr_in RemoteSocAddr;
struct sockaddr_in LocalSocAddr;
int timeout = 1000;

//���������, � ������� ����� ���������� ������� ����� ���������� ���������� � ����������. 
WSADATA WSAData;
WORD wWSAVer;

#pragma endregion

#pragma region ���������� ������

//����� ������
HANDLE hThread;
//���� ��� ��������� ������
bool bThreadStop;

#pragma endregion

#pragma region ��������� ����������
u_short LocalPort = 48569;
u_short RemotePort = 9999;
wstring IP = TEXT("192.168.2.133");
#pragma endregion


vector<BYTE> ReadBuffer;
bool bListenMode;

bool bIsOpen;

u_long WStringIPaddressToULong(const std::wstring& address)
{
	int a = 0, b = 0, c = 0, d = 0;
	swscanf_s(address.c_str(), L"%u.%u.%u.%u", &a, &b, &c, &d);
	u_long res = d << 24 | c << 16 | b << 8 | a;
	return res;
}


int ListenPort()
{
	sockaddr_in SocReceive;
	int SockReceiveSize = sizeof(SocReceive);
	char buffer[0xff];
	int lengthReceiveData;
	int bufferLength = 0xff;


	//����� ������ 
	while (!bThreadStop)
	{
		char* buff = "hello";
		int lengthWrite = sendto(Socket, buff, 5, 0, (SOCKADDR*)&RemoteSocAddr, sizeof(sockaddr_in));

		Sleep(1);
		//����� ������� �� ����
		lengthReceiveData = recvfrom(Socket, buffer, bufferLength, 0, (SOCKADDR *)&SocReceive, &SockReceiveSize);

		if (lengthReceiveData < 0)
		{
			int error = WSAGetLastError();
			int iii = 0;
		}
	}
	return 0;
}
