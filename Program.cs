namespace BarbarisDB {
    class Program {
        static void Main(string[] args) {

            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.Run(async (context) => {
                var response = context.Response;
                var request = context.Request;

                if(request.Path == "/") {

                    if(request.HasJsonContentType()) {
                        var recieved = await request.ReadFromJsonAsync<Recieved>();

                        if(recieved != null) {
                            string method = recieved.method;
                            string file = recieved.file;
                            string data = recieved.data;

                            if(method == "set") {
                                DBActions.SaveDataToDB(file, data);
                                Returned returned = new Returned("output", "Data is saved.");
                                await response.WriteAsJsonAsync(returned);
                            } else if(method == "get") {
                                Returned returned = new Returned("output", DBActions.GetDataFromDB(file, data));
                                await response.WriteAsJsonAsync(returned);
                            } else {
                                Returned returned = new Returned("error", $"Wrong request method. It must be 'set' or 'get' but you used {method}.");
                                await response.WriteAsJsonAsync(returned);
                            }
                        } else {
                            Returned returned = new Returned("error", "Request is null.");
                            await response.WriteAsJsonAsync(returned);
                        }
                    } else {
                        Returned returned = new Returned("error", "Request doesn't contain JSON.");
                        await response.WriteAsJsonAsync(returned);
                    }
                } else {
                    Returned returned = new Returned("error", "Request should be on '/'.");
                    await response.WriteAsJsonAsync(returned);
                }
            });

            app.Run(); 
        }
    }

    public record Recieved(string method, string file, string data);

    public record Returned(string type, string output);
}