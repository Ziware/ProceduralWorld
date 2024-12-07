using Godot;
using System;

public partial class MoveCamera : Camera3D {
	private float Clamp(float value, float min, float max) {
		return Math.Max(Math.Min(max, value), min);
	}

	private Vector3 TransformCameraDirection(Vector3 direction) {
		direction = direction.Rotated(Vector3.Right, Rotation.X);
		direction = direction.Rotated(Vector3.Up, Rotation.Y);
		return direction;
	}
	public override void _Ready() {
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Process(double delta) {
		Vector3 newRotation = Rotation;
		newRotation.X = Clamp(newRotation.X, (float)-Math.PI, (float)Math.PI);
		Rotation = newRotation;
		var cameraDirection = TransformCameraDirection(Vector3.Forward);
		var cameraUp = TransformCameraDirection(Vector3.Up);
		if (Input.IsKeyPressed(Key.W)) {
			Position += cameraDirection * (float)delta;
		}
		if (Input.IsKeyPressed(Key.S)) {
			Position -= cameraDirection * (float)delta;
		}
		if (Input.IsKeyPressed(Key.A)) {
			Position += cameraDirection.Rotated(cameraUp, (float)Math.PI / 2) * (float)delta;
		}
		if (Input.IsKeyPressed(Key.D)) {
			Position += cameraDirection.Rotated(cameraUp, -(float)Math.PI / 2) * (float)delta;
		}
	}
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion motion) {
			Rotate(Vector3.Up, -motion.Relative.X * 0.001f);
			RotateObjectLocal(Vector3.Right, -motion.Relative.Y * 0.001f);
		}
    }
}
