using System;
using UnityEngine;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    [Serializable]
    public class Grid<ItemType>
    {
        [SerializeField] private ItemType _item;
        [SerializeField] private int _amount;
        private InventorySetting<ItemType> _setting;

        public ItemType item
        {
            get => amount > 0 ? _item : _setting.getEmptyItem.Invoke();
            private set => _item = value;
        }
        public int amount
        {
            get => _amount;
            private set => _amount = Math.Clamp(value, 0, maxAmount);
        }
        public int maxAmount => _setting.getMaxAmount.Invoke(item);
        public InventorySetting<ItemType> setting => _setting;

        public Grid(InventorySetting<ItemType> setting)
        {
            this.item = setting.getEmptyItem();
            this._setting = setting;
            amount = 0;
        }

        public Grid(ItemType item, InventorySetting<ItemType> setting)
        {
            this.item = item;
            this._setting = setting;
            amount = 0;
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