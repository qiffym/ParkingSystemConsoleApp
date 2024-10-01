namespace ParkingSystemConsoleApp
{
    internal class Vehicle(string plateNumber, VehicleType type, string color)
    {
        public string PlateNumber { get; private set; } = plateNumber;
        public VehicleType Type { get; private set; } = type;
        public string Color { get; private set; } = color;
    }
}
