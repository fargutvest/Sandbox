#include <stdio.h> 
#include <time.h> 
#include <stdlib.h>
#include <iostream>
	  
	int main () 
	{ 
	setlocale(LC_ALL,"RUSSIAN");
	  time_t rawtime; 
	  struct tm * timeinfo; 
	  
	  time ( &rawtime ); 
	  timeinfo = localtime ( &rawtime ); 
	  printf ( "Текущее время и дата: %s", asctime (timeinfo) ); 
	  system("pause");   
	  return 0; 
	  
}