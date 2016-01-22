// ConsoleApplication1.cpp: определяет точку входа для консольного приложения.
//


#include "stdafx.h"
#include <windows.h>
#include <vector>

using std::vector;

int _tmain(int argc, _TCHAR* argv[])
{
	vector<BYTE> a;
	a.push_back(0x01);
	a.push_back(0x02);
	a.push_back(0x03);

	vector<BYTE> b;
	b.insert(b.end(), a.begin() + 1, a.begin() + 2);

	a.clear();
	a.resize(0);

	return 0;
}


