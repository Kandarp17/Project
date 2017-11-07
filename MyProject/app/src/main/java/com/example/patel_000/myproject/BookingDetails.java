package com.example.patel_000.myproject;

import android.app.ProgressDialog;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.support.v4.app.FragmentManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;

public class BookingDetails extends AppCompatActivity {
    TextView requireddate,bookingdate,status,comments,sitename,resourceid,resourcename;
    String Requireddate,Bookingdate,Status,Comments,Sitename,Resourceid,Resourcename,id,state;
    String unm,pass,link;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_booking_details);
        //Bundle bundle = this.getArguments();
        id= getIntent().getStringExtra("id");
        state= getIntent().getStringExtra("state");
        SharedPreferences sp1=getSharedPreferences("Login",0);
        link= getResources().getString(R.string.url);
        unm=sp1.getString("Unm", null);
        pass = sp1.getString("Psw", null);
        findAllView();
        WebTask wb=new WebTask();
        wb.execute();
    }
    private void findAllView() {
        requireddate=(TextView)findViewById(R.id.requireddatevalue);
        bookingdate=(TextView)findViewById(R.id.bookingdatevalue);
        status=(TextView)findViewById(R.id.statusvalue);
        comments=(TextView)findViewById(R.id.commentsvalue);
        sitename=(TextView)findViewById(R.id.snamevalue);
        resourceid=(TextView)findViewById(R.id.resourceidvalue);
        resourcename=(TextView)findViewById(R.id.rnamevalue);

    }

    public class WebTask extends AsyncTask<String,String,String> {

        ProgressDialog Dialog=null;


        @Override
        protected void onPreExecute (){
            super.onPreExecute();
            Dialog = new ProgressDialog(BookingDetails.this);
            Dialog.setIndeterminate(true);
            Dialog.setCancelable(false);
            Dialog.setTitle("Loading.....");
            Dialog.setMessage(".....Please Wait.....");
            Dialog.show();
        }

        @Override
        protected String doInBackground(String... params) {
            RequestPackage request=new RequestPackage();
            request.setUri(link+"/api/ResBookingAPI/"+state);
            request.setMethod("GET");


            request.setParam("Username",unm);
            request.setParam("Password",pass);

            String ans = (String) HttpManager.getData(request);
            try {

                JSONArray arr=new JSONArray(ans);
                for(int i=0;i<arr.length();i++) {
                    JSONObject obj = arr.getJSONObject(i);

                    if (id.equals(obj.getString("ResBookingID"))) {
                        Requireddate = obj.getString("RequiredDate");
                        Bookingdate = obj.getString("BookingDate");
                        Status = obj.getString("Status");
                        Resourceid = obj.getString("ResourceID");
                        Resourcename= obj.getString("RName");
                        Sitename = obj.getString("SName");
                        Comments = obj.getString("Comments");
                    }
                }


            } catch (JSONException e) {
                e.printStackTrace();
            } catch (Exception e) {
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

            requireddate.setText(Requireddate);
            resourcename.setText(Resourcename);
            status.setText(Status);
            resourceid.setText(Resourceid);
            bookingdate.setText(Bookingdate);
            sitename.setText(Sitename);
            comments.setText(Comments);


            //Toast.makeText(Login.this,ans,Toast.LENGTH_LONG).show();


        }
    }
}
