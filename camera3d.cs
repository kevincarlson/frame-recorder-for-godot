////////////////////////////////////////////////////////////////////////////////
//
//  Frame Recorder for Godot Game Engine
//  Copyright (C) 2021 by Kevin Carlson
//
//  This script is released under the terms of the MIT license.
//
////////////////////////////////////////////////////////////////////////////////

using Godot;
using System;

public class camera : Camera
{
	bool standalone = OS.HasFeature("standalone");
	
	public override void _Ready()
	{
		if (standalone) {
			GetViewport().Msaa = Godot.Viewport.MSAA.Msaa8x;
			GetViewport().Size = GetViewport().Size * 4;
			Directory outputDir = new Directory();
			outputDir.MakeDir("user://render");
		}
	}

	public override void _Process(float delta)
	{
		int framesDrawn = Engine.GetFramesDrawn();
		
		if (standalone && framesDrawn > 0) {
			GD.Print("Rendering Frame " + framesDrawn + "..." );
			
			GetViewport().RenderTargetClearMode = Godot.Viewport.ClearMode.OnlyNextFrame;
			var image = GetViewport().GetTexture().GetData();
			image.GenerateMipmaps();
			image.Resize((int) GetViewport().Size.x / 4, (int) GetViewport().Size.y / 4, Image.Interpolation.Trilinear);
			image.FlipY();
			
			image.SavePng("user://render/cs_" + framesDrawn + ".png");
		}
	}

	public void _on_animation_player_animation_finished(String animationName) {
		GD.Print("Animation finished.");
		GetTree().Quit();
	}
}
