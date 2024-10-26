using Godot;
using System;

public partial class Player : Area2D
{

	[Export]
	public int Speed {get; set;} = 400; //how fast the player will move (pixels/sec)

	public Vector2 ScreenSize; //size of the game window

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero; //the player's movement vector.
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if(Input.IsActionPressed("move_right")){
			velocity.X += 1;
			animatedSprite2D.Play("walk_right");
		}
		if(Input.IsActionPressed("move_left")){
			velocity.X -= 1;
			animatedSprite2D.Play("walk_left");
		}
		if(Input.IsActionPressed("move_up")){
			velocity.Y -= 1;
		}
		if(Input.IsActionPressed("move_down")){
			velocity.Y += 1;
		}

		if(velocity.Length() > 0){
			velocity = velocity.Normalized() * Speed;	//normalizes diagonal speed
		}
		if(velocity.X == 0 && velocity.Y == 0){
			animatedSprite2D.Play("default");
		}


		Position += velocity * (float)delta;
		//prevents player from walking off screen
		// Position = new Vector2(
		// 	x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
		// 	y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		// );
	}
}
