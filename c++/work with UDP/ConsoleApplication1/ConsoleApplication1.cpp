// ConsoleApplication1.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "header.h"



int _tmain(int argc, _TCHAR* argv[])
{

	//ИНИЦИАЛИЗАЦИЯ ПОДСИСТЕММЫ СОКЕТОВ
	wWSAVer = MAKEWORD(1, 1); //запрашиваемая версия библиотеки (WinSock)
	if (WSAStartup(wWSAVer, &WSAData) != 0) // загрузка библиотеки
		throw (new exception());

	//ОТКРЫТИЕ СОКЕТА
	Socket = socket(AF_INET, SOCK_DGRAM, 0);
	if (Socket == INVALID_SOCKET)
		throw (new exception());


	//НАСТРОЙКА СОКЕТА
	//установка таймаута 
	if (setsockopt(Socket, SOL_SOCKET, SO_RCVTIMEO, (char *)&timeout, sizeof(timeout)))
		throw (new exception());

	// установка возможности широковещательного адреса
	//int optval2 = 1;
	//if (setsockopt(Socket, SOL_SOCKET, SO_BROADCAST, (char *)&optval2, sizeof(optval2)))
	//	throw (new exception());

	// задание параметров удаленного сокета 
	RemoteSocAddr.sin_addr.S_un.S_addr = WStringIPaddressToULong(IP);
	RemoteSocAddr.sin_family = AF_INET;
	RemoteSocAddr.sin_port = htons(RemotePort);

	// задание параметров локального сокета 
	LocalSocAddr.sin_addr.S_un.S_addr = WStringIPaddressToULong(IP);
	LocalSocAddr.sin_family = AF_INET;
	LocalSocAddr.sin_port = htons(LocalPort);

	//добавил 19.01.15 16:55
	int res = bind(Socket, (struct sockaddr*)&LocalSocAddr, sizeof(LocalSocAddr));

	//ЗАПУСК ПРОСЛУШИВАНИЯ ПОРТА

	hThread = (HANDLE)CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)ListenPort, NULL, 0, NULL);

	bIsOpen = true;

	Sleep(10000000);
	return 0;
}

