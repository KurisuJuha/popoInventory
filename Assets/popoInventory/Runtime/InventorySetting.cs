using System;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    [Serializable]
    public class InventorySetting<ItemType>
    {
        public Func<ItemType, int> getMaxAmount { get; private set; }
        public Func<ItemType> getEmptyItem { get; private set; }

        public InventorySetting(Func<ItemType, int> getMaxAmount, Func<ItemType> getEmptyItem)
        {
            this.getMaxAmount = getMaxAmount;
            this.getEmptyItem = getEmptyItem;
        }

        public Grid<ItemType> CreateEmptyGrid() => new Grid<ItemType>(this);

        public Inventory<ItemType> CreateInventory(int size) => new Inventory<ItemType>(size, this);
    }
}