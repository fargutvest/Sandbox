#include "header.h"
int labirint (node* vihod, char** matr, int xs, int ys, int m, int n)
{
	node hod [4];
	hod[3].x = 0;
	hod[3].y = 1;
	hod[2].x = 0;
	hod[2].y = -1;
	hod[1].x = -1;
	hod[1].y = 0;
	hod[0].x = 1;
	hod[0].y = 0;
	
	bool flag = false;
	Queue Q;
	node N;
	Q.PushBack(xs, ys);
	matr[ys][xs] = '4';
	while (! Q.isEmpty())
	{
		N = Q.PopFront();
		if (N.x == 0 || N.x == m - 1 || N.y == 0 || N.y == n - 1) //проверка на границу
		{
			flag = true;
			break;
		}
		for (int i = 0; i < 4; i++)
		{
			int yn, xn;
			yn = N.y + hod[i].y;
			xn = N.x + hod[i].x;
			if (matr[yn][xn] == ' ')
			{
				Q.PushBack(xn, yn);
				matr[yn][xn] = i + '0';
			}
		}
	}
	cout << endl;
	if (!flag)
	{
		cout << "There is no exit";
		return 0;
	}
	int j = 0; //количество ходов
	vihod[j] = N;
	while (matr[N.y][N.x] != '4') //возвращаемся назад
	{
		char c = matr[N.y][N.x];
		int q = c - '0';
		N.y -= hod[q].y;
		N.x -= hod[q].x;
		vihod[++j] = N;
	}
	return j;
}