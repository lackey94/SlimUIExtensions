# SlimUIExtensions

You are free to use this however you like, this was roughly written so I don't guarantee it to work for your project, there might be bugs (more than likely will) however, I don't have the time to fully flesh this out.

I'm just leaving this open for any other person who uses SlimUI https://assetstore.unity.com/packages/tools/gui/cursor-controller-pro-161628 and need it to be expanded such that you can interact with sliders and scrollbars in Unity while using the old Unity Input System.

![image](https://user-images.githubusercontent.com/22646751/156556824-66fdf60c-bfc5-4671-b084-dd2c4c243bd7.png)

Attach Scrollblock to Viewport and Scrollbar Receiver to Scrollbar, these are instanced items that will auto reference Action Monitor.  Be sure to still reference one another's scripts in the inspector or else it won't work.

![image](https://user-images.githubusercontent.com/22646751/156557010-3f224275-993e-4949-9c79-856ed39c79b8.png)


SliderReceiver is attached to Slider Object with the Slider Script.  It will auto reference but just to save a FindObjectOfType<> call, just reference it in the inspector.

![image](https://user-images.githubusercontent.com/22646751/156557157-d73ec027-7d27-435b-bc05-6e98846a2d42.png)

Attach Rewired UI Action Monitor to the CursorController root object

![image](https://user-images.githubusercontent.com/22646751/156557302-c535b52b-d9ef-40d6-837e-7550994206b0.png)

Attach Rewired Input Provider to an Input System Profider Object that is sub to the root CursorControl object.

The CursorCotnrol object should look like this:

![image](https://user-images.githubusercontent.com/22646751/156557353-d5c8ab9d-5a67-4a35-9228-80f7c66bdaf1.png)
