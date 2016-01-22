#include "stdafx.h"
#include <windows.h>
#include <winuser.h>
#include <stdio.h>

class CSource
{
public:
	__event void MyEvent(int nValue);

	void raise(int value)
	{
		__raise MyEvent(value);
	}


};

class CReceiver
{
public:
	void MyHandler1(int nValue)
	{
		printf_s("MyHandler1 was called with value %d.\n", nValue);
	}

	void hookEvent(CSource* pSource)
	{
		__hook(&CSource::MyEvent, pSource, &CReceiver::MyHandler1);
		
	}

	void unhookEvent(CSource* pSource)
	{
		__unhook(&CSource::MyEvent, pSource, &CReceiver::MyHandler1);
	}
};



int _tmain(int argc, _TCHAR* argv[])
{
	CSource source;
	CReceiver receiver;

	receiver.hookEvent(&source);
	source.raise(123);
	receiver.unhookEvent(&source);

	system("pause");

	return 0;
}




