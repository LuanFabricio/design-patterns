var order = new Order();
order.TotalCost = 42;
order.IsClosed = true;

order.ProcessOrder(new PayByCreditCard());
order.ProcessOrder(new PayByPayPal());

class Order {
	private int totalCost = 0;
	private bool isClosed = false;

	public int TotalCost {
		get {
			return totalCost;
		}
		set {
			totalCost = value;
		}
	}

	public bool IsClosed {
		get {
			return isClosed;
		}
		set {
			isClosed = value;
		}
 }

	public void ProcessOrder(IPayStrategy payStrategy) {
		payStrategy.collectPaymentDetails();
	}
}

class PayByCreditCard : IPayStrategy {
	private string cardNumber = "";
	private string date = "";
	private string cvv = "";
	private bool isValid = false;

	private static Dictionary<string, CreditCardData> DATABASE = new Dictionary<string, CreditCardData>();

	public PayByCreditCard() {
		if (!DATABASE.ContainsKey("4242")) {
			DATABASE.Add("4242", new CreditCardData("4242", "04/02", "123"));
		}

		if (!DATABASE.ContainsKey("8080")) {
			DATABASE.Add("8080", new CreditCardData("8080", "08/08", "888"));
		}
	}

	public void collectPaymentDetails() {
		try {
			while(!this.isValid) {
				Console.WriteLine("Enter credit card number: ");
				string? cardNumber = Console.ReadLine();
				if (cardNumber == null) {
					continue;
				}
				this.cardNumber = cardNumber;

				Console.WriteLine("Enter expiration date: ");
				string? date = Console.ReadLine();
				if (date == null) {
					continue;
				}
				this.date = date;

				Console.WriteLine("Enter expiration cvv: ");
				string? cvv = Console.ReadLine();
				if (cvv == null) {
					continue;
				}
				this.cvv = cvv;

				if (this.verifyAccount()) {
					Console.WriteLine("Success!");
				} else {
					Console.WriteLine("Wrong e-mail or password!");
				}
			}
		} catch (IOException e) {
			Console.WriteLine(e);
		}
	}

	bool verifyAccount() {
		if (DATABASE.ContainsKey(this.cardNumber))
			this.isValid = DATABASE[this.cardNumber].Validate(this.cardNumber, this.date, this.cvv);
		return this.isValid;
	}

	public bool pay(int paymentAmount) {
		if (this.isValid) {
			Console.WriteLine(String.Format("Paying {0} using PayPal", paymentAmount));
		}
		return this.isValid;
	}
}

class PayByPayPal : IPayStrategy {
	private string email = "";
	private string password = "";
	private bool isValid = false;

	private static Dictionary<string, string> DATABASE = new Dictionary<string, string>();

	public PayByPayPal() {
		if (!DATABASE.ContainsKey("asdf")) {
			DATABASE.Add("asdf", "asdf@test.com");
		}

		if (!DATABASE.ContainsKey("test")) {
			DATABASE.Add("test", "test@test.com");
		}
	}

	public void collectPaymentDetails() {
		try {
			while(!this.isValid) {
				Console.WriteLine("Enter user's e-mail: ");
				string? email = Console.ReadLine();
				if (email == null) {
					continue;
				}
				this.email = email;
				Console.WriteLine("Enter user's password: ");
				string? password = Console.ReadLine();
				if (password == null) {
					continue;
				}
				this.password = password;

				if (this.verifyAccount()) {
					Console.WriteLine("Success!");
				} else {
					Console.WriteLine("Wrong e-mail or password!");
				}
			}
		} catch (IOException e) {
			Console.WriteLine(e);
		}
	}

	bool verifyAccount() {
		this.isValid = this.email.Equals(DATABASE[this.password]);
		return this.isValid;
	}

	public bool pay(int paymentAmount) {
		if (this.isValid) {
			Console.WriteLine(String.Format("Paying {0} using PayPal", paymentAmount));
		}
		return this.isValid;
	}
}

class CreditCardData {
	private string number;
	private string date;
	private string cvv;

	public CreditCardData(string number, string date, string cvv) {
		this.number = number;
		this.date = date;
		this.cvv = cvv;
	}

	public bool Validate(string number, string date, string cvv) {
		return this.number == number
			&& this.date == date
			&& this.cvv == cvv;
	}
}

public interface IPayStrategy {
	bool pay(int paymentAmount);
	void collectPaymentDetails();
}
