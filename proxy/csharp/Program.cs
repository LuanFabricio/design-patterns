var lazyProxy = new BigClassLazyProxy();
Console.WriteLine($"Lazy proxy: {0}", lazyProxy.GetName());
Console.WriteLine();

var secProxy = new SecretProxy();
Console.WriteLine($"Secret proxy: {secProxy.GetSecretOfLife("super-secret-username")}");
Console.WriteLine();
try {
	Console.WriteLine($"Secret proxy: {secProxy.GetSecretOfLife("random")}");
} catch(Exception e) {
	Console.WriteLine(e.Message);
}
