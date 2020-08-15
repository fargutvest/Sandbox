import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

public class JProgressBarTutorial {

	final static int interval = 1000;
	int i;
	Timer t;
	JButton btn;
	JProgressBar pbr;
	
	public JProgressBarTutorial() {
		
		JFrame frame = new JFrame();
		
		frame.setLayout(new FlowLayout());
		
		btn = new JButton("START TICKING");
		
		btn.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent ae) {
				i = 0;
				t.start();
				btn.setEnabled(false);
			}
		});
		
		pbr = new JProgressBar(0, 20);
		pbr.setValue(0);
		pbr.setStringPainted(true);
		
		frame.add(pbr);
		frame.add(btn);
		
		t = new Timer(interval, new ActionListener() {
			public void actionPerformed(ActionEvent ae) {
				if (i == 20) {
					t.stop();
					btn.setEnabled(true);
				}
				else {
					i++;
					pbr.setValue(i);
				}
			}
		});
		
		frame.setVisible(true);
		frame.setSize(300, 300);
	}	
}
