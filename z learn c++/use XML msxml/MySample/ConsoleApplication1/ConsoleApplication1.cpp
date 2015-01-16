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



//Моё консольное приложение будет загружать XML - документ под названием xmldata.xml

int _tmain(int argc, _TCHAR* argv[])
{
	CoInitialize(NULL);

	//Прежде всего приложение инициализирует библиотеку COM, а затем создаёт экземпляр парсера MSXML :
	CComPtr<IXMLDOMDocument> spXMLDOM;
	HRESULT hr = spXMLDOM.CoCreateInstance(__uuidof(DOMDocument));

	if (FAILED(hr))
		throw "Unable to create XML parser object";
	if (spXMLDOM.p == NULL)
		throw "Unable to create XML parser object";



	//Если нам удалось создать экземпляр парсера, мы загружаем в него XML-документ:
	VARIANT_BOOL bSuccess = false;
	hr = spXMLDOM->load(CComVariant(L"xmldata.xml"), &bSuccess);

	if (FAILED(hr))
		throw "Unable to load XML document into the parser";
	if (!bSuccess)
		throw "Unable to load XML document into the parser";



	//Поиск узла осуществляется через объект документа, поэтому мы используем IXMLDOMDocument::selectSingleNode() для обнаружения нужного узла по его имени.
	CComBSTR bstrSS(L"xmldata/xmlnode");
	CComPtr<IXMLDOMNode> spXMLNode;
	hr = spXMLDOM->selectSingleNode(bstrSS, &spXMLNode);

	if (FAILED(hr))
		throw "Unable to locate 'xmlnode' XML node";
	if (spXMLNode.p == NULL)
		throw "Unable to locate 'xmlnode' XML node";



	//Результатом поиска станет объект узла MSXML, IXMLDOMNode. Узел должен существовать где-то в документе, иначе поиск закончится неудачей. Моё приложение использует его как родителя для совершенно нового узла, который создаётся объектом XML-документа:
	CComPtr<IXMLDOMNode> spXMLChildNode;
	hr = spXMLDOM->createNode(CComVariant(NODE_ELEMENT),
		CComBSTR("xmlchildnode"),
		NULL,
		&spXMLChildNode);

	if (FAILED(hr))
		throw "Unable to create 'xmlchildnode' XML node";
	if (spXMLChildNode.p == NULL)
		throw "Unable to create 'xmlchildnode' XML node";



	//Если парсеру удалось создать новый узел, следующий шаг - разместить его в дереве XML. Метод IXMLDOMNode::appendChild() - как раз то, что нам нужно.
	CComPtr<IXMLDOMNode> spInsertedNode;
	hr = spXMLNode->appendChild(spXMLChildNode, &spInsertedNode);

	if (FAILED(hr))
		throw "Unable to move 'xmlchildnode' XML node";
	if (spInsertedNode.p == NULL)
		throw "Unable to move 'xmlchildnode' XML node";


	
	//Итак, мы уже нашли требуемый узел и добавили к нему дочерний узел; теперь посмотрим, как работать с атрибутами. Представьте себе, что вам нужно добавить к новому дочернему узлу атрибут:
	//xml="fun"
	
	CComQIPtr<IXMLDOMElement> spXMLChildElement;
	spXMLChildElement = spInsertedNode;
	if (spXMLChildElement.p == NULL)
		throw "Unable to query for 'xmlchildnode' XML element interface";

	hr = spXMLChildElement->setAttribute(CComBSTR(L"xml"), CComVariant(L"fun"));
	if (FAILED(hr))
		throw "Unable to insert new attribute";




	//Для извлечение данных предназначен метод IXMLDOMNode::get_nodeTypedValue().
	//Тип данных задаётся с использованием атрибута dt : type, например :
	//<model dt:type="string">SL-2</model>
	//<year dt : type = "int">1992< / year>
	CComVariant varValue(VT_EMPTY);
	hr = spXMLNode->get_nodeTypedValue(&varValue);
	if (FAILED(hr))
		throw "Unable to retrieve 'xmltext' text";

	if (varValue.vt == VT_BSTR) {
		// Display the results... since we're not using the
		// wide version of the STL, we need to convert the
		// BSTR to ANSI text for display...

		USES_CONVERSION;
		LPTSTR lpstrMsg = W2T(varValue.bstrVal);
	}
	else {
		// Some error
		throw "Unable to retrieve 'xmltext' text";
	}




	//Наша последняя задача - сохранить обновлённое XML-дерево на диск, что мы и делаем, используя IXMLDOMDocument::save():
	hr = spXMLDOM->save(CComVariant("updatedxml.xml"));
	if (FAILED(hr))
		throw "Unable to save updated XML document";





	return 0;
}

