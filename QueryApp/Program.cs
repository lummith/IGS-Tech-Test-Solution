using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using QueryApp;
using QueryProcessor;

try
{ 
    // get input
    string fileInput = string.Empty;
    if (File.Exists(args[0]))
        fileInput = File.ReadAllText(args[0]);
    else
    {
        Console.WriteLine("Input file does not exist in the location specified.");
        Console.ReadKey(true);
        System.Environment.Exit(0);
    }

    // send query to api
    SendAgent sa = new SendAgent();
    var instructions = await sa.sendAndReceive();

    if (string.IsNullOrEmpty(instructions)) 
    {
        Console.ReadKey(true);
        System.Environment.Exit(0);
    }
        

    string errorMessage = string.Empty;
    Processor qp = new Processor();
    var schedule = qp.ProcessQuery(fileInput, instructions, out errorMessage);

    qp.outputSchedul(schedule);

}
catch (TaskCanceledException tce)
{
    Console.WriteLine($"Exception occured while attempting: {tce.Message}");
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}

 
// build message
// send message 
// config file
// process response