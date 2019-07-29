#addin "Cake.Git"
#addin "Cake.FileHelpers"
#addin "nuget:http://6pak.opten.ch/nuget/v2-nuget/?package=Opten.Cake"

var target = Argument("target", "Default");

string feedUrl = "http://6pak.opten.ch/nuget";
string version = null;

var dest = Directory("./artifacts");

// Cleanup

Task("Clean")
	.Does(() =>
{
	if (DirectoryExists(dest))
	{
		CleanDirectory(dest);
		DeleteDirectory(dest, recursive: true);
	}
});

// Versioning

Task("Version")
	.IsDependentOn("Clean") 
	.Does(() =>
{
	if (DirectoryExists(dest) == false)
	{
		CreateDirectory(dest);
	}

	version = GitDescribe("../", false, GitDescribeStrategy.Tags, 0);

	PatchAssemblyInfo("../src/Opten.Web.Infrastructure.Pdf/Properties/AssemblyInfo.cs", version);
	FileWriteText(dest + File("Opten.Web.Infrastructure.Pdf.variables.txt"), "version=" + version);
});

// Building

Task("Restore-NuGet-Packages")
	.IsDependentOn("Version") 
	.Does(() =>
{ 
	NuGetRestore("../Opten.Web.Infrastructure.Pdf.sln", new NuGetRestoreSettings {
		NoCache = true
	});
});

Task("Build") 
	.IsDependentOn("Restore-NuGet-Packages") 
	.Does(() =>
{	
	MSBuild("../src/Opten.Web.Infrastructure.Pdf/Opten.Web.Infrastructure.Pdf.csproj", settings =>
		settings.SetConfiguration("Debug"));

	MSBuild("../src/Opten.Web.Infrastructure.Pdf/Opten.Web.Infrastructure.Pdf.csproj", settings =>
		settings.SetConfiguration("Release"));
});

Task("Pack")
	.IsDependentOn("Build")
	.Does(() =>
{
	NuGetPackWithDependencies("./Opten.Web.Infrastructure.Pdf.nuspec", new NuGetPackSettings {
		Version = version,
		BasePath = "../",
		OutputDirectory = dest
	}, feedUrl);
});

// Deploying

Task("Deploy")
	.Does(() =>
{
	// This is from the Bamboo's Script Environment variables
	string packageId = "Opten.Web.Infrastructure.Pdf";

	// Get the Version from the .txt file
	string version = EnvironmentVariable("bamboo_inject_" + packageId.Replace(".", "_") + "_version");

	if (string.IsNullOrWhiteSpace(version))
	{
		throw new Exception("Version is missing for " + packageId + ".");
	}

	// Get the path to the package
	var package = File(packageId + "." + version + ".nupkg");
            
	// Push the package
	NuGetPush(package, new NuGetPushSettings {
		Source = feedUrl,
		ApiKey = EnvironmentVariable("NUGET_API_KEY")
	});

	// Notifications
	Slack(new SlackSettings {
		ProjectName = "Opten.Web.Infrastructure.Pdf"
	});
});

Task("Default")
	.IsDependentOn("Pack");

RunTarget(target);