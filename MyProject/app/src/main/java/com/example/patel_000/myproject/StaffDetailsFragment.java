package com.example.patel_000.myproject;


import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
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
 * A simple {@link Fragment} subclass.
 */
public class StaffDetailsFragment extends Fragment implements View.OnClickListener {

    ImageView call,email;
    String unm,pass;
    Bitmap map;
    LinearLayout img;
    String name,mail,siteid,mobile, city ,dept,ref,link, id;
    View v;
    TextView proname,pronumber,proemail,prositeid,prodepartment,procity;
    public StaffDetailsFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        v=inflater.inflate(R.layout.fragment_staff_details, container, false);
        Bundle bundle = this.getArguments();
         id= bundle.getString("id","Test");
        SharedPreferences sp1=v.getContext().getSharedPreferences("Login",0);
        link= getResources().getString(R.string.url);
        unm=sp1.getString("Unm", null);
        pass = sp1.getString("Psw", null);
        findAllViews();
        call = (ImageView) v.findViewById(R.id.proiconcall);
        email = (ImageView) v.findViewById(R.id.proiconemail);
        WebTask wb=new WebTask();

        wb.execute(unm,pass);
        call.setOnClickListener(this);
        email.setOnClickListener(this);
        return v;
    }

    private void findAllViews() {
        proname=(TextView)v.findViewById(R.id.proname);
        prositeid=(TextView)v.findViewById(R.id.prosite);
        proemail=(TextView)v.findViewById(R.id.proemailid);
        pronumber=(TextView)v.findViewById(R.id.pronumber);
        procity=(TextView)v.findViewById(R.id.procity);
        prodepartment=(TextView)v.findViewById(R.id.prodepartment);
        img=(LinearLayout)v.findViewById(R.id.proimage);
    }

    @Override
    public void onClick(View v) {
        if(v==call){
            Intent intent = new Intent(android.content.Intent.ACTION_CALL, Uri.parse("tel: "+mobile));
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
            Dialog = new ProgressDialog(v.getContext());
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
