using System.Threading.Tasks;
using DotnetBleServer.Core;
using DotnetBleServer.Gatt;
using DotnetBleServer.Gatt.Description;

namespace bletest;

public class BleGattApplication
{
    public static async Task<bool> RegisterGattApplication(ServerContext serverContext)
    {
        var gattServiceDescription = new GattServiceDescription
        {
            UUID = BleParam.SERVICE_UUID,
            Primary = true
        };

        var gattCharacteristicDescription = new GattCharacteristicDescription
        {
            CharacteristicSource = new CharacteristicSource(),
            UUID = BleParam.DATA_CHARACTERISTIC_UUID,
            Flags = CharacteristicFlags.Read | CharacteristicFlags.Write | CharacteristicFlags.Notify
        };

        var gattDescriptorDescription = new GattDescriptorDescription
        {
            Value = new[] { (byte)'t' },
            UUID = BleParam.DESCRIPTOR_UUID,
            Flags = new[] { "read", "write" }
        };
        var gab = new GattApplicationBuilder();
        gab.AddService(gattServiceDescription).WithCharacteristic(gattCharacteristicDescription, new[] { gattDescriptorDescription });
        return await new GattApplicationManager(serverContext).RegisterGattApplication(gab.BuildServiceDescriptions());

    }
}
