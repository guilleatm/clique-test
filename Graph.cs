using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Clique;
   
class Graph
{
	public Node[] nodes { get; private set; }

	public Graph(int numNodes, bool createNodes = true, bool disableNodes = false)
	{
		nodes = new Node[numNodes];

		if (createNodes)
		{
			for (int i = 0; i < numNodes; i++)
			{
				nodes[i] = new Node(i);

				if (disableNodes)
				{
					nodes[i].Disable();
				}
			}
		}
	}

	public void Connect(int i, int j)
	{
		if (!nodes[i].enabled || !nodes[j].enabled) return; 
		if (nodes[i].Connected(j)) return;

		nodes[i].Connect(j);
		nodes[j].Connect(i);
	}

	public Graph CreateAntiGraph()
	{
		int size = nodes.Count();
		Graph ag = new Graph(size, createNodes: false);

		for (int i = 0; i < size; i++)
		{			
			ag.nodes[i] = nodes[i].CreateAntiNode(size);
		}

		return ag;
	}

	public Graph CreateSubGraphOf(int index)
	{
		List<int> connections = nodes[index].connections;

		Graph sg = new Graph( nodes.Length , disableNodes: true);
	
		for (int i = 0; i < connections.Count(); i++)
		{
			Node subNode = nodes[connections[i]];
			sg.nodes[subNode.index].Enable();

			sg.Connect(index, subNode.index);


			int IX = 0;
			int subIX = 0;

			while( IX < connections.Count() && subIX < subNode.connections.Count() )
			{
				if (connections[IX] == subNode.connections[subIX])
				{
					sg.Connect(subNode.index, connections[IX]);

					IX += 1;
					subIX += 1;
				}
				else if (connections[IX] > subNode.connections[subIX])
				{
					subIX += 1;
				}
				else // connections[IX] < subNode.connections[subIX]
				{
					IX += 1;
				}
			}
		}

		return sg;
	}

	public override string ToString()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();

		for (int i = 0; i < nodes.Length; i++)
		{
			sb.Append( nodes[i].ToString() );
			sb.Append('\n');
		}

		return sb.ToString();
	}


	public int[][] GetSubGraphs()
	{
		List<int[]> subGraphs = new List<int[]>();
		bool[] visited = new bool[nodes.Length];

		for (int i = 0; i < nodes.Length; i++)
		{
			if (visited[i]) continue;
			if (!nodes[i].enabled) continue;


			List<int> l = new List<int>();
			Visit(i, l, visited);

			subGraphs.Add( l.ToArray() );
		}

		return subGraphs.ToArray();
	}

	void Visit(int index, List<int> l, bool[] visited)
	{
		if (!nodes[index].enabled)
		{
			Console.WriteLine("NOT ENABLEDDDD");
			return;
		}

		if (visited[index]) return;

		visited[index] = true;
		l.Add(index);

		for (int i = 0; i < nodes[index].connections.Count(); i++)
		{
			Visit(nodes[index].connections[i], l, visited);
		}
	}



}