package com.example.patel_000.myproject;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.net.Uri;
import android.os.AsyncTask;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.os.Handler;
import android.view.MotionEvent;
import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

/**
 * An example full-screen activity that shows and hides the system UI (i.e.
 * status bar and navigation/system bar) with user interaction.
 */
public class StaffDetail extends Activity implements View.OnClickListener {

    ImageView call,email;
    String unm,pass;
    Bitmap map;
    LinearLayout img;
    String name,mail,siteid,mobile, city ,dept,ref,link, id;
    View v;
    TextView proname,pronumber,proemail,prositeid,prodepartment,procity;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_staff_detail);
        //Bundle bundle = this.getArguments();
        //id= bundle.getString("id","Test");
        id=getIntent().getStringExtra("id");
        SharedPreferences sp1=getSharedPreferences("Login",0);
        link= getResources().getString(R.string.url);
        unm=sp1.getString("Unm", null);
        pass = sp1.getString("Psw", null);
        findAllViews();
        call = (ImageView) findViewById(R.id.proiconcall);
        email = (ImageView) findViewById(R.id.proiconemail);
        WebTask wb=new WebTask();

        wb.execute();
        call.setOnClickListener(this);
        email.setOnClickListener(this);

    }
    private void findAllViews() {
        proname=(TextView)findViewById(R.id.proname);
        prositeid=(TextView)findViewById(R.id.prosite);
        proemail=(TextView)findViewById(R.id.proemailid);
        pronumber=(TextView)findViewById(R.id.pronumber);
        procity=(TextView)findViewById(R.id.procity);
        prodepartment=(TextView)findViewById(R.id.prodepartment);
        img=(LinearLayout)findViewById(R.id.proimage);
    }

    @Override
    public void onClick(View v) {
        if(v==call){
            Intent intent = new Intent(Intent.ACTION_DIAL);
            intent.setData( Uri.parse("tel: "+mobile));
            startActivity(intent);
        }else if(v==email){
            Intent email = new Intent(Intent.ACTION_SEND);
            email.putExtra(Intent.EXTRA_EMAIL, new String[]{ mail});
            email.putExtra(Intent.EXTRA_SUBJECT, "my project test");
            email.putExtra(Intent.EXTRA_TEXT, "my project testing mail");

//need this to prompts email client only
            email.setType("message/rfc822");

            startActivity(Intent.createChooser(email, "Choose an Email client :"));
        }
    }
    public class WebTask extends AsyncTask<String,String,String> {

        ProgressDialog Dialog=null;


        @Override
        protected void onPreExecute (){
            super.onPreExecute();
            Dialog = new ProgressDialog(StaffDetail.this);
            Dialog.setIndeterminate(true);
            Dialog.setCancelable(false);
            Dialog.setTitle("Loading.....");
            Dialog.setMessage(".....Please Wait.....");
            Dialog.show();
        }

        @Override
        protected String doInBackground(String... params) {
            RequestPackage request=new RequestPackage();
            request.setUri(link+"/api/StaffAPI/AllSiteStaff");
            request.setMethod("GET");


            request.setParam("Username",unm);
            request.setParam("Password",pass);

            String ans = (String) HttpManager.getData(request);
            try {

                JSONArray arr=new JSONArray(ans);
                for(int i=0;i<arr.length();i++) {
                    JSONObject obj = arr.getJSONObject(i);

                    if (id.equals(obj.getString("StaffID"))) {
                        name = obj.getString("Name");
                        mail = obj.getString("Email");
                        siteid = obj.getString("SiteID");
                        mobile = obj.getString("Mobile");
                        city = obj.getString("City");
                        dept = obj.getString("Dept");
                        ref = obj.getString("PhotoPath");
                    }
                }
                URL url = null;
                try {
                    url = new URL(link+ref);
                    HttpURLConnection connection = (HttpURLConnection) url.openConnection();
                    connection.setDoInput(true);
                    connection.connect();
                    InputStream input = connection.getInputStream();
                    map = BitmapFactory.decodeStream(input);
                } catch (MalformedURLException e) {
                    e.printStackTrace();
                }


            } catch (JSONException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }

            return "";
        }
        @Override
        protected void onPostExecute(String ans){
            super.onPostExecute(ans);
            try{
                Dialog.dismiss();

            }
            catch (Exception e){

            }

            proname.setText(name);
            prodepartment.setText(dept);
            proemail.setText(mail);
            procity.setText(city);
            pronumber.setText(mobile);
            prositeid.setText(siteid);


            BitmapDrawable background = new BitmapDrawable(map);
            img.setBackground(background);


            //Toast.makeText(Login.this,ans,Toast.LENGTH_LONG).show();


        }
    }



}
