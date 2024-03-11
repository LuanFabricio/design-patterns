var notifier = new Notifier("usuarioteste@dominiotest.com", "@usarioteste");

notifier.enable("all");
notifier.send("Hello, world!");

class Notifier : INotify {
	private Dictionary<string, NotifyImplementaion> wrapper = new Dictionary<string, NotifyImplementaion>();

	public Notifier(string email, string slackTarget) {
		this.wrapper["twitter"] = new NotifyImplementaion(new TwitterNotify());
		this.wrapper["email"] = new NotifyImplementaion(new EmailNotify(email));
		this.wrapper["slack"] = new NotifyImplementaion(new SlackNotify(slackTarget));
	}

	public void enable(string decorator) {
		if (decorator == "all") {
			foreach (var key in this.wrapper.Keys) {
				this.wrapper[key].Active();
			}
			return;
		}

		this.wrapper[decorator].Active();
	}

	public void send(string text) {
		foreach (var key in this.wrapper.Keys) {
			if(this.wrapper[key].IsActive()) {
				this.wrapper[key].send(text);
			}
		}

		Console.WriteLine(String.Format("[Notifier]: {0}", text));
	}
}

class NotifyImplementaion : INotify {
	private INotify notifier;
	private bool active = false;

	public NotifyImplementaion(INotify notifier) {
		this.notifier = notifier;
	}

	public void Active() {
		this.active = true;
	}

	public bool IsActive() {
		return active;
	}

	public void send(string text) {
		this.notifier.send(text);
	}
}

class TwitterNotify : INotify {
	public void send(string text)
	{
		Console.WriteLine(String.Format("[Twitter]: {0}", text));

	}
}

class EmailNotify : INotify {
	private string email;

	public EmailNotify(string email) {
		this.email = email;
	}

	public void send(string text) {
		Console.WriteLine(String.Format("[{0}] {1}", email, text));
	}
}

class SlackNotify : INotify {
	private string target;

	public SlackNotify(string target) {
		this.target = target;
	}

	public void send(string text) {
		Console.WriteLine(String.Format("[Slack: {0}] {1}", this.target, text));
	}
}

interface INotify {
	public void send(string text);
}
