using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.IO;
using System.Text;
using UnityEngine;

public class UniredisRequest : IDisposable
{
	public Socket socket;
	public List<Socket> ready = new List<Socket>();

	public void Connect ()
	{
		if (socket != null)
	 	{
			throw new Exception("You are already connected");
		}

		Uniredis Uniredis = Resources.Load<Uniredis>("Uniredis");

		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Connect(Uniredis.Host, Uniredis.Port);

		if (!socket.Connected)
		{
			socket.Close();
			socket = null;
		}

		socket.Blocking = false;
	}

	public void Disconnect ()
	{
		socket.Close();
		socket = null;
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
			// CALL REDIS QUIT
			Disconnect();
		}
	}
}

[CreateAssetMenu(fileName = "Uniredis", menuName = "Uniredis/Uniredis Client", order = 1)]
public class Uniredis : ScriptableObject
{
	[SerializeField]
	public string Host;
	[SerializeField]
	public int Port;

	private const string CRLF = "\r\n";

	public static void Get (MonoBehaviour launcher, string key, Action<string, string> callback)
	{
		String query = String.Format("GET {0}", key);

		launcher.StartCoroutine(Uniredis.Query(query, (result) =>
		{
			callback(null, ParseGetResponse(result));
		}));
	}

	public static void Set (MonoBehaviour launcher, string key, object value, Action<string, bool> callback)
	{
		String query = String.Format("SET {0} {1}", key, value);

		launcher.StartCoroutine(Uniredis.Query(query, (result) =>
		{
			callback(null, ParseSetResponse(result));
		}));
	}

	private static IEnumerator Query (string query, Action<string> callback) 
	{
		using (UniredisRequest redis = new UniredisRequest())
		{
			redis.Connect();

			byte[] data = new byte[1024];
			string result = String.Empty;
			int packetLength = -1;

			var request = Encoding.ASCII.GetBytes(query + CRLF);
			redis.socket.Send(request);

			while (true)
			{
				List<Socket> r = new List<Socket>();
				r.Add(redis.socket);
				Socket.Select(r, null, null, 0);

				foreach (Socket readySocket in r)	
				{
					packetLength = readySocket.Receive(data);

					if (packetLength > 0)
					{
						result = Encoding.ASCII.GetString(data, 0, packetLength);
						callback(result);
						yield break;
					}
					else
					{
						yield break;
					}
				}

				yield return null;
			}
		}
	}

	public static string ParseGetResponse (string input)
	{
		string result = null;

		if (input.Length == 0)
			return null;

		if (input[0] == '-')
			return null;

		if (input[0] == '$')
		{
			if (input == "$-1")
				return null;

			String[] substrings = input.Split(new char[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
			result = substrings[1];
		}

		return result;
	}

	public static bool ParseSetResponse (string input)
	{
		if (input.Length == 0)
			return false;

		if (input[0] == '-')
			return false;

		if (input[0] == '+')
			return true;

		return false;
	}
}
