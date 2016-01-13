#include"stdafx.h"
#include "Singleton.h"

Singleton Singleton::instance;

Singleton &Singleton::Instance()
{
	return instance;
}
Singleton::Singleton() {}
Singleton::~Singleton() {}