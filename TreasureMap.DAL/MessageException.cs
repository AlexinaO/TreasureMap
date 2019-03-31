using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap.DAL
{
    /// <summary>
    /// Class to display a message for exception
    /// </summary>
    public class MessageException : Exception
	{
		public MessageException(string message) : base(message)
		{ }
	}
}
