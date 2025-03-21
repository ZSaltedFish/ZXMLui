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

## 通用属性

ZXMLui 中所有标签都支持以下通用属性，用于设置控件的标识、内容和布局。

### Name

设置控件的名称，并用于与脚本中的 `public` 字段进行自动绑定。

例如：

XML 中写法：  
`<EditorText Name="InputField" />`

对应的脚本字段需命名为：  
`public EditorText InputField;`

ZXMLui 会在运行时自动将 XML 中 `Name="InputField"` 的控件绑定到对应的字段上。

---

### Content

控件显示的主要内容。适用于如按钮、标签等控件。

例如：  
`<EditorButton Content="Click Me" />`

---

### Position

设置控件在父容器中的位置（单位为像素，格式为 `x,y`）。

例如：  
`<EditorText Position="10,20" />`

---

### Size

设置控件的宽度和高度（格式为 `width,height`）。

例如：  
`<EditorButton Size="200,30" />`

---

这些通用属性适用于所有控件标签，推荐在布局中明确指定 `Position` 和 `Size` 以获得清晰可控的 UI 排布。


