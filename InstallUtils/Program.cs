using System;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace InstallUtils
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("'Install-Util' Started.");

            try
            {
                if (args.Length != 3)
                    throw new Exception("Invalid parameters.");

                string action = args[0].ToUpper();
                string arqOS = args[1].ToUpper();
                string targetDir = args[2].Replace("\"", ""); ;
                string batchFile = "";

                switch (action)
                {
                    case "INSTALL":
                        batchFile = "APP_Install.bat";
                        break;
                    case "COMMIT":
                        batchFile = "APP_Commit.bat";
                        break;
                    case "ROLLBACK":
                        batchFile = "APP_Rollback.bat";
                        break;
                    case "UNINSTALL":
                        batchFile = "APP_Uninstall.bat";
                        break;
                    default:
                        throw new Exception("Invalid action");
                }

                System.Console.WriteLine("Action:" + action);
                System.Console.WriteLine("targetDir:" + targetDir);

                // RUN EXTERNAL PROGRAM //
                Process process = new Process();
                process.StartInfo.Arguments = arqOS + " \"" + targetDir + "\"";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.FileName = Path.Combine(targetDir, batchFile);

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.StartInfo.CreateNoWindow = true;
                //process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                //events
                process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                        Console.WriteLine(e.Data);
                });

                process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                        Console.WriteLine(e.Data);
                });

                process.EnableRaisingEvents = true;

                Console.WriteLine("StartInfo.FileName:" + process.StartInfo.FileName);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();

                if (process.ExitCode != 0)
                    throw new Exception(String.Format("'{0}' terminated with ExitCode={1}", batchFile, process.ExitCode.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("'Install-Util' ERROR: {0}", ex.Message);
                Thread.Sleep(15000);
                Environment.Exit(1);
            }

            Console.WriteLine("'Install-Util' Finished SUCCESSFULLY.");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }

    }
}
