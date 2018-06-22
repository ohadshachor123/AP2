package com.example.ohad.imagemobile;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.content.Intent;
public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    // Linked to the 'Start' button, start the android service.
    public void startService(View view){
        Intent intent = new Intent(this, AndroidService.class);
        startService(intent);
    }

    // Linked to the 'Stop' button, stops the running android service.
    public void stopService(View view) {
        Intent intent = new Intent(this, AndroidService.class);
        stopService(intent);
    }

}
