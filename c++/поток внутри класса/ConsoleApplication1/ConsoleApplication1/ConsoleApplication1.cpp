
#include "stdafx.h"
//#include <winnt.h>
#include <process.h>
#include <assert.h>
#include <Windows.h>
#include <iostream>


#pragma region http://rsdn.ru/forum/info/FAQ.cpp.threadandmember
template<class T, DWORD(T::*mem_fun)()>

inline DWORD WINAPI thread_to_member_thunk(void* p)
{
	return (static_cast<T*>(p)->*mem_fun)();
}

//����������:

struct some
{

	DWORD f()
	{
		return 0;
	}
};

int a()
{
	some s; // � �������� ��������� (lpParam) �������� ��������� �� ������, ����� ���������� �������� ����� ������ � ���� ������ 
	CreateThread(0, 0, thread_to_member_thunk<some, &some::f>, &s, 0, 0);
	return 0;
}

#pragma endregion

#pragma region http://forum.codenet.ru/q8622/%D0%9F%D0%BE%D1%82%D0%BE%D0%BA+%D0%BA%D0%B0%D0%BA+%D1%87%D0%BB%D0%B5%D0%BD+%D0%BA%D0%BB%D0%B0%D1%81%D1%81%D0%B0



// ����������
class A
{
public:
	//A() : m_hThread(NULL), m_A(0), m_B(0) {}
	virtual ~A();
	void RunThread(void);
	static unsigned int __stdcall Thread(void*);
protected:
	virtual unsigned int CoolThread(void);
	virtual bool MyImpl(void) { return true; }
	// ������ ������
protected:
	HANDLE m_hThread;
	int m_A;
	int m_B;
	// � �.�.
};

// �����������
unsigned int __stdcall A::Thread(void* p)
{
	// ����� ������ ��������������� cast'��
	// ����� ����� ����������.
	return ((A*)p)->CoolThread();
}

unsigned int A::CoolThread()
{
	// �������� � ����������� � �������� ������,
	// ��������:
	m_A = 2;
	m_B = 1;
	MyImpl();
	// ������ ��������  
	return 0;
}

void A::RunThread()
{
	unsigned int res = 0;
	// �����: ������� �� ������� CreateThread, ������
	// ����� ��������� _beginthreadex
	m_hThread = (HANDLE)_beginthreadex(NULL, 0, Thread, this, 0, &res);
	if (!m_hThread)
		assert(0);
}

A::~A()
{
	if (m_hThread)
	{
		// ����������� ������
		DWORD res = 0;
		res = ::WaitForSingleObject(m_hThread, 1000);
		switch (res)
		{
		case WAIT_OBJECT_0:
			break;
		case WAIT_TIMEOUT:
			::TerminateThread(m_hThread, -1);
			break;
		default:
			assert(0);
		}
		::CloseHandle(m_hThread);
	}
}

#pragma endregion

int _tmain(int argc, _TCHAR* argv[])
{
	// ������  http://forum.codenet.ru

	A a;
	a.RunThread();
	return 0;
}

