#include "stdafx.h"
#include "point.h"




MyClass::~MyClass()
{

}

int MyClass::MyMethod()
{
	return 9993;
}

MyClass *classs;


#pragma region Экспортируемые функции

DLLEXPORT  int ReturnIntModify(int value)
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

//не работает, указатель возвращает но его в строку не преобразовать
DLLEXPORT int ModifyByteArr(BYTE &bytes)
{
	BYTE* b = (BYTE*)"Hello! 123";
	bytes = *b;
	return 10;
}


//не работает
DLLEXPORT int ReturnCharArrPointer(char *value)
{
	value = "testMessage";
	return 11;
}

//не работает, указатель возвращает но его в строку не преобразовать
DLLEXPORT int ReturnByteArrPointer(int &lala, int a)
{

	int *value = new int[a];
	value[0] = 0x21;
	value[1] = 0x22;
	value[2] = 0x23;
	lala = *value;
	return 3;
}

//работает
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
}
#pragma endregion


DLLEXPORT int a()
{
	classs = new MyClass();

	i = classs->MyMethod();
	return i;

}



