/**
 * Created by Гена on 1/15/2016.
 */

public class HelloWorld {
    public static void main(String[] args){
        System.out.println("HelloWorld!");
        Tv tv  = new Tv();
        tv.Off();
        tv.On();
        tv.ChangeChannel(35);
        for (int i = 0; i< 10; i++) {
            System.out.println(i);
        }
    }
}
