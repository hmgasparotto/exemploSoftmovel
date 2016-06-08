using CoreBluetooth;
using System;
using System.Collections.Generic;
using System.Text;
using Foundation;

namespace ExemploSoftmovel.iOS
{
    public class SoftmovelCentralManagerDelegate : CBCentralManagerDelegate
    {
        public override void UpdatedState(CBCentralManager central)
        {
            CBUUID[] ids = null;
            central.ScanForPeripherals(ids);
        }

        public override void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI)
        {
            base.DiscoveredPeripheral(central, peripheral, advertisementData, RSSI);
        }
    }
}
