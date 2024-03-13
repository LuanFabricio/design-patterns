var win = new Windows();

win.SetPrinter(new Epson());
win.Print();
Console.WriteLine();

win.SetPrinter(new HP());
win.Print();
Console.WriteLine();

var linux = new Linux();
linux.SetPrinter(new Epson());
linux.Print();
Console.WriteLine();

linux.SetPrinter(new HP());
linux.Print();

class Windows {
	IPrinter? printer;

	public void SetPrinter(IPrinter printer) {
		this.printer = printer;
	}

	public void Print() {
		if (this.printer == null) {
			Console.WriteLine("Please, set a printer");
			return;
		}

		Console.WriteLine("Pritting on Windows");
		this.printer.Print();
	}
}

class Linux {
	IPrinter? printer;

	public void SetPrinter(IPrinter printer) {
		this.printer = printer;
	}

	public void Print() {
		if (this.printer == null) {
			Console.WriteLine("Please, set a printer");
			return;
		}

		Console.WriteLine("Pritting on Linux");
		this.printer.Print();
	}
}

class Epson : IPrinter {
	public void Print() {
		Console.WriteLine("Printing by a Epson");
	}
}

class HP : IPrinter {
	public void Print() {
		Console.WriteLine("Printing by a HP");
	}
}

interface IPrinter {
	public void Print();
}
