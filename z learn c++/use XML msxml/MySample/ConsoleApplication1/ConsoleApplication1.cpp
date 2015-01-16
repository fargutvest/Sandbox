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



//�� ���������� ���������� ����� ��������� XML - �������� ��� ��������� xmldata.xml

int _tmain(int argc, _TCHAR* argv[])
{
	CoInitialize(NULL);

	//������ ����� ���������� �������������� ���������� COM, � ����� ������ ��������� ������� MSXML :
	CComPtr<IXMLDOMDocument> spXMLDOM;
	HRESULT hr = spXMLDOM.CoCreateInstance(__uuidof(DOMDocument));

	if (FAILED(hr))
		throw "Unable to create XML parser object";
	if (spXMLDOM.p == NULL)
		throw "Unable to create XML parser object";



	//���� ��� ������� ������� ��������� �������, �� ��������� � ���� XML-��������:
	VARIANT_BOOL bSuccess = false;
	hr = spXMLDOM->load(CComVariant(L"xmldata.xml"), &bSuccess);

	if (FAILED(hr))
		throw "Unable to load XML document into the parser";
	if (!bSuccess)
		throw "Unable to load XML document into the parser";



	//����� ���� �������������� ����� ������ ���������, ������� �� ���������� IXMLDOMDocument::selectSingleNode() ��� ����������� ������� ���� �� ��� �����.
	CComBSTR bstrSS(L"xmldata/xmlnode");
	CComPtr<IXMLDOMNode> spXMLNode;
	hr = spXMLDOM->selectSingleNode(bstrSS, &spXMLNode);

	if (FAILED(hr))
		throw "Unable to locate 'xmlnode' XML node";
	if (spXMLNode.p == NULL)
		throw "Unable to locate 'xmlnode' XML node";



	//����������� ������ ������ ������ ���� MSXML, IXMLDOMNode. ���� ������ ������������ ���-�� � ���������, ����� ����� ���������� ��������. �� ���������� ���������� ��� ��� �������� ��� ���������� ������ ����, ������� �������� �������� XML-���������:
	CComPtr<IXMLDOMNode> spXMLChildNode;
	hr = spXMLDOM->createNode(CComVariant(NODE_ELEMENT),
		CComBSTR("xmlchildnode"),
		NULL,
		&spXMLChildNode);

	if (FAILED(hr))
		throw "Unable to create 'xmlchildnode' XML node";
	if (spXMLChildNode.p == NULL)
		throw "Unable to create 'xmlchildnode' XML node";



	//���� ������� ������� ������� ����� ����, ��������� ��� - ���������� ��� � ������ XML. ����� IXMLDOMNode::appendChild() - ��� ��� ��, ��� ��� �����.
	CComPtr<IXMLDOMNode> spInsertedNode;
	hr = spXMLNode->appendChild(spXMLChildNode, &spInsertedNode);

	if (FAILED(hr))
		throw "Unable to move 'xmlchildnode' XML node";
	if (spInsertedNode.p == NULL)
		throw "Unable to move 'xmlchildnode' XML node";


	
	//����, �� ��� ����� ��������� ���� � �������� � ���� �������� ����; ������ ���������, ��� �������� � ����������. ����������� ����, ��� ��� ����� �������� � ������ ��������� ���� �������:
	//xml="fun"
	
	CComQIPtr<IXMLDOMElement> spXMLChildElement;
	spXMLChildElement = spInsertedNode;
	if (spXMLChildElement.p == NULL)
		throw "Unable to query for 'xmlchildnode' XML element interface";

	hr = spXMLChildElement->setAttribute(CComBSTR(L"xml"), CComVariant(L"fun"));
	if (FAILED(hr))
		throw "Unable to insert new attribute";




	//��� ���������� ������ ������������ ����� IXMLDOMNode::get_nodeTypedValue().
	//��� ������ ������� � �������������� �������� dt : type, �������� :
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




	//���� ��������� ������ - ��������� ���������� XML-������ �� ����, ��� �� � ������, ��������� IXMLDOMDocument::save():
	hr = spXMLDOM->save(CComVariant("updatedxml.xml"));
	if (FAILED(hr))
		throw "Unable to save updated XML document";





	return 0;
}

