# ZXMLui

ZXMLui is a UI construction toolkit for Unity Editor development. It allows developers to quickly build UnityEditor interfaces using XML-based layout definitions. Inspired by WPF/XAML, ZXMLui aims to improve development efficiency and maintainability of complex editor tools.

## Features

- Define UI layout using XML for clear and structured design
- Supports automatic callback binding
- Decouples UI structure from logic code
- Suitable for building complex Unity editor extensions

## Installation

Install via Unity Package Manager using Git URL:  
https://github.com/ZSaltedFish/ZXMLui.git?path=Packages/ZXMLui

1. Open Unity Editor  
2. Go to `Window > Package Manager`  
3. Click the `+` button in the top-left corner and choose `Add package from git URL...`  
4. Paste the URL above and confirm

## Usage
### Example

The sample demonstrates the basic usage of ZXMLui, featuring two simple controls: `EditorButton` and `EditorText`.

- Code file: `ZXMLui/Sample/SampleUI.cs`
- XML file: `ZXMLui/Sample/SampleUI.xml`

Function overview:

- `EditorText` is used to edit and input text content.
- `EditorButton` is a clickable button. When clicked, it reads the current content of `EditorText` and logs it to the console.

## Control Reference
[Controls](Controls.md)