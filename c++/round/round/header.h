#include <windows.h>
#include <fstream>
#include <math.h>
#include <stdlib.h>
#include <string.h>
using namespace std;
struct node
{
	int x, y;
	node* next;
	node()
	{x=0; y=0; next=NULL;}
	node(int x1, int y1, node* w)
	{
		x=x1; y=y1; next=w;
	}
	virtual ~node()
	{
	}
};
class Queue
{
	node* head;
	node* tail;
public:
	Queue()
	{
		head=NULL;
		tail=NULL;
	}
	bool isEmpty()
	{
		if(head==NULL)
			return true;
		return false;
	}
	void PushBack(int a, int b)
	{
		struct node* temp=new node(a, b, NULL);
		if (!isEmpty())
		{
			tail->next=temp;
			tail=tail->next;
		}
		else
		{
			tail=temp;
			head=temp;
		}
	}
	node PopFront()
	{
		if (head!=tail)
		{
			node tmp=*head;
			node* temp=head->next;
			delete head;
			head=temp;
			return tmp;
		}
		else
		{
			node temp = *head;
			delete head;
			head=tail=NULL;
			return temp;
		}
	}
	virtual ~Queue()
	{
		while(!isEmpty())
		{
			PopFront();
		}
	}
	Queue& operator=(Queue& a)
	{
		while(!isEmpty())
		{
			PopFront();
		}
		node* temp=a.head;
		while (temp!=NULL)
		{
			PushBack(temp->x, temp->y);
			temp=temp->next;
		}
		return *this;
	}
	void print(ostream& out)
	{
		if (isEmpty())
			out << "queue is empty";
		else
		{
			node* temp=head;
			while(temp!=NULL)
			{
				out << temp->x << " " << temp->y << endl;
				temp=temp->next;
			}
		}
	}
};

class Graph
{
	int m,n;
	Queue* spisk;
public:
	Graph(const Graph& G)
	{
		n=G.n;
		m=G.m;
		for (int i=0; i<n; i++)
			spisk[i]=G.spisk[i];
	}
	Graph(int n1=1, int m1=1)
	{
			n=n1;
			m=m1;
			spisk = new Queue[n];
	}
	void input (istream& in)
	{
		in >> n >> m; // кол-во вершин и ребер
		delete [] spisk;
		spisk = new Queue[n];
		int num, x1;
		for (int i=0; i<m; i++)
		{
			in >> num >> x1;
			spisk[num].PushBack(x1, 0);
			spisk[x1].PushBack(num, 0);
		}
	}
	virtual ~Graph() 
	{delete [] spisk;}
	Queue& get_queue(int index)
	{
		return spisk[index];
	}
	Graph& operator= (Graph& G)
	{
		delete[] spisk;
		n=G.n;
		m=G.m;
		spisk=new Queue[n];
		for (int i=0; i<n; i++)
			spisk[i]=G.spisk[i];
		return *this;
	}
	void cost(node* koord1)
	{
		for (int i=0; i<n; i++)
		{
			Queue q1;
			q1=spisk[i];
			while (!spisk[i].isEmpty())
				spisk[i].PopFront();
			while (!q1.isEmpty())
			{
				int p1=q1.PopFront().x;
				int rast=sqrt((double)((koord1[i].x-koord1[p1].x)*(koord1[i].x-koord1[p1].x)+(koord1[i].y-koord1[p1].y)*(koord1[i].y-koord1[p1].y))); // расстояние
				spisk[i].PushBack(p1, rast);
			}
		}
	}
	int algoritm_Deijkstra (int* a, int* kol,int start, int fin)
	{
		*kol=0;
		bool *flags=new bool [n];
		for (int i=0; i<n; i++)
			flags[i]=false;
		node* mas=new node [n];
		for (int i=0; i<n; i++)
		{
			mas[i].x=2146000000;
			mas[i].y=-1;
		}
		mas[start].x=0; mas[start].y=-1;
		node Min(0, 0, NULL), big(2147000001, 2147000001, NULL);
		for (int j=0; j<n; j++)
		{
			Min=big;
			// находим минимальный
			for(int i=0; i<n; i++)
				if (!flags[i])
					if (mas[i].x < Min.x)
					{
						Min.x=mas[i].x;
						Min.y=i;
					}
			int k=Min.y;
			flags[k]=true; // помечаем вершину
			// релаксация
			while(!spisk[k].isEmpty())
			{
				node z=spisk[k].PopFront();
				int v=z.x;
				if (mas[v].x > mas[k].x+z.y)
				{
					mas[v].x=mas[k].x+z.y;
					mas[v].y=k;
				}
			}
		}
		delete [] flags;
		int p=fin;

		while(mas[p].y!=-1)
		{
			(*kol)++;
			p=mas[p].y;
		}
		(*kol)++;
		int p1=fin;
		if (p==start)
			for (int i=0; i<*kol; i++)
			{
					a[i]=fin;
					fin=mas[fin].y;
			}
			return mas[p1].x;
	}
	/*int algoritm_Deijkstra (int* a, int* kol,int start, int fin)
	{
		*kol=0;
		Graph temp;
		temp=*this;
		bool *flags=new bool [n];
		for (int i=0; i<n; i++)
			flags[i]=false;
		node* mas=new node [n];
		for (int i=0; i<n; i++)
		{
			mas[i].x=2146000000;
			mas[i].y=-1;
		}
		mas[start].x=0; mas[start].y=-1;
		node Min(0, 0, NULL), big(2147000001, 2147000001, NULL);
		for (int j=0; j<n; j++)
		{
			Min=big;
			// находим минимальный
			for(int i=0; i<n; i++)
				if (!flags[i])
					if (mas[i].x < Min.x)
					{
						Min.x=mas[i].x;
						Min.y=i;
					}
			int k=Min.y;
			flags[k]=true; // помечаем вершину
			// релаксация
			while(!temp.spisk[k].isEmpty())
			{
				node z=temp.spisk[k].PopFront();
				int v=z.x;
				if (mas[v].x > mas[k].x+z.y)
				{
					mas[v].x=mas[k].x+z.y;
					mas[v].y=k;
				}
			}
		}
		delete [] flags;
		int p=fin;

		while(mas[p].y!=-1)
		{
			(*kol)++;
			p=mas[p].y;
		}
		(*kol)++;
		int p1=fin;
		if (p==start)
			for (int i=0; i<*kol; i++)
			{
					a[i]=fin;
					fin=mas[fin].y;
			}
			return mas[p1].x;
	}*/
	int kol_reb()
	{
		return m;
	}
	int kol_ver()
	{
		return n;
	}
	Queue ret_queue(int q) 
	{
		return spisk[q];
	}
};