package com.example.patel_000.myproject;


import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
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
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import static android.R.attr.name;


/**
 * A simple {@link Fragment} subclass.
 */
public class SiteDetailFragment extends Fragment implements View.OnClickListener {

    View v1;
    ImageView plan,location,more;
    LinearLayout layplan,laylocation,laymore;
    String unm,pass,link,Details,Address,Area,City,Sdate,Edate,Status,Sname,Sid;
    TextView details,address,area,city,sdate,edate,status,sname,sid;
    public SiteDetailFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        v1= inflater.inflate(R.layout.fragment_site_detail, container, false);
        SharedPreferences sp1=v1.getContext().getSharedPreferences("Login",0);
        link= getResources().getString(R.string.url);
        unm=sp1.getString("Unm", null);
        pass = sp1.getString("Psw", null);
        findAllViews();
        plan.setOnClickListener(this);
        more.setOnClickListener(this);
        location.setOnClickListener(this);
        WebTask wb=new WebTask();
        wb.execute(unm,pass);
        return v1;

    }

    private void findAllViews() {

        plan= (ImageView) v1.findViewById(R.id.iconplan);
        location= (ImageView) v1.findViewById(R.id.iconlocation);
        more= (ImageView) v1.findViewById(R.id.iconmore);
        layplan=(LinearLayout)v1.findViewById(R.id.plans);
        laylocation=(LinearLayout)v1.findViewById(R.id.location);
        laymore=(LinearLayout)v1.findViewById(R.id.more);
        details=(TextView)v1.findViewById(R.id.laydetails);
        address=(TextView)v1.findViewById(R.id.layaddress);
        area=(TextView)v1.findViewById(R.id.layarea);
        city=(TextView)v1.findViewById(R.id.laycity);
        sdate=(TextView)v1.findViewById(R.id.laystartdate);
        edate=(TextView)v1.findViewById(R.id.layenddate);
        status=(TextView)v1.findViewById(R.id.laystatus);
        sname=(TextView)v1.findViewById(R.id.proname);
        sid=(TextView)v1.findViewById(R.id.prosite);

    }

    @Override
    public void onClick(View v) {
        if(v==plan){
            laylocation.setVisibility(View.GONE);
            laymore.setVisibility(View.GONE);
            layplan.setVisibility(View.VISIBLE);
            plan.setBackgroundResource(R.color.colorAccent);
            location.setBackgroundResource(R.color.colorPrimaryDark);
            more.setBackgroundResource(R.color.colorPrimaryDark);
        }else if(v==location){
            layplan.setVisibility(View.GONE);
            laymore.setVisibility(View.GONE);
            laylocation.setVisibility(View.VISIBLE);
            location.setBackgroundResource(R.color.colorAccent);
            plan.setBackgroundResource(R.color.colorPrimaryDark);
            more.setBackgroundResource(R.color.colorPrimaryDark);
        }else if(v==more){
            laylocation.setVisibility(View.GONE);
            layplan.setVisibility(View.GONE);
            laymore.setVisibility(View.VISIBLE);
            more.setBackgroundResource(R.color.colorAccent);
            plan.setBackgroundResource(R.color.colorPrimaryDark);
            location.setBackgroundResource(R.color.colorPrimaryDark);
        }
    }
    private class WebTask extends AsyncTask<String,String,String> {

        ProgressDialog Dialog=null;


        @Override
        protected void onPreExecute (){
            super.onPreExecute();
            Dialog = new ProgressDialog(v1.getContext());
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


                Details= obj.getString("Details");
                Address= obj.getString("Address");
                Area= obj.getString("Area");
                City= obj.getString("SCity");
                Sid=obj.getString("SiteID");
                Sname=obj.getString("SName");
                Status=obj.getString("Status");
                Sdate= obj.getString("StartDate");
                Edate= obj.getString("EndDate");


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
            details.setText(Details);
            sdate.setText(Sdate);
            edate.setText(Edate);
            status.setText(Status);
            sname.setText(Sname);
            sid.setText(Sid);
            address.setText(Address);
            area.setText(Area);
            city.setText(City);

        }
    }

}
