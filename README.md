# popoInventory

[![openupm](https://img.shields.io/npm/v/jp.juhakurisu.popoinventory?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/jp.juhakurisu.popoinventory/) [![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE)

## 概要

popoInventoryはアイテム数を管理するインベントリクラスを追加します。

## 特徴

- 管理するアイテムのクラスは自由
- シリアライズ可能なためインスペクタ上で確認可能

## 要件

- Unity 2021.3以上

## インストール

``` cli
openupm add jp.juhakurisu.popoinventory
```

## 使用方法

``` csharp
using UnityEngine;
using JuhaKurisu.PopoTools.InventorySystem;

public class Example : Monobehaviour
{
     // アイテムとするクラスは自由に指定可能
    public Inventory<string> inventory;

    private void Start()
    {
        InventorySettings<string> settings = new InventorySettings<string>(
            item => 999, // アイテムによって最大スタック数を決められる
            () => "" // 空の場合のアイテムを決める
        );
        inventory = new Inventory<string>(
            9, // inventoryのサイズを指定
            settings // 使用する設定を指定
        );

        Grid inventoryGrid0 = inventory.grids[0]; // 中身のGridはgridsに格納されている
        Grid cursorGrid = new Grid("nhk", 334, settings); // Gridは手動でアイテム数を指定して作成することもできる

        inventoryGrid0.Add(cursorGrid, 10); // 数を指定してアイテムを移動。同じアイテムではない場合は移動しない
        inventoryGrid0.AddAll(cursorGrid); // 全て移動することも可能
        inventoryGrid0.Exchange(cursorGrid); // もちろん交換も可能

        inventory.TryAddItem(cursorGrid); // 同じアイテムか空のGridの中で一番若いGridにアイテムを移動することも可能
    }
}
```

## ライセンス

[MIT License](LICENSE)
