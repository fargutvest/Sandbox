
#include <stdio.h>
#include <math.h>
#include<stdlib.h>
int prost (int a)
{
	int i=0;
	double n1=0, q=0;
	bool b=true;
	n1=a;
	q=sqrt(n1);
	for (i=2; i<=q; ++i)
		if (a%i==0)
		{
			b=false;
			break;
		}
   if (b)
	   return a;
   else
	return i;
}
void main()
{
	int n=0;
	printf("n-?\n");
	scanf("%d", &n);
	printf("1\n");
	while (n!=1)
	{
		int r=prost(n);
		printf("%d\n", r);
		n/=r;
	}
	system("PAUSE");
}