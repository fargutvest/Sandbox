// ConsoleApplication1.cpp: определ€ет точку входа дл€ консольного приложени€.
//

#include "stdafx.h"
#include <vector>
#include <windows.h>


using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	vector<BYTE> *DataImage;

	//¬арианты доступа к вектору через указатель:

	(*DataImage)[1] = 0x55;
	DataImage->operator[](1) = 0x55;
	DataImage->at(1) = 0x55;
	(*DataImage).at(1) = 0x55;





	return 0;
}

