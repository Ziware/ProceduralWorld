using Godot;
using System;

public partial class MoveCamera : Camera3D
{
	Vector3 cameraDirection_;
	Vector3 cameraUp_;
	
	private float Clamp(float value, float min, float max) {
		return Math.Max(Math.Min(max, value), min);
	}
	public override void _Ready() {
		cameraDirection_ = Vector3.Forward;
		cameraUp_ = Vector3.Up;
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Process(double delta) {
		if (Input.IsKeyPressed(Key.W)) {
			Position += cameraDirection_ * (float)delta;
		}
		if (Input.IsKeyPressed(Key.S)) {
			Position -= cameraDirection_ * (float)delta;
		}
		if (Input.IsKeyPressed(Key.A)) {
			Position += cameraDirection_.Rotated(cameraUp_, (float)Math.PI / 2) * (float)delta;
		}
		if (Input.IsKeyPressed(Key.D)) {
			Position += cameraDirection_.Rotated(cameraUp_, -(float)Math.PI / 2) * (float)delta;
		}
		
		Vector3 newRotation = Rotation;
		newRotation.X = Clamp(newRotation.X, (float)-Math.PI, (float)Math.PI);
		Rotation = newRotation;
	}
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion motion) {
			Rotate(Vector3.Up, -motion.Relative.X * 0.001f);
			RotateObjectLocal(Vector3.Right, -motion.Relative.Y * 0.001f);
		}
    }
}
