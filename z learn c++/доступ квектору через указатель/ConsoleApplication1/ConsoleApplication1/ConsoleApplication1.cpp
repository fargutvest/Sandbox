// ConsoleApplication1.cpp: ���������� ����� ����� ��� ����������� ����������.
//

#include "stdafx.h"
#include <vector>
#include <windows.h>


using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	vector<BYTE> *DataImage;

	//�������� ������� � ������� ����� ���������:

	(*DataImage)[1] = 0x55;
	DataImage->operator[](1) = 0x55;
	DataImage->at(1) = 0x55;
	(*DataImage).at(1) = 0x55;





	return 0;
}

