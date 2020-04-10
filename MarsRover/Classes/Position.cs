using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRover.Models
{
    public enum Directions // Assigned for the direction types
    {
        N = 1,
        S = 2,
        W = 3,
        E = 4
    }

    public enum Commands // Assigned for the command types
    {
        M,
        L,
        R
    }

    public class Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Directions Direction { get; set; }

        public Position()
        {
            X = Y = 0;
            Direction = Directions.N;  
        }

        /// <summary>
        /// Checks given X and Y values for field size are integer or not
        /// </summary>
        /// <param name="maxValues"></param>
        /// <returns></returns>
        public bool IsValidFieldSize(string[] maxValues)
        {
            bool flag = true; // assigned to check the situation of condition
            int checkInt = 0;
            maxValues = maxValues.Where(a => a != "").ToArray();

            if (maxValues.Length == 2)
            {
                for (int i = 0; i < maxValues.Length; i++)
                {
                    flag = flag && int.TryParse(maxValues[i], out checkInt);
                    if (checkInt == 0)
                    {
                        flag = false;
                        break;
                    }
                }
            }
            else
            {
                return false;
            }

            return flag;
        }

        /// <summary>
        /// Checks whether assigned initial position for a rover is valid
        /// </summary>
        /// <param name="initialPosition"></param>
        /// <returns></returns>
        public bool IsValidInitialPosition(string[] initialPosition)
        {
            bool flag = true; // assigned to check the situation of condition
            int checkInt = 0;
            object directionType = null; //assigned to be able to use TryParse method
            initialPosition = initialPosition.Where(a => a != "").ToArray();

            if (initialPosition.Length == 3)
            {
                for (int i = 0; i < initialPosition.Length; i++)
                {
                    if (i != 2)
                    {
                        flag = flag && int.TryParse(initialPosition[i], out checkInt);
                        if (checkInt == 0)
                        { 
                            flag = false;
                            break;
                        }
                    }
                    else
                    {
                        flag = flag && Enum.TryParse(typeof(Directions), initialPosition[i], true, out directionType);
                        if (!flag)
                        {
                            break;
                        }
                
                    }
                }
            }
            else
            {
                return false;
            }

            return flag;
        }

        /// <summary>
        /// Controls that there is any invalid command exists in the list
        /// </summary>
        /// <param name="commandList"></param>
        /// <returns></returns>
        public bool CommandControl(char[] commandList)
        {
            bool flag = true; // assigned to check the situation of condition
            object cmdType = null; // assigned to be able to use TryParse method
            foreach (var command in commandList)
            {
                flag = flag && Enum.TryParse(typeof(Commands), command.ToString(), out cmdType);
                if (!flag)
                    break;
            }
            return flag;
        }

        /// <summary>
        /// Detects a rover that goes out of stated boundaries after that rover fulfills the commands.
        /// </summary>
        /// <param name="fieldSize"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsOutOfBound(FieldSize fieldSize, Position position)
        {
            if (position.X > fieldSize.X || position.Y > fieldSize.Y || position.X < 0 || position.Y < 0)
            {
                Console.WriteLine("Rover cannot go out of boundaries! You must enter the commands again ");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Set the new direction of a rover after it turns 90 degrees left
        /// </summary>
        public void LeftRotation90()
        {
            switch(this.Direction)
            {
                case Directions.N:
                    this.Direction = Directions.W;
                    break;
                case Directions.W:
                    this.Direction = Directions.S;
                    break;
                case Directions.S:
                    this.Direction = Directions.E;
                    break;
                case Directions.E:
                    this.Direction = Directions.N;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Set the new direction of a rover after it turns 90 degrees right
        /// </summary>
        public void RightRotation90()
        {
            switch(this.Direction)
            {
                case Directions.N:
                    this.Direction = Directions.E;
                    break;
                case Directions.E:
                    this.Direction = Directions.S;
                    break;
                case Directions.S:
                    this.Direction = Directions.W;
                    break;
                case Directions.W:
                    this.Direction = Directions.N;
                    break;
                default:
                    break;            
            }
        }

        /// <summary>
        /// Set the rover's new X or Y coordinate after that rover gets the command which orders to move forward
        /// </summary>
        public void MoveForward()
        {
            switch(this.Direction)
            {
                case Directions.N:
                    this.Y += 1;
                    break;
                case Directions.S:
                    this.Y -= 1;
                    break;
                case Directions.W:
                    this.X -= 1;
                    break;
                case Directions.E:
                    this.X += 1;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Set the rovers' final positions with making rovers fulfill the given commands one by one
        /// </summary>
        /// <param name="initialPosition"></param>
        /// <param name="commands"></param>
        /// <returns></returns>
        public Position Navigate(Position initialPosition, char[] commands)
        {
            foreach (var command in commands)
            {
                Commands cmdTypes = (Commands)Enum.Parse(typeof(Commands), command.ToString());
                switch (cmdTypes)
                {
                    case Commands.M:
                        this.MoveForward();
                        break;
                    case Commands.L:
                        this.LeftRotation90();
                        break;
                    case Commands.R:
                        this.RightRotation90();
                        break;
                    default:
                        break;
                }
            }
            return new Position { X = this.X, Y = this.Y, Direction = this.Direction };
        }

        /// <summary>
        /// Displays all rovers' final coordinates wih their facing directions
        /// </summary>
        /// <param name="positions"></param>
        public void DisplayFinalPositions(List<Position> positions)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                int roverCount = i+1;
                Console.WriteLine("Rover {0} Final Coordinates: {1} {2} {3}", roverCount, positions[i].X, positions[i].Y, positions[i].Direction);
            }
        }
    }
}
