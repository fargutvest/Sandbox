#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>

int main()
{
	loop1:
	int m=0, n=0, m1=0, n1=0;
	printf("n,m-?\n");
	scanf( "%d%d", &n, &m);
	n1=n;
	m1=m;
	while ((m!=0)&&(n!=0))
		if (n>m)
			n=n%m;
		else
			m=m%n;
	printf("for %d %d\n",n1 ,m1);
	printf("NOD is %d\n", n+m);
	printf("NOK is %d\n", (m1*n1)/(n+m));
system("pause");
goto loop1;
}