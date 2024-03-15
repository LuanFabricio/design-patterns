var autenticator = new AutenticatorDialog("Login");

autenticator.loginOrRegister.SetChecked(false);
autenticator.regUsername.SetContent("TestUser");
autenticator.regPassword.SetContent("TestPassword");
autenticator.regEmail.SetContent("testemail@testdomain.com");
autenticator.ok.OnClick();

Console.WriteLine();

autenticator.loginOrRegister.SetChecked(true);
autenticator.loginUsername.SetContent("TestUser");
autenticator.loginPassword.SetContent("TestPassword");
autenticator.ok.OnClick();

Console.WriteLine();

autenticator.loginOrRegister.SetChecked(true);
autenticator.loginUsername.SetContent("WrongUser");
autenticator.loginPassword.SetContent("WrongPassword");
autenticator.ok.OnClick();

class AutenticatorDialog : IMediator {
	string title;
	// Public for demo reasons
	public CheckBox loginOrRegister;
	public TextBox loginUsername, loginPassword;
	public TextBox regUsername, regPassword, regEmail;
	public Button ok, cancel;
	public CheckBox rememberMe;

	List<User> users;

	public AutenticatorDialog(string title) {
		this.title = title;
		this.loginOrRegister = new CheckBox(this);

		this.loginUsername = new TextBox(this);
		this.loginPassword = new TextBox(this);

		this.regUsername = new TextBox(this);
		this.regEmail = new TextBox(this);
		this.regPassword = new TextBox(this);

		this.ok = new Button(this);
		this.cancel = new Button(this);

		this.rememberMe = new CheckBox(this);

		this.users = new List<User>();
	}

	public void Notify(Component Sender, string Event) {
		if (Sender == this.loginOrRegister && Event == "check") {
			if (this.loginOrRegister.GetChecked()) {
				this.title = "Login";
			} else {
				this.title = "Register";
			}
			return;
		}
		if (Sender == this.ok && Event == "click"){
			if (loginOrRegister.GetChecked()) {
				if (this.checkAccout()) Console.WriteLine("Login realizado com sucesso!");
				else Console.WriteLine("Erro na hora de realizar o login");
			} else {
				this.users.Add(new User(
							this.regUsername.GetContent(),
							this.regPassword.GetContent(),
							this.regEmail.GetContent()));
				Console.WriteLine("Conta criada com sucesso!");
			}
		}
	}

	bool checkAccout() {
		string username = this.loginUsername.GetContent();
		string password = this.loginPassword.GetContent();

		foreach (User u in this.users) {
			if (u.Validate(username, password)) return true;
		}

		return false;
	}
}

class Button : Component {
	public Button(IMediator mediator) : base(mediator) {}

	public void OnClick() {
		this.mediator.Notify(this, "click");
	}
}

class CheckBox : Component {
	bool boxChecked = false;

	public CheckBox(IMediator mediator) : base(mediator) {}

	public void OnClick() {
		this.boxChecked = !this.boxChecked;
		this.mediator.Notify(this, "check");
	}

	public void SetChecked(bool newBoxChecked) {
		this.boxChecked = newBoxChecked;
	}

	public bool GetChecked() {
		return this.boxChecked;
	}
}

class TextBox : Component {
	string content;

	public TextBox(IMediator mediator, string initialContent = "") : base(mediator) {
		this.content = initialContent;
	}

	public void SetContent(string content) {
		this.content = content;
	}

	public string GetContent() {
		return this.content;
	}
}



class Component {
	protected IMediator mediator;

	public Component(IMediator mediator) {
		this.mediator = mediator;
	}
}

interface IMediator {
	public void Notify(Component Sender, string Event);
}

class User {
	string username, password, email;

	public User(string username, string password, string email) {
		this.username = username;
		this.password = password;
		this.email = email;
	}

	public bool Validate(string username, string password) {
		return this.username == username && this.password == password;
	}
}
