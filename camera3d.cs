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

public class camera3d : Camera
{
	[Export]
	private String _outputDir = "user://render";
	
	public override void _Ready()
	{
		GetViewport().Msaa = Godot.Viewport.MSAA.Msaa8x;
		Directory outputDir = new Directory();
		outputDir.MakeDir(_outputDir);
	}

	public override void _Process(float delta)
	{
		int framesDrawn = Engine.GetFramesDrawn();
		
		if (framesDrawn > 0)
		{
			GetViewport().RenderTargetClearMode = Godot.Viewport.ClearMode.OnlyNextFrame;
			var image = GetViewport().GetTexture().GetData();
			image.GenerateMipmaps();
			image.FlipY();
			
			image.SavePng(_outputDir + "/cs_" + framesDrawn.ToString("D8") + ".png");
		}
		
		GD.Print("Rendered Frame " + framesDrawn.ToString("D8") + "..." );
	}

	private void _on_AnimationPlayer_animation_finished(String anim_name)
	{
		GD.Print("Animation finished.");
		GetTree().Quit();
	}
}
