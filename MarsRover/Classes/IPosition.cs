using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Models
{
    public interface IPosition
    {
        Position Navigate(Position position, char[] commands);
    }
}
