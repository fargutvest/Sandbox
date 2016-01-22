#include "stdafx.h"
#include <iostream>
using namespace std;
int f;
void main()
{
	setlocale(LC_ALL,"RUSSIAN");
	int a;
	int b;
	cout <<"Найти НОД и НОК двух заданных натуральных чисел, используя алгоритм Евклида."<<endl<<endl;
	cout <<"Введи первое число:  ";
cin >> a;
cout <<"Введи второе число:  ";
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

