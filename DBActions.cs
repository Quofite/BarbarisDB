using System;
using System.IO;

namespace BarbarisDB {
    static class DBActions {

        /* 
            All data gonna be written in a such way: "key:data, key2:data2":

            id:7, name:Gleb;
            id:13, name:John, surname:Wilson
            surname:Tompson, role:admin

            This method should make data finding a little bit slow but easy-to-deal-with,
            because data won't be chained to each other by having same unneedable fields:

            id:7, name:Gleb, surname:null, role:null
            id:13, name:John, surname:Wilson, role:null
            id:null, name:null, surname:Tompson, role:admin

            The main thing is to use proper conditions of search("where id=...." for example).

            All lines will be formed in drivers.
        */

        // As lines will be formed in driver so it's the most simple method in C# history
        static public async void SaveDataToDB(string fileName, string data) {
            using(StreamWriter writer = new StreamWriter(fileName + ".bdb", true)) {
                await writer.WriteLineAsync(data);
            }
        }

        static public string GetDataFromDB(string fileName, string requestString) {
            string[] filters = requestString.Split(" ");
            string response = "";

            if(filters.Length % 2 != 0) {
                if(filters.Length <= 9) {



                    if(filters.Length == 1) {
                        if(filters[0] == "*"){
                            using(StreamReader reader = new StreamReader(fileName + ".bdb")) {
                                string? line;

                                while((line = reader.ReadLine()) != null) {
                                    string[] keysAndValues = line.Split(", ");
                                    
                                    for (int i = 0; i < keysAndValues.Length; i++) {
                                        string[] separated = keysAndValues[i].Split(":");
                                        response += separated[1] + ":";
                                    }

                                    response += ";";
                                }
                            }
                        } else {
                            string[] filter = filters[0].Split("=");

                            using(StreamReader reader = new StreamReader(fileName + ".bdb")) {
                                string? line;

                                while((line = reader.ReadLine()) != null) {
                                    string[] keysAndValues = line.Split(", ");

                                    for (int i = 0; i < keysAndValues.Length; i++) {
                                        string[] separated = keysAndValues[i].Split(":");

                                        if(separated[0] == filter[0]) {
                                            if(separated[1] == filter[1]) {
                                                for (int j = 0; j < keysAndValues.Length; j++) {
                                                    response += keysAndValues[j].Split(":")[1] + ":";
                                                }
                                            }
                                        }
                                    }

                                    response += ";";
                                }
                            }
                        }

                    }



                    if(filters.Length == 3) {


                    }
                }
            }

            response = response.Replace("::;", ";");
            response = response.Replace(":;", ";");
            response = response.Replace(";;", ";");
            return response;
        }
    }
}