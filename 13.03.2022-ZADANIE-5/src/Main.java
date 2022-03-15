import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.TimeZone;

import static java.lang.System.out;

class Czas{
    public void localTime(){
        Date dt = new Date();
        DateFormat f = new SimpleDateFormat("dd-MM-yyyy HH:mm:ss");
        out.println("Czas lokalny: " + f.format(dt));
    }

    public void globalTime(){
        Date dt = new Date();
        DateFormat f = new SimpleDateFormat("dd-MM-yyyy HH:mm:ss");
        f.setTimeZone(TimeZone.getTimeZone("UTC"));
        out.println("Czas globalny: " + f.format(dt));
    }
}
public class Main {
    public static void main(String[] args) {
        Czas czas = new Czas();
        czas.localTime();
        czas.globalTime();
    }
}
