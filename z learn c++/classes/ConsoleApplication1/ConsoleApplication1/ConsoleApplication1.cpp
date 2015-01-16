// ConsoleApplication1.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <iostream>
#include <stdio.h>
using namespace std;

class A
{

private:
	int g;
	int j;

public:
	int x;
	int y;
	int f;
	A()
	{
		x = 5;
		y = 2;
	}
	A(int x_, int y_)
	{
		this->x = x_;
		this->y = y_;
	}
	int sum()
	{
		return x + y;
	}

};

class B
{
	int b;
public:
	B operator= (B source)
	{
		this->b = source.b;
		return *this;
	}
	friend B operator+ (B b1, B b2)
	{
		B res;
		res.b = b1.b + b2.b;
		return res;
	}
	B()
	{
		b = 9;
	}

	void print()
	{
		cout << b << endl;
	}

	virtual ~B() //virtual потому что так принято 
	{}
};

int _tmain(int argc, _TCHAR* argv[])
{
	B one;
	B two;
	one.print();
	two.print();
	B three;
	three = one + two;
	three.print();

	A *_a;
	_a = new A();
	_a->x = 34;
	

	A a;
	a.x = 65;



	return 0;
}
