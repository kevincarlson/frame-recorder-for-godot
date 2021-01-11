# Frame Recorder for Godot Game Engine

A screen recording script for the Godot Game Engine, implemented in Mono/C#. Currently represents MVP, and saves video as a sequence of PNG images.

Based on [Godot video rendering demo](https://github.com/Calinou/godot-video-rendering-demo) by [Hugo Locurcio](https://hugo.pro/).

## Using

All relevant code is contained within `camera3d.cs`, and does not depend on any particular project structure. Simply add the file to an existing Godot project, and attach the script to a Camera node in a scene, to record that camera. Note that this will begin recording as soon as the scene is displayed, and will continue for as long as the camera's `_Process` loop continues.

## Limitations

At present, this does not record any audio. This particular method of recording also creates a *significant* performance hit, which makes this unsuitable for recording gameplay footage.
