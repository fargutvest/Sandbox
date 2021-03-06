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



const wchar_t *src=L""
  L"<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n"
  L"<root desc=\"Simple Prog\">\r\n"
  L"  <text>Hello World</text>\r\n"
  L"    <layouts>\r\n"
  L"    <lay pos=\"15\" bold=\"true\"/>\r\n"
  L"    <layoff pos=\"12\"/>\r\n"
  L"    <layin pos=\"17\"/>\r\n"
  L"  </layouts>\r\n"
  L"</root>\r\n";




// TElem -- a simple class to wrap up IXMLDomElement and iterat its children.
//   name()    - in <item>stuff</item> it returns "item"
//   val()     - in <item>stuff</item> it returns "stuff"
//   attr(s)   - in <item s=L"hello">stuff</item> it returns "hello"
//   subnode(b)- in <item><a>hello</a><b>there</b></item> it returns the TElem <b>there</b>
//   subval(b) - in <item><a>hello</a><b>there</b></item> it returns "there"
//   for (TElem c=e.begin(); c!=e.end(); c++) {...} - iterators over the subnodes
struct TElem
{ CComPtr<IXMLDOMElement> elem;
  CComPtr<IXMLDOMNodeList> nlist; int pos; long clen;
  //
  TElem() : elem(0), nlist(0), pos(-1), clen(0) {}
  TElem(int _clen) : elem(0), nlist(0), pos(-1), clen(_clen) {}
  TElem(CComPtr<IXMLDOMElement> _elem) : elem(_elem), nlist(0), pos(-1), clen(0) {get();}
  TElem(CComPtr<IXMLDOMNodeList> _nlist) : elem(0), nlist(_nlist), pos(0), clen(0) {get();}
  void get()
  { if (pos!=-1)
    { elem=0;
      CComPtr<IXMLDOMNode> inode;
      nlist->get_item(pos,&inode);
      if (inode==0) return;
      DOMNodeType type; inode->get_nodeType(&type);
      if (type!=NODE_ELEMENT) return;
      CComQIPtr<IXMLDOMElement> e(inode);
      elem=e;
    }
    clen=0; if (elem!=0)
    { CComPtr<IXMLDOMNodeList> iNodeList;
      elem->get_childNodes(&iNodeList);
      iNodeList->get_length(&clen);  
    }
  }
  //
  wstring name() const
  { if (!elem) return L"";
    CComBSTR bn; elem->get_tagName(&bn);
    return wstring(bn);
  }
  wstring attr(const wstring name) const
  { if (!elem) return L"";
    CComBSTR bname(name.c_str());
    CComVariant val(VT_EMPTY);
    elem->getAttribute(bname,&val);
    if (val.vt==VT_BSTR) return val.bstrVal;
    return L"";
  }
  bool attrBool(const wstring name,bool def) const
  { wstring a = attr(name);
    if (a==L"true" || a==L"TRUE") return true;
    else if (a==L"false" || a==L"FALSE") return false;
    else return def;
  }
  int attrInt(const wstring name, int def) const
  { wstring a = attr(name);
    int i, res=swscanf(a.c_str(),L"%i",&i);
    if (res==1) return i; else return def;
  }
  wstring val() const
  { if (!elem) return L"";
    CComVariant val(VT_EMPTY);
    elem->get_nodeTypedValue(&val);
    if (val.vt==VT_BSTR) return val.bstrVal;
    return L"";
  }
  TElem subnode(const wstring name) const
  { if (!elem) return TElem();
    for (TElem c=begin(); c!=end(); c++)
    { if (c.name()==name) return c;
    }
    return TElem();
  }
  wstring subval(const wstring name) const
  { if (!elem) return L"";
    TElem c=subnode(name);
    return c.val();
  }
  TElem begin() const
  { if (!elem) return TElem();
    CComPtr<IXMLDOMNodeList> iNodeList;
    elem->get_childNodes(&iNodeList);
    return TElem(iNodeList);
  }
  TElem end() const
  { return TElem(clen);
  }
  TElem operator++(int)
  { if (pos!=-1) {pos++; get();}
    return *this;
  }
  bool operator!=(const TElem &e) const
  { return pos!=e.clen;
  }
};




void test()
{
  CComPtr<IXMLDOMDocument> iXMLDoc;
  iXMLDoc.CoCreateInstance(__uuidof(DOMDocument));
 	
  // Following is a bugfix for PocketPC.
#ifdef _UNDER_CE
  gargle bargle
  iXMLDoc->put_async(VARIANT_FALSE);
  CComQIPtr<IObjectSafety,&IID_IObjectSafety> iSafety(iXMLDoc);
  if (iSafety) 
  { DWORD dwSupported, dwEnabled; 
    iSafety->GetInterfaceSafetyOptions(IID_IXMLDOMDocument,&dwSupported,&dwEnabled);
    iSafety->SetInterfaceSafetyOptions(IID_IXMLDOMDocument,dwSupported,0);
  }
#endif

  // Load the file. 
  VARIANT_BOOL bSuccess=false;
  // Can load it from a url/filename...
  //iXMLDoc->load(CComVariant(url),&bSuccess);
  // or from a BSTR...
  iXMLDoc->loadXML(CComBSTR(src),&bSuccess);

  // Get a pointer to the root
  CComPtr<IXMLDOMElement> iRootElm;
  iXMLDoc->get_documentElement(&iRootElm);

  // Thanks to the magic of CComPtr, we never need call
  // Release() -- that gets done automatically.


  TElem eroot(iRootElm);
  wstring desc = eroot.attr(L"desc");
  // returns "Simple Prog"

  TElem etext = eroot.subnode(L"text");
  wstring s = etext.val();
  // returns "Hello World"
  s = eroot.subval(L"text");
  // This is a shorter way to achieve the same thing

  TElem elays = eroot.subnode(L"layouts");
  for (TElem e=elays.begin(); e!=elays.end(); e++)
  { int pos = e.attrInt(L"pos",-1);
    bool bold = e.attrBool(L"bold",false);
    // we suggest default values, in case the attribute is missing
    wstring id = e.name();
    // returns "lay" or "layoff" or "layin"
  }
}

void MyTest()
{
	IXMLDOMDocument dpoc;

}



#ifdef UNDER_CE
int WINAPI WinMain(HINSTANCE,HINSTANCE,LPTSTR,int)
{ CoInitializeEx(NULL,COINIT_MULTITHREADED);
  test();
  CoUninitialize();
  return 0;
}
#else
int WINAPI WinMain(HINSTANCE,HINSTANCE,LPSTR,int)
{ CoInitialize(NULL);
  test();
  CoUninitialize();
  return 0;
}
#endif
