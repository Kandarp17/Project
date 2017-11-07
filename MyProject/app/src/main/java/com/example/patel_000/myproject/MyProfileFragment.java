package com.example.patel_000.myproject;


import android.app.ProgressDialog;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
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
import java.net.MalformedURLException;
import java.net.URL;


/**
 * A simple {@link Fragment} subclass.
 */
public class MyProfileFragment extends Fragment  {

    ImageView call,email;
    String unm,pass;
    Bitmap bmp;
    LinearLayout img;
    String name,mail, sitename,mobile, city ,dept,ref,link;
    View v;
    TextView proname,pronumber,proemail,prositeid,prodepartment,procity;
    public MyProfileFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        v = inflater.inflate(R.layout.fragment_myprofile, container, false);
        SharedPreferences sp1=v.getContext().getSharedPreferences("Login",0);
        link= getResources().getString(R.string.url);
        unm=sp1.getString("Unm", null);
        pass = sp1.getString("Psw", null);
        findAllViews();
        WebTask wb=new WebTask();

        wb.execute(unm,pass);
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
            request.setUri(link+"/api/StaffAPI/GetMyDetails");
            request.setMethod("GET");


            request.setParam("Username",unm);
            request.setParam("Password",pass);

            String ans = (String) HttpManager.getData(request);
            try {
                JSONArray arr=new JSONArray(ans);
                JSONObject obj = arr.getJSONObject(0);


                name = obj.getString("Name");
                mail=obj.getString("Email");
                 sitename =obj.getString("SName");
                mobile=obj.getString("Mobile");
                 city=obj.getString("City");
                 dept=obj.getString("Dept");
                ref=obj.getString("PhotoPath");
                URL url = null;
                try {
                    url = new URL(""+link+ref);
                    bmp = BitmapFactory.decodeStream(url.openConnection().getInputStream());
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
            prositeid.setText(sitename);


            BitmapDrawable myBackground = new BitmapDrawable(bmp);
            img.setBackground(myBackground);


            //Toast.makeText(Login.this,ans,Toast.LENGTH_LONG).show();


        }
    }
}
