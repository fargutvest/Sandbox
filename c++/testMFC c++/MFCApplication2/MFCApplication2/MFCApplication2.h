
// MFCApplication2.h : ������� ���� ��������� ��� ���������� PROJECT_NAME
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�������� stdafx.h �� ��������� ����� ����� � PCH"
#endif

#include "resource.h"		// �������� �������


// CMFCApplication2App:
// � ���������� ������� ������ ��. MFCApplication2.cpp
//

class CMFCApplication2App : public CWinApp
{
public:
	CMFCApplication2App();

// ���������������
public:
	virtual BOOL InitInstance();

// ����������

	DECLARE_MESSAGE_MAP()
};

extern CMFCApplication2App theApp;