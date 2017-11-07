package com.example.patel_000.myproject;


import android.app.Activity;
import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.location.Location;

import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.provider.ContactsContract;
import android.provider.MediaStore;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

import android.widget.ImageView;
import android.widget.Toast;


/**
 * A simple {@link Fragment} subclass.
 */
public class NewPhotoFragment extends Fragment implements View.OnClickListener {

    private static final int CAMERA_PIC_REQUEST = 1337;;
    private ImageView imageView;
    Button b;
    View v1;
    private GPSTracker gps;
    double longitude;
    double latitude;
    public NewPhotoFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment

        v1 = inflater.inflate(R.layout.fragment_new_photo, container, false);
        imageView=(ImageView)v1.findViewById(R.id.imageView1);
        capture();

        b=(Button)v1.findViewById(R.id.submitbtn);
        b.setOnClickListener(this);

        return v1;
    }






    private void capture() {
        Intent cameraIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        startActivityForResult (cameraIntent, CAMERA_PIC_REQUEST);
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == CAMERA_PIC_REQUEST && resultCode == Activity.RESULT_OK) {
            Bundle extras = data.getExtras();
            Bitmap b = (Bitmap) extras.get("data");
            int width = b.getWidth();
            int height = b.getHeight();
            imageView.setImageBitmap(b);

        }
    }

    @Override
    public void onClick(View v) {
        if (v == imageView) {
            capture();
        }
        if(v==b){
            gps = new GPSTracker(getActivity());

            if(!gps.canGetLocation())
            {
                gps.showSettingsAlert();
            }
            if(gps.canGetLocation()){


                longitude = gps.getLongitude();
                latitude = gps .getLatitude();

                Toast.makeText(v1.getContext(),"Longitude:"+Double.toString(longitude)+"\nLatitude:"+Double.toString(latitude),Toast.LENGTH_SHORT).show();
            }

        }
    }
    @Override
    public void onDestroy() {
        super.onDestroy();
        gps.stopUsingGPS();
    }
}
