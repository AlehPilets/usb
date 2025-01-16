using System;
using System.Management;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Waiting for the change... Click ENTER to exit.");

        ManagementEventWatcher connectWatcher = new ManagementEventWatcher();
        connectWatcher.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
        connectWatcher.EventArrived += (sender, eventArgs) =>
        {
           Console.Clear();
            Console.WriteLine($"USB device is connected. \n\nClick ENTER to exit.");
        };
        connectWatcher.Start();

        ManagementEventWatcher disconnectWatcher = new ManagementEventWatcher();
        disconnectWatcher.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
        disconnectWatcher.EventArrived += (sender, eventArgs) =>
        {
           Console.Clear();
            Console.WriteLine($"USB device is disconnected. \n\nClick ENTER to exit.");
        };
        disconnectWatcher.Start();

        Console.ReadLine();

        connectWatcher.Stop();
        disconnectWatcher.Stop();
        connectWatcher.Dispose();
        disconnectWatcher.Dispose();
    }
}