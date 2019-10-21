# Brooks_Task
Unity Files required for reproduction of Brooks task

Built on a PC using C# in Unity version 5.0.1f (free version)

https://unity3d.com/get-unity/download/archive?_ga=2.241674242.182008992.1560308299-1021339416.1560308299

Select "Unity Installer". You may need to install .NET Framework which will need a restart but you should delay restart so all of Unity can be downloaded first.

Create Unity ID and sign in.

Create new Project and open assets folder externally.

Within Assets create an Artwork, Scripts, Prefab, Sounds and Scenes folder

SETTING UP ARTWORK - SCREEN SHOULD BE IN GAME VIEW

Download the player-sprites and block-sprites .png files and place in the Artwork folder.

In Unity click on each sprite image and in the inspector the following should be true for each image:

texture type: sprite (2D and UI)
Sprite Mode: Multiple
Packing Tag:
Pixels Per Unit: 100
Generate Mip Maps: Check
Filter Mode: Point
Max Size: 2048
Format: Truecolor

click APPLY

Only do this portion if above is not true. Otherwise move onto CREATE SORTING LAYERS FOR GAME
For player sprite:
Click on player-sprites in artwork folder. In inspector click on Sprite Editor. window should appear. On slice drop down, located at the top, select automatic, center and smart.

For block sprite:
Click on block-sprites. Sprite Editor. Trim. X = 0, Y = 28, W = 100, H = 100, Pivot = Center. Apply.
Drag block-sprite_0 into the scene.

PLACING BOXES AND ASTRONAUT INTO THE SCENE

First make sure you are in the scene tab. Then drag block-sprites 0 image from the artwork folder into the scene and repeat.The first block will have coordinates of 0,0,0. Setting coordinates is done in the inspector. First you have to click on the block in the scene for the inspector to populate. Then you will see in the inspector a transform box with x,y,z values for position. This is where you put in the coordinates 0,0,0. For the second block you will give it the coordinates of 16,0,0. Change the scale for each block to x = 2 and y = 2, also in the transform box. You won't have to do anything more for the boxes.

Drag okayer-sprites 0 image into the scene and give it the coordinates 0,0,0. Astronaut should not be in the middle of the box on the left of the scene.

CREATE SORTING LAYERS FOR GAME

Click on any block sprite in hierarchy and within the sprite renderer component go to sorting layer and create two new sorting layers by hitting the + sign. call the first layer background and the second layer objects. The layers should be in the following order: background, objects and default. If this is not the order then you can drag them into the correct order.

Each block sprite should have a sorting layer or background and the player should have the sorting layer of objects. This ensures that the player is always rendered above the blocks.

FULL GAME ENVIRONMENT SHOULD BE SET - startbox and stopbox should be viewable in game view and scene view

CREATE TAGS FOR GAME OBJECT

Create two gameobjects by clicking on the gameobject dropdown and click on create empty. This will place a gameobject in the scene although it can't be seen. Click on the first gameobject and add a boxcollider (add componenet > physics 2d > box collider). Give this gameobject the following coords (0,0,0). Create another gameobject and call it gameobjectexit. give it coordintates (16,0,0). Also add a box collider to this one. The boxcollider for each gameobject should be size x = 2 and y = 1.

Click on GameObject in Hierarachy. In inspector go to Tag and add Tags startBox and stopBox. GameObject should be tag as startBox and GameObjectExit should be tag as stopBox.

ATTACHING SCRIPTS TO GAME SPRITES

Download scripts from github. Easiest as a zip and place all scripts (.cs) into the Unity Scripts folder.

Click on Main Camera and drag BlackCamera.cs to Script box

Setting Up Player

Delete missing prefab in the hierarchy.
Download and place the player1.prefab file into the Unity Prefab folder.
Drag the player1 prefab from the prefab folder into the hierarchy.
IMPORTANT: Now drag the player 1 from the hierarchy back to the prefab folder. This should change the color of the player 1 text from black to blue. This ensures that everytime you start the project the player is loaded with all presets and scripts.
Remove first script component in the player 1 prefab. This is the one between audio listener and animator.
Drag player_sprites_0 into sprite renderer box
First attached script should be Player2.cs which follows the box collider 2d component
Second attached script should be InputState.cs
Third RecPositions.cs
Fourth CursorPositions.cs
Fifth CursorPositionsY.cs
Sixth RecAcc.cs
Seventh RecVel.cs

Player2.cs, InputState.cs, RecPositions.cs and RecAcc.cs should be checked and the others should not be checked.

It is important to restart your computer before attempting to play if you needed to install .NET Framework first. Otherwise, much of the Unity functions may not be implemented you will get errors.

SETTING UP SOUNDS.

In the Sounds folder place the pick up crystal and pick up artifact here.

Drag the pick up crystal sound to the coin collect sound box in the player 1 - player 2 script GUI in the hierarchy and the artifact pick up sound into the goal box of the player 1 - player 2 script GUI.

SETTING UP CONTROLLER INPUT IN UNITY

In Unity go to Edit > Project Settings > Input. This opens the InputManager in the Inspector Window.
Drop down axes and add 4 new slots. E.g. if axes is 18 increase to 22.

Label each new axes the following
360_JoyL
360_JoyR
360_JoyRY
360_JoyLY

Each axis should have the following parameters:
Positive Button = escape
Gravity = 0
Dead = 0.2
Sensitivity = 1
Type = Joystick Axis
Axis = (360_JoyL = X axis; 360_JoyR = 4th axis (Joysticks); 360_JoyRY = 5th axis (Joysticks); 360_JoyLY = Get Motion from All Joysticks)
Joy Num = (360_JoyL & 360_JoyLY = Get Motion from all Joysticks; 360_JoyR & 360_JoyRY = Joystick 1)

Everything else should be blank

The controller for the game is a USB xbox 360 controller. Unity is a microsoft friendly software and it will easily recognize the controller upon being plugged into the computer. However, larger commercial joysticks can also be used in Unity it researchers are studying a population with reduced thumb dexterity. 

The task has been designed as the right thumbstick of an xbox controller to control the player. However, if one would like to use the left joystick all one has to do is go into the Player1 hierarchy and within the player2 script GUI uncheck the thumbstick. This switches control from right to left.

SETTING UP SCRIPT PATHS FOR DATA RECORDING AND STORAGE

Find the project folder where the assets folder for this project is located and create two folders: RecPos and RecAcc

Open the RecPosition script in unity, double click - should open on monodevelope, and replace the path in each script, should be line 11 for each RecPos and RecAcc, with the path of the new folders accordingly.

This tells the script where to store the .txt files of the astronuat position and acceleration.

Data of player position and acceleration is recorded at 100 Hz. Higher sample collection may be unstable. May depend on processing power of computer. Data is only stored after the trial is complete. 

IMPORTANT: The task is designed so if the player exits the startbox too soon, <1.5s, then the trial resets. Data from these reset trials is not stored. Data is not collected until 1 second into data collection. This was originally done for speed and because player movement is not expected prior to 1 second in the trial. To change to record complete trial length click on player 1 in hierarchy and within the rec position and rec acc scripts change the tsample from 1 to 0. Click apply at the top of the player 1 inspector panel so any changes you make are maintained whenever you restart the project.

To restart the trial begin the game and press P. Trial GUI should reset to 0.

DATA SAVING: If you hit P and do not save the data in the RecPos of RecAcc file then it will be overwritten. There is no way to recover this overwritten data. Be sure to migrate data from recpos and recacc after each participant.

FINAL NOTES

IMPORTANT: Gravity will still be affecting player with this set-up. To resolve click on player 1 in hierarchy and set gravity scale to 0.

VERY IMPORTANT: Any changes you make to the prefab and you wish to keep you have to click apply everytime. You will notice that any change you make to the prefab like gravity scale will make the text bold. This means the change is only temporary until you hit apply and the text will go unbold.

EXPERIMENTER MODIFICATIONS

If the experimenter wants to change the speed of the player of the length of time of the trial then changes to the .cs scripts on player 1 will need to be made.

For changes in speed go into player2.cs and change private float speed = 7.5f to whatever you want, line 6. If you want the speed to be modified in the script GUI within the unity inspector then you will need to change private to public and save. Monodevelop may ask if you want to covert line endings. Say no.

If you want to change the length of the trial you will need to change the float on line 29 in player2.cs from 4.6 to whatever you want. To collect more or less data then you must also change the floats on line 75 and 73 for the recpositions.cs and recacc.cs scripts respectively. It is important to change both of these to the same degree otherwise data collection will either be incomplete or not occur at all.

PROCESSING DATA

Script BrooksVar.m is a MATLAB script that will calculate scaling ratio, time in target, time outside startbox, time of last positive acceleration, time of first negative acceleration, number of successful trials and plots for average performance of both position and acceleration data.

With any questions on set-up or troubleshooting please contact hooyman.andrew@gmail.com
