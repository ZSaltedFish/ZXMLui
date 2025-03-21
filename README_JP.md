# ZXMLui

ZXMLui は Unity エディター開発向けの UI 構築ツールです。XML によるレイアウト記述で、UnityEditor の UI を素早く構築できます。WPF/XAML にインスパイアされた設計で、エディター拡張ツールの開発効率と保守性を向上させることを目的としています。

## 特徴

- XML による UI レイアウト記述。構造が明確で直感的
- コールバック関数の自動バインディングに対応
- UI 表現とロジックコードの分離を実現
- 複雑な Unity エディター拡張の構築に適している

## インストール方法

Unity Package Manager を使用して、以下の Git URL からインストールしてください：  
https://github.com/ZSaltedFish/ZXMLui.git?path=Packages/ZXMLui

1. Unity エディターを開く  
2. メニューから `Window > Package Manager` を選択  
3. 左上の `+` ボタンをクリックし、`Add package from git URL...` を選ぶ  
4. 上記の URL を貼り付けて追加

## 使用方法
### サンプル

ZXMLui の基本的な使い方を示すサンプルです。`EditorButton` と `EditorText` の2つの簡単なコントロールを使用しています。

- コードファイル：`ZXMLui/Sample/SampleUI.cs`
- XMLファイル：`ZXMLui/Sample/SampleUI.xml`

機能概要：

- `EditorText` はテキストの入力・編集を行います。
- `EditorButton` はクリック可能なボタンで、押下時に `EditorText` に入力された内容を読み取り、コンソールに出力します。

## コントロールリファレンス
[Controls](Controls.md)