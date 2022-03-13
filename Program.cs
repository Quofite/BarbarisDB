namespace BarbarisDB {
    class Program {
        static void Main(string[] args) {

            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.Run(async (context) => {
                var response = context.Response;
                var request = context.Request;

                if(request.Path == "/") {
                    Console.WriteLine("1");
                    if(request.HasJsonContentType()) {
                        var recieved = await request.ReadFromJsonAsync<Recieved>();
                        Console.WriteLine("2");
                        if(recieved != null) {
                            string method = recieved.method;
                            string file = recieved.file;
                            string data = recieved.data;
                            Console.WriteLine("3");
                            if(method == "set") {
                                DBActions.SaveDataToDB(file, data);
                                Returned returned = new Returned("output", "Data is saved");
                                await response.WriteAsJsonAsync(returned);
                            } else if(method == "get") {
                                Returned returned = new Returned("output", DBActions.GetDataFromDB(file, data));
                                await response.WriteAsJsonAsync(returned);
                            } else {
                                Returned returned = new Returned("output", "Wrong request");
                                await response.WriteAsJsonAsync(returned);
                            }
                        } else {
                            Returned returned = new Returned("output", "Request is null");
                            await response.WriteAsJsonAsync(returned);
                            Console.WriteLine("4");
                        }
                    } else {
                        Returned returned = new Returned("output", "Request doesn't contain JSON");
                        await response.WriteAsJsonAsync(returned);
                        Console.WriteLine("5");
                    }
                } else {
                    Returned returned = new Returned("output", "Request should be on '/'");
                    await response.WriteAsJsonAsync(returned);
                    Console.WriteLine("6");
                }
            });

            Console.WriteLine("7");
            app.Run(); 
        }
    }

    public record Recieved(string method, string file, string data);

    public record Returned(string type, string output);
}