package com.barbaris;

public class Program {
    public static void main(String[] args) throws Exception {
        System.out.println(BarbarisDriver.exec("http://localhost:5000/", "set", "testfile", "id:3, name:Ivan"));
        System.out.println(BarbarisDriver.exec("http://localhost:5000", "get", "testfile", "id=3 and name=Ivan"));
    }
}
