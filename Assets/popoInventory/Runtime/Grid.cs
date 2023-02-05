using System;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class Grid<ItemType>
    {
        public ItemType item
        {
            get => amount > 0 ? _item : getEmptyItem.Invoke();
            private set => _item = value;
        }
        ItemType _item;
        public int amount
        {
            get => _amount;
            set => _amount = Math.Clamp(value, 0, maxAmount);
        }
        int _amount;
        public int maxAmount => getMaxAmount.Invoke(item);
        event Func<ItemType, int> getMaxAmount;
        event Func<ItemType> getEmptyItem;

        public Grid(ItemType item, Func<ItemType, int> getMaxAmount, Func<ItemType> getEmptyItem)
        {
            this.item = item;
            amount = 0;
            this.getMaxAmount = getMaxAmount;
            this.getEmptyItem = getEmptyItem;
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