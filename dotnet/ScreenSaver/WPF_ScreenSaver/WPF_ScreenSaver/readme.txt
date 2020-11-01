Last Changed: 20 Dec 2006
Version: 0.1
Author: Karen Corby (http://scorbs.com/work)
http://scorbs.com/2006/12/21/wpf-screen-saver-template

WPF Screen Saver Application Readme
----------------------------------
This project builds a WPF screen saver application.


Deployment Instructions
-----------------------
In order to deploy your screen saver, you must rename the 
RELEASE build of the screen saver executable to have a ".scr" extension:
	1.  Go to bin\Release folder of your project.
	2.  Rename the .exe to .scr.  
	    (e.g."ScreenSaver.exe" becomes "ScreenSaver.scr")
	3.  If using .NET Application Settings, see important note below.

User Installation Instructions
------------------------------
To install on a client machine:
	1.  Copy the .scr file (& any dependent files) to convenient location on your C: drive.
	2.  Right click the .scr file.
	3.  Select Install.

To configure on a client machine:
	1.  In the Windows Screen Saver Dialog, select the screen saver.
	2.  Click Settings button.

To uninstall:
	1.  Delete .scr file.

Files
-----
The following main files are used in this project:
	1.  App.xaml/App.xaml.cs - sets up screen saver application.
	2.  Window1.xaml/Window1.xaml.cs - main visuals of screen saver
	3.  Settings.xaml/Settings.xaml.cs - settings window of screen saver


Modes
-----
The screen saver can be launched with different command line arguments:
	<no args>  Display screen saver
	"/s"	   Display screen saver
	"/c"	   Show settings window


Debugging 
---------
In DEBUG configurations, the screen saver window's "topmost"-ness and 
"shutdown on key/mouse input" are disable.  To close the window, hit Ctl-F4.

To debug the settings window...
	1.  Go to the project properties pane. 
	    (Right click the project in the Solution Explorer 
             & select "Properties"). 
	2.  Select Debug on the left tabs.  
	3.  Find "Command Line Args" textbox under "Start Options".  
	4.  Enter: /c


Saving Screen Saver Settings
----------------------------
I highly recommend using the .NET Application Settings feature to 
save user-configured screen saver settings.  

More details at http://msdn2.microsoft.com/en-us/library/k4s6c3a0.aspx

For an example, see http://scorbs.com/work.


Important Note:
	By default, the .NET Application Settings framework stores 
	user settings based on the executing assembly name.  

	The Windows Screen Saver Dialog launches the screen saver (for 
	settings & preview) using the full assembly name 
	(e.g. MyCoolScreenSaver.scr).

	However, when launching the screensaver for real, Windows 
	uses the shortened version (e.g. MYCOOL~1.SCR).  Since the 
	name is different, the settings are loaded from a different place.

	If you use .NET Application Settings with the default settings store,
	YOU MUST GIVE YOUR SCREEN SAVER ASSEMBLY A NAME WITH 
	8 CHARACTERS OR LESS.


Multimon
--------
The screen saver duplicates the visuals on each monitor 
by creating an additional instances of Window1.


Release Notes
-------------
- Does not support real time preview in the Windows Screen Saver Dialog's 
  embedded display.



