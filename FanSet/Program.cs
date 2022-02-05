using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;

namespace FanSet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();

            Dictionary<string, int> pairs = new Dictionary<string, int>();

            int j;
            string k;
            for (int i = 0; i < args.Length-1; i+=2)
            {
                k = args[i].ToLower();

                if (int.TryParse(args[i + 1], out j))
                {
                    if (j >= 0 && j <= 100)
                    {
                        if (!pairs.ContainsKey(k))
                            pairs.Add(k, j);
                        else
                            pairs[k] = j;
                    }
                }
            }

            Computer computer = new Computer
            {
                GPUEnabled = false,
                CPUEnabled = false,
                MainboardEnabled = true,
                HDDEnabled = false,
                RAMEnabled = false,
                FanControllerEnabled = true
            };

            computer.Open();

            foreach (var hardware in computer.Hardware)
            {
                foreach (var subhardware in hardware.SubHardware)
                {
                    subhardware.Update();
                    
                    foreach (var sensor in subhardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Control)
                        {
                            if (pairs.Count == 0)
                            {
                                Console.Write(sensor.Name);
                                Console.Write(" == ");
                                Console.Write(Math.Round(sensor.Value ?? 0));
                                Console.WriteLine('%');
                            }
                            else
                            {
                                k = sensor.Name.ToLower();

                                if (pairs.TryGetValue(k, out j))
                                {
                                    Console.Write(sensor.Name);
                                    Console.Write(" = ");
                                    sensor.Control.SetSoftware(j);
                                    Console.Write(j);
                                    Console.WriteLine("%");
                                }
                            }
                        }
                    }
                }
            }
            //computer.Close(); -- resets the fans, bad

            //finally a nice usecase for reflection
            Type.GetType("OpenHardwareMonitor.Hardware.Ring0, OpenHardwareMonitorLib").GetMethod("Close").Invoke(null, null);
            Type.GetType("OpenHardwareMonitor.Hardware.Opcode, OpenHardwareMonitorLib").GetMethod("Close").Invoke(null, null);
        }
    }
}
