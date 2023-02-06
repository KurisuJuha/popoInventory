using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    [Serializable]
    public class Inventory<ItemType>
    {
        public ReadOnlyCollection<Grid<ItemType>> grids => Array.AsReadOnly(_grids);
        [SerializeField] private Grid<ItemType>[] _grids;
        private InventorySetting<ItemType> setting;

        public Inventory(int size, InventorySetting<ItemType> setting)
        {
            _grids = new Grid<ItemType>[size];
            this.setting = setting;

            for (int i = 0; i < size; i++) _grids[i] = setting.CreateEmptyGrid();
        }
    }
}