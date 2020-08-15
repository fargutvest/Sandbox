package com.michaldabski.filemanager.folders;

import android.app.Activity;
import android.os.Environment;
import android.os.Bundle;


public class FolderActivity extends Activity
{
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		String[] list = Environment.getExternalStorageDirectory().list();
		super.onCreate(savedInstanceState);
	}
}
