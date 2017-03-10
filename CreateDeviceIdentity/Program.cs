using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using Microsoft.ServiceBus;

namespace CreateDeviceIdentity
{
    internal class Program
    {
        private static RegistryManager _registryManager;

        public static void Main(string[] args)
        {
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;
            _registryManager = RegistryManager.CreateFromConnectionString(Config.Config.IoTHubConnectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }

        private static async Task AddDeviceAsync()
        {
            Device device;
            try
            {
                device = await _registryManager.AddDeviceAsync(new Device(Config.Config.DeviceName));
                Trace.WriteLine(device.Authentication.SymmetricKey.PrimaryKey);
                Console.WriteLine($"Generated device key: {device.Authentication.SymmetricKey.PrimaryKey}");
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await _registryManager.GetDeviceAsync(Config.Config.DeviceName);
                Trace.WriteLine(device.Authentication.SymmetricKey.PrimaryKey);
                Console.WriteLine($"Found existing device key: {device.Authentication.SymmetricKey.PrimaryKey}");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + ex.InnerException?.Message);
            }
        }
    }
}
