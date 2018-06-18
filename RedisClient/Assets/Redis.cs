using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.IO;
using System.Text;
using UnityEngine;

public class Redis : IDisposable
{
	[HideInInspector]
	public string Host;
	[HideInInspector]
	public int Port;

	private Socket socket;
	private byte[] data;

	private const string CRLF = "\r\n";

	public Redis (string host)
	{
		this.Host = host;
		this.Port = 6379;
	}

	public class RedisException : Exception
	{
		public RedisException(string message) : base(message) {}
	}

	private void Connect ()
	{
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Connect(Host, Port);
		// socket.Blocking = false;

		if (!socket.Connected)
		{
			socket.Close();
			socket = null;
			return;
		}

		data = new byte[4096];
	}

	public void Disconnect ()
	{
		socket.Close();
	}

	public string GetKey (string key)
	{
		String query = String.Format("GET {0}", key);
		return Query(query);
	}

	public bool SetKey (string key, string value)
	{
		String query = String.Format("SET {0} \"{1}\"", key, value);
		string result = Query(query);

		if (result != "+OK" + CRLF)
		{
			throw new RedisException(result);
		}

		return true;
	}

	private string Query (string query)
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

	public void Dispose ()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose (bool disposing)
	{
		if (disposing)
		{
			Query("QUIT");
			socket.Close();
			socket = null;
		}
	}
}
