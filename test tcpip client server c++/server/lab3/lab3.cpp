/* - ---
- TCP_ECHO.C - демонстрационный сервер TCP (служба echo): -
- принимает соединения от клиентов, -
- принимает сообщения и возвращает их клиентам. -
- Одновременно поддерживает только одно соединение. -
- Вызов: tcp_echo [<порт>]; завершение: <Ctrl-C> -
- Порт по умолчанию - 7 -
- В проект должен быть включен файл  wsock32.lib
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
	//разбор командной строки: номер порта
	if (argc > 1)
	if (sscanf(argv[1], "%u", &nPort) < 1)
		fprintf(stderr, "Ошибочный порт: %s, use default", nPort);
	//инициализация подсистемы сокетов
	wWSAVer = MAKEWORD(1, 1);
	if (WSAStartup(wWSAVer, &WSAData) != 0) {
		puts("Ошибка инициализации подсистемы WinSocket");
		return -1;
	}
	//создание локального сокета
	SockBase = socket(PF_INET, SOCK_STREAM, 0);
	if (SockBase == INVALID_SOCKET) {
		fputs("Ошибка создания сокета\n", stderr);
		return -1;
	}
	//привязка базового сокета к локальному адресу
	memset(&SockAddrBase, 0, sizeof(SockAddrBase));
	SockAddrBase.sin_family = AF_INET;
	SockAddrBase.sin_addr.S_un.S_addr = INADDR_ANY;
	SockAddrBase.sin_port = htons(nPort); //(<номер_порта_сервера>);
	if (bind(SockBase,
		(struct sockaddr*) &SockAddrBase, sizeof(SockAddrBase)
		) != 0)
	{
		fprintf(stderr, "Ошибка привязки к локальному порту: %u\n",
			ntohs(SockAddrBase.sin_port)
			);
		return -1;
	}
	//включение режима "прослушивания"
	if (listen(SockBase, 2) != 0) { //очередь на 2 места
		closesocket(SockBase);
		fputs("Ошибка включения режима прослушивания\n", stderr);;
		return -1;
	}
	fprintf(stderr,
		"Сервер запущен, порт %u\n",
		ntohs(SockAddrBase.sin_port)
		);
	//основной рабочий цикл - прием и обслуживание соединений
	while (1) { //для сервера цикл обычно бесконечен
		nAddrSize = sizeof (SockAddrPeer);
		SockData = accept(SockBase,
			(struct sockaddr*)&SockAddrPeer, &nAddrSize
			);
		if (SockData == INVALID_SOCKET) {
			fputs("Ошибка приема соединения\n", stderr);
			continue;
		}
		//цикл обслуживания одного соединения
		while (1) {
			nCnt = recv(SockData, DataBuffer, sizeof(DataBuffer)-1, 0);
			if (nCnt <= 0)
				break;

			char resp[sizeof(answer)+sizeof(DataBuffer)];
			strcat(strcpy(resp, answer), DataBuffer);

			send(SockData, resp, nCnt, 0); //возврат "эха"
		}

		





		shutdown(SockData, 2);
		closesocket(SockData); SockData = INVALID_SOCKET;
	}
	//завершение - здесь никогда не достигается!
	shutdown(SockBase, 2);
	Sleep(100);
	closesocket(SockBase); SockBase = INVALID_SOCKET;
	WSACleanup();
	return 0;
}
/* - ---
- TCP_SEND.C - демонстрационный клиент TCP: -
- отсылает на сервер одно или несколько сообщений, -
- после каждого ждет получения ответа и отображает его -
- Вызов: tcp_send <адрес/имя> <порт> <сообщение1> ... -
- Завершение: исчерпание списка сообщений или <Ctrl-C> -
- В проект должен быть включен файл wsock32.lib -
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
//командная строка
if (argc < 3) {
puts ("Недостаточно аргументов\n");
puts ("Вызов: TCP_SEND <addr/name> <port> <msg1> ...\n");
return -1;
}
//инициализация подсистемы сокетов
wWSAVer = MAKEWORD (1, 1);
if (WSAStartup (wWSAVer, &WSAData) != 0) {
puts ("Ошибка инициализации WinSocket");
return -1;
}
//подготовка адреса сервера
memset (&SockAddrServer, 0, sizeof (SockAddrServer));
SockAddrServer.sin_family = AF_INET;
if (strcmp (argv[1], "255.255.255.255") == 0) //адрес broadcast
SockAddrServer.sin_addr.S_un.S_addr = INADDR_BROADCAST;
else {
SockAddrServer.sin_addr.S_un.S_addr = inet_addr (argv[1]);
if (SockAddrServer.sin_addr.S_un.S_addr == INADDR_NONE) {
if ((pHostEnt = gethostbyname (argv[1])) == NULL) {
fprintf (stderr, "Хост не опознан: %s\n", argv[1]);
return -1;
}
SockAddrServer.sin_addr = *(struct in_addr*)(pHostEnt->h_addr_list[0]);
}
}
if (sscanf (argv[2], "%u", &nPortServer) < 1) {
fprintf (stderr, "Ошибочный номер порта: %s\n", argv[2]);
return -1;
}
SockAddrServer.sin_port = htons ((unsigned short)nPortServer);
//создание локального сокета
SockData = socket (PF_INET, SOCK_STREAM, 0);
if (SockData == INVALID_SOCKET) {
fputs ("Ошибка создания сокета\n", stderr);
return -1;
}
//запрос на установление соединения
if (connect (SockData,
(const struct sockaddr*)&SockAddrServer, sizeof(SockAddrServer)) != 0)
{
fprintf (stderr, "Ошибка соединения с %s:%u\n",
inet_ntoa (SockAddrServer.sin_addr),
ntohs (SockAddrServer.sin_port)
);
closesocket (SockData);
return -1;
}
fprintf (stdout, "Установлено соединение с сервером: %s:%u\n",
inet_ntoa (SockAddrServer.sin_addr),
ntohs (SockAddrServer.sin_port)
);
//рабочий цикл
for (i=3; i<argc; ++i) { //остальные аргументы командной строки
//отослать сообщение
fprintf (stdout, "Отсылка: \"%s\" \n", argv[i]);
nMsgLen = strlen (argv[i]) + 1;
if (send (SockData, argv[i], nMsgLen, 0) < nMsgLen) {
fprintf (stderr, "Ошибка отсылки: \"%s\"\n", argv[i]);
continue;
}
//принять и отобразить ответ
fprintf (stdout, "Прием...");
nMsgLen = recv (SockData, DataBuffer, sizeof(DataBuffer)-1, 0);
if (nMsgLen <= 0) { //ошибка приема ответа
fputs ("Ошибка приема\n", stderr);
continue;
}
DataBuffer [nMsgLen] = '\0';
fprintf (stdout, "\b\b\b: \"%s\" \n", DataBuffer); }
//завершение
shutdown (SockData, 2);
Sleep (100);
closesocket (SockData); SockData = INVALID_SOCKET;
WSACleanup ();
return 0;
}
*/