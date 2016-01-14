#include <stdio.h>
#include <math.h>
#include<stdlib.h>
void main()
{
	int m=0, n=0, j=0,b;
	double s=0, a=0;
	printf("m,n-?\n");
	scanf("%d%d", &m, &n);
	for (int i=m; i<=n; ++i)
	{
		s=0;
		j=i;
		a=1;
		b=10;
		while (i/b>0)
		{
			a++; //считаем сюда количество цифр числа
			b*=10;
		}
		while (j!=0)
		{
			s+=pow(j%10,a);
			j/=10;
		}
		if (s==i)
			printf("%d\n", i);
	}
	system("pause");
}