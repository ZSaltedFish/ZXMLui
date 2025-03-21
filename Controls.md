# 控件与属性一览

本文件列出当前 ZXMLui 支持的所有控件及其可用属性，包括通用属性与控件特有属性。

## 通用属性（适用于所有控件）

| 属性名    | 类型     | 说明                           | 示例                             |
|-----------|----------|--------------------------------|----------------------------------|
| Name      | string   | 控件名称，用于字段自动绑定     | `Name="MyButton"`               |
| Content   | string   | 显示内容，用于按钮、标签等     | `Content="Click Me"`            |
| Position  | string   | 坐标，格式为 `x,y`             | `Position="10,20"`              |
| Size      | string   | 尺寸，格式为 `width,height`    | `Size="200,40"`                 |

---

## EditorText

| 属性名         | 类型     | 说明                               | 示例                                     |
|----------------|----------|------------------------------------|------------------------------------------|
| RichText       | bool     | 是否启用富文本（默认 `false`）     | `RichText="true"`                        |
| FontSize       | int      | 文本字号                           | `FontSize="14"`                          |
| Color          | string   | 文本颜色，格式 `r,g,b,a`           | `Color="255,0,0,255"`                    |
| OnContentChange| string   | 文本变化回调，函数需带一个参数     | `OnContentChange="OnTextChanged"`       |

---

## EditorButton

| 属性名    | 类型     | 说明                           | 示例                             |
|-----------|----------|--------------------------------|----------------------------------|
| OnClick   | string   | 点击时调用的函数名             | `OnClick="OnClickHandler"`       |

---

> 所有控件均支持“通用属性”中列出的参数。字段绑定通过在类中定义 `public` 字段名与 `Name` 一致来实现。例如 `Name="InputField"` 对应字段为 `public EditorText InputField;`

