GameServer server = new GameServer();
GameClient client1 = new GameClient();
GameClient client2 = new GameClient();

server.subscribe(client1);
server.subscribe(client2);

server.move(Action.MOVE_DOWN);
server.move(Action.MOVE_DOWN);
server.move(Action.MOVE_DOWN);
server.move(Action.MOVE_DOWN);

server.log();

client1.log();
client2.log();

public enum Action {
	MOVE_UP,
	MOVE_DOWN,
	MOVE_RIGHT,
	MOVE_LEFT,
}

public interface ISubscriber {
	void notify(Action action);
}

public interface IGameMove {
	void move(Action action);
}
