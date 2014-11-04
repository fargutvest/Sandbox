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
#include <fstream>
#include <vector>
#include <sstream>
#include <map>
using std::vector;
using std::string;

#define DLLEXPORT extern "C" __declspec(dllexport)

DLLEXPORT int ReturnIntModify(int value)
{
	return value*value;
}

DLLEXPORT int *ReturnIntArr()
{
//	int *arr = new int[]{1111, 2222, 3333, 4444};
	
	//return arr;
	return new int[]{1111, 2222, 3333, 4444};
}

DLLEXPORT char *ReturnCharArr()
{
	char *f = "testMessage";
	return f;
}

DLLEXPORT int ReturnCharArrPointer(char *value)
{
	 value = "testMessage";
	return 11;
}


DLLEXPORT int ReturnByteArrPointer(int &lala)
{
	int *value = new int[4];
	value[0] = 0x21;
	value[1] = 0x22;
	value[2] = 0x23;
	lala = *value;
	return 3;
}



DLLEXPORT char *testChar3()
{
	vector <BYTE> bufferResponce;
	bufferResponce.push_back(0x32);
	bufferResponce.push_back(0x67);

	char *f;
	f = new char[bufferResponce.size() + 1];
	f[bufferResponce.size()] = '\0';
	for (int i = 0; i < bufferResponce.size(); i++)
	{
		f[i] = bufferResponce[i];
	}


	
	
	return f;
};