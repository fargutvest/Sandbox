#include <stdio.h>
#include <math.h>
#include <stdlib.h>
void main ()
{loop1:
	int n=0, i=0;
	double n1=0, q=0;
	bool b=true;
	printf("n-?\n");
	scanf("%d", &n);
	n1=n;
	q=sqrt(n1);
	for (i=2; i<=q; ++i)
		if (n%i==0)
		{
			b=false;
			break;
		}
    if (!b)
		printf("Ne Prostoe\n");
	else
		printf("Prostoe\n");
	system("pause");
	goto loop1;
}
