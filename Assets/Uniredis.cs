using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.IO;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Redis", menuName = "Uniredis/Uniredis Client", order = 1)]
public class Uniredis : ScriptableObject
{
	[SerializeField]
	private string Host;
	[SerializeField]
	private int Port;

	private static Socket socket;
	private static byte[] data;

	private const string CRLF = "\r\n";

	public class UniredisException : Exception
	{
		public UniredisException(string message) : base(message) 
		{
		}
	}

	public static void Connect ()
	{
		if (socket != null)
		{
			throw new UniredisException("You are already connected");
		}

		Uniredis uniredis = Resources.Load<Uniredis>("Uniredis");

		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Connect(uniredis.Host, uniredis.Port);
		// socket.Blocking = false;

		if (!socket.Connected)
		{
			socket.Close();
			socket = null;
		}

		data = new byte[4096];
	}

	public static string GetKey (string key)
	{
		String query = String.Format("GET {0}", key);
		return Query(query);
	}

	public static bool SetKey (string key, string value)
	{
		String query = String.Format("SET {0} \"{1}\"", key, value);
		string result = Query(query);

		if (result != "+OK" + CRLF)
		{
			throw new UniredisException(result);
		}

		return true;
	}

	private static string Query (string query)
	{
		if (socket == null)
			Connect();

		string result = String.Empty;

		while (true)
		{
			data = Encoding.ASCII.GetBytes(query + CRLF);
			int length = socket.Send(data);

			byte[] dataReceived = new byte[4096];
			int dataReceiveLength = socket.Receive(dataReceived);

			if (length <= 0)
			{
				break;
			}

			result = Encoding.ASCII.GetString(dataReceived, 0, dataReceiveLength);
			return result;
		}

		return result;
	}

	public static void Disconnect ()
	{
		Query("QUIT");
		socket.Close();
		socket = null;
	}
}
