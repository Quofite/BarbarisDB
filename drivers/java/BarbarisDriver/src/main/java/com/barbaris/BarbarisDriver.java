package com.barbaris;

import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.URL;
import java.net.URLConnection;

public class BarbarisDriver {
    public static String exec(String host, String method, String file, String data) throws Exception {
        // testing data
        if (dataTest(host, method, data)) {

            // making connection
            URL dbHost = new URL(host);
            URLConnection connection = dbHost.openConnection();

            // setting options and headers for connection
            connection.setDoOutput(true);
            connection.setDoInput(true);
            connection.setRequestProperty("Content-Type", "application/json");

            // creating json for server
            Gson gson = new Gson();
            JsonSendModel jsm = new JsonSendModel();
            jsm.setMethod(method);
            jsm.setFile(file);
            jsm.setData(data);

            // sending request to server
            OutputStreamWriter wr = new OutputStreamWriter(connection.getOutputStream());
            wr.write(gson.toJson(jsm));
            wr.flush();

            // getting response from server as json
            BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
            String inputLine;
            Gson g = new Gson();
            inputLine = in.readLine();
            JsonGetModel model = g.fromJson(inputLine, JsonGetModel.class);

            in.close();

            if(model.type.equals("error")) {
                System.out.println("Error: " + model.output);
                return "There is an error in your request. Check the log in terminal.";
            } else if (model.output.startsWith("Error:")) {
                System.out.println(model.output);
                return "There is an error in your request. Check the log in terminal.";
            } else {
                return model.output;
            }
        } else {
            return "There is an error in your request. Check the log in terminal.";
        }
    }

    private static boolean dataTest(String host, String method, String data) {
        if(host.indexOf("http://") == 0 || host.indexOf("https://") == 0) {
            if(method.equals("set")) {
                if(data.endsWith(";")) {
                    return true;
                } else {
                    System.out.println("Data error: Data string does not end with semicolon (;).");
                    return false;
                }
            } else if(method.equals("get")) {
                return true;
            } else {
                System.out.println("Method error: Method is not \"get\" or \"set\", try to check the case.");
                return false;
            }
        } else {
            System.out.println("Host error: HTTP or HTTPS did not find.");
            return false;
        }
    }
}