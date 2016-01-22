// Win32Project1.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "Singleton.h"


int _tmain(int argc, _TCHAR* argv[])
{
	
	int old = Singleton::Instance().field;
	Singleton::Instance().field = old + 25;
	int _new = Singleton::Instance().field;

	return 0;
}

