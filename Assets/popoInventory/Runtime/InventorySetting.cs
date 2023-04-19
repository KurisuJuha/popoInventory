using System;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class InventorySettings<ItemType>
    {
        public readonly Func<ItemType, int> getMaxAmount;
        public readonly Func<ItemType> getEmptyItem;
        public readonly Func<ItemType, ItemType> copyItem;

        public InventorySettings(Func<ItemType, int> getMaxAmount, Func<ItemType> getEmptyItem, Func<ItemType, ItemType> copyItem)
        {
            this.getMaxAmount = getMaxAmount;
            this.getEmptyItem = getEmptyItem;
            this.copyItem = copyItem;
        }

        public InventoryGrid<ItemType> CreateEmptyGrid() => new InventoryGrid<ItemType>(this);
        public InventoryGrid<ItemType> CreateGrid(ItemType item) => new InventoryGrid<ItemType>(item, this);

        public Inventory<ItemType> CreateInventory(int size) => new Inventory<ItemType>(size, this);
    }
}