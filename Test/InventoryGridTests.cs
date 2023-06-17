﻿using JuhaKurisu.PopoTools.InventorySystem;

namespace Test;

public class InventoryGridTests
{
    [Test]
    public void IsAddableItem_アイテムが一つもないGridにアイテムを足す_足せると判断される()
    {
        CreateInventoryAndGrid(out _, out var grid);

        var b = grid.IsAddableItem(new TestItem("Emptyじゃないよ！！！"));
        Assert.That(b, Is.True);
    }

    [Test]
    public void IsAddableItem_アイテムが入っている状態で違う種類のアイテムをGridに足す_足せないと判断される()
    {
        CreateInventoryAndGrid(out _, out var grid);

        Assert.Multiple(() =>
        {
            Assert.That(grid.TryAddItem(new TestItem("もとから入ってるアイテム")), Is.True);
            Assert.That(grid.IsAddableItem(new TestItem("さっきとは違う種類のアイテム")), Is.False);
        });
    }

    [Test]
    public void IsAddableItem_同じ種類のアイテムを追加する_足せると判断される()
    {
        CreateInventoryAndGrid(out _, out var grid);

        Assert.Multiple(() =>
        {
            Assert.That(grid.TryAddItem(new TestItem("同じやつ")), Is.True);
            Assert.That(grid.IsAddableItem(new TestItem("同じやつ")), Is.True);
        });
    }

    [Test]
    public void IsAddableItem_アイテムが上限のいっぱいまで足す_足せる()
    {
        CreateInventoryAndGrid(out _, out var grid);
        for (var i = 0; i < 100; i++) Assert.That(grid.TryAddItem(new TestItem("足すやつ")), Is.True);
    }

    [Test]
    public void IsAddableItem_アイテムが上限いっぱいの状態でもう一つ足す_足せない()
    {
        CreateInventoryAndGrid(out _, out var grid);
        for (var i = 0; i < 100; i++) Assert.That(grid.TryAddItem(new TestItem("足すやつ")), Is.True);

        Assert.That(grid.TryAddItem(new TestItem("足すやつ")), Is.False);
    }

    [Test]
    public void TryAddItem_IsAddableItemと同じ結果が返ってくるか_帰ってくる()
    {
        CreateInventoryAndGrid(out _, out var grid);

        var isAddableItem = grid.IsAddableItem(new TestItem("てーすと"));

        Assert.That(grid.TryAddItem(new TestItem("てーすと")), Is.EqualTo(isAddableItem));
    }

    [Test]
    public void IsSubtractableItem_アイテムが一つもない状態から引く_引けない()
    {
        CreateInventoryAndGrid(out _, out var grid);

        Assert.That(grid.IsSubtractableItem(), Is.False);
    }

    [Test]
    public void IsSubtractableItem_アイテムが一つある状態から引く_引ける()
    {
        CreateInventoryAndGrid(out _, out var grid);
        Assert.Multiple(() =>
        {
            Assert.That(grid.TryAddItem(new TestItem("一つだけ")), Is.True);
            Assert.That(grid.IsSubtractableItem(), Is.True);
        });
    }

    [Test]
    public void TrySubtractItem_IsSubtractableItemと同じ結果が返ってくるか_返ってくる()
    {
        CreateInventoryAndGrid(out _, out var grid);
        Assert.That(grid.TryAddItem(new TestItem("てすとあいてむ")), Is.True);

        var isSubtractableItem = grid.IsSubtractableItem();

        Assert.That(isSubtractableItem, Is.EqualTo(grid.TrySubtractItem(out _)));
    }

    [Test]
    public void TryAddItem_アイテムが本当にに足されているか_足されている()
    {
        CreateInventoryAndGrid(out _, out var grid);
        Assert.Multiple(() =>
        {
            Assert.That(grid.TryAddItem(new TestItem("てすとあいてむ")), Is.True);
            Assert.That(grid.Items, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void TrySubtractItem_アイテムが本当に引かれているか_引かれている()
    {
        CreateInventoryAndGrid(out _, out var grid);
        Assert.Multiple(() =>
        {
            Assert.That(grid.TryAddItem(new TestItem("てすとあいてむ")), Is.True);
            Assert.That(grid.Items, Has.Count.EqualTo(1));
            Assert.That(grid.TrySubtractItem(out _), Is.True);
            Assert.That(grid.Items, Is.Empty);
        });
    }

    [SetUp]
    public void Setup()
    {
    }

    private void CreateInventoryAndGrid(out TestInventorySettings inventorySettings,
        out InventoryGrid<TestInventorySettings, TestItem> grid)
    {
        inventorySettings = new TestInventorySettings(100, new TestItem(""));
        grid = new InventoryGrid<TestInventorySettings, TestItem>(inventorySettings, 100);
    }
}