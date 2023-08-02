using System;
using System.Collections.Generic;
using System.Collections;

namespace Clique;

struct Node
{
	int _index;
	public int index
	{
		get => Math.Abs(_index);
		private set => _index = value;
	}
	public List<int> connections { get; set; }
	public bool enabled => _index >= 0;
	public Node(int i)
	{
		_index = i;
		connections = new List<int>();
	}

	public void Connect(int i)
	{
		// Enabled is checked
		connections.Add(i);
		
		// #SLOW
		connections.Sort();
	}

	public bool Connected(int i) 
	{
		return connections.Contains(i);
	}

	public void Enable()
	{
		index = Math.Abs(index);
	}

	public void Disable()
	{
		index = -Math.Abs(index);
	}

	public Node CreateAntiNode(int size)
	{
		Node an = new Node(index);

		if (!enabled)
		{
			an.Disable();
			return an;
		}

		int connectionIX = 0;
		for (int i = 0; i < size; i++)
		{
			if (i == index) continue;

			if (connectionIX < connections.Count() && connections[connectionIX] == i)
			{
				connectionIX += 1;
			}
			else
			{
				an.Connect(i);
			}
		}

		return an;
	}

	public override string ToString()
	{
		if (!enabled)
		{
			return $"{index}: disabled";
		}

		System.Text.StringBuilder sb = new System.Text.StringBuilder($"{index}: ");

		for (int i = 0; i < connections.Count; i++)
		{
			sb.Append( connections[i].ToString() );
			sb.Append(' ');
		}

		return sb.ToString();
	}
}