#include "Windows.h"
#define DLLEXPORT extern "C" __declspec(dllexport)

DLLEXPORT void TestPost(int id)
{
	PostMessage(HWND(id), 0x57, WM_KEYDOWN, 0);
}