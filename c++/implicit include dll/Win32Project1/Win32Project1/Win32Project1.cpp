// Win32Project1.cpp : Defines the entry point for the console application.
//

#pragma comment(lib,"WIZNETLIB.dll")
#pragma comment(lib,"Delayimp.dll")
#pragma comment(lib,"/DELAYLOAD:WIZNETLIB.dll")
#include "stdafx.h"
#include "WIZnetLib.h"

int _tmain(int argc, _TCHAR* argv[])
{
	int r = Open();
	return 0;
	
}

