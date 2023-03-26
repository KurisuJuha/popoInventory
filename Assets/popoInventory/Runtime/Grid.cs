using System;
using UnityEngine;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    [Serializable]
    public class Grid<ItemType>
    {
        [SerializeField] private ItemType _item;
        [SerializeField] private int _amount;
        private InventorySettings<ItemType> _settings;

        public ItemType item => _item;
        public int amount => _amount;

        public InventorySettings<ItemType> settings => _settings;

        public Grid(InventorySettings<ItemType> settings)
        {
            this._settings = settings;
            this._item = settings.getEmptyItem();
            this._amount = 0;

            MaintainConsistency();
        }

        public Grid(ItemType item, int amount, InventorySettings<ItemType> settings)
        {
            this._settings = settings;
            this._item = item;
            this._amount = amount;

            MaintainConsistency();
        }

        public Grid(ItemType item, InventorySetting<ItemType> setting, bool toMaximize)
        public Grid(ItemType item, InventorySettings<ItemType> setting, bool toMaximize = false)
        {
            this._settings = setting;
            this._item = item;
            this._amount = toMaximize ? setting.getMaxAmount(item) : 1;

            MaintainConsistency();
        }

        public void AddAll(Grid<ItemType> otherGrid)
        {
            bool isSameItem = item.Equals(otherGrid.item);

            if (!isSameItem && amount != 0) return;

            int p = Math.Clamp(otherGrid.amount, 0, settings.getMaxAmount.Invoke(otherGrid.item) - amount);
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

            int p = Math.Clamp(addAmount, 0, settings.getMaxAmount.Invoke(otherGrid.item) - amount);
            _amount += p;
            otherGrid._amount -= p;
            _item = otherGrid.item;

            MaintainConsistency();
            otherGrid.MaintainConsistency();
        }

        public void Exchange(Grid<ItemType> otherGrid)
        {
            _item = otherGrid._item;
            _amount = otherGrid._amount;

            MaintainConsistency();
            otherGrid.MaintainConsistency();
        }

        public void MaintainConsistency()
        {
            // 数が0ならemptyアイテムに
            if (amount <= 0) _item = settings.getEmptyItem();

            // 数が0以下なら0に
            if (amount <= 0) _amount = 0;

            // 数が最大値よりも大きいなら最大値に
            int maxAmount = settings.getMaxAmount(item);
            if (amount > maxAmount) _amount = maxAmount;
        }
    }
}