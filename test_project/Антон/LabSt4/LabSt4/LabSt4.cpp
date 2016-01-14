#include <stdio.h>
#include <math.h>
void main()
{ 
	int a=0, b=0, c=0;
	double d=0, x1=0 ,x2=0;
	printf (" for ax^2+bx+c=0 enter a,b,c\n");
	scanf("%d%d%d", &a, &b, &c);
	d=(b*b)-(4*a*c);
	if (d==0)
		printf("x=%d",-b/(2*a));
	else
		if (d>0)
		{
			x1=(sqrt(d)-b)/(2*a);
			x2=(-sqrt(d)-b)/(2*a);
			printf("x1=%f, x2=%f", x1, x2);
		}
		else
			printf("D<0");
}
