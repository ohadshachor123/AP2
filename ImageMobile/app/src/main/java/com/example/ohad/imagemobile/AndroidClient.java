package com.example.ohad.imagemobile;

import android.support.v4.media.MediaMetadataCompat;
import android.util.Log;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;
public class AndroidClient {
    private Socket mySocket;
    private final String IP = "10.0.2.2";
    private final int port = 52486;
    private final String imageCommand = "{\"Type\":6,\"Args\":[\"Name\"]}";
    public AndroidClient() {
        try {
            InetAddress address = InetAddress.getByName(IP);
            mySocket = new Socket(address, port);
            Log.i("TCP", "SucessfullyConnected");
        }catch(Exception e) {
            Log.e("TCP", "Connection error: ", e);
            mySocket = null;
        }
    }

    public void closeClient() {
        if (mySocket != null) {
            try {
                mySocket.close();
                Log.i("TCP", "Sucessfully closed.");
            } catch(Exception e) {
                Log.e("TCP", "Close socket error:  ", e);
            }
        }
    }

    public void sendImage(File image) {
        if (mySocket != null) {
            try {
                FileInputStream fis = new FileInputStream(image);
                ByteArrayOutputStream bos = new ByteArrayOutputStream();
                byte[] buf = new byte[1024];
                try {
                    for (int readNum; (readNum = fis.read(buf)) != -1; ) {
                        bos.write(buf, 0, readNum);
                    }
                }catch(Exception e) {
                        Log.e("IMAGES", " Error sending the name.");
                    }
                byte[] fileAsBytes = bos.toByteArray();
                OutputStream output = mySocket.getOutputStream();
                InputStream input = mySocket.getInputStream();
                byte[] proveReadImage = new byte[1];
                // write the image to the server
                output.write(image.toPath().getFileName().toString().getBytes());
                int i = input.read(proveReadImage);
                Thread.sleep(500);
                if (i == 1) {
                    output.write(fileAsBytes);
                }
                output.flush();

            } catch(Exception e) {
                Log.e("IMAGES", "ERROR WITH " + image.getName());
            }
        }
    }
}
