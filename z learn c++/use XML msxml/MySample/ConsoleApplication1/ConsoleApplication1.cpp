//https://rsdn.ru/article/xml/msxml.xml


#include "stdafx.h"
#include <windows.h>
#include <msxml.h>
#include <objsafe.h>
#include <objbase.h>
#include <atlbase.h>
#pragma warning( push )
#pragma warning( disable: 4018 4786)
#include <string>
#pragma warning( pop )
using namespace std;



//строка с xml документом
CComBSTR xml = "<?xml version =\"1.0\"?><Settings MyArttribute =\"TextMyAttribute\"><IP>192.168.1.1</IP><MAC>C0:C1:C2:C3:C4:C5</MAC><Port>5555</Port></Settings>";

//Обьявдение обьектов нодов
CComPtr<IXMLDOMNode> spXMLNodeSettings;
CComPtr<IXMLDOMNode> spXMLNodeIP;
CComPtr<IXMLDOMNode> spXMLNodePortImage;

CComQIPtr<IXMLDOMElement> spXMLElement;

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
	hr = spXMLDOM->selectSingleNode(CComBSTR(L"Settings/IP"), &spXMLNodeIP);

	//Чтение текста из ноды.
	BSTR text;
	spXMLNodeIP->get_text(&text);

	//Запись текста в ноду
	spXMLNodeIP->put_text(CComBSTR("192.168.1.2"));

	//Создание дочерней ноды c текстом.
	hr = spXMLDOM->selectSingleNode(CComBSTR(L"Settings"), &spXMLNodeSettings);
	hr = spXMLDOM->createNode(CComVariant(NODE_ELEMENT), CComBSTR("PortImage"), NULL, &spXMLNodePortImage);
	spXMLNodePortImage->put_text(CComBSTR("7777"));
	hr = spXMLNodeSettings->appendChild(spXMLNodePortImage, NULL);


	//Чтение аттрибута.
	spXMLElement = spXMLNodeSettings;
	CComVariant var;
	spXMLElement->getAttribute(CComBSTR("MyAttribute"), &var);

	//Добавление атрибута.
	spXMLElement = spXMLNodeSettings;
	hr = spXMLElement->setAttribute(CComBSTR(L"MyAttribute"), CComVariant(L"TextAttribute"));


	//Сохранение в файл.
	hr = spXMLDOM->save(CComVariant("xmldata.xml"));
	
	return 0;
}


