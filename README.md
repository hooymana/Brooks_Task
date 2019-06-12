# Brooks_Task
Unity Files required for reproduction of Brooks task

Built on a PC using C# in Unity version 5.0.1f (free version)

https://unity3d.com/get-unity/download/archive?_ga=2.241674242.182008992.1560308299-1021339416.1560308299

Select "Unity Installer". You may need to install .NET Framework which will need a restart but you should delay restart so all of Unity can be downloaded first.

Create Unity ID and sign in.

Create new Project and open assets folder externally.

Within Assets create an Artwork, Scripts, Prefab, and Scenes folder

SETTING UP ARTWORK - SCREEN SHOULD BE IN GAME VIEW

Download the player-sprites and block-sprites .png files and placde in the Artwork folder.

In Unity click on each sprite image and in the inspector the following should be true for each image:

texture type: sprite (2D and UI)
Sprite Mode: Multiple
Packing Tag:
Pixels Per Unit: 100
Generate Mip Maps: Check
Filter Mode: Point
Max Size: 2048
Format: Truecolor

APPLY

For player sprite:
Click on Sprite Editor. window should appear. On slice drop down select automatic, center and smart

For block sprite:
Click on Sprite Editor. Trim. X = 0, Y = 28, W = 100, H = 100, Pivot = Center. Apply.
Drag block-sprite_0 into sprite renderer - sprite box for each block sprite in hierarchy.

FULL GAME ENVIRONMENT SHOULD BE SET

CREATE TAGS FOR GAME OBJECT

Click on GameObject in Hierarachy. In inspector go to Tag and add Tags startBox and stopBox. GameObject should be tag as startBox and GameObjectExit should be tag as stopBox.

ATTACHING SCRIPTS



The Brooks file is specific to unity and it contains all of the graphics necessary for the game in the proper dimension and spacing.

The Player 1.prefab file is also specific to unity and should be dragged into the scenes hierarchy. The prefab is default for the right joystick but if the user clicks the "Thumbstick" option under the Player 2 (script) drop down control will be given to the left joy stick.

The controller for the game is a USB xbox 360 controller. Unity is a microsoft friendly software and it will easily recognize the controller upon being plugged into the computer. However, larger commercial joysticks can also be used in Unity it researchers are studying a population with reduced thumb dexterity. 
