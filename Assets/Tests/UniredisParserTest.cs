using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class UniredisParserTest
{
	[Test]
	public void GetResponseGreen ()
	{
		string result = Uniredis.ParseGetResponse("$4\r\nciao\r\n");
		Assert.AreEqual(result, "ciao");
	}

	[Test]
	public void GetResponseRed ()
	{
		string result = Uniredis.ParseGetResponse("$4\r\nciao\r\n");
		Assert.AreNotEqual(result, "ciaoo");
	}

	[Test]
	public void GetEmptyResponseGreen ()
	{
		string result = Uniredis.ParseGetResponse("");
		Assert.AreEqual(result, null);
	}

	[Test]
	public void GetEmptyResponseRed ()
	{
		string result = Uniredis.ParseGetResponse("");
		Assert.AreNotEqual(result, "ciao");
	}

	[Test]
	public void GetErrorResponseGreen ()
	{
		string result = Uniredis.ParseGetResponse("$-1");
		Assert.AreEqual(result, null);
	}

	[Test]
	public void GetErrorResponseRed ()
	{
		string result = Uniredis.ParseGetResponse("$-1");
		Assert.AreNotEqual(result, "ciao");
	}

	[Test]
	public void GetRandomResponseGreen ()
	{
		string result = Uniredis.ParseGetResponse("asjdnq38jnasda2");
		Assert.AreEqual(result, null);
	}

	[Test]
	public void GetRandomResponseRed ()
	{
		string result = Uniredis.ParseGetResponse("asjdnq38jnasda2");
		Assert.AreNotEqual(result, "ciao");
	}

	[Test]
	public void SetResponseGreen ()
	{
		bool result = Uniredis.ParseSetResponse("+OK");
		Assert.AreEqual(result, true);
	}

	[Test]
	public void SetResponseRed ()
	{
		bool result = Uniredis.ParseSetResponse("+OK");
		Assert.AreNotEqual(result, false);
	}

	[Test]
	public void SetEmptyResponseGreen ()
	{
		bool result = Uniredis.ParseSetResponse("");
		Assert.AreEqual(result, false);
	}

	[Test]
	public void SetEmptyResponseRed ()
	{
		bool result = Uniredis.ParseSetResponse("");
		Assert.AreNotEqual(result, true);
	}

	[Test]
	public void SetErrorResponseGreen ()
	{
		bool result = Uniredis.ParseSetResponse("-ERR test");
		Assert.AreEqual(result, false);
	}

	[Test]
	public void SetErrorResponseRed ()
	{
		bool result = Uniredis.ParseSetResponse("-ERR test");
		Assert.AreNotEqual(result, true);
	}

	[Test]
	public void SetRandomResponseGreen ()
	{
		bool result = Uniredis.ParseSetResponse("df98273rhsdas");
		Assert.AreEqual(result, false);
	}

	[Test]
	public void SetRandomResponseRed ()
	{
		bool result = Uniredis.ParseSetResponse("df98273rhsdas");
		Assert.AreNotEqual(result, true);
	}
}
