# SlimUIExtensions

This Github Repo extends an existing Unity Asset, please purchase here: https://assetstore.unity.com/packages/tools/gui/cursor-controller-pro-161628
This Github Repo extends the existing Unity Asset to work in conjunction with Rewired, however, it can easily be modiifed to work with other input systems or with Unity's default input system.  I will not help you with converting your code to work with other systems.  If you encounter a bug, please let me know.

This asset extension is free to use as long as credit is given per the provided License agreement.

Now that formalities are done with, here is how to use the extension.

--------------------------------
1) Download and add all of the scripts except for RewiredInputProvider.cs into Assets/SlimUI/CursorControllerPro/Scripts/
2) Place RewiredInputProvider.cs into Assets/SlimUI/CursorControllerPro/Scripts/InputSystem/InputProviders/
3) Create or take from the Demo scene a CursorController gameobject setup like the following:

![image](https://user-images.githubusercontent.com/22646751/158924203-6d33a388-56c0-40d8-8f3b-07d8f46f5e68.png)

4) On the root gameobject, under CursorController.cs in the Inspector, with the gameobject selected, add RewiredUIActionMonitor.cs

![image](https://user-images.githubusercontent.com/22646751/158924272-b17a52bc-bb56-42f9-a820-4f533519eaaa.png)

5) Under CursorController root gameobject, add the following scripts to the empty gameobject "InputSystemProvider": "RewiredEventSystem.cs", "RewiredStandaloneInputModule.cs" - These two are provided by Rewired and are not found within this Repo.
6) To the same gameobject, add "RewiredInputProvider.cs", your InputSystemProvider gameobject should now look like this:

![image](https://user-images.githubusercontent.com/22646751/158924467-b6010180-d82a-4396-a93f-16b4d86d2436.png)

7) Ensure your Cursor Camera is setup just like the following:

![image](https://user-images.githubusercontent.com/22646751/158924529-ee8fe1c5-5f84-499e-99ce-a501d1d72371.png)

Scroll Bars (Not Dropdown Menus):
1) Attach to the Viewport of the Scroll View, "ScrollbarReceiverExtended.cs"
2) Attach to Scrollbar Vertical "ScrollbarReceiver.cs"
3) Assign the inspector objects
4) Do not mark "Is Scrollbar Instanced" unless the scrollbar is an instanced scrollbar, meaning that the scrollbar will be destroyed while it can be hovered.  This option is designed for Drop Down menus in mind.

![image](https://user-images.githubusercontent.com/22646751/158925562-689827d1-c589-4177-beeb-14f9d4ebb944.png)

Drop Down Menus (Scroll Bar)
1) Attach to the Viewport "ScrollBlock.cs"
2) Attach to the Scrollbar, "ScrollbarReceiver.cs", be sure to check true for "Is Scrollbar Instanced"
3) Assign Inspector objects

![image](https://user-images.githubusercontent.com/22646751/158925701-0c3fe9fc-4170-407c-9490-336b457e7313.png)

Sliders
1) On the gameobject with the "Slider" component attached, attach "SliderReceiver.cs"
2) Assign Inspector Objects

![image](https://user-images.githubusercontent.com/22646751/158925646-63250a7b-0a26-49bb-8235-c220bf748fae.png)
