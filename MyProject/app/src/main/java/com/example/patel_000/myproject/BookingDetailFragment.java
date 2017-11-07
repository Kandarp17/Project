package com.example.patel_000.myproject;


import android.app.ProgressDialog;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;


/**
 * A simple {@link Fragment} subclass.
 */
public class BookingDetailFragment extends Fragment {
    TextView requireddate,bookingdate,status,comments,sitename,resourceid,resourcename;
    String Requireddate,Bookingdate,Status,Comments,Sitename,Resourceid,Resourcename,id,state;
    String unm,pass,link;
    View v1;


    public BookingDetailFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        v1= inflater.inflate(R.layout.fragment_booking_detail, container, false);
        Bundle bundle = this.getArguments();
        id= bundle.getString("id","Test");
        state= bundle.getString("state","Test");
        findAllView();
        SharedPreferences sp1=v1.getContext().getSharedPreferences("Login",0);
        link= getResources().getString(R.string.url);
        unm=sp1.getString("Unm", null);
        pass = sp1.getString("Psw", null);
        WebTask wb= new WebTask();
        wb.execute();
        return v1;
    }
    private void findAllView() {
        requireddate=(TextView)v1.findViewById(R.id.requireddatevalue);
        bookingdate=(TextView)v1.findViewById(R.id.bookingdatevalue);
        status=(TextView)v1.findViewById(R.id.statusvalue);
        comments=(TextView)v1.findViewById(R.id.commentsvalue);
        sitename=(TextView)v1.findViewById(R.id.snamevalue);
        resourceid=(TextView)v1.findViewById(R.id.resourceidvalue);
        resourcename=(TextView)v1.findViewById(R.id.rnamevalue);

    }

    public class WebTask extends AsyncTask<String,String,String> {

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
