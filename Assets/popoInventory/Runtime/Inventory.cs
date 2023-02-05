using System;
using System.Collections.ObjectModel;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class Inventory<ItemType>
    {
        public ReadOnlyCollection<Grid<ItemType>> grids => Array.AsReadOnly(_grids);
        readonly Grid<ItemType>[] _grids;
        readonly InventorySetting<ItemType> setting;

        public Inventory(int size, InventorySetting<ItemType> setting)
        {
            _grids = new Grid<ItemType>[size];
            this.setting = setting;

            for (int i = 0; i < size; i++) _grids[i] = new Grid<ItemType>(setting);
        }
    }
}