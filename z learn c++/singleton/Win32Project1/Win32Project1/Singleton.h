class Singleton
{
public:
	int field = 123;
	static Singleton& Instance();
	

private:
	static Singleton instance;
	Singleton();
	~Singleton();
};

