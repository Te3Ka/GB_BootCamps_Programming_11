const int N = 5_000;
const int MIN = -100;
const int MAX = 100;

int[,] serialMatrixResult = new int[N, N];
int[,] threadMatrixResult = new int[N, N];

//Печать массива в консоль
void PrintArrayToConsole(int[,] printArray)
{

    for (int i = 0; i < printArray.GetLength(0); i++)
    {
    	Console.Write("[");
    	for(int j = 0; j < printArray.GetLength(1); j++)
    	{
    		if (j == printArray.GetLength(1) - 1)
        	Console.Write($"{printArray[i, j], 10}");
        else
        	Console.Write($"{printArray[i, j], 10}, ");
    	}
    	Console.WriteLine("]");
    }
}

//Создание случайной матрицы
int[,] GenerateMatrix(int rows, int columns)
{
	Random rand = new Random();
	int[,] resultMatrix = new int[rows, columns];
	for(int i = 0; i < resultMatrix.GetLength(0); i++)
	{
		for(int j = 0; j < resultMatrix.GetLength(1); j++)
		{
			resultMatrix[i, j] = rand.Next(MIN, MAX);
		}
	}
	return resultMatrix;
}

void serialMatrixPow(int[,] matrFirst, int[,] matrSecond)
{
	if (matrFirst.GetLength(1) != matrSecond.GetLength(0)) throw new Exception("Нельзя умножать такие матрицы");
		for(int i = 0; i < matrFirst.GetLength(0); i++)
		{
			for(int j = 0; j < matrSecond.GetLength(1); j++)
			{
				for(int k = 0; k < matrSecond.GetLength(0); k++)
				{
					serialMatrixResult[i, j] += matrFirst[i,k] * matrSecond[k, j];
				}
			}
		}
}


int[,] firstMatrix = GenerateMatrix(N, N);
Console.WriteLine("Первая матрица готова");
int[,] secondMatrix = GenerateMatrix(N, N);
Console.WriteLine("Вторая матрица готова");
serialMatrixPow(firstMatrix, secondMatrix);
Console.WriteLine("Перемноженная матрица одним потоком готова");
