using CoreBluetooth;
using System;
using System.Collections.Generic;
using System.Text;
using Foundation;

namespace ExemploSoftmovel.iOS
{
    public class SoftmovelPeripheralDelegate : CBPeripheralDelegate
    {
        public override void UpdatedValue(CBPeripheral peripheral, CBDescriptor descriptor, NSError error)
        {
            base.UpdatedValue(peripheral, descriptor, error);
        }

        public override void DiscoveredService(CBPeripheral peripheral, NSError error)
        {
            base.DiscoveredService(peripheral, error);
        }
    }
}
