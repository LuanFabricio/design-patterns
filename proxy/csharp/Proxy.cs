public class BigClassLazyProxy : IBigClass {
	BigClass? bigClass;

	public string GetName() {
		if (this.bigClass == null) {
			this.bigClass = new BigClass();
		}

		return this.bigClass.GetName();
	}
}

public class SecretProxy {
	SecretDataClass secret = new SecretDataClass();

	public int GetSecretOfLife(string username) {
		if ("super-secret-username" == username) {
			return this.secret.SecretOfLife();
		}
		throw new Exception("Invalid user");
	}
}
