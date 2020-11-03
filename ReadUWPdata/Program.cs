using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;

namespace ReadUWPdata
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = @"C:\Program Files (x86)\Windows Kits\10\App Certification Kit\microsoft.windows.softwarelogo.appxlauncher.exe HIDPictireCapture_v94a07n04mgnm!HIDPictireCapture";
            ////"URL:test-app2app";//@"shell:appsFolder\HIDPictireCapture_v94a07n04mgnm!HIDPictireCapture";
            //startInfo.UseShellExecute = false;

            //Process process = new Process();
            // process.StartInfo = startInfo;
            //process.StartInfo.CreateNoWindow = false;
            //process.StartInfo.RedirectStandardOutput = true;

            //process.Start();


            //var testAppUri = new Uri("test-app2app:"); // The protocol handled by the launched app
            //var options = new LauncherOptions();
            //options.TargetApplicationPackageFamilyName = "HIDPictireCapture_v94a07n04mgnm";

            //var inputData = new ValueSet();
            //inputData["TestData"] = "Test data";

            //string theResult = "";
            //var result = Windows.System.Launcher.LaunchUriForResultsAsync(testAppUri, options, inputData);
            //if (result.Status == LaunchUriStatus.Success &&
            //    result.Result != null &&
            //    result.Result.ContainsKey("ReturnedData"))
            //{
            //    ValueSet theValues = result.Result;
            //    theResult = theValues["ReturnedData"] as string;
            //}

            try
            {

                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files (x86)\Windows Kits\10\App Certification Kit\microsoft.windows.softwarelogo.appxlauncher.exe",
                    Arguments = "HIDPictireCapture_v94a07n04mgnm!HIDPictireCapture",
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                };

                ExecuteProcessAsync(processStartInfo).Wait();

                //Process appProcess = Process.Start(processStartInfo);
                //await Task.Delay(3000);
                //var processes = Process.GetProcessesByName("QSPictireCaptureUWP");
                //Process picCaptureProcess = processes.FirstOrDefault();
                ////appProcess.StartInfo = processStartInfo;

                ////appProcess.Start();
                //StringBuilder sb = new StringBuilder();
                //while (!picCaptureProcess.HasExited)
                //{
                //    sb.Append(picCaptureProcess.StandardOutput.ReadToEnd());
                //    Console.WriteLine(sb.ToString());
                //}


                /* var newProcess = Process.Start(@"C:\Program Files (x86)\Windows Kits\10\App Certification Kit\microsoft.windows.softwarelogo.appxlauncher.exe",
                                "HIDPictireCapture_v94a07n04mgnm!HIDPictireCapture");
                 await Task.Delay(3000);
                 var processes = Process.GetProcessesByName("QSPictireCaptureUWP");

                 Process picCaptureProcess = processes.FirstOrDefault();

                 if (picCaptureProcess == null)
                 {
                     Console.WriteLine("No Process with HIDPictireCapture is running");
                     Console.ReadKey();
                     return;
                 }

                 picCaptureProcess.StartInfo.RedirectStandardError = true;
                 picCaptureProcess.StartInfo.RedirectStandardOutput = true;
                 picCaptureProcess.StartInfo.CreateNoWindow = false;


                 //picCaptureProcess.StartInfo = new ProcessStartInfo
                 //{
                 //    RedirectStandardError = true,
                 //    RedirectStandardOutput = true,
                 //    CreateNoWindow = false
                 //};

                 StringBuilder sb = new StringBuilder();
                 picCaptureProcess.OutputDataReceived += (s, e) =>
                 {
                     lock (sb)
                     {
                         sb.Append(e.Data);
                     }
                 };
                 //picCaptureProcess.ErrorDataReceived += (s, e) => {
                 //    lock (sb)
                 //    {
                 //        sb.Append("! > " + e.Data);
                 //    }
                 //};

                 //picCaptureProcess.BeginErrorReadLine();
                 //picCaptureProcess.BeginOutputReadLine();

                 picCaptureProcess.WaitForExit();

                 while (!picCaptureProcess.HasExited)
                 {
                     sb.Append(picCaptureProcess.StandardOutput.ReadToEnd());
                     Console.WriteLine(sb.ToString());
                 }
                 //if (picCaptureProcess.HasExited)
                 //{
                 //    //sb.Append(picCaptureProcess.StandardOutput.ReadToEnd());
                 //    Console.WriteLine(sb.ToString());
                 //    Console.WriteLine("process is stopped");
                 //}
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred {ex}");
                Console.ReadKey();
            }
        }


        private static Task<bool> ExecuteProcessAsync(ProcessStartInfo psi)
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = psi,
                EnableRaisingEvents = true,
            };

            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            process.Exited += (sender, e) =>
            {
                try
                {
                    process.WaitForExit();
                    tcs.TrySetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            };

            process.Start();

            if (psi.RedirectStandardOutput)
            {
                process.OutputDataReceived += (s, e) => {
                    Console.WriteLine(e.Data); 
                };
                process.BeginOutputReadLine();
            }

            //if (psi.RedirectStandardError)
            //{
            //    process.ErrorDataReceived += (s, e) => { Console.WriteLine(e.Data); };
            //    process.BeginErrorReadLine();
            //}

            //if (psi.RedirectStandardInput)
            //{
            //    process.StandardInput.Close();
            //}

            return tcs.Task;
        }
    }
}
