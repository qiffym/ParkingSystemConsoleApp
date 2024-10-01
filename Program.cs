namespace ParkingSystemConsoleApp
{
    internal class Program
    {
        private static ParkingLot? parkingLot;

        public static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            while (true)
            {
                var input = Console.ReadLine().Split(' ');
                var command = input[0];

                if (command == "create_parking_lot")
                {
                    CreateParkingLot(input);
                    continue;
                }

                if (parkingLot == null)
                {
                    Console.WriteLine("Parking lot has not been created yet.");
                    continue;
                }

                switch (command)
                {
                    case "park":
                        ParkVehicle(input);
                        break;

                    case "leave":
                        LeaveSlot(input);
                        break;

                    case "status":
                        parkingLot.Status();
                        break;

                    case "type_of_vehicles":
                        ReportByType(input);
                        break;

                    case "registration_numbers_for_vehicles_with_odd_plate":
                        parkingLot.ReportByPlateOddEven(true);
                        break;

                    case "registration_numbers_for_vehicles_with_even_plate":
                        parkingLot.ReportByPlateOddEven(false);
                        break;

                    case "registration_numbers_for_vehicles_with_colour":
                        ReportByColor(input);
                        break;

                    case "slot_numbers_for_vehicles_with_colour":
                        ReportSlotsByColor(input);
                        break;

                    case "slot_number_for_registration_number":
                        ReportSlotByPlate(input);
                        break;

                    case "exit":
                        return;

                    default:
                        Console.WriteLine("Invalid command \n");
                        break;
                }
            }
        }

        private static void CreateParkingLot(string[] input)
        {
            int totalLots = int.Parse(input[1]);
            parkingLot = new ParkingLot(totalLots);
            Console.WriteLine($"Created a parking lot with {totalLots} slots \n");
        }

        private static void ParkVehicle(string[] input)
        {
            var plateNumber = input[1];
            var color = input[2];
            var type = (VehicleType)Enum.Parse(typeof(VehicleType), input[3], true);
            var vehicle = new Vehicle(plateNumber, type, color);
            parkingLot?.CheckIn(vehicle);
        }

        private static void LeaveSlot(string[] input)
        {
            int slotNumber = int.Parse(input[1]);
            parkingLot?.CheckOut(slotNumber);
        }

        private static void ReportByType(string[] input)
        {
            var vehicleType = (VehicleType)Enum.Parse(typeof(VehicleType), input[1], true);
            parkingLot?.ReportByType(vehicleType);
        }

        private static void ReportByColor(string[] input)
        {
            var reportColor = input[1];
            parkingLot?.ReportByColor(reportColor);
        }

        private static void ReportSlotsByColor(string[] input)
        {
            var slotColor = input[1];
            parkingLot?.ReportSlotsByColor(slotColor);
        }

        private static void ReportSlotByPlate(string[] input)
        {
            var plateNumber = input[1];
            parkingLot?.ReportSlotByPlate(plateNumber);
        }
    }
}