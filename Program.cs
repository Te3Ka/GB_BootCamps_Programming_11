const int N = 1000;
const int MIN = -100;
const int MAX = 100;
const int THREADS_NUMBER = 8;

int[,] serialMatrixResult = new int[N, N];
int[,] threadMatrixResult = new int[N, N];

//Печать массива в консоль
void PrintArrayToConsole(int[,] printArray)
{

    for (int i = 0; i < printArray.GetLength(0); i++)
    {
        Console.Write("[");
        for (int j = 0; j < printArray.GetLength(1); j++)
        {
            if (j == printArray.GetLength(1) - 1)
                Console.Write($"{printArray[i, j],10}");
            else
                Console.Write($"{printArray[i, j],10}, ");
        }
        Console.WriteLine("]");
    }
}

//Создание случайной матрицы
int[,] GenerateMatrix(int rows, int columns)
{
    Random rand = new Random();
    int[,] resultMatrix = new int[rows, columns];
    for (int i = 0; i < resultMatrix.GetLength(0); i++)
    {
        for (int j = 0; j < resultMatrix.GetLength(1); j++)
        {
            resultMatrix[i, j] = rand.Next(MIN, MAX);
        }
    }
    return resultMatrix;
}

//Линейное умножение матрицы
void SerialMatrixPow(int[,] matrFirst, int[,] matrSecond)
{
    if (matrFirst.GetLength(1) != matrSecond.GetLength(0)) throw new Exception("Нельзя умножать такие матрицы");
    for (int i = 0; i < matrFirst.GetLength(0); i++)
    {
        for (int j = 0; j < matrSecond.GetLength(1); j++)
        {
            for (int k = 0; k < matrSecond.GetLength(0); k++)
            {
                serialMatrixResult[i, j] += matrFirst[i, k] * matrSecond[k, j];
            }
        }
    }
}

//Подготовка к разделению на потоки
void PrepareParallelMatrixPow(int[,] matrFirst, int[,] matrSecond)
{
    if (matrFirst.GetLength(1) != matrSecond.GetLength(0)) throw new Exception("Нельзя умножать такие матрицы");
    int eachThreadCalc = N / THREADS_NUMBER;
    var threadsList = new List<Thread>();
    for (int i = 0; i < THREADS_NUMBER; i++)
    {
        int startPos = i * eachThreadCalc;
        int endPos = (i + 1) * eachThreadCalc;
        if (i == THREADS_NUMBER - 1) endPos = N;
        threadsList.Add(new Thread(() => ParallelMatrixPow(matrFirst, matrSecond, startPos, endPos)));
        threadsList[i].Start();
    }
    for (int i = 0; i < THREADS_NUMBER; i++)
    {
        threadsList[i].Join();
    }
}

//Параллельное умножение матриц
void ParallelMatrixPow(int[,] matrFirst, int[,] matrSecond, int startPos, int endPos)
{
    for (int i = startPos; i < endPos; i++)
    {
        for (int j = 0; j < matrSecond.GetLength(1); j++)
        {
            for (int k = 0; k < matrSecond.GetLength(0); k++)
            {
                threadMatrixResult[i, j] += matrFirst[i, k] * matrSecond[k, j];
            }
        }
    }
}

//Сравнение двух матриц на тождественность
bool EqualityMatrix(int[,] matrFirst, int[,] matrSecond)
{
    bool res = true;

    for (int i = 0; i < matrFirst.GetLength(0); i++)
    {
        for (int j = 0; j < matrFirst.GetLength(1); j++)
        {
            res = res && (matrFirst[i, j] == matrSecond[i, j]);
        }
    }

    return res;
}

int[,] firstMatrix = GenerateMatrix(N, N);
Console.WriteLine("Первая матрица готова");
int[,] secondMatrix = GenerateMatrix(N, N);
Console.WriteLine("Вторая матрица готова");
SerialMatrixPow(firstMatrix, secondMatrix);
Console.WriteLine("Перемноженная матрица одним потоком готова");
PrepareParallelMatrixPow(firstMatrix, secondMatrix);
Console.WriteLine("Перемноженная матрица несколькими потоками готова");
Console.WriteLine(EqualityMatrix(serialMatrixResult, threadMatrixResult));