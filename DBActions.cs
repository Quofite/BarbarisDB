using System;
using System.IO;

namespace BarbarisDB {
    static class DBActions {

        /* 
            All data gonna be written in a such way: "key:data, key2:data2":

            id:7, name:Gleb
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

        // method for getting data from db
        static public string GetDataFromDB(string fileName, string requestString) {
            string[] filters = requestString.Split(" ");    // request string parsed to array
            string response = "";

            if(filters.Length % 2 != 0) {       // checking if number of filters is uneven
                if(filters.Length <= 3) {       // and if it's not more than 3

                    // case if amount = 1
                    if(filters.Length == 1) {

                        // if filter is *(all)
                        if(filters[0] == "*") {
                            using(StreamReader reader = new StreamReader(fileName + ".bdb")) {
                                string? line;

                                while((line = reader.ReadLine()) != null) {
                                    string[] keysAndValues = line.Split(", ");  // spliting data clusters from each other
                                    
                                    for (int i = 0; i < keysAndValues.Length; i++) {
                                        // getting data from key
                                        string[] separated = keysAndValues[i].Split(":");
                                        response += separated[1] + ":";
                                    }

                                    response += ";";    // adding ';' at the end of line
                                }
                            }
                        } else {    // and if it's 'key=value' format
                            string[] filter = filters[0].Split("=");    // separating requested parameter to key and value

                            using(StreamReader reader = new StreamReader(fileName + ".bdb")) {
                                string? line;

                                while((line = reader.ReadLine()) != null) {
                                    string[] keysAndValues = line.Split(", ");

                                    for (int i = 0; i < keysAndValues.Length; i++) {
                                        string[] separated = keysAndValues[i].Split(":");

                                        // here is almost same system but with checking for key and value
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
                        // spliting all filters
                        string[] keyAndValue1 = filters[0].Split("=");  // key=value
                        string logicFilter = filters[1];                // and/or
                        string[] keyAndValue2 = filters[2].Split("=");  // key=value

                        using(StreamReader reader = new StreamReader(fileName + ".bdb")) {
                            string? line;
                            byte needableCoincidences = default;

                            // for 'and' condition is necessary to get both filter consided
                            // but for 'or' condition is enought to get any of filters
                            if(logicFilter == "and")
                                needableCoincidences = 2;
                            else if(logicFilter == "or")
                                needableCoincidences = 1;
                            else
                                return "Unknown logic filter";

                            while((line = reader.ReadLine()) != null) {
                                string[] keysAndValues = line.Split(", ");
                                byte lineChecks = 0;    // number of considences

                                for (int i = 0; i < keysAndValues.Length; i++) {
                                    string[] separated = keysAndValues[i].Split(":");

                                    // this system is simmilar to previous both but instead of writing line to response
                                    // it's increasing itterator
                                    if(separated[0] == keyAndValue1[0]) {
                                        if(separated[1] == keyAndValue1[1]) {
                                            lineChecks++;
                                        }
                                    }
                                            
                                    if(separated[0] == keyAndValue2[0]) {
                                        if(separated[1] == keyAndValue2[1]) {
                                            lineChecks++;
                                        }
                                    }
                                }

                                // and if this itterator is more than needable than we write data to response
                                if(lineChecks >= needableCoincidences) {
                                    for (int j = 0; j < keysAndValues.Length; j++) {
                                        response += keysAndValues[j].Split(":")[1] + ":";
                                    }

                                    response += ";";
                                }  
                            }
                        }
                    }
                } else {
                    return "Too many parameters";
                }
            } else {
                return "Wrong statement";
            }

            // getting rid of odd statements in response
            response = response.Replace("::;", ";");
            response = response.Replace(":;", ";");
            response = response.Replace(";;", ";");
            return response;
        }
    }
}