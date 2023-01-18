
using Microsoft.EntityFrameworkCore;
using MO.Core;
using MO.Core.Helpers;

namespace MO.DataStore.EFCore
{
    public static class DbInitializer
    {
		private static List<Post>? InitialPostCollection;
		//private static List<PostTag>? InitialPostTagCollection;
        public static async void Initialize(MODbContext context)
        {
			InitialPostCollection = new List<Post>();
			//InitialPostTagCollection = new List<PostTag>();
            //  Look for any posts
            if (context.Posts.Any())
            {
                return; // Db has already been seeded
            }

			await InitDb(context);

        }

		private static async Task GeneratePosts(MODbContext context)
		{
			var _posts = new Post[]
			{
				new Post {
					Title = $"Making VS Code more accessible (and productive) with custom keybindings",
					Content = $"Being involved in the OmniSharp project, I had the pleasure of working a lot with VS Code extension development over the past several years. Given that background, a coworker asked me recently if I had any ideas for improving his user experience with VS Code. In particular, being a screen reader user, he relies heavily on keyboard navigation and being able to quickly move focus between UI elements is critical for his productivity.",
					PubDate = new DateTime(2022, 10, 28),
					LastModified = new DateTime(2022, 11, 05),
					PostCoverPhoto = "vs-code.png" ,
					Tags = new[]
					{
						new PostTag{ TagName = "C#"},
						new PostTag{ TagName = "VSCode"},
						new PostTag{ TagName = "Prime Numbers"}
					}
				},

				new Post {
					Title = "JSON Modules in JavaScript",
					Content = $"The ECMAScript modules system (import and export keywords) by default can import only JavaScript code.\r\n\r\nBut it's often convenient to keep an application's configuration inside of a JSON file, and as result, you might want to import a JSON file directly into an ES module.\r\n\r\nFor a long time, importing JSON was supported by commonjs modules format.\r\n\r\nFortunately, a new proposal at stage 3 named JSON modules proposes a way to import JSON into an ES module. Let's see how JSON modules work.",
					PubDate = new DateTime(2021, 12, 03) ,
					PostCoverPhoto = "ecma-script.jpg",
					Tags = new[] { new PostTag { TagName = "Web" }, new PostTag { TagName = "Asp.Net" }, new PostTag { TagName = "C#" }
					}
				},

				new Post {
					Title = "How to set the default user for a WSL distro that has been manually installed with wsl --import",
					Content = $"I've blogged before on how to easily move WSL distributions between Windows 10 machines with import and export. I recently did a full fresh install of Windows 11 and wanted to bring my existing highly customized Ubuntu installation along with me.\r\n\r\nYou can tar up (zip up) the user-mode parts of your WSL2 distributions like this:\r\n\r\nwsl --export Ubuntu-20.04 c:\\Temp\\UbuntuBackup.tar\r\nThe part after --export is the distribution name that you can see from running wsl --list -v. The last argument is a full path and filename for the archive you want created.",
					PubDate = new DateTime(2021, 10, 28),
					LastModified = new DateTime(2022, 08, 13),
					PostCoverPhoto = "wsl-distro.png",
					Tags = new[] { new PostTag { TagName = "Windows" }, new PostTag { TagName = "Windows 10" }, new PostTag { TagName = "WSL" }, new PostTag { TagName = "Ubuntu" }
					}
				},
				new Post {
					Title = "Using Sass in ASP.NET Core, including Blazor",
					Content = "Sass is a css-preprocessor that allows us to create stylesheets for web applications using mixins, variables, functions, and nested rules. These SASS files are then compiled into standard css files for our use. What is nice, ASP.NET Core and Blazor can integrate Sass into the build process. This allows us to write our stylesheets in sass first or even import and modify existing frameworks. Today, we are going to use sass to import the Bulma library and change the primary color with just a few lines of code.",
					PubDate = new DateTime(2019, 07, 03),
					LastModified = new DateTime(2021, 03, 21),
					PostCoverPhoto = "sass-asp-net-core.jpg",
					Tags = new[]
					{
						new PostTag{ TagName = "Asp.Net Core"},
						new PostTag{ TagName = "Blazor"},
						new PostTag{ TagName = "C#"},
						new PostTag{ TagName = "Web"},
						new PostTag{ TagName = "CSS"},
						new PostTag{ TagName = "Sass"}
					}
				},
				new Post {
					Title = "Using Jupyter Notebook with Jekyll",
					Content = $"In the last post, I tested out the functionality of Jupyter Notebook, a platform that I am just starting to get acquainted with. I’m pleased with how that experimental post turned out, although there are still things to modify and improve. In this post, I explain how I was able to automate the process of converting Jupyter Notebook into markdown using shell script and python commands.",
					PubDate = new DateTime(2019, 11, 30),
					LastModified = new DateTime(2022, 08, 01),
					PostCoverPhoto = "jupyter-notebook-jekyll.jpg",
					Tags = new[]
					{
						new PostTag{ TagName = "Jupyter Notebook"},
						new PostTag{ TagName = "Python"}
					}
				},
				new Post {
					Title = "Revisiting Eigenvectors",
					Content = $"<p>In linear algebra, an eigenvector of a linear transformation is roughly <a href=\"https://en.wikipedia.org/wiki/Eigenvalues_and_eigenvectors\">defined</a> as follows:</p> <p>This definition, while seemingly abstract and cryptic, distills down into a simple equation when written in matrix form:</p>" ,
					PubDate = new DateTime(2020, 01, 03),
					PostCoverPhoto = "eigenvectors-blog-post.jpg" ,
					Tags = new[]
					{
						new PostTag{ TagName = "Eigenvectors"},
						new PostTag{ TagName = "Math"},
						new PostTag{ TagName = "Python"},
					}
				},
				new Post {
					Title = "JavaScript - Add Event Listener to Multiple Elements in Vanilla JS",
					Content = $"This is a super quick post to show how to add an event listener to multiple HTML elements in pure JavaScript.\r\n\r\nI had to do this recently to handle click events on one or more modal popup elements in a single page.\r\n\r\nJavaScript event bubbling\r\nEvents in javascript are propagated (or bubbled) up the DOM tree from the target element through its parents all the way to the root (html) element, document and window objects.\r\n\r\nSo to listen for events on multiple elements you can add a single event listener to the document object and check the target element.",
					PubDate = new DateTime(2023, 01, 03),
					PostCoverPhoto = "javascript-event-listners.jpg",
					Tags = new[]
					{
						new PostTag{ TagName = "JavaScript"},
						new PostTag{ TagName = "Web"}
					}
				},
				new Post {
					Title = "GitHub Composite Actions are fast way to templatize workflows",
					Content = $"I’ve had a love/hate relationship with CI/CD for a long while ever since I remember it being a thing. In those early days the ‘tools’ were basically everyone’s homegrown scripts, batch files, random daemon hosts, etc. Calling something a workflow was a stretch. It was for that reason I just wasn’t a believer, it was just too ‘hard’ for the average dev. I, like many, would build from my machine and direct deploy or copy over to file shares (NOTE: LOTS of people still do this). Well the tools have gotten WAY better across the board from many different vendors and your options for great tools exist. I’ve been privileged to work with Damian Brady and Abel Wang to educate me on the ways of CI/CD a bit. I know Damian has a mantra about right-click publish, but that only made me want to make it simpler for devs.\r\n\r\nNOTE: Did you know that for most projects in .NET working in VS you can use right-click Publish to generate a CI/CD workflow for you, further reducing the complexity?",
					PubDate = new DateTime(2021, 12, 17),
					PostCoverPhoto =  "github-composite-actions.png"
				},
				new Post {
					Title = "A sneak peek at Bayesian Inference",
					Content = $"So far on this blog, we have looked the mathematics behind distributions, most notably binomial, Poisson, and Gamma, with a little bit of exponential. These distributions are interesting in and of themselves, but their true beauty shines through when we analyze them under the light of Bayesian inference. In today’s post, we first develop an intuition for conditional probabilities to derive Bayes’ theorem. From there, we motivate the method of Bayesian inference as a means of understanding probability.\r\n\r\nConditional ProbabilityPermalink\r\nSuppose a man believes he may have been affected with a flu after days of fever and coughing. At the nearest hospital, he is offered to undergo a clinical examination that is known to have an accuracy of 90 percent, i.e. it will return positive results to positive cases 90 percent of the time. However, it is also known that the test produces false positives 50 percent of the time. In other words, a healthy, unaffected individual will test positive with a probability of 50 percent.",
					PubDate = new DateTime(2019, 11, 30)
				},
				new Post {
					Title = "How ListSeparator Depends on Runtime and Operating System",
					Content = $"In the two previous blog posts from this series, we discussed how socket errors and socket orders depend on the runtime and operating systems. For some, it may be obvious that some things are indeed specific to the operating system or the runtime, but often these issues come as a surprise and are only discovered when running our code on different systems. An interesting example that may bite us at runtime is using ListSeparator in our code. It should give us a common separator for list elements in a string. But is it really common? Let’s start our investigation by printing ListSeparator for the Russian language:\r\n\r\nConsole.WriteLine(new CultureInfo(\"ru-ru\").TextInfo.ListSeparator);\r\nOn Windows, you will get the same result for .NET Framework, .NET Core, and Mono: the ListSeparator is ; (a semicolon). You will also get a semicolon on Mono+Unix. However, on .NET Core+Unix, you will get a non-breaking space.",
					PubDate = new DateTime(2020, 05, 20)
				},
				new Post {
					Title = "MathJax in Markdown",
					Content = $"Adding mathematical formulae to HTML pages is easy these days using MathJax. But I create all my documents in Markdown format on my Mac. This post shows how to add mathematical formulae to your Markdown documents on the Mac and have them preview and export to PDF correctly.\r\n\r\nMathJax in Markdown\r\nAdding mathematical formulae to a markdown document simply requires you to use the MathJax delimiters to start and end each formula as follows:\r\n\r\nFor centered formulae, use \\\\[ and \\\\].\r\nFor inline formulae, use \\\\( and \\\\).\r\nFor example, the formula:\r\n\r\n\\\\[ x = {{-b \\pm \\sqrt{{b^2-4ac}} \\over 2a}} \\\\]\r\nRenders like this from markdown:\r\n\r\nOr we can go inline where the code \\\\( ax^2 + \\sqrt{{bx}} + c = 0 \\\\) renders as .",
					PubDate = new DateTime(2022, 09, 13),
					PostCoverPhoto = "MathJax.png",
					Tags = new[]
					{
						new PostTag{ TagName = "Web"},
						new PostTag{ TagName = "JavaScript"},
						new PostTag{ TagName = "MathJax"},
						new PostTag{ TagName = "Maths"},
						new PostTag{ TagName = "Web"},
						new PostTag{ TagName = "HTML"}
					}
				},
				new Post {
					Title = "Role based JWT Tokens in ASP.NET Core APIs",
					Content = $"Authentication and Authorization in ASP.NET Core continues to be the most fiddly component for configuration. It seems almost on every app I run into some sort of sticking point with Auth. Four versions in have brought three different authentication implementations and feature churn has also left a wave of out of date information in its wake. Today I got stuck in one of those Groundhog Day loops looking at outdated information with JWT Tokens for a Web API with Role based authorization.\r\n\r\nThe current iteration of JWT Token setup in ASP.NET Core actually works very well, as long as you get the right incantations of config settings strung together. Part of the problem with Auth configuration is that most of settings have nothing to do with the problem at hand and deal with protocol ceremony. For example, setting Issuer and Audience seems totally arcane but it's part of the requirements for JWT Tokens and do need to be configured. Luckily there are only a few of those settings that are actually required and most of it is boilerplate.\r\n\r\nI've not found this information all in one place, and today I barked up the wrong tree for a couple of hours in regards to Role authorization with JWT Tokens, where my app would validate non-role Authorizations, but not role based ones. So, now that I managed to get it all working, I'm writing it down so I can find it next time around.",
					PubDate = new DateTime(2021, 03, 09),
					PostCoverPhoto = "dot-net.png",
					Tags = new[]
					{
						new PostTag{ TagName = "Web"},
						new PostTag{ TagName = "Asp.Net"},
						new PostTag{ TagName = "C#"}
					}
				},
				new Post {
					Title = "Python-Like enumerate() In C++17",
					Content = $"Python has a handy built-in function called enumerate(), which lets you iterate over an object (e.g. a list) and have access to both the index and the item in each iteration. You use it in a for loop, like this:\r\n\r\nfor i, thing in enumerate(listOfThings):\r\n    print(\"The %dth thing is %s\" % (i, thing))\r\nIterating over listOfThings directly would give you thing, but not i, and there are plenty of situations where you’d want both (looking up the index in another data structure, progress reports, error messages, generating output filenames, etc).\r\n\r\nC++ range-based for loops work a lot like Python’s for loops. Can we implement an analogue of Python’s enumerate() in C++? We can!\r\n\r\nC++17 added structured bindings (also known as “destructuring” in other languages), which allow you to pull apart a tuple type and assign the pieces to different variables, in a single statement. It turns out that this is also allowed in range for loops. If the iterator returns a tuple, you can pull it apart and assign the pieces to different loop variables.\r\n\r\nThe syntax for this looks like:\r\n\r\nstd::vector<std::tuple<ThingA, ThingB>> things;\r\n...\r\nfor (auto [a, b] : things)\r\n{{\r\n    // a gets the ThingA and b gets the ThingB from each tuple\r\n}}\r\nSo, we can implement enumerate() by creating an iterable object that wraps another iterable and generates the indices during iteration. Then we can use it like this:\r\n\r\nstd::vector<Thing> things;\r\n...\r\nfor (auto [i, thing] : enumerate(things))\r\n{{\r\n    // i gets the index and thing gets the Thing in each iteration\r\n}}\r\nThe implementation of enumerate() is pretty short, and I present it here for your use:\r\n\r\n#include <tuple>\r\n\r\ntemplate <typename T,\r\n          typename TIter = decltype(std::begin(std::declval<T>())),\r\n          typename = decltype(std::end(std::declval<T>()))>\r\nconstexpr auto enumerate(T && iterable)\r\n{{\r\n    struct iterator\r\n    {{\r\n        size_t i;\r\n        TIter iter;\r\n        bool operator != (const iterator & other) const {{ return iter != other.iter; }}\r\n        void operator ++ () {{ ++i; ++iter; }}\r\n        auto operator * () const {{ return std::tie(i, *iter); }}\r\n    }};\r\n    struct iterable_wrapper\r\n    {{\r\n        T iterable;\r\n        auto begin() {{ return iterator{{ 0, std::begin(iterable) }}; }}\r\n        auto end() {{ return iterator{{ 0, std::end(iterable) }}; }}\r\n    }};\r\n    return iterable_wrapper{{ std::forward<T>(iterable) }};\r\n}}\r\nThis uses SFINAE to ensure it can only be applied to iterable types, and will generate readable error messages if used on something else. It accepts its parameter as an rvalue reference so you can apply it to temporary values (e.g. directly to the return value of a function call) as well as to variables and members.",
					PubDate = new DateTime(2023, 01, 05),
					Tags = new[]
					{
						new PostTag{ TagName = "Web"},
						new PostTag{ TagName = "Python"}
					}
				},
				new Post {
					Title = "It’s all in the Host Class – Dependency Injection with .NET",
					Content = $"Without Dependency Injection\r\nLet’s start with a small sample where dependency injection is not used. Here, the GreetingService class offers a simple Greet method to return a string:\r\n\r\n1\r\n2\r\n3\r\n4\r\npublic class GreetingService\r\n{{\r\n    public string Greet(string name) => $\"Hello, {{name}}\";\r\n}}\r\nThis GreetingService class is used from the Action method in the HelloController:\r\n\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\npublic class HelloController\r\n{{\r\n    public string Action(string name)\r\n    {{\r\n        var service = new GreetingService();\r\n        string message = service.Greet(name);\r\n        return message.ToUpper();\r\n    }}\r\n}}\r\nFinally, the Main method of the Program class instantiates the HelloController and invokes the Action method.\r\n\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\nclass Program\r\n{{\r\n    static void Main()\r\n    {{\r\n        var controller = new HelloController();\r\n        string result = controller.Action(\"Stephanie\");\r\n        Console.WriteLine(result);\r\n    }}\r\n}}\r\nThe application runs writing a greeting message to the console. What’s the issue with this implementation? The HelloController has a string dependency on the GreetingService class. If the implementation of the GreetingService should be changed, e.g. offering one impementation where a database is accessed, or a REST service invoked, the HelloController needs to be changed as well. Also, creating a unit test for the Actionmethod of the HelloController, the test shouldn’t cover the GreetingService. With a unit test of the Action method we only want to test the implementation of this method, and no other dependencies. Let’s solve this in the next step.\r\n\r\nInversion of Control – With Dependency Injection\r\nInversion of control is a design principle where – as the name says – the one who has control is inverted. Instead of a method (typically one within a library) that defines the complete functionality on its own, the caller can supply code. This code in turn is invoked by the called method.\r\n\r\nUsing .NET, inversion of control can be implemented by using delegates or interfaces. To allow the HelloController not to take a dependency of the GreetingService class, the IGreetingService interface is introduced. This interface defines all the requirements for the HelloController, the Greet method:\r\n\r\n1\r\n2\r\n3\r\n4\r\npublic interface IGreetingService\r\n{{\r\n    string Greet(string name);\r\n}}\r\nThe GreetingService class implements this interface:\r\n\r\n1\r\n2\r\n3\r\n4\r\npublic class GreetingService : IGreetingService\r\n{{\r\n    public string Greet(string name) => $\"Hello, {{name}}\";\r\n}}\r\nNow the HelloController class can be changed to take a dependency only on the interface IGreetingService. This interface is injected in the constructor of the HelloController.\r\n\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\npublic class HelloController\r\n{{\r\n    private readonly IGreetingService _greetingService;\r\n    public HelloController(IGreetingService greetingService)\r\n    => _greetingService = greetingService;\r\n \r\n    public string Action(string name)\r\n    {{\r\n        string message = _greetingService.Greet(name);\r\n        return message.ToUpper();\r\n    }}\r\n}}\r\nThe class to be used for the interface IGreetingService now needs to be defined outside of the HelloController – passing an object implementing the interface IGreetingService on instantiating of the object HelloController. This is where the term inversion of control is used: The control what type is used is now passed to the outside.",
					PubDate = new DateTime(2020, 05, 15),
					PostCoverPhoto = "dot-net.png",
					Tags = new[]
					{
						new PostTag{ TagName = "Web"},
						new PostTag{ TagName = "Asp.Net"},
						new PostTag{ TagName = "C#"},
						new PostTag{ TagName = "Dependency Injection"}
					}
				},
				new Post {
					Title = "Parallel.ForEachAsync in .NET 6",
					Content = $"Great tweet from Oleg Kyrylchuk (follow him!) showing how cool Parallel.ForEachAsync is in .NET 6. It's new! Let's look at this clean bit of code in .NET 6 that calls the public GitHub API and retrieves n number of names and bios, given a list of GitHub users:\r\n\r\nusing System.Net.Http.Headers;\r\nusing System.Net.Http.Json;\r\n \r\nvar userHandlers = new []\r\n{{\r\n    \"users/okyrylchuk\",\r\n    \"users/shanselman\",\r\n    \"users/jaredpar\",\r\n    \"users/davidfowl\"\r\n}};\r\n \r\nusing HttpClient client = new()\r\n{{\r\n    BaseAddress = new Uri(\"https://api.github.com\"),\r\n}};\r\nclient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(\"DotNet\", \"6\"));\r\n \r\nParallelOptions parallelOptions = new()\r\n{{\r\n    MaxDegreeOfParallelism = 3\r\n}};\r\n \r\nawait Parallel.ForEachAsync(userHandlers, parallelOptions, async (uri, token) =>\r\n{{\r\n    var user = await client.GetFromJsonAsync<GitHubUser>(uri, token);\r\n \r\n    Console.WriteLine($\"Name: {{user.Name}}\\nBio: {{user.Bio}}\\n\");\r\n}});\r\n \r\npublic class GitHubUser\r\n{{\r\n    public string Name {{ get; set; }}\r\n    public string  Bio {{ get; set; }}\r\n}}\r\nLet's note a few things in this sample Oleg shared. First, there's no Main() as that's not required (but you can have it if you want).\r\n\r\nWe also see just two usings, bringing other namespaces into scope. Here's what it would look like with explicit namespaces:\r\n\r\nusing System;\r\nusing System.Net.Http;\r\nusing System.Net.Http.Headers;\r\nusing System.Net.Http.Json;\r\nusing System.Threading.Tasks;\r\nWe've got an array of users to look up in userHandlers. We prep an HttpClient and setup some ParallelOptions, giving our future ForEach the OK to \"fan out\" to up to three degrees of parallelism - that's the max number of concurrent tasks we will enable in one call. If it's -1 there is no limit to the number of concurrently running operations.\r\n\r\nThe really good stuff is here. Tight and clean:",
					PubDate = new DateTime(2021, 10, 21),
					PostCoverPhoto = "dot-net.png",
					Tags = new[]
					{
						new PostTag{ TagName = ".Net6"},
						new PostTag{ TagName = "C#"},
						new PostTag{ TagName = "Github"}
					}
				},
				new Post {
					Title = "Plotting Prime Numbers",
					Content = $"<p>Today’s article was inspired by a question that came up on a Korean mathematics Facebook group I’m part of. The gist of the question could probably be translated into something like this:</p> <div class=\"language-python highlighter-rouge\"><div class=\"highlight\"><pre class=\"highlight\"><code><span class=\"kn\">import</span> <span class=\"nn\">math</span>\r\n<span class=\"kn\">import</span> <span class=\"nn\">sympy</span>\r\n<span class=\"kn\">import</span> <span class=\"nn\">numpy</span> <span class=\"k\">as</span> <span class=\"n\">np</span>\r\n<span class=\"kn\">import</span> <span class=\"nn\">matplotlib.pyplot</span> <span class=\"k\">as</span> <span class=\"n\">plt</span>\r\n<span class=\"o\">%</span><span class=\"n\">matplotlib</span> <span class=\"n\">inline</span>\r\n<span class=\"o\">%</span><span class=\"n\">config</span> <span class=\"n\">InlineBackend</span><span class=\"p\">.</span><span class=\"n\">figure_format</span><span class=\"o\">=</span><span class=\"s\">'retina'</span>\r\n<span class=\"n\">plt</span><span class=\"p\">.</span><span class=\"n\">style</span><span class=\"p\">.</span><span class=\"n\">use</span><span class=\"p\">(</span><span class=\"s\">'dark_background'</span><span class=\"p\">)</span>\r\n</code></pre></div></div> <p>First, let’s write a function that accepts some number as input and transforms it into cartesian representation of polar coordinates. The output itself is cartesian, but the coordinates they represent correspond to polar coordinate locations. We could understand this function as a transformation <span class=\"MathJax_Preview\" style=\"color: inherit; display: none;\"></span><span class=\"MathJax\" id=\"MathJax-Element-1-Frame\" tabindex=\"0\" style=\"position: relative;\" data-mathml=\"<math xmlns=&quot;http://www.w3.org/1998/Math/MathML&quot;><mi>C</mi><mo>:</mo><mrow class=&quot;MJX-TeXAtom-ORD&quot;><mi mathvariant=&quot;double-struck&quot;>R</mi></mrow><mo stretchy=&quot;false&quot;>&amp;#x2192;</mo><msup><mrow class=&quot;MJX-TeXAtom-ORD&quot;><mi mathvariant=&quot;double-struck&quot;>R</mi></mrow><mn>2</mn></msup></math>\" role=\"presentation\"><nobr aria-hidden=\"true\"><span class=\"math\" id=\"MathJax-Span-1\" style=\"width: 5.841em; display: inline-block;\"><span style=\"display: inline-block; position: relative; width: 5.018em; height: 0px; font-size: 116%;\"><span style=\"position: absolute; clip: rect(1.609em, 1005.02em, 2.745em, -999.998em); top: -2.584em; left: 0em;\"><span class=\"mrow\" id=\"MathJax-Span-2\"><span class=\"mi\" id=\"MathJax-Span-3\" style=\"font-family: MathJax_Math-italic;\">C<span style=\"display: inline-block; overflow: hidden; height: 1px; width: 0.041em;\"></span></span><span class=\"mo\" id=\"MathJax-Span-4\" style=\"font-family: MathJax_Main; padding-left: 0.276em;\">:</span><span class=\"texatom\" id=\"MathJax-Span-5\" style=\"padding-left: 0.276em;\"><span class=\"mrow\" id=\"MathJax-Span-6\"><span class=\"mi\" id=\"MathJax-Span-7\" style=\"font-family: MathJax_AMS;\">R</span></span></span><span class=\"mo\" id=\"MathJax-Span-8\" style=\"font-family: MathJax_Main; padding-left: 0.276em;\">→</span><span class=\"msubsup\" id=\"MathJax-Span-9\" style=\"padding-left: 0.276em;\"><span style=\"display: inline-block; position: relative; width: 1.138em; height: 0px;\"><span style=\"position: absolute; clip: rect(3.215em, 1000.71em, 4.116em, -999.998em); top: -3.995em; left: 0em;\"><span class=\"texatom\" id=\"MathJax-Span-10\"><span class=\"mrow\" id=\"MathJax-Span-11\"><span class=\"mi\" id=\"MathJax-Span-12\" style=\"font-family: MathJax_AMS;\">R</span></span></span><span style=\"display: inline-block; width: 0px; height: 3.999em;\"></span></span><span style=\"position: absolute; top: -4.387em; left: 0.707em;\"><span class=\"mn\" id=\"MathJax-Span-13\" style=\"font-size: 70.7%; font-family: MathJax_Main;\">2</span><span style=\"display: inline-block; width: 0px; height: 3.999em;\"></span></span></span></span></span><span style=\"display: inline-block; width: 0px; height: 2.588em;\"></span></span></span><span style=\"display: inline-block; overflow: hidden; vertical-align: -0.089em; border-left: 0px solid; width: 0px; height: 1.139em;\"></span></span></nobr><span class=\"MJX_Assistive_MathML\" role=\"presentation\"><math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mi>C</mi><mo>:</mo><mrow class=\"MJX-TeXAtom-ORD\"><mi mathvariant=\"double-struck\">R</mi></mrow><mo stretchy=\"false\">→</mo><msup><mrow class=\"MJX-TeXAtom-ORD\"><mi mathvariant=\"double-struck\">R</mi></mrow><mn>2</mn></msup></math></span></span><script type=\"math/tex\" id=\"MathJax-Element-1\">C: \\mathbb{{R}} \\to \\mathbb{{R}}^2</script> such that</p>",
					PubDate = new DateTime(2023, 01, 04),
					PostCoverPhoto = "plotting-prime-nubers-python.png",
					Tags = new[]
					{
						new PostTag{ TagName = "Maths"},
						new PostTag{ TagName = "Python"},
						new PostTag{ TagName = "Prime Numbers"}
					}
				},
				new Post {
					Title = "End-to-End Serverless Real-Time IoT with Python",
					Content = $"<p>In this article, we'll look at how to build an end-to-end IoT solution using Python. We'll send telemetry from a device to <a href=\"https://docs.microsoft.com/azure/iot-hub/about-iot-hub?WT.mc_id=anthonychuca-blog-antchu\">Azure IoT Hub</a>. Then we'll use <a href=\"https://docs.microsoft.com/azure/azure-functions/functions-overview?WT.mc_id=anthonychuca-blog-antchu\">Azure Functions</a> and <a href=\"https://docs.microsoft.com/azure/azure-signalr/?WT.mc_id=anthonychuca-blog-antchu\">Azure SignalR Service</a> to send messages from IoT Hub to a Python app in real-time.</p> <p>Azure IoT Hub is a managed service that facilitates bi-directional communication between your IoT application and the devices it manages. It has a lot of capabilities; we'll use it to ingest telemetry from IoT devices.</p>",
					PubDate = new DateTime(2019, 10, 13),
					PostCoverPhoto = "end-to-end-serverless.png",
					Tags = new[]
					{
						new PostTag{ TagName = "Azure"},
						new PostTag{ TagName = "Python"},
						new PostTag{ TagName = "Azure Functions"}
					}
				},
			};
			
			foreach (var item in _posts)
			{
				// Add Exceprt
				item.Excerpt = StringHelper.ShortenAndFormatText(item.Content, 512);
				item.Title = StringHelper.ToTitleCase(item.Title);
				//  Add slug
				item.Slug = StringHelper.CreateSlug(item.Title);
				//  Publish
				item.IsPublished = true;

				if (item.Tags != null)
				{
					foreach (var t in item.Tags)
					{
						//var tag = await context.PostTags.FirstOrDefaultAsync(x => x.Id == t.Id);
						//var tag = await context.PostTags.FindAsync(t.Id);
						t.TagNameEncoded = StringHelper.CreateTagSlug(t.TagName);
					}

				}

				//	Submit post so we get the id
				//await context.Posts.AddAsync(item);			

				InitialPostCollection.Add(item);
			}
		}

		private static async Task SavePost(MODbContext context)
		{
			foreach (var item in InitialPostCollection)
			{
				await context.Posts.AddAsync(item);
			}
			await context.SaveChangesAsync();
		}



		public static async Task InitDb(MODbContext context)
		{

			//  Generate Post
			Task taskPost = Task.Factory.StartNew(async delegate
			{ await GeneratePosts(context); });
			taskPost.Wait();

			if(taskPost.IsCompleted)
			{
				/*Task taskSavePost = Task.Factory.StartNew(async delegate
				{ await SavePost(context); });
				taskSavePost.Wait();*/

				InitialPostCollection.ForEach(p => context.Posts.Add(p));
				context.SaveChangesAsync().Wait();
			}

			/*if(taskPost.IsCompleted)
			{
				Task taskUpdatePostTag = Task.Factory.StartNew(async delegate
				{ await UpdatePostTag(context); });
				taskUpdatePostTag.Wait();

			}*/
		}
	}
}
