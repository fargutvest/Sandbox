#define DLLEXPORT extern "C" __declspec(dllexport)

class MyClass
{
public:
	MyClass();
	MyClass(int param);
	~MyClass();
	int MyMethod();
};

int i = 34;



MyClass::MyClass()
{

}