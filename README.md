# Unity-Power-Of-Two-Textures
Unity Power Of Two Textures is very small and helpful package that helps developers to generate power of two textures very fast in editor.
This package can help you to optimize your textures in game, because better compression mods are working only with POT (power of two) textures.

## POTter window

You can find the editor window here

![Location](https://github.com/user-attachments/assets/d90e1466-40e1-43f5-a008-9d5050d0f03f)

### Modes
You can choose from two modes:
* CreateNew
* OverrideThis

The first one creates a new texture based on the old one.
In the window you can set some parameters:
* Create texture backup (POTter/Backups) flag
* Directory of the new texture
* Name of the new texture
* Optional copy texture settings flag

![Interface 1](https://github.com/user-attachments/assets/ae5f5730-6736-45c7-a691-b8c3d6f4efbe)

The second mode allows you to rewrite chosen texture data. It can help if you dont want to deal with texture copy.
You can also **UNDO** this texture modification.

![Interface 2](https://github.com/user-attachments/assets/1e158411-5b45-4a81-a484-994221ff7c9c)

And this is an ugly example of creating a texture copy

![Result](https://github.com/user-attachments/assets/4dd61912-be90-4c64-87e4-1d072fbb888d)

## Custom textures importer

Custom textures importer window appears automaticly when you import new textures to your project

https://github.com/user-attachments/assets/36116384-7bb3-45f7-9ef9-3920dc0a7fdf

You can always disable this window in POTter/Settings/TexturesImporterSettings -> NeverShowAgain if you don`t need it.
This window helps you to convert many NPOT textures to POT in one click.

https://github.com/user-attachments/assets/b3a68f0e-6802-4728-85c6-6649152eab32

Backups are also possible (Potter/Backups)




