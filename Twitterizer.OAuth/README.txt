***********************************************************
Twitterizer Web Request Builder Utility Library
***********************************************************

---------------
| Description |
---------------
	This library provides an extremely simple class designed to prepare and execute non-authorized and OAuth requests.

----------------
| Simple Usage |
----------------
	There are only two classes in the library: WebRequestBuilder and OAuthToken.

	The OAuthToken class provides a means of communicating OAuth token values to the request builder. 
	It has 4 properties:
		AccessToken			- The Access token key value
		AccessTokenSecret	- The access token secret value
		ConsumerKey			- The consumer token key value
		ConsumerSecret		- The consumer token secret value

	--------------
	Step 1: Instantiate a new WebRequestBuilder class
	--------------
		Option 1
			WebRequestBuilder requestBuilder = new WebRequestBuilder(new Uri("http://..."), HTTPVerb.GET);

		Option 2
			WebRequestBuilder requestBuilder = new WebRequestBuilder(new Uri("http://..."), HTTPVerb.GET, myOAuthTokens);

	--------------
	Step 2: Provide any additional configuration settings and querystring/form fields.
	--------------
		requestBuilder.Proxy = new WebProxy("http://localhost:8080/");

		requestBuilder.Parameters.Add("screen_name", "twitterapi");
		requestBuilder.Parameters.Add("count", "10");

	--------------
	Step 3: Execute the request
	--------------
		HttpWebResponse response = requestBuilder.ExecuteRequest();

-----------------
| Other Methods |
-----------------
	All of these useful methods are exposed publicly, in case you wish to incorporate partial functionality or modify the default behavior:

	--------------
	public string GenerateAuthorizationHeader()
	--------------
		Based on the parameters provided and OAuth parameters generated, this method will generate the HTTP authorization header.
		During execution the header is added to the request: HttpWebRequest.Headers.Add("Authorization", GenerateAuthorizationHeader());

	--------------
	public static string GenerateNonce()
	--------------
		Generates a random string. The string is the hexidecimal string value of a random number between 123400 and int.MaxValue.

	--------------
	public static string GenerateTimeStamp()
	--------------
		Builds a timestamp of the system time in a UNIX string format.

	--------------
	public static string NormalizeUrl(Uri url)
	--------------
		Normalizes a URL according to the OAuth specifications. This is for placement in the base string for signature generation.

	--------------
	public HttpWebRequest PrepareRequest()
	--------------
		Invoking this method will take all of the information provided to the request builder and return a fully prepare HttpWebRequest object.
		If tokens were provided when the builder was instantiated, the resulting web request will include the signature.

	--------------
	public void SetupOAuth()
	--------------
		Invoking this method will instruct the builder to add any OAuth parameters nessisary to execute the request. The parameters can be accessed through the OAuthParameters property.

	--------------
	public static string UrlEncode(string value)
	--------------
		Encodes a string value for use in a querystring or form field as specified in the OAuth protocol. This method differs from other Url encoding methods and is written specifically for use in OAuth requests.

-----------
| License |
-----------
	Copyright (c) 2010, Patrick "Ricky" Smith
	All rights reserved.

	Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

	 - Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 
	 - Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in 
	   the documentation and/or other materials provided with the distribution.
   
	 - Neither the name Twitterizer nor the names of its contributors may be used to endorse or promote products derived from 
	   this software without specific prior written permission.

	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
	THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
	CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
	PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
	LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
	SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.