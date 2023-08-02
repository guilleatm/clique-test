using Clique;
using System.Diagnostics;

// N-QUEENS GRAPH

Stopwatch stopwatch = new Stopwatch(); 
stopwatch.Start();
 
const int SIZE = 4;
Graph g = CreateNQGraph(SIZE);

stopwatch.Stop();
Console.Write( g.ToString() );

Console.WriteLine($"GRAPH CREATED in {stopwatch.ElapsedMilliseconds} ms");


// N-QUEENS ANTI-GRAPH

stopwatch.Restart();

Graph ag = g.CreateAntiGraph();

stopwatch.Stop();

Console.WriteLine( ag.ToString() );


Console.WriteLine($"ANTI-GRAPH CREATED in {stopwatch.ElapsedMilliseconds} ms");

int max = 0;

for (int i = 0; i < ag.nodes.Length; i++)
{

	Graph a = ag.CreateSubGraphOf(i);
	Graph b = a.CreateAntiGraph();

	Console.WriteLine( a.ToString() );
	Console.WriteLine();
	Console.WriteLine( b.ToString() );

	int[][] t = b.GetSubGraphs();

	Console.WriteLine(i + ": " + t.Length);
	max = Math.Max(max, t.Length);

	// foreach (var _t in t)
	// {
	// 	PrintV(_t);
	// }

	// break;
}

Console.WriteLine(max);









Graph CreateNQGraph(int size)
{
	Graph g = new Graph(size * size);

	for (int row = 0; row < size; row++)
	{
		for (int col = 0; col < size; col++)
		{
			ConnectBoard(g, size, row, col);
		}
	}

	return g;
}

void ConnectBoard(Graph g, int size, int i, int j)
{
	int index = i * size + j;
	int otherIndex;

	// ROW
	for (int col = 0; col < size; col++)
	{
		if (col == j) continue;
		otherIndex = i * size + col;
		g.Connect(index, otherIndex);
	}

	// COLUMN
	for (int row = 0; row < size; row++)
	{
		if (row == i) continue;
		otherIndex = row * size + j;
		g.Connect(index, otherIndex);
	}

	// DIAGONAL 1

	int diagonal = j - i;

	if (diagonal > 0)
	{
		for (int col = diagonal, row = 0; col < size; col++, row++)
		{
			if (col == j) continue;
			otherIndex = row * size + col;
			g.Connect(index, otherIndex);
		}
	}
	else
	{
		for (int row = -diagonal, col = 0; row < size; row++, col++)
		{
			if (row == i) continue;
			otherIndex = row * size + col;
			g.Connect(index, otherIndex);
		}
	}

	// DIAGONAL 2

	diagonal = (size - 1 - j) - i;

	if (diagonal >= 0)
	{
		for (int col = size - diagonal - 1, row = 0; col >= 0; col--, row++)
		{
			if (col == j) continue;
			otherIndex = row * size + col;
			g.Connect(index, otherIndex);
		}
	}
	else
	{
		for (int row = -diagonal, col = size - 1; row < size; row++, col--)
		{
			if (row == i) continue;
			otherIndex = row * size + col;
			g.Connect(index, otherIndex);
		}
	}
}

void PrintV( int[] v)
{
	for (int i = 0; i < v.Length; i++)
	{
		Console.Write($"{v[i]} ");
	}
	Console.WriteLine();
}

// bool check(vector<int>& sol) { // O(n^2)
// 	for (int i = 0; i < sol.size(); i++) {
// 		for (int j = 0; j < sol.size(); j++) {
// 			if (i != j) {
// 				if (i == j || sol[i] == sol[j]) { // Mateixa fila o columna
// 					return false;
// 				} else if (i - sol[i] == j - sol[j] || i + sol[i] == j + sol[j]) {
// 					return false;
// 				}
// 			}
// 		}
// 	}
// 	return true;
// }