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

import java.io.File;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

public class AndroidService extends Service {

    private BroadcastReceiver broadcastReceiver;
    private IntentFilter wifiFilter;

    public AndroidService(){}
    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }
    @Override
    public void onCreate() {
        super.onCreate();
        this.wifiFilter = new IntentFilter();
        this.wifiFilter.addAction("android.net.wifi.supplicant.CONNECTION_CHANGE");
        this.wifiFilter.addAction("android.net.wifi.STATE_CHANGE");
        // Here put the Code of Service
    }

    public int onStartCommand(Intent intent, int flag, int startId) {
        Toast.makeText(this,"Service starting...", Toast.LENGTH_LONG).show();
        // Create the broadcast receiver
        this.broadcastReceiver = new BroadcastReceiver() {
            @Override
            public void onReceive(Context context, Intent intent) {
                WifiManager wifiManager = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);
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


                /*
                 * Detect when wifi is connected than start transferring
                 */
                if (networkInfo != null) {
                    if (networkInfo.getType() == ConnectivityManager.TYPE_WIFI) {
                        if (networkInfo.getState() == NetworkInfo.State.CONNECTED) {
                            Toast.makeText(context,"wifi is on", Toast.LENGTH_LONG).show();

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

    public void onDestroy() {
        Toast.makeText(this,"Service ending...", Toast.LENGTH_LONG).show();
    }
}
