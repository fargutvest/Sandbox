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

using std::vector;
using std::string;
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	/*vector <BYTE> buffResponce(4);
	buffResponce[1] = 0x13;
	buffResponce[2] = 0xe8;

	unsigned char hi = buffResponce[1];
	unsigned char lo = buffResponce[2];

	int valHi = hi << 8;
	int valLo = lo;
	int val =  valHi + valLo;
	string s = to_string(val);*/

	vector <BYTE> buffResponce(4);
	buffResponce[1] = 0x7;
	buffResponce[2] = 0xee;

	BYTE hi = buffResponce[1];
	BYTE lo = buffResponce[2];

	int valHi = hi << 8;
	int valLo = lo;
	int value = valHi + valLo;

	
	char *fw = (char*)to_string(value).c_str();
	

	return 0;
}



