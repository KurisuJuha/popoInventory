using System;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class InventorySettings<ItemType>
    {
        public Func<ItemType, int> getMaxAmount { get; private set; }
        public Func<ItemType> getEmptyItem { get; private set; }

        public InventorySettings(Func<ItemType, int> getMaxAmount, Func<ItemType> getEmptyItem)
        {
            this.getMaxAmount = getMaxAmount;
            this.getEmptyItem = getEmptyItem;
        }

        public InventoryGrid<ItemType> CreateEmptyGrid() => new InventoryGrid<ItemType>(this);
        public InventoryGrid<ItemType> CreateGrid(ItemType item) => new InventoryGrid<ItemType>(item, this);

        public Inventory<ItemType> CreateInventory(int size) => new Inventory<ItemType>(size, this);
    }
}