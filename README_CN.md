# ZXMLui

ZXMLui 是一个用于 Unity 编辑器开发的 UI 构建工具，允许开发者通过 XML 描述方式快速生成 UnityEditor 界面。设计灵感来自 WPF/XAML，旨在提升编辑器工具开发的效率与可维护性。

## 特性

- 使用 XML 描述 UI，结构清晰，逻辑直观
- 支持自动回调绑定
- 解耦界面构建与逻辑代码
- 适合构建复杂的 Unity 编辑器插件

## 安装方式

通过 Unity Package Manager 添加 Git URL：
https://github.com/ZSaltedFish/ZXMLui.git?path=Packages/ZXMLui

1. 打开 Unity 编辑器
2. 进入 Window > Package Manager
3. 点击左上角 + 按钮，选择 Add package from git URL...
4. 粘贴上述地址并确认

## 使用方法
### 参考示例

以下示例展示了 ZXMLui 的基本用法，包含两个简单控件：`EditorButton` 和 `EditorText`。

- 代码文件：`ZXMLui/Sample/SampleUI.cs`
- XML 文件：`ZXMLui/Sample/SampleUI.xml`

功能说明：

- `EditorText` 用于编辑一段文本内容，支持输入修改。
- `EditorButton` 是一个按钮控件，点击后会读取当前 `EditorText` 中的内容，并在控制台输出该文本。
里面包含两个简单的控件，`EditorButton`与`EditorText`，实现功能为编辑Text中的文字，点击按钮可以在控制台上打印对应的LOG。


