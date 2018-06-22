package com.example.ohad.imagemobile;

import android.app.Service;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiManager;
import android.os.Environment;
import android.os.IBinder;
import android.support.annotation.Nullable;
import android.support.v4.app.NotificationCompat;
import android.util.Log;
import android.widget.Toast;
import android.support.v4.app.NotificationManagerCompat;

import java.io.FilenameFilter;
import java.util.Arrays;

import java.io.File;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

public class AndroidService extends Service {

    private BroadcastReceiver broadcastReceiver;
    private IntentFilter wifiFilter;

    public AndroidService() {}

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public void onCreate() {
        super.onCreate();
        // Initializing the event filter, which determines which event we listen to.
        this.wifiFilter = new IntentFilter();
        this.wifiFilter.addAction("android.net.wifi.supplicant.CONNECTION_CHANGE");
        this.wifiFilter.addAction("android.net.wifi.STATE_CHANGE");
    }

    // Accepts a File object and returns whether its a valid image or not.
    private static boolean isImage(File f) {
        String path = f.toString();
        boolean ans = false;
        String[] arrayFilters = {".jpg", ".png", ".JPG", ".PNG", ".jpeg", ".JPEG"};
        for (String filter : arrayFilters) {
            ans = ans || path.endsWith(filter);
        }
        return ans;
    }

    // When the service starts, registers to events.
    public int onStartCommand(Intent intent, int flag, int startId) {
        Toast.makeText(this, "Service starting...", Toast.LENGTH_LONG).show();

        // Initialize the broadcast listener.
        this.broadcastReceiver = new BroadcastReceiver() {
            @Override
            public void onReceive(final Context context, Intent intent) {
                NetworkInfo networkInfo = intent.getParcelableExtra(WifiManager.EXTRA_NETWORK_INFO);

//                final int notifyId = 1;
//                final NotificationManager nm = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);
//                /*
//                 * Create notification builder
//                 */
//                final NotificationManager notificationManager =
//                        (NotificationManager) context.getSystemService(Context.NOTIFICATION_SERVICE);
//                NotificationChannel channel = new NotificationChannel("default",
//                        "Channel name",
//                        NotificationManager.IMPORTANCE_DEFAULT);
//                channel.setDescription("Channel description");
//                notificationManager.createNotificationChannel(channel);
//                final NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(context, "default");
//                notificationBuilder.setSmallIcon(R.drawable.ic_launcher_background);
//                notificationBuilder.setContentTitle("Transferring Images status");
//                notificationBuilder.setContentText("In progress");


                // Upon our valid wifi event.
                if (networkInfo != null) {
                    if (networkInfo.getType() == ConnectivityManager.TYPE_WIFI) {
                        if (networkInfo.getState() == NetworkInfo.State.CONNECTED) {
                            final NotificationManager notificationManager = (NotificationManager) context.getSystemService(Context.NOTIFICATION_SERVICE);
                            final NotificationCompat.Builder builder = new NotificationCompat.Builder(context, "default");//                notificationBuilder.setSmallIcon(R.drawable.ic_launcher_background);
                            builder.setSmallIcon(R.drawable.ic_launcher_background);
                            builder.setContentTitle("Picture Transfer").setContentText("Transfer in progress").setPriority(NotificationCompat.PRIORITY_LOW);
                            NotificationChannel channel = new NotificationChannel("default", "Image Transfer Channel", NotificationManager.IMPORTANCE_DEFAULT);
                            channel.setDescription("progress bar for sending images to the server.");
                            notificationManager.createNotificationChannel(channel);
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    // Get the list of images from DCIM folder.
                                    File dcim = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DCIM);
                                    List<File> pictures = new LinkedList<File>();
                                    addImages(dcim, pictures);
                                    /**
                                     * For each image, we connect to the server's File-Accepting
                                     * port, and we send the image. Then we close the connection.
                                     */
                                    int counter = 1, amountImages = pictures.size();
                                    for(File image: pictures) {
                                        builder.setContentText("Sending: " + image.getName()).setProgress(amountImages, counter, false);
                                        notificationManager.notify(1, builder.build());
                                        AndroidClient client = new AndroidClient();
                                        client.sendImage(image);
                                        client.closeClient();
                                        counter++;
                                    }
                                        builder.setProgress(0,0, false);
                                        builder.setContentTitle("Transfer Completed").setContentText("All images were transferred");
                                        notificationManager.notify(1, builder.build());
                                }
                            }).start();


//
//
//                            new Thread(new Runnable() {
//                                @Override
//                                public void run() {
//                                    try {
//                                        mutex.lock();
//                                        int icr = 0;
//                                        getPictures();
//                                        if (pictures.size() == 0) { return; }
//                                        Client client = new Client();
//
//                                        for (File pic: pictures) {
//                                            icr += (100/pictures.size());
//                                            client.connectToServerAndSend(pic);
//                                            notificationBuilder.setProgress(100, icr, false);
//                                            notificationManager.notify(notifyId, notificationBuilder.build());
//                                        }
//                                        notificationBuilder.setProgress(0,0, false);
//
//                                        notificationBuilder.setContentTitle("Transfer Completed Successfully");
//                                        notificationBuilder.setContentText("Finished transferring");
//                                        notificationManager.notify(notifyId, notificationBuilder.build());
//                                        mutex.unlock();
//                                    }catch (Exception ex) {
//                                        Log.e("Photos Transfer", " error in photos transfer", ex);
//                                    }
//                                }}).start();
                        }
                    }
                }
            }
        };
        this.registerReceiver(this.broadcastReceiver, wifiFilter);
        return START_STICKY;
    }

    // When the service stops, unregister to the events and show a message.
    public void onDestroy() {
        this.unregisterReceiver(this.broadcastReceiver);
        Toast.makeText(this, "Service ending...", Toast.LENGTH_LONG).show();
    }

    // Adds all the images in the folder(and its sub-folders) to the given list.
    private static void addImages(File folder, List<File> picturesList) {
        if (folder != null) {
            File[] files = folder.listFiles();
            if (files != null) {
                for (File f : files) {
                    if (isImage(f)) {
                        picturesList.add(f);
                    } else if (folder.isDirectory()) {
                        addImages(f, picturesList);
                    }
                }
            }
        }
    }


}
