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

vector<BYTE> Value;

typedef void(*callback_function)(int* len, BYTE* value);
callback_function DataReceived;



DLLEXPORT void Invoke()
{
	Value.push_back(0xbb);
	Value.push_back(0xcc);

	if (DataReceived != NULL)
		DataReceived((int*)Value.size(), Value.data());
}

DLLEXPORT void Subscribe(callback_function callback)
{
	DataReceived = callback;
}

DLLEXPORT BYTE *GetData(int *len)
{
	Value.push_back(0xff);
	Value.push_back(0xaa);

	*len = Value.size();
	return Value.data();
}









