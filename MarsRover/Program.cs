using MarsRover.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Position> positionList = new List<Position>();
            FieldSize fieldSize = new FieldSize();     
            Position position = new Position();
            char[] commandList;
            bool flag = true;
            
            
            Console.WriteLine("Enter the Field Size For the Rovers by Giving X and Y Coordinate Values: ");
            string[] maxValues = Console.ReadLine().Trim().Split(' ').Where(a => a != "").ToArray();
            while (!position.IsValidFieldSize(maxValues)) // Valid field size values must be assigned
            {             
                Console.WriteLine("Please Enter Valid Integer Values For X and Y Coordinates (Ex:5 5): ");
                maxValues = Console.ReadLine().Trim().Split(' ').Where(a => a != "").ToArray();
            }

            fieldSize.X = Convert.ToInt32(maxValues[0]);
            fieldSize.Y = Convert.ToInt32(maxValues[1]);

            while (flag)
            {
                Console.WriteLine("Enter an Initial Position For a Rover: ");
                string[] initialPosition = Console.ReadLine().Trim().ToUpper().Split(' ').Where(a => a != "").ToArray();
                while (!position.IsValidInitialPosition(initialPosition)) // Valid position values must be given
                {
                    Console.WriteLine("Please Enter Valid Initial Position For the Rover (Ex: 1 3 N): ");
                    initialPosition = Console.ReadLine().Trim().Split(' ').Where(a => a != "").ToArray();
                }

                do
                {
                    position.X = Convert.ToInt32(initialPosition[0]);
                    position.Y = Convert.ToInt32(initialPosition[1]);
                    position.Direction = (Directions)Enum.Parse(typeof(Directions), initialPosition[2], true);


                    Console.WriteLine("Enter Commands For the Rover: ");
                    commandList = Console.ReadLine().Trim().ToUpper().ToCharArray();
                    while (!position.CommandControl(commandList)) // Command list must not include an unstated command
                    {
                        Console.WriteLine("Please Create Valid Command List (Ex: MMLMRLMML): ");
                        commandList = Console.ReadLine().Trim().ToUpper().ToCharArray();
                    }
                    
                } while (!position.IsOutOfBound(fieldSize, position.Navigate(position, commandList))); // Users must enter new command list 
                                                                                                       // if old command list makes the rover goes out of boundaries  
                position.X = Convert.ToInt32(initialPosition[0]);
                position.Y = Convert.ToInt32(initialPosition[1]);
                position.Direction = (Directions)Enum.Parse(typeof(Directions), initialPosition[2], true);

                positionList.Add(position.Navigate(position, commandList));

                do
                {
                    Console.Write("Is there another rover you want to navigate (Y/N)? ");
                    char choice = Convert.ToChar(Console.ReadLine().ToUpper());
                    if (choice == 'N')
                    {
                        position.DisplayFinalPositions(positionList);
                        flag = false;
                        break;
                    }
                    else if (choice == 'Y')
                    {
                        flag = true;
                        break;
                    }

                } while (flag);
               

            }
        }
    }
}
