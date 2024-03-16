var creator1 = new ConcreteCreator1();
creator1.LogProductData();
Console.WriteLine();

var creator2 = new ConcreteCreator2();
creator2.LogProductData();

public abstract class Creator {
	public abstract IProduct factoryMethod();

	public void LogProductData() {
		var product = this.factoryMethod();

		Console.WriteLine($"[{product.GetName()}] - {product.Operation()}");
	}
}

public class ConcreteCreator1 : Creator {
	public override IProduct factoryMethod() {
		return new Product1();
	}
}

public class ConcreteCreator2 : Creator {
	public override IProduct factoryMethod() {
		return new Product2();
	}
}

public class Product1 : IProduct {
	public string GetName() {
		return "product1";
	}

	public string Operation() {
		return "operation1";
	}
}

public class Product2 : IProduct {
	public string GetName() {
		return "Product 2 name";
	}

	public string Operation() {
		return "Operation 2";
	}
}

public interface IProduct {
	public string GetName();
	public string Operation();
}
