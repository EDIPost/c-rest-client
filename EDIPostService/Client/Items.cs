using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDIPostService.Client
{
    public class Items
    {
        public List<Item> items = new List<Item>();


        /// <summary>
        /// Adds the item to the container
        /// </summary>
        /// <param name="i">Item to be added</param>
        public void addItem(Item i)
        {
            items.Add(i);
        }
    }
}
