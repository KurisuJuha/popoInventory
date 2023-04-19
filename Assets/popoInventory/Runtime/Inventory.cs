using System;
using System.Linq;
using System.Collections.ObjectModel;
using UnityEngine;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    [Serializable]
    public class Inventory<ItemType>
    {
        public ReadOnlyCollection<InventoryGrid<ItemType>> grids => Array.AsReadOnly(_grids);
        [SerializeField] private InventoryGrid<ItemType>[] _grids;
        public InventorySettings<ItemType> setting { get; private set; }

        public Inventory(int size, InventorySettings<ItemType> setting)
        {
            _grids = new InventoryGrid<ItemType>[size];
            this.setting = setting;

            for (int i = 0; i < size; i++) _grids[i] = setting.CreateEmptyGrid();
        }

        public Inventory(Inventory<ItemType> sourceInventory)
        {
            this.setting = sourceInventory.setting;
            this._grids = sourceInventory._grids.Select(grid => new InventoryGrid<ItemType>(grid)).ToArray();
        }

        public bool TryAddItem(InventoryGrid<ItemType> grid)
        {
            // 同じアイテムに出来るだけ入れてみる
            foreach (var inventoryGrid in grids)
            {
                // 同じアイテムなら入れる
                if (inventoryGrid.item.Equals(grid.item))
                    inventoryGrid.AddAll(grid);
            }
            // 空のgridに入れる
            foreach (var inventoryGrid in grids)
            {
                // 空のgridなら入れる
                if (inventoryGrid.item.Equals(setting.getEmptyItem()))
                    inventoryGrid.AddAll(grid);
            }

            return grid.amount == 0;
        }
    }
}