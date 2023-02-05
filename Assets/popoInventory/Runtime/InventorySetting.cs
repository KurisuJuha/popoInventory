using System;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class InventorySetting<ItemType>
    {
        public Func<ItemType, int> getMaxAmount { get; private set; }
        public Func<ItemType> getEmptyItem { get; private set; }

        public InventorySetting(Func<ItemType, int> getMaxAmount, Func<ItemType> getEmptyItem)
        {
            this.getMaxAmount = getMaxAmount;
            this.getEmptyItem = getEmptyItem;
        }
    }
}