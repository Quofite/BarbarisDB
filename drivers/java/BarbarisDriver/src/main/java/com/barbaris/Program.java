package com.barbaris;

public class Program {
    public static void main(String[] args) throws Exception {
        System.out.println(BarbarisDriver.exec("http://localhost:5000", "set", "testfile", "id:4, name:Gleb;"));
        System.out.println(BarbarisDriver.exec("http://localhost:5000", "get", "testfile", "*"));
    }
}
