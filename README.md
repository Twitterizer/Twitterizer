Twitterizer
===========

Example
-------
	var response = TwitterStatus.Update(credentials, "Twitterizer is fantastic!");
	if (response.Result == Success)
	{
		DisplaySuccessMessageFor(response.ResponseObject);
	}
	else
	{
		DisplayErrorMessage(response.ErrorMessage);
	}



	var myFeed = TwitterTimeline.HomeTimeline(credentials);

History
-------
Twitterizer started in May of 2008 as an excuse for me to explore design patterns, have a little fun coding, and stretch out my architectural legs. I never set out to create an open source library, much less a popular one (in the last 30 days of its life, Twitterizer.net pulled 8,560 unique visitors and nearly 52k views). 

In 2009 the [Smuxi](https://github.com/meebey/smuxi) project made use of the Twitterizer library to support Twitter as a built-in feature in their popular IRC client. As of 2013 Twitterizer is part of the Smuxi project umbrella and thus actively maintained. In August 2012 Smuxi ported Twitterizer to Newtonson.Json 4.5.8, in August 2013 Twitterizer was ported to the Twitter v1.1 API, in November 2013 proxy support was added to the Streaming API, and in Janary 2014 Twitterizer was ported to enforced HTTPS.

Plans for the future
---------

### Version 2.4.3
The next minor version will contain only high priority bug fixes while Version 3 is in development.

### Version 3
1. Drop support of Silverlight (unless developers come forward to own it)
2. Twitterizer project to be async-only using the [TPL](http://msdn.microsoft.com/en-us/library/dd460717.aspx)
3. Drop Twitterizer.OAuth library (let's face it, it's not a very great product) and, in fact:
4. Evaluate relying upon an external OAuth library (DotNetOpenAuth, for example)
5. Sync command classes and objects with Twitter's 1.1 documentation
6. Redesign unit tests; properly mock objects and run tests using static json data
7. Establish continuous deployment to Nuget

Contribute
---------

1. [Create or claim an issue](https://github.com/Twitterizer/Twitterizer/issues)
2. [Fork the repository](https://help.github.com/articles/fork-a-repo/)
3. Create a branch named appropriately for the issue (based on the develop branch)
4. Make happy code
5. [Submit a pull request](https://help.github.com/articles/using-pull-requests/) to merge your changes to the develop branch
6. Watch for comments or questions about your changes

### Branch Naming
From now on, all work will be performed in feature or bug branches. The will follow this pattern: \<issue_type>/\<issue_number>-\<short-description>.

For example, say I was working on issue #63, the branch would be called `bug/63-fix-unit-tests`
