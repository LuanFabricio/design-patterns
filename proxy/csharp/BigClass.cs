public class BigClass : IBigClass {
	string name = "AndroidStudio";

	public string GetName() {
		return this.name;
	}
}

interface IBigClass {
	public string GetName();
}
