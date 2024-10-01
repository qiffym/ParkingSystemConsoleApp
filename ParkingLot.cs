using System.Text.RegularExpressions;

namespace ParkingSystemConsoleApp
{
    internal class ParkingLot(int totalLots)
    {
        public int TotalLots { get; private set; } = totalLots;
        public Dictionary<int, Vehicle> Lots { get; private set; } = [];
        public Queue<int> AvailableSlots { get; private set; } = new Queue<int>(Enumerable.Range(1, totalLots));

        public bool CheckIn(Vehicle vehicle)
        {
            if (Lots.Values.Any(v => v.PlateNumber == vehicle.PlateNumber))
            {
                Console.WriteLine("Vehicle already exists \n");
                return false;
            }

            if (AvailableSlots.Count > 0)
            {
                int slot = AvailableSlots.Dequeue();
                Lots.Add(slot, vehicle);
                Console.WriteLine($"Allocated slot number: {slot} \n");
                return true;
            }

            Console.WriteLine("Sorry, parking lot is full \n");
            return false;
        }

        public bool CheckOut(int slotNumber)
        {
            if (Lots.ContainsKey(slotNumber))
            {
                Lots.Remove(slotNumber);
                AvailableSlots.Enqueue(slotNumber);
                Console.WriteLine($"Slot number {slotNumber} is free \n");
                return true;
            }
            Console.WriteLine("Slot number not found \n");
            return false;
        }

        public void Status()
        {
            Console.WriteLine("Slot\tNo.Registration\t\tType\tColour");
            foreach (var lot in Lots)
            {
                Console.WriteLine($"{lot.Key}\t{lot.Value.PlateNumber}\t\t{lot.Value.Type}\t{lot.Value.Color}");
            }
            Console.WriteLine("");
        }

        public void ReportByType(VehicleType type)
        {
            Console.WriteLine(Lots.Values.Count(v => v.Type == type) + "\n");
        }

        public void ReportByPlateOddEven(bool isOdd)
        {
            var vehicles = Lots.Values.Where(v =>
            {
                var match = Regex.Match(v.PlateNumber, @"\d+");
                if (match.Success)
                {
                    int number = int.Parse(match.Value);
                    return (number % 2 != 0) == isOdd;
                }
                return false;
            });
            Console.WriteLine(string.Join(", ", vehicles.Select(v => v.PlateNumber)) + "\n");
        }

        public void ReportByColor(string color)
        {
            var vehicles = Lots.Values.Where(v => v.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(string.Join(", ", vehicles.Select(v => v.PlateNumber)) + "\n");
        }

        public void ReportSlotsByColor(string color)
        {
            var slots = Lots.Where(l => l.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase)).Select(l => l.Key);
            Console.WriteLine(string.Join(", ", slots) + "\n");
        }

        public void ReportSlotByPlate(string plateNumber)
        {
            var slot = Lots.FirstOrDefault(l => l.Value.PlateNumber == plateNumber).Key;
            if (slot != 0)
                Console.WriteLine(slot + "\n");
            else
                Console.WriteLine("Not found \n");
        }
    }
}
