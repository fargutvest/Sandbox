
#include "stdafx.h"
#include <iostream>

using namespace std;

int main()
{
	setlocale(LC_ALL,"RUSSIAN");
	int a;
	cout<<"����������, �������� �� �������� ����������� ����� �������"<<endl<<endl;

loop1:
	cout<<"������� �����:  ";
cin>>a;
int t=2;
bool p=true;
while(true)
{
//	cout<<"t="<<t<<"; "<<"a%t= "<<a%t<<" "; 
	if (a%t==0) p = false;
	t++;
	if (t==a)
	{
		if (p==true) cout<<a<<" ������� �����"<<endl;
        if (p==false) cout<<a<<" ��������� �����"<<endl;

		goto loop1;
	}
}


	return 0;
}

