#include "stdafx.h"
#include <iostream>
using namespace std;
int f;
void main()
{
	setlocale(LC_ALL,"RUSSIAN");
	int a;
	int b;
	cout <<"����� ��� � ��� ���� �������� ����������� �����, ��������� �������� �������."<<endl<<endl;
	cout <<"����� ������ �����:  ";
cin >> a;
cout <<"����� ������ �����:  ";
cin >> b;
int mem1=a;
int mem2=b;
while(true)
{
	if (mem1>mem2)
	{
		mem1 = mem1-mem2;
	}
	if (mem2>mem1)
	{
        mem2= mem2-mem1;
	}
	if (mem1==mem2)
	{
		cout<<mem1<<" "<<mem2<<endl;
		system("PAUSE"); 
		return;
	}

}

system("PAUSE"); 
}

