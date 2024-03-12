Console.WriteLine("===========================================LINUX===================================================");
var App = new Application(new LinuxGUI());
App.AddButton();
App.AddButton();
App.AddButton();
App.AddCheckbox();
App.AddCheckbox();
App.AddCheckbox();
Console.WriteLine("Linux btns: {0}", App.GetButtonList().Count);
Console.WriteLine("Linux checkboxes: {0}", App.GetCheckboxList().Count);

Console.WriteLine("==========================================WINDOWS==================================================");

App = new Application(new WinGUI());
App.AddButton();
App.AddButton();
App.AddButton();
App.AddCheckbox();
Console.WriteLine("Windows btns: {0}", App.GetButtonList().Count);
Console.WriteLine("Windows checkboxes: {0}", App.GetCheckboxList().Count);


class Application {
	IGUIFactory Factory;
	List<Button> Buttons = new List<Button>();
	List<Checkbox> Checkboxes = new List<Checkbox>();

	public Application(IGUIFactory Factory) {
		this.Factory = Factory;
	}

	public void AddButton() {
		this.Buttons.Add(this.Factory.CreateButton());
	}

	public void AddCheckbox() {
		this.Checkboxes.Add(this.Factory.CreateCheckbox());
	}

	public List<Button> GetButtonList() {
		return this.Buttons;
	}

	public List<Checkbox> GetCheckboxList() {
		return this.Checkboxes;
	}
}

class LinuxGUI : IGUIFactory {
	public Button CreateButton() {
		return new Button("Test btn");
	}

	public Checkbox CreateCheckbox() {
		return new Checkbox("Test checkbox");
	}
}

class WinGUI : IGUIFactory {
	public Button CreateButton() {
		return new Button("Test btn");
	}

	public Checkbox CreateCheckbox() {
		return new Checkbox("Test checkbox");
	}
}

class Button {
	string Text;

	public Button(string Text) {
		this.Text = Text;
	}
}

class Checkbox {
	string Text;
	bool Toggle;

	public Checkbox(string Text, bool Toggle = false) {
		this.Text = Text;
		this.Toggle = Toggle;
	}
}

interface IGUIFactory {
	Button CreateButton();
	Checkbox CreateCheckbox();
}
