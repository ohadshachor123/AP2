package com.example.ohad.imagemobile;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }


    @Override
    public void onPointerCaptureChanged(boolean hasCapture) {

    }

    public void startService(View view){
//        Intent intent = new Intent(this, ImageServiceService.class);
//        startService(intent);'
        Toast.makeText(this,"This is a toast, for good health and START...", Toast.LENGTH_SHORT).show();
    }

    public void stopService(View view) {
//        Intent intent = new Intent(this, ImageServiceService.class);
//        stopService(intent);
        Toast.makeText(this,"As this toast, we stopped.", Toast.LENGTH_SHORT).show();
    }

}
