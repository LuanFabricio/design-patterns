public class Entity {
	public Vector2 pos = new Vector2();
	public Vector2 size = new Vector2();

	public Entity (Vector2 pos, Vector2 size) {
		this.pos = pos;
		this.size = size;
	}

	public Entity (float pos_x, float pos_y, float width, float height) {
		this.pos.x = pos_x;
		this.pos.y = pos_y;
		this.size.x = width;
		this.size.y = height;
	}

}
