// ConsoleApplication1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "targetver.h"
#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <winsock.h>
#include <locale.h>
#include <stdlib.h>
#include <string.h>
#include <string>
#include <list>
#include <iostream>
#include <vector>
#include <list>
#include <sstream>
#import <msxml3.dll> raw_interfaces_only


using std::vector;
using std::string;
using namespace std;

//using namespace MSXML2;

char szPathToReceptorSpecificData[MAX_PATH];
char szPortName[256];

char* widetochar(wchar_t* w_src, char* c_dest, int c_dest_max_len)
{
	memset(c_dest, 0, c_dest_max_len * sizeof(char));

	int len = WideCharToMultiByte(CP_ACP, 0,
		w_src, (int)wcslen(w_src),
		NULL, 0, NULL, NULL);
	len = min(len, c_dest_max_len - 1);

	WideCharToMultiByte(CP_ACP, 0,
		w_src, (int)wcslen(w_src),
		c_dest, len,
		NULL, NULL);

	return c_dest;
}

int _tmain(int argc, _TCHAR* argv)
{
	wchar_t * xmlstr;
	bool bConfig;

	MSXML2::IXMLDOMDocumentPtr pdoc;
	MSXML2::IXMLDOMElementPtr pdocroot;
	_bstr_t bstr = (_bstr_t)xmlstr;

	::CoInitialize(NULL);
	HRESULT hr = pdoc.CreateInstance();
	if (FAILED(hr))
		return false;

	VARIANT_BOOL *IsSucessfull = false;

	if (!pdoc->loadXML(bstr, IsSucessfull))
		return false;

	pdocroot = pdoc->get_firstChild;
	if (pdocroot->get_nodeName != (_bstr_t)"Detector")
		return false;

	if (bConfig)
	{
		pdocroot->getAttribute(TEXT("ReceptorSpecificData"),NULL);
		widetochar(bstr, szPathToReceptorSpecificData, MAX_PATH);
	}
	else
	{
		pdocroot->getAttribute(TEXT("Port"), NULL);

		if (bstr.length() > 0)
			widetochar(bstr, szPortName, 256);
		else
			_snprintf(szPortName, 256, "COM1");


	return 0;

}



