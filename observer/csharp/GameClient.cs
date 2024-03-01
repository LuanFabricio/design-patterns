public class GameClient : ISubscriber, IGameMove {
	const float MOV_SPEED = 10.0F;
	Entity player = new Entity(42.0F, 42.0F, 42.0F, 42.0F);

	public void move(Action action) {
		switch (action) {
			case Action.MOVE_UP:
				this.player.pos.y -= MOV_SPEED;
				break;
			case Action.MOVE_DOWN:
				this.player.pos.y += MOV_SPEED;
				break;
			case Action.MOVE_LEFT:
				this.player.pos.x -= MOV_SPEED;
				break;
			case Action.MOVE_RIGHT:
				this.player.pos.x += MOV_SPEED;
				break;
		}
	}

	public void notify(Action action) {
		this.move(action);
	}

	public void log() {
		Console.WriteLine($"Position: {this.player.pos.x}, {this.player.pos.y}");
		Console.WriteLine($"Size: {this.player.size.x}, {this.player.size.y}");
	}
}
