using System;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class Grid<ItemType>
    {
        public ItemType item
        {
            get => amount > 0 ? _item : setting.getEmptyItem.Invoke();
            private set => _item = value;
        }
        ItemType _item;
        public int amount
        {
            get => _amount;
            set => _amount = Math.Clamp(value, 0, maxAmount);
        }
        int _amount;
        public int maxAmount => setting.getMaxAmount.Invoke(item);
        public readonly InventorySetting<ItemType> setting;

        public Grid(InventorySetting<ItemType> setting)
        {
            this.item = setting.getEmptyItem();
            amount = 0;
            this.setting = setting;
        }

        public Grid(ItemType item, InventorySetting<ItemType> setting)
        {
            this.item = item;
            amount = 0;
            this.setting = setting;
        }

        public void AddAll(Grid<ItemType> otherGrid)
        {
            bool isSameItem = item.Equals(otherGrid.item);

            if (!(isSameItem || amount == 0)) return;

            int p = Math.Clamp(otherGrid.amount, 0, maxAmount - amount);
            amount += p;
            otherGrid.amount -= p;
        }
    }
}