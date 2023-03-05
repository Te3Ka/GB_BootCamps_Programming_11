const int N = 10;
const int MIN = 0;
const int MAX = 100;

//Печать массива в консоль
void PrintArrayToConsole(int[,] printArray)
{

    for (int i = 0; i < printArray.GetLength(0); i++)
    {
    	Console.Write("[");
    	for(int j = 0; j < printArray.GetLength(1); j++)
    	{
    		if (j == printArray.GetLength(1) - 1)
        	Console.Write($"{printArray[i, j], 2}");
        else
        	Console.Write($"{printArray[i, j], 2}, ");
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
	if (matrFirst.GetLength(1) != matrSecond.GetLength(0) throw new Exception("Нельзя умножать такие матрицы");

}

int[,] serialMatrixResult = new int[N, N];
int[,] threadMatrixResult = new int[N, N];
int[,] firstMatrix = GenerateMatrix(N, N);
int[,] secondMatrix = GenerateMatrix(N, N);

Console.WriteLine("Первая матрица:");
PrintArrayToConsole(firstMatrix);
Console.WriteLine();
Console.WriteLine("Вторая матрица:");
PrintArrayToConsole(secondMatrix);
Console.WriteLine();