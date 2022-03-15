import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) throws IOException {
	    File file = new File("C:\\Users\\Dominik\\Desktop\\plik.txt");

        try {
            FileInputStream in = new FileInputStream(file);

            int c;
            while ((c = in.read()) != -1){
                out.write(c);
            }
        }catch (FileNotFoundException e){
            out.println("Nie znaleziono pliku!");
            System.exit(1);
        }
    }
}
