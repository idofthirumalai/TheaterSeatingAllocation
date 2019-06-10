using System;
using System.Collections.Generic;
using TheaterSearch.Business.Handlers;
using TheaterSearch.Business.Helpers;
using TheaterSearch.Business.Interfaces;
using TheaterSeating.Interfaces;

namespace TheaterSeating
{
    public class Program
    {
        //Main program to read the Theater layout and Ticket Requests from user as inputs
        /** Sample input:
            6 6
            3 5 5 3
            4 6 6 4
            2 8 8 2
            6 6 
 
            Smith 2
            Jones 5
            Davis 6
            Wilson 100
            Johnson 3
            Williams 4
            Brown 8
            Miller 12
         * */
        public static void Main(string[] args)
        {
            List<string> inputAllLines = new List<string>();

            Console.WriteLine("Please provide the inputs (Theater Layout and Ticket Requests) and end with '#'.");

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "#")
                    break;
                
                inputAllLines.Add(input);
            }
            
            ITheaterSearchHelper theaterSearchHelper = new TheaterSearchHelper();
            ITheaterSearch theaterSearch = new TheaterSeatingSearch(theaterSearchHelper);

            TheaterSeat theaterSeat = new TheaterSeat(theaterSearch);

            try
            {
                //Passing the inputs and get the theater seats
                var requests = theaterSeat.GetTheaterSeats(inputAllLines);

                Console.WriteLine("\n*****Theater Seats Allocation..*****\n");

                //Printing the output
                foreach (var request in requests)
                {
                    Console.WriteLine(request);
                }

                Console.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine("Internal Error occured" + e.StackTrace);
            }
        }
    }
}
