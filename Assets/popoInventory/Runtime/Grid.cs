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

        public ItemType item => _item;
        public int amount => _amount;

        public InventorySetting<ItemType> setting => _setting;

        public Grid(InventorySetting<ItemType> setting)
        {
            this._setting = setting;
            this._item = setting.getEmptyItem();
            this._amount = 0;

            MaintainConsistency();
        }

        public Grid(ItemType item, InventorySetting<ItemType> setting)
        {
            this._setting = setting;
            this._item = item;
            this._amount = 1;

            MaintainConsistency();
        }

        public Grid(ItemType item, InventorySetting<ItemType> setting, int amount)
        {
            this._setting = setting;
            this._item = item;
            this._amount = amount;

            MaintainConsistency();
        }

        public Grid(ItemType item, InventorySetting<ItemType> setting, bool toMaximize)
        {
            this._setting = setting;
            this._item = item;
            this._amount = toMaximize ? setting.getMaxAmount(item) : 1;

            MaintainConsistency();
        }

        public void AddAll(Grid<ItemType> otherGrid)
        {
            bool isSameItem = item.Equals(otherGrid.item);

            if (!isSameItem && amount != 0) return;

            int p = Math.Clamp(otherGrid.amount, 0, setting.getMaxAmount.Invoke(otherGrid.item) - amount);
            _amount += p;
            otherGrid._amount -= p;
            _item = otherGrid.item;

            MaintainConsistency();
            otherGrid.MaintainConsistency();
        }

        public void Add(Grid<ItemType> otherGrid, int addAmount)
        {
            bool isSameItem = item.Equals(otherGrid.item);

            if (!isSameItem && amount != 0) return;

            int p = Math.Clamp(addAmount, 0, setting.getMaxAmount.Invoke(otherGrid.item) - amount);
            _amount += p;
            otherGrid._amount -= p;
            _item = otherGrid.item;

            MaintainConsistency();
            otherGrid.MaintainConsistency();
        }

        public void MaintainConsistency()
        {
            // 数が0ならemptyアイテムに
            if (amount <= 0) _item = setting.getEmptyItem();

            // 数が0以下なら0に
            if (amount <= 0) _amount = 0;

            // 数が最大値よりも大きいなら最大値に
            int maxAmount = setting.getMaxAmount(item);
            if (amount > maxAmount) _amount = maxAmount;
        }
    }
}