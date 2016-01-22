#include <iostream>
#include <fstream>
using namespace std;
struct node
{
	int x, y;
	struct node* next;
	node()
	{x=0; y=0;next=NULL;}
	node(int x1, int y1, node* next1)
	{
		x=x1; y=y1; next=next1;
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
	Queue(const Queue& B)
	{
		head=B.head;
		tail=B.tail;
	}
	virtual ~Queue()
	{
		while(!isEmpty())
		{
			PopFront();
		}
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
		node NODE= *head;
		if (head!=tail)
		{
			node* temp=head->next;
			delete head;
			head=temp;
		}
		else
		{
			delete head;
			head=tail=NULL;
		}
		return NODE;
	}
	bool isEmpty()
	{
		return head==NULL;
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
int labirint(node* vihod, char** matr, int xs, int ys, int m, int n);