# Controls and Attributes Overview

This document lists all currently supported controls in ZXMLui and their available attributes, including both common and control-specific properties.

## Common Attributes (Applicable to All Controls)

| Attribute | Type   | Description                                        | Example                             |
|----------|--------|----------------------------------------------------|-------------------------------------|
| Name     | string | Control name, used for field binding               | `Name="MyControl"`                  |
| Content  | string | Display text, for controls that support text       | `Content="Click Me"`                |
| Position | string | Control position, format: `x,y`                    | `Position="10,20"`                  |
| Size     | string | Control size, format: `width,height`               | `Size="200,40"`                     |
| Anchor   | string | Anchoring, comma-separated (e.g., `Left,Top,Right`)| `Anchor="Left,Top,Right"`           |
| Margin   | string | Margin, format: `left,top,right,bottom`            | `Margin="0,0,0,0"`                  |

---

## ZKnight.ZXMLui.EditorPanel

Used as a container for layout and grouping. Can be nested. No visual appearance.

## ZKnight.ZXMLui.EditorText

| Attribute       | Type   | Description                                        | Example                                  |
|-----------------|--------|----------------------------------------------------|------------------------------------------|
| RichText        | bool   | Enables rich text rendering (`false` by default)   | `RichText="true"`                        |
| FontSize        | int    | Font size                                          | `FontSize="14"`                          |
| Color           | string | Text color in **RGB hexadecimal only** (e.g. `FFFFFF`) | `Color="FF0000"`                          |
| OnContentChange | string | Callback function name for text changes            | `OnContentChange="OnTextChanged"`        |

---

## ZKnight.ZXMLui.EditorButton

| Attribute      | Type   | Description                                        | Example                                       |
|----------------|--------|----------------------------------------------------|-----------------------------------------------|
| OnBtnClick     | string | Callback function name when the button is clicked | `OnBtnClick="OnClickHandler"`                |
| RichText       | bool   | Enables rich text rendering (`false` by default)   | `RichText="true"`                            |
| FontSize       | int    | Font size                                          | `FontSize="16"`                              |
| Bg             | string | Background image path (e.g. `Assets/Textures/...`) | `Bg="Assets/Textures/button_bg.png"`         |
| Hover          | string | Hover image path                                   | `Hover="Assets/Textures/button_hover.png"`   |
| ContentColor   | string | Text color in **RGB hexadecimal only** (e.g. `FFFFFF`) | `ContentColor="FFCC00"`                      |

## ZKnight.ZXMLui.EditorImage

| Attribute       | Type   | Description                                            | Example                                                           |
|-----------------|--------|--------------------------------------------------------|-------------------------------------------------------------------|
| Bg              | string | Path to the image resource                             | `Bg="Packages/com.zknight.uflowchart/Resources/MeshBg.png"`       |
| ImageScaleMode  | string | Image scaling mode. Options: `Tile`, `Stretch`, etc.   | `ImageScaleMode="Tile"`                                           |

---

> All controls support the common attributes listed in the "Common Attributes" section.  
> Field binding is done by defining a `public` field in the class with the same name as the `Name` value.  
> For example, `Name="InputField"` should match a field like `public EditorText InputField;`