// ConsoleApplication1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <winuser.h>
#include <stdio.h>



class CSource {
public:
	__event void MyEvent(int nValue);
};

class CReceiver {
public:
	void MyHandler1(int nValue) {
		printf_s("MyHandler1 was called with value %d.\n", nValue);
	}

	void MyHandler2(int nValue) {
		printf_s("MyHandler2 was called with value %d.\n", nValue);
	}

	void hookEvent(CSource* pSource) {
		__hook(&CSource::MyEvent, pSource, &CReceiver::MyHandler1);
		__hook(&CSource::MyEvent, pSource, &CReceiver::MyHandler2);
	}

	void unhookEvent(CSource* pSource) {
		__unhook(&CSource::MyEvent, pSource, &CReceiver::MyHandler1);
		__unhook(&CSource::MyEvent, pSource, &CReceiver::MyHandler2);
	}
};



int _tmain(int argc, _TCHAR* argv[])
{
	CSource source;
	CReceiver receiver;

	receiver.hookEvent(&source);
	__raise source.MyEvent(123);
	receiver.unhookEvent(&source);

	system("pause");

	return 0;

	
}




