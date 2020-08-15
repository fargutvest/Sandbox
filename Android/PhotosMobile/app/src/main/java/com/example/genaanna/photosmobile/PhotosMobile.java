package com.example.genaanna.photosmobile;

import android.app.Activity;
import android.os.Environment;
import android.os.Bundle;


public class PhotosMobile extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        String[] list = Environment.getExternalStorageDirectory().list();
        super.onCreate(savedInstanceState);
    }
}
