package com.example.patel_000.myproject;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Environment;
import android.preference.PreferenceManager;
import android.provider.MediaStore;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Base64;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Toast;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.util.Arrays;
import java.util.Date;

public class NewPhoto extends AppCompatActivity implements View.OnClickListener {
    final int CAMERA_REQUEST = 13323;
    private static final int GALLERY_REQUEST = 1889;
    private ImageView imageView;
    Button b;
    View v1;
    Bitmap bm1;
    private GPSTracker gps;
    double longitude;
    double latitude;
    public String fileName, Path, time;
    private static final int REQUEST_CAMERA = 1;
    protected static final int SELECT_FILE = 2;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_photo);

        //capture();

        b = (Button) findViewById(R.id.submitbtn);
        b.setOnClickListener(this);
        selectImage();
    }
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (resultCode == RESULT_OK) {

            if (requestCode == REQUEST_CAMERA) {
                File filepath = Environment.getExternalStorageDirectory();

                File dir = new File(filepath.getAbsolutePath()
                        + "/" + getResources().getString(R.string.app_name) + "/");

                try {

                    File f = new File(filepath.getAbsolutePath()
                            + "/" + getResources().getString(R.string.app_name) + "/" + fileName);
                    Path = f.getAbsolutePath();


                    Bitmap bm;
                    BitmapFactory.Options btmapOptions = new BitmapFactory.Options();


                    bm = BitmapFactory.decodeFile(Path, btmapOptions);
                    // MyTask task = new MyTask();
                    //  task.execute(Npath);


                    imageView.setImageBitmap(bm);


                } catch (Exception e) {

                    Toast.makeText(this, "err::" + Arrays.toString(e.getStackTrace()), Toast.LENGTH_LONG).show();

                }
            } else if (requestCode == SELECT_FILE) {
                Uri selectedImageUri = data.getData();

                Path = getPath(selectedImageUri, NewPhoto.this);

                //  MyTask task = new MyTask();
                // task.execute(Npath);

                Bitmap bm;
                BitmapFactory.Options btmapOptions = new BitmapFactory.Options();


                bm = BitmapFactory.decodeFile(Path, btmapOptions);

                imageView.setImageBitmap(bm);


            }

        }
    }

    @Override
    public void onClick(View v) {
        if (v == imageView) {
            selectImage();
        }
        if (v == b) {
            gps = new GPSTracker(this);

            if (!gps.canGetLocation()) {
                gps.showSettingsAlert();
            }
            if (gps.canGetLocation()) {

                longitude = gps.getLongitude();
                latitude = gps.getLatitude();

                Toast.makeText(this, "Longitude:" + Double.toString(longitude) + "\nLatitude:" + Double.toString(latitude), Toast.LENGTH_SHORT).show();
            }
            WebTask wt = new WebTask();
            wt.execute();

        }
    }

    private void selectImage() {

        final CharSequence[] items = {"Take Photo",
                "Cancel"};

        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("Add Photo!");
        builder.setItems(items, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int item) {
                if (items[item].equals("Take Photo")) {

                    Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
                    Date d = new Date();

                    time = "" + d.getTime();
                    fileName = time + ".jpg";


                    // Find the SD Card path
                    File filepath = Environment.getExternalStorageDirectory();
                    // Create a new folder in SD Card
                    File dir = new File(filepath.getAbsolutePath()
                            + "/" + getResources().getString(R.string.app_name) + "/");
                    if (!dir.exists()) {
                        //noinspection ResultOfMethodCallIgnored
                        dir.mkdirs();
                    }

                    // Create a name for the saved image
                    File f = new File(dir, fileName);
                    intent.putExtra(MediaStore.EXTRA_OUTPUT, Uri.fromFile(f));
                    startActivityForResult(intent, REQUEST_CAMERA);
                } else if (items[item].equals("Choose from Library")) {
                    Intent intent = new Intent(
                            Intent.ACTION_PICK,
                            android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
                    intent.setType("image/*");
                    startActivityForResult(
                            Intent.createChooser(intent, "Select File"),
                            SELECT_FILE);
                } else if (items[item].equals("Cancel")) {
                    dialog.dismiss();
                }
            }
        });
        builder.show();
    }

    @SuppressWarnings("deprecation")
    public String getPath(Uri uri, Activity activity) {
        String[] projection = {MediaStore.MediaColumns.DATA};
        Cursor cursor = activity
                .managedQuery(uri, projection, null, null, null);
        int column_index = cursor.getColumnIndexOrThrow(MediaStore.MediaColumns.DATA);
        cursor.moveToFirst();
        return cursor.getString(column_index);
    }

    private String getRealPathFromURI(String contentURI) {
        Uri contentUri = Uri.parse(contentURI);
        @SuppressLint("Recycle") Cursor cursor = getContentResolver().query(contentUri, null, null, null, null);
        if (cursor == null) {
            return contentUri.getPath();
        } else {
            cursor.moveToFirst();
            int index = cursor.getColumnIndex(MediaStore.Images.ImageColumns.DATA);
            return cursor.getString(index);
        }
    }

    private class WebTask extends AsyncTask<String, String, String> {
        ProgressDialog pd = null;

        protected void onPreExecute() {
            super.onPreExecute();
            pd = new ProgressDialog(NewPhoto.this);
            pd.setIndeterminate(true);
            pd.setCancelable(false);
            pd.setTitle("Loading....");
            pd.setMessage("Please wait...");
            pd.show();
        }

        @Override
        protected String doInBackground(String... arr) {

            Intent i = getIntent();

            RequestPackage rp = new RequestPackage();
            rp.setUri(getResources().getString(R.string.url) +"/api/SitePhotoAPI/UploadImage");
            rp.setMethod("POST");
            BitmapFactory.Options btmapOptions = new BitmapFactory.Options();
            bm1 = BitmapFactory.decodeFile(Path, btmapOptions);

            ByteArrayOutputStream stream = new ByteArrayOutputStream();
            bm1.compress(Bitmap.CompressFormat.JPEG, 90, stream); //compress to which format you want.
            byte[] byte_arr = stream.toByteArray();


            String image_str = Base64.encodeToString(byte_arr, Base64.DEFAULT);
            SharedPreferences sp = PreferenceManager.getDefaultSharedPreferences(NewPhoto.this);
            String Uname = sp.getString("Uname", null);
            String Pass = sp.getString("Pass", null);
            rp.setParam("un", Uname);
            rp.setParam("pw", Pass);
            rp.setParam("Feedback", image_str);
            rp.setParam("Latitude",""+latitude);
            rp.setParam("Longitude",""+longitude);
            String ans =  HttpManager.getData(rp);
            return ans;

        }

        @Override
        protected void onPostExecute(String ans) {
            super.onPostExecute(ans);
            try{
                pd.dismiss();

            }
            catch (Exception e){

            }
            Toast.makeText(NewPhoto.this,ans, Toast.LENGTH_LONG).show();
        }
    }
}
