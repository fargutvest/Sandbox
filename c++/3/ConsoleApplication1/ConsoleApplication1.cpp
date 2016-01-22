// ConsoleApplication1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "targetver.h"
#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <winsock.h>
#include <locale.h>
#include <stdlib.h>
#include <string.h>
#include <string>
#include <list>
#include <iostream>
#include <vector>
#include <list>
#include <sstream>
#import <msxml3.dll> raw_interfaces_only


using std::vector;
using std::string;
using namespace std;

//using namespace MSXML2;

char szPathToReceptorSpecificData[MAX_PATH];
char szPortName[256];

char* widetochar(wchar_t* w_src, char* c_dest, int c_dest_max_len)
{
	memset(c_dest, 0, c_dest_max_len * sizeof(char));

	int len = WideCharToMultiByte(CP_ACP, 0,
		w_src, (int)wcslen(w_src),
		NULL, 0, NULL, NULL);
	len = min(len, c_dest_max_len - 1);

	WideCharToMultiByte(CP_ACP, 0,
		w_src, (int)wcslen(w_src),
		c_dest, len,
		NULL, NULL);

	return c_dest;
}

int _tmain(int argc, _TCHAR* argv)
{
	wstring s(L"C0");
	wstringstream ss(s);
	int i = 0;
	ss >> hex >> i;
}




