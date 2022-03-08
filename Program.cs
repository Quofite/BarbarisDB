namespace BarbarisDB {
    class Program {
        static void Main(string[] args) {
            /*
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
            */



            // this commands below used to test
            DBActions.SaveDataToDB("testfile", "id:4, name:gleb");
            DBActions.SaveDataToDB("testfile", "id:34, surname:nikitin");
            DBActions.SaveDataToDB("testfile", "name:gleb, surname:what");

            Console.WriteLine(DBActions.GetDataFromDB("testfile", "name=gleb"));
        }
    }

}