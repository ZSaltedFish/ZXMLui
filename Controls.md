# 控件与属性一览

本文件列出当前 ZXMLui 支持的所有控件及其可用属性，包括通用属性与控件特有属性。

## 通用属性（适用于所有控件）

| 属性名   | 类型   | 说明                                        | 示例                                |
|----------|--------|---------------------------------------------|-------------------------------------|
| Name     | string | 控件名称，用于字段绑定                      | `Name="MyControl"`                  |
| Content  | string | 显示内容，适用于支持文本的控件              | `Content="Click Me"`                |
| Position | string | 控件位置，格式为 `x,y`                      | `Position="10,20"`                  |
| Size     | string | 控件大小，格式为 `width,height`             | `Size="200,40"`                     |
| Anchor   | string | 对齐方式，逗号分隔（如 `Left,Top,Right`）   | `Anchor="Left,Top,Right"`           |
| Margin   | string | 边距，格式为 `left,top,right,bottom`        | `Margin="0,0,0,0"`                  |

---
## ZKnight.ZXMLui.EditorPanel

用于布局、包裹其他控件的空容器控件。可嵌套使用，无视觉样式。

## ZKnight.ZXMLui.EditorText

| 属性名         | 类型     | 说明                                           | 示例                                     |
|----------------|----------|------------------------------------------------|------------------------------------------|
| RichText       | bool     | 是否启用富文本（默认 `false`）                 | `RichText="true"`                        |
| FontSize       | int      | 文本字号                                    | `FontSize="14"`                          |
| Color          | string   | 文本颜色，**仅支持 RGB 十六进制**（如 `FFFFFF`）| `Color="FF0000"`                          |
| OnContentChange| string   | 文本变化回调方法名                            | `OnContentChange="OnTextChanged"`        |

---

## ZKnight.ZXMLui.EditorButton

| 属性名        | 类型     | 说明                                           | 示例                                       |
|---------------|----------|------------------------------------------------|--------------------------------------------|
| OnBtnClick    | string   | 点击按钮时调用的函数名                         | `OnBtnClick="OnClickHandler"`              |
| RichText      | bool     | 是否启用富文本（默认 `false`）                 | `RichText="true"`                          |
| FontSize      | int      | 文本字号                                       | `FontSize="16"`                            |
| Bg            | string   | 背景贴图路径（如 `Assets/Textures/...`）       | `Bg="Assets/Textures/button_bg.png"`       |
| Hover         | string   | 悬停贴图路径                                   | `Hover="Assets/Textures/button_hover.png"` |
| ContentColor  | string   | 文本颜色，**仅支持 RGB 十六进制**（如 `FFFFFF`）| `ContentColor="FFCC00"`                    |

## ZKnight.ZXMLui.EditorImage

| 属性名        | 类型   | 说明                                  | 示例                                                           |
|---------------|--------|---------------------------------------|----------------------------------------------------------------|
| Bg            | string | 图片资源路径                          | `Bg="Packages/com.zknight.uflowchart/Resources/MeshBg.png"`    |
| ImageScaleMode| string | 图片缩放模式（如 `Tile`、`Stretch`）  | `ImageScaleMode="Tile"`                                        |

## ZKnight.ZXMLui.EditorToggle

| 属性名 | 类型  | 说明                        | 示例                          |
|--------|-------|-----------------------------|-------------------------------|
| IsOn   | bool  | 是否默认开启，`true` 或 `false`（默认 `false`） | `IsOn="true"`   

---

> 所有控件均支持“通用属性”中列出的参数。字段绑定通过在类中定义 `public` 字段名与 `Name` 一致来实现。例如 `Name="InputField"` 对应字段为 `public EditorText InputField;`

