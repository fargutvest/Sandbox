//https://rsdn.ru/article/xml/msxml.xml


#include "stdafx.h"
#include <windows.h>
//#include <msxml.h>
#include <MsXml6.h>
#include <objsafe.h>
#include <objbase.h>
#include <atlbase.h>
#pragma warning( push )
#pragma warning( disable: 4018 4786)
#include <string>
#pragma warning( pop )
using namespace std;



//строка с xml документом
CComBSTR xml = "<?xml version =\"1.0\"?><Settings><IP>192.168.1.1</IP><MAC>C0:C1:C2:C3:C4:C5</MAC><PortImageLocal>7000</PortImageLocal><LineLength>760</LineLength></Settings>";

//Обьявление обьектов нодов
CComPtr<IXMLDOMNode> spXMLNodeSettings;
CComPtr<IXMLDOMNode> spXMLNodeIP;
CComPtr<IXMLDOMNode> spXMLNodeMAC;
CComPtr<IXMLDOMNode> spXMLNodePortImageLocal;
CComPtr<IXMLDOMNode> spXMLNodeLineLength;

//Константы имен нодов
const wstring IP = TEXT("IP");
const wstring MAC = TEXT("MAC");
const wstring PortImageLocal = TEXT("PortImageLocal");
const wstring LineLength = TEXT("LineLength");

//Контейнеры для параметров
wstring IPvalue;
wstring MACvalue;
wstring PortImageLocalvalue;
wstring LineLengthvalue;



int _tmain(int argc, _TCHAR* argv[])
{
	CoInitialize(NULL);

	//Инициализация библиотеки COM, создание экземпляра парсера MSXML.
	CComPtr<IXMLDOMDocument> spXMLDOM;
	HRESULT hr = spXMLDOM.CoCreateInstance(__uuidof(DOMDocument));

	//Загрузка XML-документа из файла.
	VARIANT_BOOL bSuccess = false;
	//hr = spXMLDOM->load(CComVariant(L"xmldata.xml"), &bSuccess);

	//Загрузка XML-документа из строки.
	hr = spXMLDOM->loadXML(xml, &bSuccess);


	//Поиск интересующей ноды.
	hr = spXMLDOM->selectSingleNode(CComBSTR(L"Settings"), &spXMLNodeSettings);

	CComPtr<IXMLDOMNodeList> list;
	spXMLNodeSettings->get_childNodes(&list);

	long length = 0;
	hr = list->get_length(&length);
	for (long i = 0; i < length; i++)
	{
		CComPtr<IXMLDOMNode> node;
		hr = list->get_item(i, &node);

		BSTR text;
		hr = node->get_text(&text);

		BSTR name;
		hr = node->get_nodeName(&name);

		if (name == IP)
		{
			IPvalue = text;
		}
		if (name == MAC)
		{
			MACvalue = text;
		}
		if (name == PortImageLocal)
		{
			PortImageLocalvalue = text;
		}
		if (name == LineLength)
		{
			LineLengthvalue = text;
		}
	}



	//Чтение текста из ноды.
	BSTR text;
	spXMLNodeIP->get_text(&text);





	return 0;
}


