namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class InventorySettings<TItem> : IInventorySettings<TItem>
    {
        public delegate TItem CopyItemCallback(TItem item);

        public delegate TItem GenerateEmptyItemCallback();

        public delegate bool IsEmptyItemCallback(TItem item);

        public delegate bool IsSameItemCallback(TItem itemA, TItem itemB);

        public delegate int ItemMaxAmountCallback(TItem item);

        private readonly CopyItemCallback copyItem;
        private readonly GenerateEmptyItemCallback generateEmptyItem;
        private readonly IsEmptyItemCallback isEmptyItem;
        private readonly IsSameItemCallback isSameItem;
        private readonly ItemMaxAmountCallback itemMaxAmount;

        public InventorySettings(CopyItemCallback copyItem, IsSameItemCallback isSameItem,
            IsEmptyItemCallback isEmptyItem, ItemMaxAmountCallback itemMaxAmount,
            GenerateEmptyItemCallback generateEmptyItem)
        {
            this.copyItem = copyItem;
            this.isSameItem = isSameItem;
            this.isEmptyItem = isEmptyItem;
            this.itemMaxAmount = itemMaxAmount;
            this.generateEmptyItem = generateEmptyItem;
        }

        public bool IsSameItem(TItem itemA, TItem itemB)
        {
            return isSameItem.Invoke(itemA, itemB);
        }

        public bool IsEmptyItem(TItem item)
        {
            return isEmptyItem.Invoke(item);
        }

        public int ItemMaxAmount(TItem item)
        {
            return itemMaxAmount.Invoke(item);
        }

        public TItem GenerateEmptyItem()
        {
            return generateEmptyItem.Invoke();
        }

        public TItem CopyItem(TItem item)
        {
            return copyItem.Invoke(item);
        }
    }
}