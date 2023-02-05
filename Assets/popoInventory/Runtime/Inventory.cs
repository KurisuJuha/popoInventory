using System;
using System.Collections.ObjectModel;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class Inventory<ItemType>
    {
        public ReadOnlyCollection<Grid<ItemType>> grids => Array.AsReadOnly(_grids);
        readonly Grid<ItemType>[] _grids;
        event Func<ItemType, int> getMaxAmount;
        event Func<ItemType> getEmptyItem;

        public Inventory(int size, Func<ItemType, int> getMaxAmount, Func<ItemType> getEmptyItem)
        {
            _grids = new Grid<ItemType>[size];
            this.getMaxAmount = getMaxAmount;
            this.getEmptyItem = getEmptyItem;


            for (int i = 0; i < size; i++) _grids[i] = new Grid<ItemType>(getEmptyItem.Invoke(), getMaxAmount, getEmptyItem);
        }
    }
}