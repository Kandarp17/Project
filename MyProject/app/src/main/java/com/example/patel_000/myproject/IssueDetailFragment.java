package com.example.patel_000.myproject;


import android.app.ProgressDialog;
import android.content.SharedPreferences;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
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

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;


/**
 * A simple {@link Fragment} subclass.
 */
public class IssueDetailFragment extends Fragment {
    LinearLayout reply;
    TextView title,detail,status,reportdate,replydate,replymessage;
    String Title,Detail,Status,Reportdate,Replydate,Replymessage,id;
    View v1;
    String unm,pass,link;

    public IssueDetailFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        v1=inflater.inflate(R.layout.fragment_issue_detail, container, false);
        Bundle bundle = this.getArguments();
        id= bundle.getString("id","Test");
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
        title=(TextView)v1.findViewById(R.id.titlevalue);
        detail=(TextView)v1.findViewById(R.id.detailvalue);
        status=(TextView)v1.findViewById(R.id.statusvalue);
        reportdate=(TextView)v1.findViewById(R.id.reportdatevalue);
        replydate=(TextView)v1.findViewById(R.id.replydatevalue);
        replymessage=(TextView)v1.findViewById(R.id.replymessagevalue);

        reply=(LinearLayout) v1.findViewById(R.id.reply);

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
            request.setUri(link+"/api/IssueAPI/AllIssue");
            request.setMethod("GET");


            request.setParam("Username",unm);
            request.setParam("Password",pass);

            String ans = (String) HttpManager.getData(request);
            try {

                JSONArray arr=new JSONArray(ans);
                for(int i=0;i<arr.length();i++) {
                    JSONObject obj = arr.getJSONObject(i);

                    if (id.equals(obj.getString("IssueID"))) {
                        Title = obj.getString("Title");
                        Detail = obj.getString("Details");
                        Status = obj.getString("Status");
                        Reportdate = obj.getString("ReportingTime");
                        Replydate = obj.getString("ReplyTime");
                        Replymessage = obj.getString("ReplyMsg");
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
            if(Status.equals("Pending")){
                reply.setVisibility(View.GONE);

            }

            title.setText(Title);
            detail.setText(Detail);
            status.setText(Status);
            reportdate.setText(Reportdate);
            replydate.setText(Replydate);
            replymessage.setText(Replymessage);



            //Toast.makeText(Login.this,ans,Toast.LENGTH_LONG).show();


        }
    }


}
