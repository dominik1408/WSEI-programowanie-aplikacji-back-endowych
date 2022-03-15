import static java.lang.System.out;

class CRC {
    int[] CRC_TABLE;

    public CRC(){
        CRC_TABLE = new int[256];

        for (int i = 0; i < 256; ++i) {
            int code = -1;
            for (int j = 0; j < 8; ++j) {
                code = ((code & 0x01) != 0 ? 0xEDB88320 ^ (code >>> 1) : (code >>> 1));
            }
            CRC_TABLE[i] = code;
        }
    }
    public int crc32(String text){
        int crc = -1;
        for (int i = 0; i < text.length(); ++i) {
            int code = Character.codePointAt(text, i);
            crc = CRC_TABLE[(code ^ crc) & 0xFF] ^ (crc >>> 8);
        }
        return (-1 ^ crc) >>> 0;
    }
}

public class Main {
    public static void main(String[] args) {
        CRC crc = new CRC();
        out.println(crc.crc32("This is example text..."));
    }
}
