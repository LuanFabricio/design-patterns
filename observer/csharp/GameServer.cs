public class GameServer : IGameMove {
	const float MOV_SPEED = 10.0F;
	private Entity player;
	private List<ISubscriber> subscribers = new List<ISubscriber>();

	public GameServer() {
		Vector2 player_pos = new Vector2();
		player_pos.x = 42.0F;
		player_pos.y = 42.0F;
		Vector2 player_size = new Vector2();
		player_size.x = 42.0F;
		player_size.y = 42.0F;
		this.player = new Entity(player_pos, player_size);
	}

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
		this.notifyAll(action);
	}

	public void subscribe(ISubscriber subscriber) {
		this.subscribers.Add(subscriber);
	}

	public void notifyAll(Action action) {
		foreach (ISubscriber s in this.subscribers) {
			s.notify(action);
		}
	}

	public void log() {
		Console.WriteLine($"Position: {this.player.pos.x}, {this.player.pos.y}");
		Console.WriteLine($"Size: {this.player.size.x}, {this.player.size.y}");
	}

}
