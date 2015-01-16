// ConsoleApplication1.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <vector>
#include <Windows.h>
using namespace std;

void CopyVectorByte(vector<BYTE> *destination, u_int begin_dest, vector<BYTE> *source, u_int begin_src, u_int count)
{

	if ((*source).size() < begin_src + count)
		return;

	if ((*destination).size() < begin_dest + count)
	{
		(*destination).reserve(begin_dest + count);
		(*destination).resize(begin_dest + count);
	}
		

	for (u_int i = 0; i < count; i++)
	{
		(*destination)[i + begin_dest] = (*source)[i + begin_src];
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	vector<BYTE> b1;
	vector<BYTE> b2;
	for (int i = 0; i < 20; i++)
	{
		b1.push_back(i*i);
	}

	CopyVectorByte(&b2, 10, &b1, 3, 2);

	return 0;
}

