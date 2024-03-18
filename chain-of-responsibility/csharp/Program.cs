var auth1 = new AuthenticationHandler();
var auth2 = new AuthorizationHandler();

auth1.SetHandler(auth2);

var request = new Request("usuario_test", "pass_test");
auth1.Handle(request);
Console.WriteLine();

request = new Request("usuario_____", "pass_test");
auth1.Handle(request);
Console.WriteLine();

request = new Request("user_test", "pass_test");
auth1.Handle(request);
