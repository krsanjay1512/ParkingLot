using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Parking
    {
        public List<ParkingSlot> Slot { get; set; }
        public List<ParkingTicket> Ticket { get; set; }
        int ticketId = 1;
        int slotNumber = 1;
        public void Initialization()
        {
            try
            {
                this.Slot = new List<ParkingSlot>();
                this.Ticket = new List<ParkingTicket>();
                Console.WriteLine("No. of Two Wheelers Slots : ");
                int TwoWheelerSpace = int.Parse(Console.ReadLine());
                for (int i = 0; i < TwoWheelerSpace; i++)
                {
                    this.Slot.Add(new ParkingSlot(slotNumber++, VehicleSlotType.TwoWheelerSlot, false));
                }
                Console.WriteLine("No. of Four Wheelers Slots : ");
                int FourWheelerSpace = int.Parse(Console.ReadLine());
                for (int i = 0; i < FourWheelerSpace; i++)
                {
                    this.Slot.Add(new ParkingSlot(slotNumber++, VehicleSlotType.FourWheelerSlot, false));
                }
                Console.WriteLine("No. of Heavy Vehicles Slots : ");
                int HeavyVehicleSpace = int.Parse(Console.ReadLine());
                for (int i = 0; i < HeavyVehicleSpace; i++)
                {
                    this.Slot.Add(new ParkingSlot(slotNumber++, VehicleSlotType.HeavyVehicleSlot, false));
                }
                int TotalVehicleSpace = TwoWheelerSpace + FourWheelerSpace + HeavyVehicleSpace;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
              
            }
            
        }
        public void ParkVehicleOption()
        {
            try
            {
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1. Press 1 to park a Two Wheeler Vehicle: ");
                Console.WriteLine("2. Press 2 to park a Four Wheeler  Vehicle: ");
                Console.WriteLine("3. Press 3 to park a Heavy Vehile: ");
                int UserOption = int.Parse(Console.ReadLine());
                switch (UserOption)
                {
                    case 1:
                        ParkVehicle(VehicleSlotType.TwoWheelerSlot);
                        break;
                    case 2:
                        ParkVehicle(VehicleSlotType.FourWheelerSlot);
                        break;
                    case 3:
                        ParkVehicle(VehicleSlotType.HeavyVehicleSlot);
                        break;
                    default:
                        Console.WriteLine("Enter valid option");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void ParkVehicle(VehicleSlotType vehicleSlot)
        {
            if (Slot.Any(i => i.VehicleSlotType == vehicleSlot && i.VehicleOccupancyStatus == false)) //
            {
                var slot = this.Slot.FirstOrDefault(i => i.VehicleSlotType == vehicleSlot && i.VehicleOccupancyStatus == false);
                slot.VehicleOccupancyStatus = true;
                Console.WriteLine("Enter Vehicle Number");
                string VehicleNumber = Console.ReadLine();
                AcknowledgeTicket(ticketId, VehicleNumber, DateTime.Now, slot.SlotNumber);
                ticketId++;
            }
            else
            {
                Console.WriteLine("Empty slot not available");
            }
        }
        public void AcknowledgeTicket(int ticketid, string vehicleNumber, DateTime inTImeTicket, int slotNumber)
        {
            this.Ticket.Add(new ParkingTicket(ticketid, vehicleNumber, inTImeTicket, slotNumber));
            Console.WriteLine("<==------Successfully Parked-----==>");
            Console.WriteLine();
            Console.WriteLine("Ticket Id: " + ticketid);
            Console.WriteLine("Vehicle Number: " + vehicleNumber);
            Console.WriteLine("Slot Number: " + slotNumber);
            Console.WriteLine("In Time: " + inTImeTicket);
            Console.WriteLine("==-----Thank You-----==");
        }
        public void UnParkVehicle()
        {
            Console.Clear();
            Console.WriteLine("Enter the Ticket ID");
            int EnteredId = int.Parse(Console.ReadLine());
            var RequiredTicket = Ticket.FirstOrDefault(i => i.Id == EnteredId);
            RequiredTicket.OutTimeTicket = DateTime.Now;
            Slot.FirstOrDefault(i => i.SlotNumber == RequiredTicket.SlotNumber).VehicleOccupancyStatus = false;
            Console.WriteLine("<==-----Successfully Unparked-----==>");
            Console.WriteLine("Ticket Id: ", RequiredTicket.Id);
            Console.WriteLine("Vehicle Number: " + RequiredTicket.VehicleNumber);
            Console.WriteLine("Slot Number: " + RequiredTicket.SlotNumber);
            Console.WriteLine("In Time: " + RequiredTicket.InTimeTicket);
            Console.WriteLine("Out Time: " + RequiredTicket.OutTimeTicket);
            Console.WriteLine("<==-----Thank You-----==>");
        }
        public void Options()
        {
            Console.WriteLine("Enter Your Choice");
            Console.WriteLine("1) Park a Vehicle: ");
            Console.WriteLine("2) Unpark a Vehicle: ");
            Console.WriteLine("3) Exit");
            int UserOption = int.Parse(Console.ReadLine());
            switch (UserOption)
            {
                case 1:
                    ParkVehicleOption();
                    break;
                case 2:
                    UnParkVehicle();
                    break;
                case 3:
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Please Re-enter correct option!!");
                    break;
            }
        }
    }
    public enum VehicleSlotType
    {
        TwoWheelerSlot,
        FourWheelerSlot,
        HeavyVehicleSlot
    }
    public class ParkingSlot
    {
        public ParkingSlot(int slotNumber, VehicleSlotType vehicleSlotType, bool vehicleOccupancyStatus)
        {
            SlotNumber = slotNumber;
            VehicleSlotType = vehicleSlotType;
            VehicleOccupancyStatus = vehicleOccupancyStatus;
        }
        public int SlotNumber { get; set; }
        public VehicleSlotType VehicleSlotType { get; set; }
        public bool VehicleOccupancyStatus { get; set; }
    }
    public class ParkingTicket
    {
        public ParkingTicket(int id, string vehicleNumber, DateTime inTimeTicket, int slotNumber)
        {
            Id = id;
            VehicleNumber = vehicleNumber;
            InTimeTicket = inTimeTicket;
            SlotNumber = slotNumber;
        }
        public int Id { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime InTimeTicket { get; set; }
        public int SlotNumber { get; set; }
        public DateTime OutTimeTicket { get; set; }
    }
    class program
    {
        public static void Main(String[] argas)
        {
            Parking park = new Parking();
            park.Initialization();
            while (true)
            {
                park.Options();
            }
        }
    }
}

