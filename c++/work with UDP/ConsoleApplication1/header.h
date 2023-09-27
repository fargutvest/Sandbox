#define WIN32_LEAN_AND_MEAN             // Исключите редко используемые компоненты из заголовков Windows
// Файлы заголовков Windows:
#define WIN32_LEAN_AND_MEAN
#pragma comment(lib, "ws2_32.lib")
#include "stdafx.h"
#include <winsock.h>
#include <string.h>
#include <string>
#include <vector>
#include <process.h>

using namespace std;


#pragma region сокет команд UDP
SOCKET Socket = INVALID_SOCKET;
struct sockaddr_in RemoteSocAddr;
struct sockaddr_in LocalSocAddr;
int timeout = 1000;

//структура, в которой после выполнения функции будет находиться информация о библиотеке. 
WSADATA WSAData;
WORD wWSAVer;

#pragma endregion

#pragma region переменные потока

//хэндл потока
HANDLE hThread;
//флаг для остановки потока
bool bThreadStop;

#pragma endregion

#pragma region Настройки соединения
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


	//прием данных 
	while (!bThreadStop)
	{
		char* buff = "hello";
		int lengthWrite = sendto(Socket, buff, 5, 0, (SOCKADDR*)&RemoteSocAddr, sizeof(sockaddr_in));

		Sleep(1);
		//Прием буффера из сети
		lengthReceiveData = recvfrom(Socket, buffer, bufferLength, 0, (SOCKADDR *)&SocReceive, &SockReceiveSize);

		if (lengthReceiveData < 0)
		{
			int error = WSAGetLastError();
			int iii = 0;
		}
	}
	return 0;
}
