#include "stdafx.h"
#include <stdlib.h>
#include <string.h>
#include <iostream>
#include <stdio.h>
#include <cmath>

using namespace std;
int f;
void main()
{
	setlocale(LC_ALL,"RUSSIAN");
	int a;
	int b;
	cout <<"������� �� ������� ��� ����� ����������, ������� ��������� � �������� ��������� ����������� �����."<<endl<<endl;
	cout <<"����� ������ ������� ���������:  ";
cin >> a;
cout <<"����� ������� ������� ���������:  ";
cin >> b;

int ff;
char digs[20];
int i=a;
for (; i<b;i++)
{
	int yyy = i;
for (int razradov=1;yyy>0;razradov++)
{
	yyy/=10;
	ff=razradov;
}

sprintf(digs,"%d",i);
int ghg=0;
for (int igg=0;igg<ff;igg++)
{
	double ffd = ff;
	double ll = digs[igg];
	ghg = ghg + pow(ll,ffd);
}


}




cout<<"� ������ ����� "<<ff<<" ����\n\n";
cin.get();
cin.get();    
 

  /*

   if (strlen(digs) > idig)
    printf ("%d-th digit in %d is '%c'\n",idig,num,digs[idig-1]);
  else
    printf ("there is no %d-th digit in %d\n",idig,num);


*/




system("PAUSE"); 
}


