
// MFCApplication1.h : ������� ���� ��������� ��� ���������� MFCApplication1
//
#pragma once

#ifndef __AFXWIN_H__
	#error "�������� stdafx.h �� ��������� ����� ����� � PCH"
#endif

#include "resource.h"       // �������� �������


// CMFCApplication1App:
// � ���������� ������� ������ ��. MFCApplication1.cpp
//

class CMFCApplication1App : public CWinAppEx
{
public:
	CMFCApplication1App();


// ���������������
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// ����������
	UINT  m_nAppLook;
	BOOL  m_bHiColorIcons;

	virtual void PreLoadState();
	virtual void LoadCustomState();
	virtual void SaveCustomState();

	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CMFCApplication1App theApp;
