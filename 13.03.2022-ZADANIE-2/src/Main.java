import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Scanner;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) throws FileNotFoundException {
        File file = new File("C:\\Users\\Dominik\\Desktop\\zadanie2.txt");
        FileOutputStream fos = new FileOutputStream(file);
        Scanner scan = new Scanner(System.in);
        out.println("Wprowadź jakiś ciąg znaków:");
        String in = scan.nextLine();

        try {
            byte[] b = in.getBytes();
            fos.write(b);

            fos.close();
            out.println("Plik został poprawnie zapisany!");
        }catch (IOException e){
            e.printStackTrace();
        }
    }
}
