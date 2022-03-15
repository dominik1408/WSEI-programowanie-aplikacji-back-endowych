package com.company;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import static java.lang.System.out;

public class Main {

    public static void main(String[] args) throws IOException {
        FileInputStream in = null;

        try {
            in = new FileInputStream("C:\\Users\\Dominik\\Desktop\\plik.txt");

            int c;
            while ((c = in.read()) != -1) {
                out.write(c);
            }
        } catch (FileNotFoundException e) {
            out.println("Nie znaleziono pliku!");
            System.exit(1);
        }
    }
}
