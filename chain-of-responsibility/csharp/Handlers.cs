public class AuthenticationHandler : HandlerBase {
	public override void Handle(Request request) {
		if (isValidAccount(request) && this.next != null) {
			this.next.Handle(request);
			return;
		}

		Console.WriteLine("Not valid account");
	}

	bool isValidAccount(Request request) {
		return request.username.EndsWith("test") && request.password.EndsWith("test");
	}
}

public class AuthorizationHandler : HandlerBase {
	public override void Handle(Request request) {
		if (isValidPermission(request)) {
			if (this.next != null) {
				this.next.Handle(request);
			} else {
				Console.WriteLine("Valid account!");
			}
			return;
		}
		Console.WriteLine("Account not authorized");
	}

	bool isValidPermission(Request request) {
		return (request.username.Length + request.password.Length) == 21;
	}
}

public abstract class HandlerBase : IHandler {
	protected IHandler? next;

	public void SetHandler(IHandler handler) {
		this.next = handler;
	}

	public abstract void Handle(Request request);
}

public interface IHandler {
	public void SetHandler(IHandler handler);
	public void Handle(Request request);
}
