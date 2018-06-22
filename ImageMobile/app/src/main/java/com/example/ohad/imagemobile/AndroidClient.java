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

// An abstract for sending/receiving data from the server.
public class AndroidClient {
    private Socket mySocket;
    // Connection information.
    private final String IP = "10.0.2.2";
    private final int port = 52486;

    // Constructor automaticly connects to the server.
    public AndroidClient() {
        try {
            InetAddress address = InetAddress.getByName(IP);
            mySocket = new Socket(address, port);
            Log.i("TCP", "Sucessfully Connected");
        } catch (Exception e) {
            Log.e("TCP", "Connection error: ", e);
            mySocket = null;
        }
    }

    // Closes the socket.
    public void closeClient() {
        if (mySocket != null) {
            try {
                mySocket.close();
                Log.i("TCP", "Sucessfully closed.");
            } catch (Exception e) {
                Log.e("TCP", "Close socket error:  ", e);
            }
        }
    }

    // Sends an image from a stored file.
    public void sendImage(File image) {
        if (mySocket != null) {
            try {
                /** read the file to a byte array **/
                FileInputStream fis = new FileInputStream(image);
                ByteArrayOutputStream bos = new ByteArrayOutputStream();
                byte[] buf = new byte[1024];
                try {
                    for (int readNum; (readNum = fis.read(buf)) != -1; ) {
                        bos.write(buf, 0, readNum);
                    }
                } catch (Exception e) {
                    Log.e("IMAGES", " Error reading the file.");
                }
                byte[] fileAsBytes = bos.toByteArray();

                //Sending the file over the socket
                OutputStream output = mySocket.getOutputStream();
                InputStream input = mySocket.getInputStream();
                byte[] flag = new byte[1];
                // write the image's name to the server
                output.write(image.toPath().getFileName().toString().getBytes());
                // Waiting for a response from the server, to know it's ready to receive the image.
                int i = input.read(flag);
                Thread.sleep(500);
                if (i == 1) {
                    // Sending the image file.
                    output.write(fileAsBytes);
                }
                output.flush();
            } catch (Exception e) {
                Log.e("IMAGES", "ERROR WITH " + image.getName());
            }
        }
    }
}
