# Unity-Power-Of-Two-Textures
Unity Power Of Two Textures is very small and helpful package that helps developers to generate power of two textures very fast in editor.
This package can help you to optimize your textures in game, because better compression mods are working only with POT (power of two) textures.

## Some information

You can find the editor window here

![Location](https://github.com/user-attachments/assets/d90e1466-40e1-43f5-a008-9d5050d0f03f)

### Modes
You can choose from two modes:
* CreateNew
* OverrideThis

The first one creates a new texture based on the old one.
In the window you can set some parameters:
* Directory of the new texture
* Name of the new texture
* Optional copy texture settings flag

![Interface 1](https://github.com/user-attachments/assets/b68502a9-296f-4f72-ba8c-8cf5bcc9192a)

The second mode allows you to rewrite chosen texture data. It can help if you dont want to deal with texture copy.
You can also **UNDO** this texture modification.

![Interface 2](https://github.com/user-attachments/assets/1e158411-5b45-4a81-a484-994221ff7c9c)

And this is an ugly example of creating a texture copy

![Result](https://github.com/user-attachments/assets/4dd61912-be90-4c64-87e4-1d072fbb888d)
