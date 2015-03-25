// ConsoleApplication1.cpp: определяет точку входа для консольного приложения.
//

//#define WIN32_LEAN_AND_MEAN             // Исключите редко используемые компоненты из заголовков Windows
// Файлы заголовков Windows:
//#define WIN32_LEAN_AND_MEAN
//#pragma comment(lib,"wsock32.lib")
#pragma comment(lib,"ws2_32.lib")
#include "stdafx.h"
#include <WinSock2.h>
#include <string>
#include <stdio.h>
//#include <netdb.h>

using namespace std;



int _tmain(int argc, _TCHAR* argv[])
{
	// ИНИЦИАЛИЗАЦИЯ ПОДСИСТЕММЫ СОКЕТОВ
	WSADATA WSAData;
	WSAStartup(WINSOCK_VERSION, &WSAData);


	struct hostent *lh = gethostbyname("localhost");
	struct sockaddr_in adr1;
	memcpy(&adr1.sin_addr, lh->h_addr_list[0], lh->h_length);

	char* out = "WinSock ERR";
	WSADATA wsaData;

	char chInfo[64];
	if (!gethostname(chInfo, sizeof(chInfo)))
	{
		hostent *sh;
		sh = gethostbyname((char*)&chInfo);
		if (sh != NULL)
		{
			int nAdapter = 0;
			while (sh->h_addr_list[nAdapter])
			{
				struct sockaddr_in adr;
				memcpy(&adr.sin_addr, sh->h_addr_list[nAdapter], sh->h_length);
				out = inet_ntoa(adr.sin_addr);
				nAdapter++;
			}
		}
	}


	WSACleanup();


	return 0;
}

