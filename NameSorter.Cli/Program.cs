
using NameSorter.Core.Application;
using NameSorter.Core.IOHelper;
using NameSorter.Core.ParseHelper;
using NameSorter.Core.SortHelper;

if (args.Length != 1)
{
    return 1;
}

var inputPath = args[0];

var fileProvider = new FileProvider();
var parser = new NameParser();
var sorter = new NamesSorter();
var nameSorterApp = new NameSorterApp(fileProvider, parser, sorter);

try
{
    return await nameSorterApp.RunAsync(inputPath);
}
catch (Exception ex)
{
    Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
}
    return 1;